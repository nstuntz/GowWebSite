﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GowEntities : DbContext
    {
        public GowEntities()
            : base("name=GowEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alliance> Alliances { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityInfo> CityInfoes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }
        public virtual DbSet<UserAlliance> UserAlliances { get; set; }
    
        public virtual int CreateExitingCitySetup(string userName, string password, string cityName, Nullable<int> cityX, Nullable<int> cityY, Nullable<int> allianceID, Nullable<int> rSSType, Nullable<int> sHLevel)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var cityNameParameter = cityName != null ?
                new ObjectParameter("CityName", cityName) :
                new ObjectParameter("CityName", typeof(string));
    
            var cityXParameter = cityX.HasValue ?
                new ObjectParameter("CityX", cityX) :
                new ObjectParameter("CityX", typeof(int));
    
            var cityYParameter = cityY.HasValue ?
                new ObjectParameter("CityY", cityY) :
                new ObjectParameter("CityY", typeof(int));
    
            var allianceIDParameter = allianceID.HasValue ?
                new ObjectParameter("AllianceID", allianceID) :
                new ObjectParameter("AllianceID", typeof(int));
    
            var rSSTypeParameter = rSSType.HasValue ?
                new ObjectParameter("RSSType", rSSType) :
                new ObjectParameter("RSSType", typeof(int));
    
            var sHLevelParameter = sHLevel.HasValue ?
                new ObjectParameter("SHLevel", sHLevel) :
                new ObjectParameter("SHLevel", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateExitingCitySetup", userNameParameter, passwordParameter, cityNameParameter, cityXParameter, cityYParameter, allianceIDParameter, rSSTypeParameter, sHLevelParameter);
        }
    
        public virtual ObjectResult<GetOldestLogin_Result> GetOldestLogin(Nullable<int> machineID)
        {
            var machineIDParameter = machineID.HasValue ?
                new ObjectParameter("MachineID", machineID) :
                new ObjectParameter("MachineID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOldestLogin_Result>("GetOldestLogin", machineIDParameter);
        }
    
        public virtual int CreateExistingCitySetup(string userName, string password, string cityName, Nullable<int> cityX, Nullable<int> cityY, Nullable<int> allianceID, Nullable<int> rSSType, Nullable<int> sHLevel)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var cityNameParameter = cityName != null ?
                new ObjectParameter("CityName", cityName) :
                new ObjectParameter("CityName", typeof(string));
    
            var cityXParameter = cityX.HasValue ?
                new ObjectParameter("CityX", cityX) :
                new ObjectParameter("CityX", typeof(int));
    
            var cityYParameter = cityY.HasValue ?
                new ObjectParameter("CityY", cityY) :
                new ObjectParameter("CityY", typeof(int));
    
            var allianceIDParameter = allianceID.HasValue ?
                new ObjectParameter("AllianceID", allianceID) :
                new ObjectParameter("AllianceID", typeof(int));
    
            var rSSTypeParameter = rSSType.HasValue ?
                new ObjectParameter("RSSType", rSSType) :
                new ObjectParameter("RSSType", typeof(int));
    
            var sHLevelParameter = sHLevel.HasValue ?
                new ObjectParameter("SHLevel", sHLevel) :
                new ObjectParameter("SHLevel", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateExistingCitySetup", userNameParameter, passwordParameter, cityNameParameter, cityXParameter, cityYParameter, allianceIDParameter, rSSTypeParameter, sHLevelParameter);
        }
    
        public virtual int CreateExistingCitySetupFull(string userName, string password, string cityName, string pIN, Nullable<int> cityX, Nullable<int> cityY, Nullable<int> allianceID, Nullable<int> rSSType, Nullable<int> sHLevel, Nullable<int> rSSBank, Nullable<int> silverBank, Nullable<int> rssMarches, Nullable<int> silverMarches, Nullable<bool> upgrade, Nullable<int> loginDelayMin, Nullable<bool> shield, Nullable<System.DateTime> lastShieldDate, Nullable<bool> bank, Nullable<bool> rally, Nullable<int> rallyX, Nullable<int> rallyY, Nullable<bool> hasGoldMine)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var cityNameParameter = cityName != null ?
                new ObjectParameter("CityName", cityName) :
                new ObjectParameter("CityName", typeof(string));
    
            var pINParameter = pIN != null ?
                new ObjectParameter("PIN", pIN) :
                new ObjectParameter("PIN", typeof(string));
    
            var cityXParameter = cityX.HasValue ?
                new ObjectParameter("CityX", cityX) :
                new ObjectParameter("CityX", typeof(int));
    
            var cityYParameter = cityY.HasValue ?
                new ObjectParameter("CityY", cityY) :
                new ObjectParameter("CityY", typeof(int));
    
            var allianceIDParameter = allianceID.HasValue ?
                new ObjectParameter("AllianceID", allianceID) :
                new ObjectParameter("AllianceID", typeof(int));
    
            var rSSTypeParameter = rSSType.HasValue ?
                new ObjectParameter("RSSType", rSSType) :
                new ObjectParameter("RSSType", typeof(int));
    
            var sHLevelParameter = sHLevel.HasValue ?
                new ObjectParameter("SHLevel", sHLevel) :
                new ObjectParameter("SHLevel", typeof(int));
    
            var rSSBankParameter = rSSBank.HasValue ?
                new ObjectParameter("RSSBank", rSSBank) :
                new ObjectParameter("RSSBank", typeof(int));
    
            var silverBankParameter = silverBank.HasValue ?
                new ObjectParameter("SilverBank", silverBank) :
                new ObjectParameter("SilverBank", typeof(int));
    
            var rssMarchesParameter = rssMarches.HasValue ?
                new ObjectParameter("RssMarches", rssMarches) :
                new ObjectParameter("RssMarches", typeof(int));
    
            var silverMarchesParameter = silverMarches.HasValue ?
                new ObjectParameter("SilverMarches", silverMarches) :
                new ObjectParameter("SilverMarches", typeof(int));
    
            var upgradeParameter = upgrade.HasValue ?
                new ObjectParameter("Upgrade", upgrade) :
                new ObjectParameter("Upgrade", typeof(bool));
    
            var loginDelayMinParameter = loginDelayMin.HasValue ?
                new ObjectParameter("LoginDelayMin", loginDelayMin) :
                new ObjectParameter("LoginDelayMin", typeof(int));
    
            var shieldParameter = shield.HasValue ?
                new ObjectParameter("Shield", shield) :
                new ObjectParameter("Shield", typeof(bool));
    
            var lastShieldDateParameter = lastShieldDate.HasValue ?
                new ObjectParameter("LastShieldDate", lastShieldDate) :
                new ObjectParameter("LastShieldDate", typeof(System.DateTime));
    
            var bankParameter = bank.HasValue ?
                new ObjectParameter("Bank", bank) :
                new ObjectParameter("Bank", typeof(bool));
    
            var rallyParameter = rally.HasValue ?
                new ObjectParameter("Rally", rally) :
                new ObjectParameter("Rally", typeof(bool));
    
            var rallyXParameter = rallyX.HasValue ?
                new ObjectParameter("RallyX", rallyX) :
                new ObjectParameter("RallyX", typeof(int));
    
            var rallyYParameter = rallyY.HasValue ?
                new ObjectParameter("RallyY", rallyY) :
                new ObjectParameter("RallyY", typeof(int));
    
            var hasGoldMineParameter = hasGoldMine.HasValue ?
                new ObjectParameter("HasGoldMine", hasGoldMine) :
                new ObjectParameter("HasGoldMine", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateExistingCitySetupFull", userNameParameter, passwordParameter, cityNameParameter, pINParameter, cityXParameter, cityYParameter, allianceIDParameter, rSSTypeParameter, sHLevelParameter, rSSBankParameter, silverBankParameter, rssMarchesParameter, silverMarchesParameter, upgradeParameter, loginDelayMinParameter, shieldParameter, lastShieldDateParameter, bankParameter, rallyParameter, rallyXParameter, rallyYParameter, hasGoldMineParameter);
        }
    
        public virtual ObjectResult<GetOldestLogin2_Result> GetOldestLogin2(string computerName)
        {
            var computerNameParameter = computerName != null ?
                new ObjectParameter("ComputerName", computerName) :
                new ObjectParameter("ComputerName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOldestLogin2_Result>("GetOldestLogin2", computerNameParameter);
        }
    }
}