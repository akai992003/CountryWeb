
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
    public class SerpeoController: Controller
    {
         public IActionResult Seekapply() //就醫資料申請
        {
            return View();
        }
         public IActionResult Sersociety() //社會服務
        {
            return View();
        }
         public IActionResult Patientshare() //病友分享
        {
            return View();
        }
         public IActionResult Satisfactionques() //滿意度調查
        {
            return View();
        }
        public IActionResult Trafficguide() //到院指南
        {
            return View();
        }
        public IActionResult Commonques() //常見問題
        {
            return View();
        }
    }
}