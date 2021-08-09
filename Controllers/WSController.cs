using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CountryWeb.Data;
using CountryWeb.Helper;
using CountryWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ServiceReference;

namespace CountryWeb.Controllers
{
    [ApiController]
    [EnableCors("CorsDomain")]
    public class WSController : ControllerBase
    {
        private readonly IVPAZService IvP;

        // Echo 2021-08-06 宣告相依性注入
        private readonly IVPMOService IvP_Mo;
        private readonly ICovid19Service ICovid19;
        private readonly INHIQP701Service INHIQP701;
        private readonly JwtHelpers IJwt;
        private IConfiguration Iconf { get; }
        private string issuer { get; }
        private string signKey { get; }

        public WSController(IVPAZService IVPAZService, IConfiguration Configuration, ICovid19Service ICovid19Service, JwtHelpers JwtHelpers, INHIQP701Service INHIQP701Service
         // Echo 2021-08-06 加入相依性注入
         , IVPMOService IVPMOService)
        {
            this.IvP = IVPAZService;
            this.ICovid19 = ICovid19Service;
            this.IJwt = JwtHelpers;
            this.Iconf = Configuration;
            this.issuer = UStore.GetUStore(Iconf["JwtSettings:Issuer"], "Issuer");
            this.signKey = UStore.GetUStore(Iconf["JwtSettings:SignKey"], "SignKey");
            this.INHIQP701 = INHIQP701Service;

            // Echo 2021-08-06 使用相依性注入
            this.IvP_Mo = IVPMOService;
        }

        [HttpPost("~/Fetch")]
        public JObject Fetch()
        {
            // 安全驗證碼
            var sec = SecurityModule.CreateRecaptchaString();
            var SECURITY = sec[0];
            var Answer = sec[1];

            // AZ 預約日期清單
            var VP = this.IvP.GetvP1();
            JArray Data1 = new JArray();
            foreach (var p in VP)
            {
                var J = new JObject {
                        { "head", p.head },
                        { "id", p.id.ToString() },
                    };

                if (p.Fulls == true)
                {
                    J["disabled"] = true;
                }

                Data1.Add(J);
            }

            // Echo 2021-08-06 莫得那 預約日期清單
            var MP = this.IvP_Mo.GetvP1();
            JArray Data_Mo = new JArray();
            foreach (var p in MP)
            {
                var J = new JObject {
                        { "head", p.head },
                        { "id", p.id.ToString() },
                    };

                if (p.Fulls == true)
                {
                    J["disabled"] = true;
                }

                Data_Mo.Add(J);
            }

            // token
            string token = this.IJwt.GenerateToken_3min(issuer, signKey);
            var result = new JObject { // 
                { "security", SECURITY },
                { "answer", Answer },
                { "vp1", Data1 },
                { "token", token },
                { "vpMo", Data_Mo }, // Echo 2021-08-06 將莫得那 預約日期清單回傳到前端
            };

            return result;
        }

        [Authorize]
        [HttpPost("~/CheckOne")]
        public JObject CheckOne(dtoCovid19 dto)
        {
            var currentUser = HttpContext.User;
            var result = new JObject { // 
                { "msg", "" },
                { "code", "300" }, // 
            };
            var dateS = "";
            var weekS = "";

            /* 5分鐘時效*/
            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {

                #region 判斷token的時間與 call api 的時間差
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                TimeSpan ts = DateTime.Now - date;

                if (ts.TotalMinutes >= 5)
                {
                    result["msg"] = "已超過網頁安全時間(五分鐘),請重整網頁後再預約";
                    result["code"] = "300";
                    return result;
                }
                #endregion

                // * Echo 2021-08-08 判斷疫苗種類
                #region 可能會額滿
                if (dto.vptype == "1")
                {
                    var vP2 = this.IvP.GetvP2(dto.vpid);
                    if (vP2 == null)
                    {
                        result["msg"] = "不正確的操作方式";
                        result["code"] = "600";
                        return result;
                    }
                    if (vP2.cnt2 >= vP2.cnt)
                    {
                        // 已額滿
                        result["msg"] = string.Format("{0} {1} 已額滿", vP2.date1.ToString("yyyy-MM-dd"), vP2.week);
                        result["code"] = "400";
                        return result;
                    }

                    dateS = vP2.date1.ToString("yyyy-MM-dd");
                    weekS = vP2.week;
                }
                else if (dto.vptype == "2")
                {
                    var vP2 = this.IvP_Mo.GetvP2(dto.vpid);
                    if (vP2 == null)
                    {
                        result["msg"] = "不正確的操作方式";
                        result["code"] = "600";
                        return result;
                    }
                    if (vP2.cnt2 >= vP2.cnt)
                    {
                        // 已額滿
                        result["msg"] = string.Format("{0} {1} 已額滿", vP2.date1.ToString("yyyy-MM-dd"), vP2.week);
                        result["code"] = "400";
                        return result;
                    }

                    dateS = vP2.date1.ToString("yyyy-MM-dd");
                    weekS = vP2.week;
                }

                #endregion

                // * Echo 2021-08-08 判斷疫苗種類
                #region 已預約過的不可再預約

                var cod = this.ICovid19.GetOneAZ(dto.id);
                if (cod != null)
                {
                    result["msg"] = string.Format("您已預約AZ {0} - {1} 的時段", cod.date2, cod.week); ;
                    result["code"] = "500";
                    return result;
                }

                var codMO = this.ICovid19.GetOneMO(dto.id);
                if (codMO != null)
                {
                    result["msg"] = string.Format("您已預約默德納 {0} - {1} 的時段", codMO.date2, codMO.week); ;
                    result["code"] = "500";
                    return result;
                }

                #endregion

                // Echo 110.08.02 第二劑不需call Api
                var doChecked = false;
                if (dto.idtypes != "6")
                {
                    //akai 110.07.12暫時移除使用健保API檢核身份類別
                    //akai 110.08.02 加回api檢核機制。
                    #region call 健保局 api 確認是否有造冊
                    var _dto3 = new dto3();
                    _dto3.id = dto.id;
                    _dto3.sValidSDate = DateTime.Now.ToString("yyyyMMdd");
                    var q = GetVaccLstDataAsync(_dto3).Result;
                    if (q.RtnCode == "00" && q.oVaccLstData == "Y")
                    {
                        doChecked = true;
                    }
                    #endregion
                }

                if (doChecked == true || dto.idtypes == "6")
                {
                    /* 預約成功 */
                    result["msg"] = string.Format("請於 {0} - {1} 至 宏恩綜合醫院疫苗門診 施打疫苗,謝謝", dateS, weekS);
                    result["code"] = "200";
                    if (dto.vptype == "1")
                    {
                        this.ICovid19.NewOneAZ(dto);
                    }
                    else
                    {
                        this.ICovid19.NewOneMO(dto);

                    }

                }
                else
                {
                    /* 預約失敗 */
                    result["msg"] = "查無符合身份類別，如有疑義請循序洽造冊單位、衛生局、疾管署釐清，謝謝";
                    result["code"] = "700";
                    return result;
                }
            }

            return result;

        }

        [HttpPost("~/FetchA2E")]
        public JObject FetchA2E()
        {
            // 安全驗證碼
            var sec = SecurityModule.CreateRecaptchaString();
            var SECURITY = sec[0];
            var Answer = sec[1];

            // token
            string token = this.IJwt.GenerateToken_3min(issuer, signKey);
            var result = new JObject { // 
                { "security", SECURITY },
                { "answer", Answer },
                { "token", token },
            };

            return result;
        }

        [Authorize]
        [HttpPost("~/CheckA2E")]
        public JObject CheckA2E(dtoA2E dto)
        {
            var currentUser = HttpContext.User;
            var result = new JObject { // 
                { "msg", "" },
                { "code", "300" }, // 
            };

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                /* 判斷token的時間與 call api 的時間差*/
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                TimeSpan ts = DateTime.Now - date;

                if (ts.TotalMinutes >= 5)
                {
                    result["msg"] = "已超過網頁安全時間(五分鐘)";
                    result["code"] = "300";
                    return result;
                }

                /* 已預約過的不可再預約 */
                var cod = this.ICovid19.GetA2E(dto.id);
                if (cod != "")
                {
                    result["msg"] = cod;
                    result["code"] = "500";
                    return result;
                }

                /* 預約成功 */
                // 預約完要出現提醒示窗。(預約施打的日期時間，提醒事項)
                result["msg"] = string.Format("登記成功,謝謝");
                result["code"] = "200";
                this.ICovid19.NewA2E(dto);

            }

            return result;

        }

        [HttpPost("~/Search1")]
        public JObject Search1(dtoId dto)
        {
            var result = new JObject { // 
                { "msg", "" },
            };

            var qAZ = this.ICovid19.GetOneAZ(dto.id);
            if (qAZ != null)
            {
                result["guid"] = qAZ.guid;
                result["id"] = qAZ.id;
                result["name"] = qAZ.name;
                result["msg"] = string.Format("已預約AZ {0} - {1} 的時段", qAZ.date2, qAZ.week);
                var dNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var _date1 = new DateTime(qAZ.date1.Year, qAZ.date1.Month, qAZ.date1.Day, 0, 0, 0);
                if (_date1 == dNow.AddDays(1) && DateTime.Now.Hour > 15)
                {
                    result["show1"] = 0;
                }
                else if (qAZ.date1 > DateTime.Now)
                {
                    result["show1"] = 1;
                }
                else
                {
                    result["show1"] = 0;
                }
                result["idcod"] = 1;

                return result;
            }

            // Echo 2021-08-09 Add
            var qMo = this.ICovid19.GetOneMO(dto.id);
            if (qMo != null)
            {
                result["guid"] = qMo.guid;
                result["id"] = qMo.id;
                result["name"] = qMo.name;
                result["msg"] = string.Format("已預約莫得那 {0} - {1} 的時段", qMo.date2, qMo.week);
                var dNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var _date1 = new DateTime(qMo.date1.Year, qMo.date1.Month, qMo.date1.Day, 0, 0, 0);
                if (_date1 == dNow.AddDays(1) && DateTime.Now.Hour > 15)
                {
                    result["show1"] = 0;
                }
                else if (qMo.date1 > DateTime.Now)
                {
                    result["show1"] = 1;
                }
                else
                {
                    result["show1"] = 0;
                }
                result["idcod"] = 1;
                return result;
            }

            result["idcod"] = 0;
            result["msg"] = "查無紀錄";
            result["show1"] = 0;
            return result;

        }

        [HttpPost("~/Cancel1")]
        public JObject Cancel1(dtoGuid dto)
        {
            var result = new JObject { // 
                { "msg", "" },
            };

            this.ICovid19.CancelOneAZ(dto.guid);

            return result;
        }

        private async Task<dto1> GetVaccLstDataAsync(dto3 dto)
        {

            NHIQP701SoapClient.EndpointConfiguration endpoint = new NHIQP701SoapClient.EndpointConfiguration();
            NHIQP701SoapClient myService = new NHIQP701SoapClient(endpoint, "https://medvpnws.nhi.gov.tw:7008/qp7e2000/NHIQP701.asmx");
            // AuthorizationSoapHeader MyAuthHeader = new AuthorizationSoapHeader();

            // MyAuthHeader.AppName = FDSServiceAppName;
            // MyAuthHeader.AppID = Guid.Parse(MyAppID);

            var NHIQ = this.INHIQP701.GetOne();
            var q = new dto2();

            q.sHospId = "1101020036";
            q.sPatId = dto.id;
            q.sValidSDate = dto.sValidSDate;
            q.sClientRandom = NHIQ.sClientRandom;
            q.sSignature = NHIQ.sSignature;
            q.sSamId = "000000081174";
            var json = JsonSerializer.Serialize(q);

            var entries = await myService.GetVaccLstDataAsync(json);
            var res = entries.Body.GetVaccLstDataResult;
            // {"RtnCode":"01","oVaccLstData":null}

            var res2 = JsonSerializer.Deserialize<dto1>(res);
            // ViewBag.RtnCode = res2.RtnCode;
            // ViewBag.oVaccLstData = res2.oVaccLstData;
            // var dto1 = new dto3();
            // dto1.id = "";
            return res2;

        }

    }
}