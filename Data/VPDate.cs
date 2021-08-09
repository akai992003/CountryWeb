using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

namespace CountryWeb.Data
{
    /* 疫苗預約日期 */
    public class VPDate
    {
        [Key]
        public int Id { get; set; }
        public string Head { get; set; }
        public DateTime Date1 { get; set; }
        public string Week { get; set; }
        public int VPtypes { get; set; }
        public int Cnt { get; set; }
    }

    /* 預約身份 */
    public class VPCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int deleted { get; set; }
    }

    /* 疫苗種類 */
    public class VPType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int deleted { get; set; }
    }

    public class dtoVP
    {
        public int id { get; set; }
        public string head { get; set; }
        public DateTime date1 { get; set; }
        public string week { get; set; }
        public int cnt { get; set; }
        public int cnt2 { get; set; }
    }

    public class dtoVP2
    {
        public int id { get; set; }
        public string head { get; set; }
        public bool Fulls { get; set; }
    }

    public interface IVPDateService
    {
        // 取得預約日期列表
        Task<List<dtoVP2>> GetVP1(int vpType);

        // 用id取得單一預約日期的資訊
        Task<dtoVP> GetVP2(string id);

        // 取得接種身份類別
        Task<List<VPCategory>> GetVPCategoryList();

        // 取得疫苗種類
        Task<string> GetVPType(int id);
    }

    public class VPDateService : IVPDateService
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }

        public VPDateService(IConfiguration IConfiguration, TgContext TgContext)
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

        // 取得預約日期列表
        public async Task<List<dtoVP2>> GetVP1(int vpType)
        {
            var l = new List<dtoVP2>();
            try
            {
                // * Echo 2021-08-08 Rename SP
                var SqlStr = string.Format("execute SP_GetVP {0};", vpType);
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = await cn.QueryAsync<dtoVP>(SqlStr);

                    foreach (var p in q)
                    {
                        if (DateTime.Today < p.date1)
                        {
                            var vp = new dtoVP2();
                            vp.id = p.id;

                            var c = "";
                            if (p.cnt2 >= p.cnt)
                            {
                                c = "已額滿";
                                vp.Fulls = true;
                            }
                            else
                            {
                                vp.Fulls = false;
                            }
                            var head = string.Format("{0} {1}年{2}月{3}日 {4} {5}", p.head, p.date1.ToString("yyyy"), p.date1.ToString("MM"), p.date1.ToString("dd"), p.week, c);

                            vp.head = head;
                            l.Add(vp);
                        }
                    }
                    return l;
                }
            }
            catch (System.Exception)
            {
                return l;
            }
        }

        // 用 id 取得單一預約日期的資訊
        public async Task<dtoVP> GetVP2(string id)
        {
            if (id == null || id == "")
            {
                return null;
            }
            try
            {
                // * Echo 2021-08-08 Rename SP
                var SqlStr = string.Format("execute SP_GetVPCnt {0}", id);
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = await cn.QueryAsync<dtoVP>(SqlStr);
                    var p = q.FirstOrDefault();
                    if (p != null)
                    {
                        return p;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        // 取得接種身份類別
        public async Task<List<VPCategory>> GetVPCategoryList()
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.VPCategory
                               where p.deleted == 0
                               orderby p.Id
                               select p).ToListAsync();
                return q;
            }
        }

        // 取得疫苗種類
        public async Task<string> GetVPType(int id)
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.VPType
                               where p.Id == id
                               && p.deleted == 0
                               orderby p.Id
                               select p).FirstOrDefaultAsync();
                if (q == null)
                {
                    return "AZ";
                }
                else
                {
                    return q.Name;
                }
            }
        }
    }
}