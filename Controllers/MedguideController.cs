// 就醫指南
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
    public class MedguideController : Controller
    {
        public IActionResult Mednotice() //權益與須知
        {
            return View();
        }
        public IActionResult Admission() //住院服務
        {
            return View();
        }
        public IActionResult Emergencyservice() //急診服務
        {
            return View();
        }
         public IActionResult Charge() //收費說明
        {
            return View();
        }
    }
}