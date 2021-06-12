using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CountryWeb.Data
{
    public class Covid19
    {
        [Key]
        public Guid Guid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
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
        // public string Answer{ get; set; } 
    }


}