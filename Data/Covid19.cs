using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;

namespace CountryWeb.Data
{
    public class Covid19
    {
        [Key]
        public Guid Guid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Addr_country { get; set; }
        public string Addr_city { get; set; }
        public string Addr_detal { get; set; }
        public int VPId { get; set; }
        public int Idtypes { get; set; }
    }

    public class A2E
    {
        [Key]
        public Guid Guid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Addr_country { get; set; }
        public string Addr_city { get; set; }
        public string Addr_detal { get; set; }
        public int Done { get; set; }
    }

    public class dtoCovid19
    {
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
        public string idtypes { get; set; }
    }

    public class dtoA2E
    {
        public string id { get; set; }
        public string name { get; set; }
        public string birthday_year { get; set; }
        public string birthday_month { get; set; }
        public string birthday_day { get; set; }
        public string phone { get; set; }
        public string sECURITY { get; set; }
        public string addr_country { get; set; }
        public string addr_city { get; set; }
        public string addr_detal { get; set; }
    }

    public class dtoId
    {
        public string id { get; set; }
    }

    public class dtoGuid
    {
        public Guid guid { get; set; }
    }

    public class dtoCovid19VIP
    {
        public Guid guid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public DateTime date1 { get; set; }
        public string date2 { get; set; }
        public string week { get; set; }


    }

    public interface ICovid19Service
    {
        /// <summary>
        /// 新增疫苗預約
        /// </summary>
        Task NewOne(dtoCovid19 dto);

        /// <summary>
        /// 判斷是否有預約疫苗
        /// </summary>
        dtoCovid19VIP GetOne(string id);

        /// <summary>
        /// 取消疫苗預約
        /// </summary>
        void CancelOne(Guid guid);

        /// <summary>
        /// 是否殘劑預約
        /// </summary>
        string GetA2E(string id);

        /// <summary>
        /// 新增殘劑預約
        /// </summary>
        Task NewA2E(dtoA2E dto);

        /// <summary>
        /// 殘劑預約人數
        /// </summary>
        int GetA2ECnt();
    }

    public class Covid19Service : ICovid19Service
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }
        public Covid19Service(IConfiguration IConfiguration, TgContext TgContext)
        {
            this.IConf = IConfiguration;
            if (TgContext.ConnS == null || TgContext.ConnS == "")
            {
                this._dapperconn = UStore.GetUStore(this.IConf["ConnectionStrings:Root"], "Root");
            }
            else
            {
                this._dapperconn = TgContext.ConnS;
            }
        }

        /// <summary>
        /// 新增疫苗預約
        /// </summary>
        public async Task NewOne(dtoCovid19 dto)
        {
            var d = new Covid19();
            d.Guid = Guid.NewGuid();
            d.Id = dto.id;
            d.Name = dto.name;
            var bir = new DateTime(int.Parse(dto.birthday_year), int.Parse(dto.birthday_month), int.Parse(dto.birthday_day), 0, 0, 0);
            d.Birthday = bir.ToString("yyyyMMdd");
            d.Addr_country = dto.addr_country;
            d.Addr_city = dto.addr_city;
            d.Addr_detal = dto.addr_detal;
            d.Phone = dto.phone;
            d.VPId = int.Parse(dto.vpid);
            d.Idtypes = int.Parse(dto.idtypes);

            using (var context = new TgContext())
            {
                await context.Covid19.AddAsync(d);
                await context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// 判斷是否有預約疫苗
        /// </summary>
        public dtoCovid19VIP GetOne(string id)
        {
            using (var context = new TgContext())
            {
                var q = (from p in context.Covid19
                         join p2 in context.VP on p.VPId equals p2.Id
                         where p.Id == id
                         && p.VPId > 0
                         select new dtoCovid19VIP
                         {
                             guid = p.Guid,
                             id = p.Id,
                             name = p.Name,
                             date1 = p2.Date1,
                             date2 = p2.Date1.ToString("yyyy-MM-dd"),
                             week = p2.Week
                         }).FirstOrDefault();
                if (q != null)
                {
                    // 已預約
                    return q;
                    // return string.Format("已預約 {0} - {1} 的時段", q.date, q.week);
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// 取消疫苗預約
        /// </summary>
        public void CancelOne(Guid guid)
        {
            using (var context = new TgContext())
            {
                var q = context.Covid19.Find(guid);
                if (q != null)
                {
                    context.Covid19.Attach(q);
                    q.VPId = 0;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 新增殘劑預約
        /// </summary>
        public async Task NewA2E(dtoA2E dto)
        {
            var d = new A2E();
            d.Guid = Guid.NewGuid();
            d.Id = dto.id;
            d.Name = dto.name;
            var bir = new DateTime(int.Parse(dto.birthday_year), int.Parse(dto.birthday_month), int.Parse(dto.birthday_day), 0, 0, 0);
            d.Birthday = bir.ToString("yyyyMMdd");
            d.Addr_country = dto.addr_country;
            d.Addr_city = dto.addr_city;
            d.Addr_detal = dto.addr_detal;
            d.Phone = dto.phone;
            d.Done = 0;
            using (var context = new TgContext())
            {
                await context.A2E.AddAsync(d);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 是否殘劑預約
        /// </summary>
        public string GetA2E(string id)
        {
            using (var context = new TgContext())
            {
                //A2E
                var q = (from p in context.A2E
                         where p.Id == id
                         select p).FirstOrDefault();
                if (q != null)
                {
                    return string.Format("您已有預約紀錄了");
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 殘劑預約人數
        /// </summary>
        public int GetA2ECnt()
        {
            using (var context = new TgContext())
            {
                var q = (from p in context.A2E
                         where p.Done == 0
                         select p);
                if (q.FirstOrDefault() != null)
                {
                    return q.ToList().Count;

                }
                else
                {
                    return 0;
                }
            }
        }


    }
}