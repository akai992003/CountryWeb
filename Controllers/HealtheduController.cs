using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CountryWeb.Controllers
{
    public class HealtheduController : Controller
    {
        public IActionResult Epidemicnewknow() //防疫新知
        {
            return View();
        }
        public IActionResult Healthform() //衛教單張
        {
            var a1 = new List<dtoA2>();
            a1.Add(new dtoA2 { title = "全部" });
            a1.Add(new dtoA2 { title = "心臟內科" });


            var a2 = new List<dtoA2>();
            a2.Add(new dtoA2
            {
                publish_date = "2021-06-10",
                title = "戒檳之衛教指導",
            }
            );
            a2.Add(new dtoA2
            {
                publish_date = "2021-06-09",
                title = "什麼是口腔癌前病變",
            }
           );

            ViewBag.A1 = a1;
            ViewBag.A2 = a2;

            return View();
        }
    }

    public class dtoA2
    {
        public string publish_date { get; set; }
        public string title { get; set; }

    }
}