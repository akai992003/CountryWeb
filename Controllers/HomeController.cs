using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CountryWeb.Data;
using CountryWeb.Helper;
using CountryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ServiceReference;

namespace CountryWeb.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Iconf { get; }
        private string uRI { get; }
        private readonly ICovid19Service ICovid19;
        private readonly INHIQP701Service INHIQP701;
        private readonly IVPService IvP;

        public HomeController(IConfiguration Configuration, ICovid19Service ICovid19Service
        , INHIQP701Service INHIQP701Service, IVPService IVPService)
        {
            this.Iconf = Configuration;
            this.uRI = UStore.GetUStore(Iconf["ConnectionStrings:uRI"], "uRI");
            this.ICovid19 = ICovid19Service;
            this.INHIQP701 = INHIQP701Service;
            this.IvP = IVPService;
        }

        public IActionResult A2E()
        {
            var cnt = this.ICovid19.GetA2ECnt();
            // 830 + done = 0
            ViewBag.Cnt = 1316 + cnt;
            ViewBag.uRI = this.uRI;
            return View();
        }

        public IActionResult Index()
        {
            // var ct = this.IvP.GetVPCategoryList();
            var disableds = "disabled";
            if (DateTime.Now >= new DateTime(2021, 7, 1, 12, 0, 0))
            {
                disableds = "";
            }
            ViewBag.disableds = disableds;
            ViewBag.uRI = this.uRI;
            // ViewBag.ct = ct;
            return View();
        }


        [HttpPost]
        //button 送出時做的事
        public async Task<IActionResult> Test(dto3 dto)
        {

            NHIQP701SoapClient.EndpointConfiguration endpoint = new NHIQP701SoapClient.EndpointConfiguration();
            NHIQP701SoapClient myService = new NHIQP701SoapClient(endpoint, "https://medvpnws.nhi.gov.tw:7008/qp7e2000/NHIQP701.asmx");
            // AuthorizationSoapHeader MyAuthHeader = new AuthorizationSoapHeader();

            // MyAuthHeader.AppName = FDSServiceAppName;
            // MyAuthHeader.AppID = Guid.Parse(MyAppID);
            var NHIQ = this.INHIQP701.GetOne();
            var q = new dto2();

            q.sHospId = "1101020036";
            q.sPatId = dto.id;
            q.sValidSDate = DateTime.Now.ToString("yyyyMMdd"); // "20210709"
            q.sClientRandom = NHIQ.sClientRandom;
            q.sSignature = NHIQ.sSignature;
            q.sSamId = "000000081174";
            var json = JsonSerializer.Serialize(q);

            var entries = await myService.GetVaccLstDataAsync(json);
            var res = entries.Body.GetVaccLstDataResult;
            // {"RtnCode":"01","oVaccLstData":null}

            var res2 = JsonSerializer.Deserialize<dto1>(res);
            ViewBag.RtnCode = res2.RtnCode;
            ViewBag.oVaccLstData = res2.oVaccLstData;

            // var dto1 = new dto3();
            // dto1.id = "";
            // dto1.sValidSDate = DateTime.Now.ToString("yyyyMMdd");
            // return View("Test", dto1);
            return RedirectToAction("Test", "Home", new { RtnCode = res2.RtnCode, oVaccLstData = res2.oVaccLstData });

        }

        //Form load 時做的事
        [HttpGet]
        public IActionResult Test(string RtnCode = "", string oVaccLstData = "")
        {
            if (RtnCode != "")
            {
                ViewBag.RtnCode = RtnCode;
                ViewBag.oVaccLstData = oVaccLstData;
            }
            else
            {
                ViewBag.RtnCode = "";
                ViewBag.oVaccLstData = "";
            }
            var dto3 = new dto3();
            dto3.id = "F125600799";
            dto3.sValidSDate = DateTime.Now.ToString("yyyyMMdd");
            return View(dto3);
        }
    }

}