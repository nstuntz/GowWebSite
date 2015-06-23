using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GowWebSite.Models
{
    public class CreateCityFullModel
    {
        [EmailAddress]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CityName { get; set; }
        public string PIN { get; set; }
        [Range(1, 510)]
        public int CityX { get; set; }
        [Range(1, 1022)]
        public int CityY { get; set; }
        public int AllianceID { get; set; }
        public int ResourceTypeID { get; set; }
        [Range(1, 21)]
        public int SHLevel { get; set; }
        [Range(1, 4)]
        public int RSSBank { get; set; }
        [Range(1, 4)]
        public int SilverBank { get; set; }
        [Range(0, 5)]
        public int RSSMarches { get; set; }
        [Range(0, 5)]
        public int SilverMarches { get; set; }
        public bool Upgrade { get; set; }
        public bool Bank { get; set; }
        public bool Shield { get; set; }
        [Range(0, 6000)]
        public DateTime? LastShieldDate { get; set; }
        public int LoginDelayMin { get; set; }
        public bool Rally { get; set; }
        [Range(0, 510)]
        public int RallyX { get; set; }
        [Range(0, 1022)]
        public int RallyY { get; set; }
        public bool HasGoldMine { get; set; }

        public CreateCityFullModel()
        {
            RSSBank = 1;
            SilverBank = 1;
            RSSMarches = 1;
            SilverMarches = 1;
            Upgrade = false;
            Bank = true;
            Shield = false;
            LoginDelayMin = 180;
            Rally = false;
            RallyX = 0;
            RallyY = 0;
        }
        //string userName, 
        //    string password, 
        //        string cityName, 
        //            string pIN, 
        //                Nullable<int> cityX, 
        //Nullable<int> cityY, 
        //    Nullable<int> allianceID, 
        //Nullable<int> rSSType, 
        //    Nullable<int> sHLevel, 
        //Nullable<int> rSSBank, 
        //    Nullable<int> silverBank, 
        //Nullable<int> rssMarches, 
        //    Nullable<int> silverMarches, 
        //Nullable<bool> upgrade, 
        //    Nullable<int> loginDelayMin, 
        //Nullable<bool> shield, 
        //    Nullable<System.DateTime> lastShieldDate, 
        //Nullable<bool> bank
    }
}