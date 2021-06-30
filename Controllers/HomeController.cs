using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Data;
using CountryWeb.Helper;
using CountryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CountryWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IvPService IvP;
        private readonly JwtHelpers IJwt;
        private readonly ICovid19Service ICovid19;
        private IConfiguration Iconf { get; }
        private string issuer { get; }
        private string signKey { get; }

        // TODO 新增使用身份證字號預約查詢功能。
        public HomeController(ILogger<HomeController> logger,
        IvPService IvPService, ICovid19Service ICovid19Service,
        JwtHelpers JwtHelpers,
        IConfiguration Configuration)
        {
            _logger = logger;
            this.IvP = IvPService;
            this.ICovid19 = ICovid19Service;
            this.IJwt = JwtHelpers;
            this.Iconf = Configuration;
            this.issuer = UStore.GetUStore(Iconf["JwtSettings:Issuer"], "Issuer");
            this.signKey = UStore.GetUStore(Iconf["JwtSettings:SignKey"], "SignKey");
        }


        public async Task<IActionResult> IndexAsync()
        {
            var d = new dtoCovid19();
            // // test
            // d.Id = "A224671095";
            // d.Name = "Echo Kao";
            // d.Birthday = "19780212";
            // d.Phone = "0988670002";
             
            // // test

            var sec = SecurityModule.CreateRecaptchaString();
            d.sECURITY = sec[0];
            ViewBag.Answer = sec[1];

            var vP1 = await this.IvP.GetvP1();
            List<SelectListItem> Vpid = new List<SelectListItem>();
            foreach (var p in vP1)
            {
                if (p.Fulls == true)
                {
                    Vpid.Add(new SelectListItem()
                    {
                        Text = p.head,
                        Value = p.id.ToString(),
                        Disabled = true
                    });
                }
                else
                {
                    Vpid.Add(new SelectListItem()
                    {
                        Text = p.head,
                        Value = p.id.ToString()
                    });
                }
            }

            ViewBag.Vpid = Vpid;

            string token = this.IJwt.GenerateToken_3min(issuer, signKey);
            ViewBag.token = token;

            return View(d);
        }

        // [HttpPost]
        // public async Task<IActionResult> Covid19AddAsync(dtoCovid19 dto)
        // {
        //     //todo 已預約過的不可再預約。
        //     //todo 預約完要出現提醒示窗。(預約施打的日期時間，提醒事項)
        //     await this.ICovid19.NewOne(dto);
        //     return RedirectToAction("Index", "Home");
        // }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult tel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}