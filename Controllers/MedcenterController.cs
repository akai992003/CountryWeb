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
    public class MedcenterController : Controller
    {
        public IActionResult Medteam() //醫療團隊
        {
            return View();
        }
         public IActionResult Medaffteam() //醫事團隊
        {
            return View();
        }
         public IActionResult Medespec() //特色醫療
        {
            return View();
        }
    }
}