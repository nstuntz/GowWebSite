//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GowWebSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CityInfo
    {
        public CityInfo()
        {
            this.CollectAthenaGift = false;
            this.RedeemCode = "String.Empty";
        }
    
        public int CityID { get; set; }
        public bool Bank { get; set; }
        public bool Rally { get; set; }
        public bool Tent { get; set; }
        public bool Shield { get; set; }
        public Nullable<int> RallyX { get; set; }
        public Nullable<int> RallyY { get; set; }
        public Nullable<int> TentX { get; set; }
        public Nullable<int> TentY { get; set; }
        public Nullable<System.DateTime> LastRally { get; set; }
        public Nullable<System.DateTime> LastBank { get; set; }
        public Nullable<System.DateTime> LastUpgrade { get; set; }
        public Nullable<System.DateTime> LastAthenaGift { get; set; }
        public System.DateTime LastShield { get; set; }
        public Nullable<int> ProductionPerHour { get; set; }
        public int LastUpgradeBuilding { get; set; }
        public int StrongHoldLevel { get; set; }
        public int TreasuryLevel { get; set; }
        public bool NeedRSS { get; set; }
        public bool Upgrade { get; set; }
        public bool HeroUpgradeNeeded { get; set; }
        public bool CollectAthenaGift { get; set; }
        public string RedeemCode { get; set; }
        public int RSSBankNum { get; set; }
        public int SilverBankNum { get; set; }
        public int RssMarches { get; set; }
        public int SilverMarches { get; set; }
        public Nullable<System.DateTime> TreasuryDue { get; set; }
        public bool HasGoldMine { get; set; }
    
        public virtual City City { get; set; }
    }
}
