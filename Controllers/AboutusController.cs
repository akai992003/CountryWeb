// 認識宏恩
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
    public class AboutusController : Controller
    {
        public IActionResult Manageteam() //管理團隊
        {
            return View();
        }
        public IActionResult Mission() //願景使命
        {
            return View();
        }
        public IActionResult Importevent() //重大事記
        {
            return View();
        }
        public IActionResult Organization() //組織架構
        {
            return View();
        }
    }
}