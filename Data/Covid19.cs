using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;

namespace CountryWeb.Data {
    public class Covid19 {
        [Key]
        public Guid Guid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Addr_country { get; set; }
        public string Addr_city { get; set; }
        public string Addr_detal { get; set; }
        public int vPId { get; set; }
    }

    public class dtoCovid19 {
        public string id { get; set; }
        public string name { get; set; }
        public string birthday_year { get; set; }
        public string birthday_month { get; set; }
        public string birthday_day { get; set; }
        public string phone { get; set; }
        public string vpid { get; set; } // 預約日期
        public string sECURITY { get; set; }
        public string addr_country { get; set; }
        public string addr_city { get; set; }
        public string addr_detal { get; set; }
    }

    public interface ICovid19Service {
        Task NewOne (dtoCovid19 dto);
        string GetOne (string id);
    }

    public class Covid19Service : ICovid19Service {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }
        public Covid19Service (IConfiguration IConfiguration, TgContext TgContext) {
            this.IConf = IConfiguration;
            if (TgContext.ConnS == null || TgContext.ConnS == "") {
                this._dapperconn = UStore.GetUStore (this.IConf["ConnectionStrings:Root"], "Root");
            } else {
                this._dapperconn = TgContext.ConnS;
            }
        }

        public async Task NewOne (dtoCovid19 dto) {
            var d = new Covid19 ();
            d.Guid = Guid.NewGuid ();
            d.Id = dto.id;
            d.Name = dto.name;
            var bir = new DateTime (int.Parse (dto.birthday_year), int.Parse (dto.birthday_month), int.Parse (dto.birthday_day), 0, 0, 0);
            d.Birthday = bir.ToString ("yyyyMMdd");
            // d.Birthday_year = dto.Birthday_year;
            // d.Birthday_month = dto.Birthday_month;
            // d.Birthday_day = dto.Birthday_day;
            d.Phone = dto.phone;
            d.vPId = int.Parse (dto.vpid);
            using (var context = new TgContext ()) {
                await context.Covid19.AddAsync (d);
                await context.SaveChangesAsync ();
            }
        }

        public string GetOne (string id) {
            using (var context = new TgContext ()) {
                var q = (from p in context.Covid19 join p2 in context.vP on p.vPId equals p2.id where p.Id == id select new {
                    date = p2.date1.ToString ("yyyy-MM-dd"),
                        week = p2.week
                }).FirstOrDefault ();
                if (q != null) {
                    // 以預約
                    return string.Format ("您已預約 {0} - {1} 的時段", q.date, q.week);
                } else {
                    return "";
                }
            }

        }
    }
}