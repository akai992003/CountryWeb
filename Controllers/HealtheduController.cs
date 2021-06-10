using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Data;
using CountryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace CountryWeb.Controllers
{
    public class HealtheduController : Controller
    {
        // Echo Add
        private readonly InewsListsService InewsLists;
        public HealtheduController(InewsListsService InewsListsService)
        {
            this.InewsLists = InewsListsService;
        }

        public IActionResult Epidemicnewknow() //防疫新知
        {
            var a1 = new List<dtoA2>();
            a1.Add(new dtoA2
            {
                publish_date = "2021-06-10",
                title = "常見腸道寄生蟲病",
            });
            a1.Add(new dtoA2
            {
                publish_date = "2021-06-09",
                title = "登革熱防治",
            });
            a1.Add(new dtoA2
            {
                publish_date = "2021-06-08",
                title = "H5N1流感",
            });
            ViewBag.A1 = a1;
            return View();
        }

        // Echo:改成 async
        public async Task<IActionResult> HealthformAsync() //衛教單張
        {
            // var a1 = new List<dtoA2>();
            // a1.Add(new dtoA2 { title = "全部" });
            // a1.Add(new dtoA2 { title = "心臟內科" });
            // a1.Add(new dtoA2 { title = "外科" });

            // Echo
            var a1 = await this.InewsLists.GetNews1();
            var a2 = await this.InewsLists.GetNews2();
            // var a2 = new List<dtoNews1>();
            // a2.Add(new dtoNews1
            // {
            //     type = "心臟內科",
            //     publish_date = "2021-06-10",
            //     title = "心臟病日常生活須知",
            // });
            // a2.Add(new dtoNews1
            // {
            //     type = "心臟內科",
            //     publish_date = "2021-06-09",
            //     title = "心臟衰竭護理指導",
            // });
            // a2.Add(new dtoNews1
            // {
            //     type = "心臟內科",
            //     publish_date = "2021-06-08",
            //     title = "冠狀動脈疾病(心絞痛與心肌梗塞)",
            // });

            ViewBag.A1 = a1;
            ViewBag.A2 = a2;

            return View();
        }
    }

    public class dtoA2
    {
        public string type { get; set; }
        public string publish_date { get; set; }
        public string title { get; set; }

    }
}