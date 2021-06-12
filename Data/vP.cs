using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;

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
        public DateTime date1 { get; set; }
        public string week { get; set; }
        public int Cnt { get; set; }
        public int Cnt2 { get; set; }

    }

    public class dtovP2
    {
        public int id { get; set; }
        public string head { get; set; }
        public bool Fulls { get; set; }
    }

    public interface IvPService
    {
        Task<List<dtovP2>> GetvP1();
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

        public async Task<List<dtovP2>> GetvP1()
        {
            // using (var context = new TgContext())
            // {
            // var q = await (from p in context.vP
            //                where p.id > 16
            //                select new dtovP
            //                {
            //                    id = p.id,
            //                    head = string.Format("{0} {1}年{2}月{3}日 {4}",p.head,p.date1.ToString("yyyy"),p.date1.ToString("MM"),p.date1.ToString("dd") , p.week)  
            //                }).ToListAsync();
            // return q;
            // }    
            var SqlStr = string.Format("execute SP_GetvP");
            using (var cn = new SqlConnection(this._dapperconn))
            {
                var q = await cn.QueryAsync<dtovP>(SqlStr);
                // return q.ToList();
                var l = new List<dtovP2>();
                foreach (var p in q)
                {
                    var vp = new dtovP2();
                    vp.id = p.id;

                    var c = "";
                    if (p.Cnt2 >= p.Cnt)
                    {
                        c = "已額滿";
                        vp.Fulls = true;
                    } else {
                        vp.Fulls = false;
                    }
                    var head = string.Format("{0} {1}年{2}月{3}日 {4} {5}", p.head, p.date1.ToString("yyyy"), p.date1.ToString("MM"), p.date1.ToString("dd"), p.week, c);

                    vp.head = head;
                    l.Add(vp);
                }
                return l;
            }

        }
    }
}