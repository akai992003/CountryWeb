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
    public class HealtheduController: Controller
    {
        public IActionResult Epidemicnewknow() //防疫新知
        {
            return View();
        }
         public IActionResult Healthform() //衛教單張
        {
            return View();
        }
    }
}