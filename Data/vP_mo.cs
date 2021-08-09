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
    public class VP_MO
    {
        [Key]
        public int Id { get; set; }
        public string Head { get; set; }
        public DateTime Date1 { get; set; }
        public string Week { get; set; }
        public int Cnt { get; set; }
    }

    public class dtoVPMO
    {
        public int id { get; set; }
        public string head { get; set; }
        public DateTime date1 { get; set; }
        public string week { get; set; }
        public int cnt { get; set; }
        public int cnt2 { get; set; }
    }

    public class dtoVP2MO
    {
        public int id { get; set; }
        public string head { get; set; }
        public bool Fulls { get; set; }
    }

    public interface IVPMOService
    {
        /// <summary>
        /// 取得預約日期列表
        /// </summary>
        List<dtoVP2MO> GetvP1();

        /// <summary>
        /// 用id取得單一預約日期的資訊
        /// </summary>
        dtoVPMO GetvP2(string id);


        /// <summary>
        /// 取得接種身份類別
        /// </summary>
        List<VPCategory> GetVPCategoryList();
    }


    public class VPMOService : IVPMOService
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }
        public VPMOService(IConfiguration IConfiguration, TgContext TgContext)
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
        /// 取得預約日期列表
        /// </summary>
        public List<dtoVP2MO> GetvP1()
        {
            var l = new List<dtoVP2MO>();
            try
            {
                // Echo Add on 2021-08-06 這裡沒改到sp名稱
                // * Echo 2021-08-08 Rename SP
                var SqlStr = string.Format("execute SP_GetVPMO");
                using (var cn = new SqlConnection(this._dapperconn))
                {
                    var q = cn.Query<dtoVPMO>(SqlStr);

                    foreach (var p in q)
                    {
                        if (DateTime.Today < p.date1)
                        {
                            var vp = new dtoVP2MO();
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
                //throw;
            }


        }

        /// <summary>
        /// 用id取得單一預約日期的資訊
        /// </summary>
        public dtoVPMO GetvP2(string id)
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
                    var q = cn.Query<dtoVPMO>(SqlStr);
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

        /// <summary>
        /// 取得接種身份類別
        /// </summary>
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