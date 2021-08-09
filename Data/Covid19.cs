using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountryWeb.Data
{
    // * Echo 2021-08-09
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

        // 疫苗種類
        public int VPtypes { get; set; }

        // * Echo 2021-08-08 新增結構
        public DateTime CreateTime { get; set; }
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

        // * Echo 2021-08-08 Add
        public string vptype { get; set; } // 疫苗種類 1=AZ , 2=Moderna , 3=高端 , 4=BNT
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
        public int vptype { get; set; } // 疫苗種類 1=AZ , 2=Moderna , 3=高端 , 4=BNT
    }

    public interface ICovid19Service
    {

        // 新增疫苗預約
        Task NewOne(dtoCovid19 dto);

        // 判斷是否有預約疫苗
        Task<dtoCovid19VIP> GetOne(string id);

        // 取消疫苗預約
        Task CancelOne(Guid guid);

        // 是否殘劑預約
        Task<string> GetA2E(string id);

        // 新增殘劑預約
        Task NewA2E(dtoA2E dto);

        // 殘劑預約人數
        Task<int> GetA2ECnt();
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

        // 新增疫苗預約
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
            d.VPtypes = int.Parse(dto.vptype); // echo
            d.CreateTime = DateTime.Now; // echo

            using (var context = new TgContext())
            {
                await context.Covid19.AddAsync(d);
                await context.SaveChangesAsync();
            }
        }

        // 判斷是否有預約疫苗
        public async Task<dtoCovid19VIP> GetOne(string id)
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.Covid19
                               join p2 in context.VPDate on p.VPId equals p2.Id
                               where p.Id == id
                               && p.VPId > 0
                               select new dtoCovid19VIP
                               {
                                   guid = p.Guid,
                                   id = p.Id,
                                   name = p.Name,
                                   date1 = p2.Date1,
                                   date2 = p2.Date1.ToString("yyyy-MM-dd"),
                                   week = p2.Week,
                                   vptype = p.VPtypes
                               }).FirstOrDefaultAsync();
                if (q != null)
                {
                    // 已預約
                    return q;
                }
                else
                {
                    return null;
                }
            }

        }

        // 取消疫苗預約
        public async Task CancelOne(Guid guid)
        {
            using (var context = new TgContext())
            {
                var q = context.Covid19.Find(guid);
                if (q != null)
                {
                    context.Covid19.Attach(q);
                    q.VPId = 0;
                    q.CreateTime = DateTime.Now; // * Echo 2021-08-08 Add
                    await context.SaveChangesAsync();
                }
            }
        }

        #region 殘劑

        // 新增殘劑預約
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

        // 是否殘劑預約
        public async Task<string> GetA2E(string id)
        {
            using (var context = new TgContext())
            {
                //A2E
                var q = await (from p in context.A2E
                               where p.Id == id
                               select p).FirstOrDefaultAsync();
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

        // 殘劑預約人數
        public async Task<int> GetA2ECnt()
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.A2E
                               where p.Done == 0
                               select p).ToListAsync();
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

        #endregion


    }
}
