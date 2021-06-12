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
using Microsoft.Extensions.Logging;

namespace CountryWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IvPService IvP;
        private readonly ICovid19Service ICovid19;

        public HomeController(ILogger<HomeController> logger, IvPService IvPService, ICovid19Service ICovid19Service)
        {
            _logger = logger;
            this.IvP = IvPService;
            this.ICovid19 = ICovid19Service;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var d = new dtoCovid19();
            // // test
            //     d.Id ="A224671095";
            //     d.Name ="Echo Kao";
            //     d.Birthday = "19780212";
            //     d.Phone = "0988670002";
            // // test

            var sec = SecurityModule.CreateRecaptchaString();
            d.SECURITY = sec[0];
            ViewBag.Answer = sec[1];

            var vP1 = await this.IvP.GetvP1();
            List<SelectListItem> vPId = new List<SelectListItem>();
            foreach (var p in vP1)
            {
                if (p.Fulls == true)
                {
                    vPId.Add(new SelectListItem()
                    {
                        Text = p.head,
                        Value = p.id.ToString(),
                        Disabled = true
                    });
                }
                else
                {
                    vPId.Add(new SelectListItem()
                    {
                        Text = p.head,
                        Value = p.id.ToString()
                    });
                }
            }

            ViewBag.vPId = vPId;
            return View(d);
        }

        [HttpPost]
        public async Task<IActionResult> Covid19AddAsync(dtoCovid19 dto)
        {
            await this.ICovid19.NewOne(dto);
            return RedirectToAction("Index", "Home");
        }

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