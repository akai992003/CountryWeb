using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CountryWeb.Data
{
    /* AZ 疫苗預約日期 */
    public class VP_AZ
    {
        [Key]
        public int Id { get; set; }
        public string Head { get; set; }
        public DateTime Date1 { get; set; }
        public string Week { get; set; }
        public int Cnt { get; set; }
    }

    /* Moderna 疫苗預約日期 */
    public class VP_MO
    {
        [Key]
        public int Id { get; set; }
        public string Head { get; set; }
        public DateTime Date1 { get; set; }
        public string Week { get; set; }
        public int Cnt { get; set; }
    }

    public class VPCategory
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

    public interface IVPService
    {
        // 取得AZ預約日期列表
        List<dtoVP2> GetVPAZ1();

        // 用id取得AZ單一預約日期的資訊
        dtoVP GetVPAZ2(string id);

        // 取得 Moderna 預約日期列表
        List<dtoVP2> GetVPMO1();

        // 用id取得 Moderna 單一預約日期的資訊
        dtoVP GetVPMO2(string id);

        // 取得接種身份類別
        List<VPCategory> GetVPCategoryList();
    }

    public class VPService : IVPService
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }
        public VPService(IConfiguration IConfiguration, TgContext TgContext)
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

        // 取得 AZ 預約日期列表
        public List<dtoVP2> GetVPAZ1()
        {
            var l = new List<dtoVP2>();
            try
            {
                // * Echo 2021-08-08 Rename SP
                var SqlStr = string.Format("execute SP_GetVPAZ");
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = cn.Query<dtoVP>(SqlStr);

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

        // 用 id 取得 AZ 單一預約日期的資訊
        public dtoVP GetVPAZ2(string id)
        {
            if (id == null || id == "")
            {
                return null;
            }
            try
            {
                // * Echo 2021-08-08 Rename SP
                var SqlStr = string.Format("execute SP_GetVPAZCnt {0}", id);
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = cn.Query<dtoVP>(SqlStr);
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
                // throw;
            }

        }

        // 取得 Moderna 預約日期列表
        public List<dtoVP2> GetVPMO1()
        {
            var l = new List<dtoVP2>();
            try
            {
                // Echo Add on 2021-08-06 這裡沒改到sp名稱
                // * Echo 2021-08-08 Rename SP
                var SqlStr = string.Format("execute SP_GetVPMO");
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = cn.Query<dtoVP>(SqlStr);

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

        // 用 id 取得 Moderna 單一預約日期的資訊
        public dtoVP GetVPMO2(string id)
        {
            if (id == null || id == "")
            {
                return null;
            }
            try
            {
                var SqlStr = string.Format("execute SP_GetVPMOCnt {0}", id);
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = cn.Query<dtoVP>(SqlStr);
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
        public List<VPCategory> GetVPCategoryList()
        {
            using (var context = new TgContext())
            {
                var q = (from p in context.VPCategory
                         where p.deleted == 0
                         orderby p.Id
                         select p).AsEnumerable().AsQueryable();
                return q.ToList();
            }
        }
    }
}