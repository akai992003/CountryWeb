using System;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CountryWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsDomain")]
    // [Authorize]
    public class WSController : ControllerBase
    {
        private readonly IvPService IvP;
        private readonly ICovid19Service ICovid19;

        public WSController(IvPService IvPService, ICovid19Service ICovid19Service)
        {
            this.IvP = IvPService;
            this.ICovid19 = ICovid19Service;
        }


        [HttpPost]
        [Route("CheckOne")]
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
                if (ts.TotalMinutes >= 3)
                {
                    result["msg"] = "已超過網頁安全時間";
                    result["code"] = "300";
                    return result;
                }

                /* 可能會額滿 */
                var vP2 = this.IvP.GetvP2(dto.Vpid);
                if (vP2 == null)
                {
                    result["msg"] = "不正確的操作方式";
                    result["code"] = "600";
                    return result;

                }

                if (vP2.Cnt2 >= vP2.Cnt)
                {
                    // 已額滿
                    result["msg"] = string.Format("{0} {1} 已額滿", vP2.date1.ToString("yyyy-MM-dd"), vP2.week);
                    result["code"] = "400";
                    return result;
                }

                /* 已預約過的不可再預約 */
                var cod = this.ICovid19.GetOne(dto.Id);
                if (cod != "")
                {
                    result["msg"] = cod;
                    result["code"] = "500";
                    return result;
                }

                /* 預約成功 */
                // 預約完要出現提醒示窗。(預約施打的日期時間，提醒事項)
                result["msg"] = string.Format("請於 {0} - {1} 至 {2} 施打疫苗,謝謝", vP2.date1.ToString("yyyy-MM-dd"), vP2.week, vP2.head);
                result["code"] = "200";
                this.ICovid19.NewOne(dto);

            }

            return result;


        }

    }
}