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
            ViewBag.Cnt = 1316 + cnt;
            ViewBag.uRI = this.uRI;
            return View();
        }

        public IActionResult Index()
        {
            var disableds = "disabled";
            if (DateTime.Now >= new DateTime(2021, 7, 1, 12, 0, 0))
            {
                disableds = "";
            }
            ViewBag.disableds = disableds;
            ViewBag.uRI = this.uRI;
            return View();
        }

        // [ServiceContract]
        // public interface ISayHello
        // {
        //     [OperationContract]
        //     string Hello(string name);
        // }
        [HttpPost]
        [Route("Test")]
        //button 送出時做的事
        public async Task<IActionResult> Test(dto3 dto)
        {

            NHIQP701SoapClient.EndpointConfiguration endpoint = new NHIQP701SoapClient.EndpointConfiguration();
            NHIQP701SoapClient myService = new NHIQP701SoapClient(endpoint, "https://medvpnws.nhi.gov.tw:7008/qp7e2000/NHIQP701.asmx");
            // AuthorizationSoapHeader MyAuthHeader = new AuthorizationSoapHeader();

            // MyAuthHeader.AppName = FDSServiceAppName;
            // MyAuthHeader.AppID = Guid.Parse(MyAppID);
            var q = new dto2();

            q.sHospId = "1101020036";
            q.sPatId = dto.id;
            q.sValidSDate = "20210709";
            q.sClientRandom = "2914BE66D25DC7903520";
            q.sSignature = "8C532784879A030E49CF34AA241CA42C3065584C726C0A9F6A468F5FE519E6585690053582547CAFDB249177215AE602D14A79C62452501E8A3D0FEFE6A2C1B75311C57B3B70A9AEF30000C49DD36E863B8280155AFAE074D23A2563904C60A128EA54EEF4E5F8BD0ABBF6B00E94402ABD7A0A95271ABC9D2DFE09206432325C00000008117418DB824443A8B962576F3A9EB5CA74DFF096E95DD1C421CA953A5A069E739747D4EED2D78D5479F3C4E847496D28EE66B8893E02A62D7A31D672DF783BD403F69D85F534638C645B99186121EE13F49A3157662995D29317015C9CF9BF76E8D2C2B7CF01AE5E8060E417189EBD7FDCD5864AD074CA1EC01FAC66";
            q.sSamId = "000000081174";
            var json = JsonSerializer.Serialize(q);

            var entries = await myService.GetVaccLstDataAsync(json);
            var res = entries.Body.GetVaccLstDataResult;
            // {"RtnCode":"01","oVaccLstData":null}

            var res2 = JsonSerializer.Deserialize<dto1>(res);
            ViewBag.RtnCode = res2.RtnCode;
            ViewBag.oVaccLstData = res2.oVaccLstData;
            var dto1 = new dto3();
            dto1.id = "";
            return View(dto1);

        }
        //Form load 時做的事
        public IActionResult Test()
        {
            var dto = new dto3();
            dto.id = "F125600799";
            return View(dto);
        }
    }

    public class dto1
    {
        public string RtnCode { get; set; }
        public string oVaccLstData { get; set; }
    }

    public class dto2
    {
        public string sHospId { get; set; }
        public string sPatId { get; set; }
        public string sValidSDate { get; set; }
        public string sClientRandom { get; set; }
        public string sSignature { get; set; }
        public string sSamId { get; set; }
    }
    public class dto3
    {
        public string id { get; set; }
    }
}