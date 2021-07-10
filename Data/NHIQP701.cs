using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Helper;
using Microsoft.Extensions.Configuration;


namespace CountryWeb.Data
{
    public class NHIQP701
    {
        [Key]
        public Guid Guid { get; set; }
        public string sClientRandom { get; set; }
        public string sSignature { get; set; }

    }

    public interface INHIQP701Service
    {
        NHIQP701 GetOne();
    }

    public class NHIQP701Service : INHIQP701Service
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }
        public NHIQP701Service(IConfiguration IConfiguration, TgContext TgContext)
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

        public NHIQP701 GetOne()
        {
            var g = Guid.Parse("5EA60285-5D5D-4070-B9A4-68B91A8C04BB");
            using (var context = new TgContext())
            {
                // var q = (from p in context.NHIQP701
                // where p.Guid == g
                //          select p).FirstOrDefault();
                var q = context.NHIQP701.Find(g);
                return q;

            }
        }
    }

}