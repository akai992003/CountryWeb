using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CountryWeb.Data
{
    public class vP
    {
        [Key]
        public int id { get; set; }
        public string head { get; set; }
        public DateTime date1 { get; set; }
        public string week { get; set; }
        public int Cnt { get; set; }
    }

    public class dtovP
    {
        public int id { get; set; }
        public string head { get; set; }

    }

    public interface IvPService
    {
        Task<List<dtovP>> GetvP1();
    }

    public class vPService : IvPService
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }
        public vPService(IConfiguration IConfiguration, TgContext TgContext)
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

        public async Task<List<dtovP>> GetvP1()
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.vP
                               where p.id > 16
                               select new dtovP
                               {
                                   id = p.id,
                                   head = string.Format("{0} {1}年{2}月{3}日 {4}",p.head,p.date1.ToString("yyyy"),p.date1.ToString("MM"),p.date1.ToString("dd") , p.week)  
                               }).ToListAsync();
                return q;
            }
        }
    }
}