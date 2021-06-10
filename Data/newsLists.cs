using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using CountryWeb.Helper;
using Microsoft.EntityFrameworkCore;

namespace CountryWeb.Data
{
    public class newsLists
    {
        [Key]
        public int no { get; set; }
        public string cat1 { get; set; }
        public string cat2 { get; set; }
        public string cat3 { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string en_title { get; set; }
        public string desc { get; set; }
        public string en_desc { get; set; }
        public string content { get; set; }
        public string en_content { get; set; }
        public string publish_date { get; set; }

    }

    public class dtoNews1
    {
        public string type { get; set; }
        public string publish_date { get; set; }
        public string title { get; set; }
    }

    public interface InewsListsService
    {
        Task<List<dtoNews1>> GetNews1();
        Task<List<dtoNews1>> GetNews2();
    }

    public class newsListsService : InewsListsService
    {
        private IConfiguration IConf { get; }
        private string _dapperconn { get; set; }

        public newsListsService(IConfiguration IConfiguration, TgContext TgContext)
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

        // 衛教單張的上方下拉選單
        public async Task<List<dtoNews1>> GetNews1()
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.newsLists
                               where p.cat2 != null
                               group p by new { p.cat2 } into g
                               select new dtoNews1
                               {
                                   title = g.Key.cat2
                               }).ToListAsync();
                return q;
            }
        }

        // 衛教單張的表格內容
        public async Task<List<dtoNews1>> GetNews2()
        {
            using (var context = new TgContext())
            {
                var q = await (from p in context.newsLists
                               where p.cat2 != null
                               orderby p.cat2 descending
                               orderby p.publish_date descending
                               orderby p.title ascending
                               select new dtoNews1
                               {
                                   type = p.cat2,
                                   publish_date = p.publish_date,
                                   title = p.title,
                               }).ToListAsync();
                return q;
            }
        }

    }

}