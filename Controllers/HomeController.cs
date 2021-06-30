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
        private IConfiguration Iconf { get; }
        private string uRI { get; }
        private readonly ICovid19Service ICovid19;

        public HomeController(IConfiguration Configuration, ICovid19Service ICovid19Service)
        {
            this.Iconf = Configuration;
            this.uRI = UStore.GetUStore(Iconf["ConnectionStrings:uRI"], "uRI");
            this.ICovid19 = ICovid19Service;
        }

        public IActionResult A2E()
        {

            var cnt = this.ICovid19.GetA2ECnt();
            // 830 + done = 0
            ViewBag.Cnt = 830 + cnt;
            ViewBag.uRI = this.uRI;
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.uRI = this.uRI;
            return View();
        }




    }
}