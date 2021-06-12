using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int vPId { get; set; }
    }

    public class dtoCovid19
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string vPId { get; set; } // 預約日期
        public string SECURITY { get; set; }
    }

    public interface ICovid19Service
    {
        Task NewOne(dtoCovid19 dto);
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

        public async Task NewOne(dtoCovid19 dto)
        {
            var d = new Covid19();
            d.Guid = Guid.NewGuid();
            d.Id = dto.Id;
            d.Name = dto.Name;
            d.Birthday = dto.Birthday;
            d.Phone = dto.Phone;
            d.vPId = int.Parse(dto.vPId);
            using (var context = new TgContext())
            {
                await context.Covid19.AddAsync(d);
                await context.SaveChangesAsync();
            }
        }
    }
}