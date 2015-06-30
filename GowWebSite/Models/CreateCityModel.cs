using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GowWebSite.Models
{
    public class CreateCityModel
    {
        [EmailAddress]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CityName { get; set; }
        [Range(1,510)]
        public int CityX { get; set; }
        [Range(1, 1022)]
        public int CityY { get; set; }
        public string Alliance { get; set; }
        public int ResourceTypeID { get; set; }
        [Range(1, 21)]
        public int SHLevel { get; set; }
    }
}