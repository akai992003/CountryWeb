using System;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Data;
using CountryWeb.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CountryWeb.Controllers
{
    [ApiController]
    [EnableCors("CorsDomain")]
    public class WSController : ControllerBase
    {
        private readonly IvPService IvP;
        private readonly ICovid19Service ICovid19;
        private readonly JwtHelpers IJwt;
        private IConfiguration Iconf { get; }
        private string issuer { get; }
        private string signKey { get; }

        public WSController(IvPService IvPService, IConfiguration Configuration, ICovid19Service ICovid19Service, JwtHelpers JwtHelpers)
        {
            this.IvP = IvPService;
            this.ICovid19 = ICovid19Service;
            this.IJwt = JwtHelpers;
            this.Iconf = Configuration;
            this.issuer = UStore.GetUStore(Iconf["JwtSettings:Issuer"], "Issuer");
            this.signKey = UStore.GetUStore(Iconf["JwtSettings:SignKey"], "SignKey");
        }

        [HttpPost("~/Fetch")]
        public JObject Fetch()
        {
            // 安全驗證碼
            var sec = SecurityModule.CreateRecaptchaString();
            var SECURITY = sec[0];
            var Answer = sec[1];

            // 預約日期清單
            var VP = this.IvP.GetvP1();
            JArray Data1 = new JArray();
            foreach (var p in VP)
            {
                var J = new JObject { //
                        { "head", p.head }, //
                        { "id", p.id.ToString() }, //
                        
                    };

                if (p.Fulls == true)
                {
                    J["disabled"] = true;
                }

                Data1.Add(J);
            }

            // token
            string token = this.IJwt.GenerateToken_3min(issuer, signKey);
            var result = new JObject { // 
                { "security", SECURITY },
                { "answer", Answer },
                { "vp1", Data1 },
                { "token", token },
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

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                /* 判斷token的時間與 call api 的時間差*/
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                TimeSpan ts = DateTime.Now - date;

                if (ts.TotalMinutes >= 5)
                {
                    result["msg"] = "已超過網頁安全時間(五分鐘),請重整網頁後再預約";
                    result["code"] = "300";
                    return result;
                }

                /* 可能會額滿 */
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

                /* 已預約過的不可再預約 */
                var cod = this.ICovid19.GetOne(dto.id);
                if (cod != "")
                {
                    result["msg"] = cod;
                    result["code"] = "500";
                    return result;
                }

                /* 預約成功 */
                // 預約完要出現提醒示窗。(預約施打的日期時間，提醒事項)

                result["msg"] = string.Format("請於 {0} - {1} 至 宏恩綜合醫院疫苗門診 施打疫苗,謝謝", vP2.date1.ToString("yyyy-MM-dd"), vP2.week);
                result["code"] = "200";
                this.ICovid19.NewOne(dto);

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

                /* TODO 已預約過的不可再預約 */
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

        //
        [HttpPost("~/Search1")]
        public JObject Search1(dtoId dto)
        {
            var result = new JObject { // 
                { "msg", "" },
            };

            var msg = this.ICovid19.GetOne(dto.id);
            if (msg != "")
            {
                result["msg"] = msg;

            } else {
                result["msg"] = "查無紀錄";
            }

            return result;
        }

    }
}