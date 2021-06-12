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

        public HomeController(ILogger<HomeController> logger, IvPService IvPService)
        {
            _logger = logger;
            IvP = IvPService;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var d = new dtoCovid19 ();

            var sec = SecurityModule.CreateRecaptchaString ();
            d.SECURITY = sec[0];
            ViewBag.Answer = sec[1];
            
            var vP1 = await this.IvP.GetvP1();
            List<SelectListItem> vPId = new List<SelectListItem>();
            foreach (var p in vP1)
            {
                vPId.Add(new SelectListItem()
                {
                    Text = p.head,
                    Value = p.id.ToString()
                });
            }

            ViewBag.vPId = vPId;
            return View(d);
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