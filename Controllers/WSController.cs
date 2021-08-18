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
        private readonly IVPDateService IVPDate;
        private readonly ICovid19Service ICovid19;
        private readonly INHIQP701Service INHIQP701;
        private readonly JwtHelpers IJwt;
        private IConfiguration Iconf { get; }
        private string issuer { get; }
        private string signKey { get; }
       
        public WSController(IVPDateService IVPDateService, IConfiguration Configuration, ICovid19Service ICovid19Service, JwtHelpers JwtHelpers, INHIQP701Service INHIQP701Service)
        {
            this.IVPDate = IVPDateService;
            this.ICovid19 = ICovid19Service;
            this.IJwt = JwtHelpers;
            this.Iconf = Configuration;
            this.issuer = UStore.GetUStore(Iconf["JwtSettings:Issuer"], "Issuer");
            this.signKey = UStore.GetUStore(Iconf["JwtSettings:SignKey"], "SignKey");
            this.INHIQP701 = INHIQP701Service;

        }

        /* 畫面一開起來時.先ajax load的資料 */
        [HttpPost("~/Fetch")]
        public async Task<JObject> FetchAsync()
        {
            #region 安全驗證碼

            var sec = SecurityModule.CreateRecaptchaString();
            var SECURITY = sec[0];
            var Answer = sec[1];

            #endregion

            #region AZ預約日期清單

            var VP = await this.IVPDate.GetVP1((int)VPTypename.AZ);
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

            #endregion

            // Echo 2021-08-06 莫得那 預約日期清單
            #region Moderna預約日期清單

            var MP = await this.IVPDate.GetVP1((int)VPTypename.Moderna);
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
            #endregion


            //2021-08-11 小愷新增 取得預約身份類別
            #region 
            var az = await this.IVPDate.GetVPCategoryList((int)VPTypename.AZ);
            JArray Category_AZ = new JArray();
            foreach (var p in az)
            {
                var J = new JObject {
                        { "head", p.Name},
                        { "id", p.Id.ToString() },
                    };


                Category_AZ.Add(J);
            }

            var mo = await this.IVPDate.GetVPCategoryList((int)VPTypename.Moderna);
            JArray Category_MO = new JArray();
            foreach (var p in mo)
            {
                var J = new JObject {
                        { "head", p.Name},
                        { "id", p.Id.ToString() },
                    };

                Category_MO.Add(J);
            }

            #endregion

            #region Token

            string token = this.IJwt.GenerateToken_3min(issuer, signKey);
            var result = new JObject { // 
                { "security", SECURITY },
                { "answer", Answer },
                { "vp1", Data1 },
                { "token", token },
                { "vpMo", Data_Mo }, // Echo 2021-08-06 將莫得那 預約日期清單回傳到前端
                { "cateAZ", Category_AZ }, // akai 2021-08-11 將AZ 預約身份類別傳回前端
                { "cateMO", Category_MO }, // akai 2021-08-11 將Moderna 預約身份類別傳回前端
            };

            #endregion

            return result;
        }

        /* 使用者按下送出後執行的動作 */
        [Authorize]
        [HttpPost("~/CheckOne")]
        public async Task<JObject> CheckOneAsync(dtoCovid19 dto)
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

                var vP2 = await this.IVPDate.GetVP2(dto.vpid);
                if (vP2 == null)
                {
                    /* 會有此錯誤 多半是 預約檔裡沒有對應的預約id */
                    result["msg"] = "不正確的操作方式";
                    result["code"] = "600";
                    return result;
                }
                if (vP2.cnt2 >= vP2.cnt)
                {
                    /* 已額滿 */
                    result["msg"] = string.Format("{0} {1} 已額滿", vP2.date1.ToString("yyyy-MM-dd"), vP2.week);
                    result["code"] = "400";
                    return result;
                }

                dateS = vP2.date1.ToString("yyyy-MM-dd");
                weekS = vP2.week;

                #endregion

                // * Echo 2021-08-08 判斷疫苗種類
                #region 已預約過的不可再預約

                var cod = await this.ICovid19.GetOne(dto.id);
                if (cod != null)
                {
                    var vptype = await this.IVPDate.GetVPType(cod.vptype);
                    result["msg"] = string.Format("您已預約 {2} {0} - {1} 的時段", cod.date2, cod.week, vptype); ;
                    result["code"] = "500";
                    return result;
                }

                #endregion

                // Echo 110.08.02 第二劑不需call Api
                #region 第二劑
                var doChecked = false;
                var isCheck = await this.IVPDate.GetVPCategoryCheck(int.Parse(dto.idtypes));
                if (isCheck == 1)
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
                #endregion

                /* 如果身份不是第二劑 , 或是有造冊 , 則可預約 */
                if (doChecked == true || isCheck == 0)
                {
                    /* 預約成功 */
                    result["msg"] = string.Format("請於 {0} - {1} 至 宏恩綜合醫院疫苗門診 施打疫苗,謝謝", dateS, weekS);
                    result["code"] = "200";
                    await this.ICovid19.NewOne(dto);
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

        /* 預約殘劑  畫面一開起來時.先load的資料 */
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

        /* 預約殘劑 使用者按下送出後執行的動作 */
        [Authorize]
        [HttpPost("~/CheckA2E")]
        public async Task<JObject> CheckA2EAsync(dtoA2E dto)
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
                var cod = await this.ICovid19.GetA2E(dto.id);
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
                await this.ICovid19.NewA2E(dto);
            }

            return result;

        }

        /* 查詢預約 */
        [HttpPost("~/Search1")]
        public async Task<JObject> Search1Async(dtoId dto)
        {
            var result = new JObject { // 
                { "msg", "" },
            };

            var q = await this.ICovid19.GetOne(dto.id);
            if (q != null)
            {
                /* 有預約資料 */
                var vptype = await this.IVPDate.GetVPType(q.vptype);
                result["guid"] = q.guid;
                result["id"] = q.id;
                result["name"] = q.name;
                result["msg"] = string.Format("已預約 {2} {0} - {1} 的時段", q.date2, q.week, vptype);
                var dNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var _date1 = new DateTime(q.date1.Year, q.date1.Month, q.date1.Day, 0, 0, 0);
                if (_date1 == dNow.AddDays(1) && DateTime.Now.Hour > 15)
                {
                    result["show1"] = 0;
                }
                else if (q.date1 > DateTime.Now)
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

        /* 取消預約 */
        [HttpPost("~/Cancel1")]
        public JObject Cancel1(dtoGuid dto)
        {
            var result = new JObject { // 
                { "msg", "" },
            };

            this.ICovid19.CancelOne(dto.guid);

            return result;
        }


        /* 健保局API */
        private async Task<dto1> GetVaccLstDataAsync(dto3 dto)
        {
            NHIQP701SoapClient.EndpointConfiguration endpoint = new NHIQP701SoapClient.EndpointConfiguration();
            NHIQP701SoapClient myService = new NHIQP701SoapClient(endpoint, "https://medvpnws.nhi.gov.tw:7008/qp7e2000/NHIQP701.asmx");

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

            var res2 = JsonSerializer.Deserialize<dto1>(res);
            return res2;
        }

    }
}