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
    
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityInfo> CityInfoes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }
        public virtual DbSet<UserCity> UserCities { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CityPayItem> CityPayItems { get; set; }
        public virtual DbSet<PayItem> PayItems { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<UserDemographic> UserDemographics { get; set; }
        public virtual DbSet<UserPayItem> UserPayItems { get; set; }
        public virtual DbSet<MachineLoginTracker> MachineLoginTrackers { get; set; }
        public virtual DbSet<Core> Cores { get; set; }
        public virtual DbSet<Piece> Pieces { get; set; }
    
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
    
        public virtual int CreateExistingCitySetup(string userName, string password, string cityName, Nullable<int> cityX, Nullable<int> cityY, string alliance, Nullable<int> rSSType, Nullable<int> sHLevel, string createUserEmail)
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
    
            var allianceParameter = alliance != null ?
                new ObjectParameter("Alliance", alliance) :
                new ObjectParameter("Alliance", typeof(string));
    
            var rSSTypeParameter = rSSType.HasValue ?
                new ObjectParameter("RSSType", rSSType) :
                new ObjectParameter("RSSType", typeof(int));
    
            var sHLevelParameter = sHLevel.HasValue ?
                new ObjectParameter("SHLevel", sHLevel) :
                new ObjectParameter("SHLevel", typeof(int));
    
            var createUserEmailParameter = createUserEmail != null ?
                new ObjectParameter("CreateUserEmail", createUserEmail) :
                new ObjectParameter("CreateUserEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateExistingCitySetup", userNameParameter, passwordParameter, cityNameParameter, cityXParameter, cityYParameter, allianceParameter, rSSTypeParameter, sHLevelParameter, createUserEmailParameter);
        }
    
        public virtual int CreateExistingCitySetupFull(string userName, string password, string cityName, string pIN, Nullable<int> cityX, Nullable<int> cityY, string alliance, Nullable<int> rSSType, Nullable<int> sHLevel, Nullable<int> rSSBank, Nullable<int> silverBank, Nullable<int> rssMarches, Nullable<int> silverMarches, Nullable<bool> upgrade, Nullable<int> loginDelayMin, Nullable<bool> shield, Nullable<System.DateTime> lastShieldDate, Nullable<bool> bank, Nullable<bool> rally, Nullable<int> rallyX, Nullable<int> rallyY, Nullable<bool> hasGoldMine, string createUserEmail)
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
    
            var allianceParameter = alliance != null ?
                new ObjectParameter("Alliance", alliance) :
                new ObjectParameter("Alliance", typeof(string));
    
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
    
            var createUserEmailParameter = createUserEmail != null ?
                new ObjectParameter("CreateUserEmail", createUserEmail) :
                new ObjectParameter("CreateUserEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateExistingCitySetupFull", userNameParameter, passwordParameter, cityNameParameter, pINParameter, cityXParameter, cityYParameter, allianceParameter, rSSTypeParameter, sHLevelParameter, rSSBankParameter, silverBankParameter, rssMarchesParameter, silverMarchesParameter, upgradeParameter, loginDelayMinParameter, shieldParameter, lastShieldDateParameter, bankParameter, rallyParameter, rallyXParameter, rallyYParameter, hasGoldMineParameter, createUserEmailParameter);
        }
    
        public virtual ObjectResult<GetOldestLogin2_Result> GetOldestLogin2(string computerName)
        {
            var computerNameParameter = computerName != null ?
                new ObjectParameter("ComputerName", computerName) :
                new ObjectParameter("ComputerName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOldestLogin2_Result>("GetOldestLogin2", computerNameParameter);
        }
    
        public virtual int FilterPieces(Nullable<bool> other, Nullable<bool> overallAttack, Nullable<bool> infantryAttack, Nullable<bool> rangedAttack, Nullable<bool> cavalryAttack, Nullable<bool> overallHealth, Nullable<bool> infantryHealth, Nullable<bool> rangedHealth, Nullable<bool> cavalryHealth, Nullable<bool> overallDefence, Nullable<bool> infantryDefence, Nullable<bool> rangedDefence, Nullable<bool> cavalryDefence, Nullable<bool> overallAttackDebuff, Nullable<bool> infantryAttackDebuff, Nullable<bool> rangedAttackDebuff, Nullable<bool> cavalryAttackDebuff, Nullable<bool> overallHealthDebuff, Nullable<bool> infantryHealthDebuff, Nullable<bool> rangedHealthDebuff, Nullable<bool> cavalryHealthDebuff, Nullable<bool> overallDefenceDebuff, Nullable<bool> infantryDefenceDebuff, Nullable<bool> rangedDefenceDebuff, Nullable<bool> cavalryDefenceDebuff)
        {
            var otherParameter = other.HasValue ?
                new ObjectParameter("Other", other) :
                new ObjectParameter("Other", typeof(bool));
    
            var overallAttackParameter = overallAttack.HasValue ?
                new ObjectParameter("OverallAttack", overallAttack) :
                new ObjectParameter("OverallAttack", typeof(bool));
    
            var infantryAttackParameter = infantryAttack.HasValue ?
                new ObjectParameter("InfantryAttack", infantryAttack) :
                new ObjectParameter("InfantryAttack", typeof(bool));
    
            var rangedAttackParameter = rangedAttack.HasValue ?
                new ObjectParameter("RangedAttack", rangedAttack) :
                new ObjectParameter("RangedAttack", typeof(bool));
    
            var cavalryAttackParameter = cavalryAttack.HasValue ?
                new ObjectParameter("CavalryAttack", cavalryAttack) :
                new ObjectParameter("CavalryAttack", typeof(bool));
    
            var overallHealthParameter = overallHealth.HasValue ?
                new ObjectParameter("OverallHealth", overallHealth) :
                new ObjectParameter("OverallHealth", typeof(bool));
    
            var infantryHealthParameter = infantryHealth.HasValue ?
                new ObjectParameter("InfantryHealth", infantryHealth) :
                new ObjectParameter("InfantryHealth", typeof(bool));
    
            var rangedHealthParameter = rangedHealth.HasValue ?
                new ObjectParameter("RangedHealth", rangedHealth) :
                new ObjectParameter("RangedHealth", typeof(bool));
    
            var cavalryHealthParameter = cavalryHealth.HasValue ?
                new ObjectParameter("CavalryHealth", cavalryHealth) :
                new ObjectParameter("CavalryHealth", typeof(bool));
    
            var overallDefenceParameter = overallDefence.HasValue ?
                new ObjectParameter("OverallDefence", overallDefence) :
                new ObjectParameter("OverallDefence", typeof(bool));
    
            var infantryDefenceParameter = infantryDefence.HasValue ?
                new ObjectParameter("InfantryDefence", infantryDefence) :
                new ObjectParameter("InfantryDefence", typeof(bool));
    
            var rangedDefenceParameter = rangedDefence.HasValue ?
                new ObjectParameter("RangedDefence", rangedDefence) :
                new ObjectParameter("RangedDefence", typeof(bool));
    
            var cavalryDefenceParameter = cavalryDefence.HasValue ?
                new ObjectParameter("CavalryDefence", cavalryDefence) :
                new ObjectParameter("CavalryDefence", typeof(bool));
    
            var overallAttackDebuffParameter = overallAttackDebuff.HasValue ?
                new ObjectParameter("OverallAttackDebuff", overallAttackDebuff) :
                new ObjectParameter("OverallAttackDebuff", typeof(bool));
    
            var infantryAttackDebuffParameter = infantryAttackDebuff.HasValue ?
                new ObjectParameter("InfantryAttackDebuff", infantryAttackDebuff) :
                new ObjectParameter("InfantryAttackDebuff", typeof(bool));
    
            var rangedAttackDebuffParameter = rangedAttackDebuff.HasValue ?
                new ObjectParameter("RangedAttackDebuff", rangedAttackDebuff) :
                new ObjectParameter("RangedAttackDebuff", typeof(bool));
    
            var cavalryAttackDebuffParameter = cavalryAttackDebuff.HasValue ?
                new ObjectParameter("CavalryAttackDebuff", cavalryAttackDebuff) :
                new ObjectParameter("CavalryAttackDebuff", typeof(bool));
    
            var overallHealthDebuffParameter = overallHealthDebuff.HasValue ?
                new ObjectParameter("OverallHealthDebuff", overallHealthDebuff) :
                new ObjectParameter("OverallHealthDebuff", typeof(bool));
    
            var infantryHealthDebuffParameter = infantryHealthDebuff.HasValue ?
                new ObjectParameter("InfantryHealthDebuff", infantryHealthDebuff) :
                new ObjectParameter("InfantryHealthDebuff", typeof(bool));
    
            var rangedHealthDebuffParameter = rangedHealthDebuff.HasValue ?
                new ObjectParameter("RangedHealthDebuff", rangedHealthDebuff) :
                new ObjectParameter("RangedHealthDebuff", typeof(bool));
    
            var cavalryHealthDebuffParameter = cavalryHealthDebuff.HasValue ?
                new ObjectParameter("CavalryHealthDebuff", cavalryHealthDebuff) :
                new ObjectParameter("CavalryHealthDebuff", typeof(bool));
    
            var overallDefenceDebuffParameter = overallDefenceDebuff.HasValue ?
                new ObjectParameter("OverallDefenceDebuff", overallDefenceDebuff) :
                new ObjectParameter("OverallDefenceDebuff", typeof(bool));
    
            var infantryDefenceDebuffParameter = infantryDefenceDebuff.HasValue ?
                new ObjectParameter("InfantryDefenceDebuff", infantryDefenceDebuff) :
                new ObjectParameter("InfantryDefenceDebuff", typeof(bool));
    
            var rangedDefenceDebuffParameter = rangedDefenceDebuff.HasValue ?
                new ObjectParameter("RangedDefenceDebuff", rangedDefenceDebuff) :
                new ObjectParameter("RangedDefenceDebuff", typeof(bool));
    
            var cavalryDefenceDebuffParameter = cavalryDefenceDebuff.HasValue ?
                new ObjectParameter("CavalryDefenceDebuff", cavalryDefenceDebuff) :
                new ObjectParameter("CavalryDefenceDebuff", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FilterPieces", otherParameter, overallAttackParameter, infantryAttackParameter, rangedAttackParameter, cavalryAttackParameter, overallHealthParameter, infantryHealthParameter, rangedHealthParameter, cavalryHealthParameter, overallDefenceParameter, infantryDefenceParameter, rangedDefenceParameter, cavalryDefenceParameter, overallAttackDebuffParameter, infantryAttackDebuffParameter, rangedAttackDebuffParameter, cavalryAttackDebuffParameter, overallHealthDebuffParameter, infantryHealthDebuffParameter, rangedHealthDebuffParameter, cavalryHealthDebuffParameter, overallDefenceDebuffParameter, infantryDefenceDebuffParameter, rangedDefenceDebuffParameter, cavalryDefenceDebuffParameter);
        }
    
        public virtual int FilterCores(Nullable<bool> other, Nullable<bool> overallAttack, Nullable<bool> infantryAttack, Nullable<bool> rangedAttack, Nullable<bool> cavalryAttack, Nullable<bool> overallHealth, Nullable<bool> infantryHealth, Nullable<bool> rangedHealth, Nullable<bool> cavalryHealth, Nullable<bool> overallDefence, Nullable<bool> infantryDefence, Nullable<bool> rangedDefence, Nullable<bool> cavalryDefence, Nullable<bool> overallAttackDebuff, Nullable<bool> infantryAttackDebuff, Nullable<bool> rangedAttackDebuff, Nullable<bool> cavalryAttackDebuff, Nullable<bool> overallHealthDebuff, Nullable<bool> infantryHealthDebuff, Nullable<bool> rangedHealthDebuff, Nullable<bool> cavalryHealthDebuff, Nullable<bool> overallDefenceDebuff, Nullable<bool> infantryDefenceDebuff, Nullable<bool> rangedDefenceDebuff, Nullable<bool> cavalryDefenceDebuff)
        {
            var otherParameter = other.HasValue ?
                new ObjectParameter("Other", other) :
                new ObjectParameter("Other", typeof(bool));
    
            var overallAttackParameter = overallAttack.HasValue ?
                new ObjectParameter("OverallAttack", overallAttack) :
                new ObjectParameter("OverallAttack", typeof(bool));
    
            var infantryAttackParameter = infantryAttack.HasValue ?
                new ObjectParameter("InfantryAttack", infantryAttack) :
                new ObjectParameter("InfantryAttack", typeof(bool));
    
            var rangedAttackParameter = rangedAttack.HasValue ?
                new ObjectParameter("RangedAttack", rangedAttack) :
                new ObjectParameter("RangedAttack", typeof(bool));
    
            var cavalryAttackParameter = cavalryAttack.HasValue ?
                new ObjectParameter("CavalryAttack", cavalryAttack) :
                new ObjectParameter("CavalryAttack", typeof(bool));
    
            var overallHealthParameter = overallHealth.HasValue ?
                new ObjectParameter("OverallHealth", overallHealth) :
                new ObjectParameter("OverallHealth", typeof(bool));
    
            var infantryHealthParameter = infantryHealth.HasValue ?
                new ObjectParameter("InfantryHealth", infantryHealth) :
                new ObjectParameter("InfantryHealth", typeof(bool));
    
            var rangedHealthParameter = rangedHealth.HasValue ?
                new ObjectParameter("RangedHealth", rangedHealth) :
                new ObjectParameter("RangedHealth", typeof(bool));
    
            var cavalryHealthParameter = cavalryHealth.HasValue ?
                new ObjectParameter("CavalryHealth", cavalryHealth) :
                new ObjectParameter("CavalryHealth", typeof(bool));
    
            var overallDefenceParameter = overallDefence.HasValue ?
                new ObjectParameter("OverallDefence", overallDefence) :
                new ObjectParameter("OverallDefence", typeof(bool));
    
            var infantryDefenceParameter = infantryDefence.HasValue ?
                new ObjectParameter("InfantryDefence", infantryDefence) :
                new ObjectParameter("InfantryDefence", typeof(bool));
    
            var rangedDefenceParameter = rangedDefence.HasValue ?
                new ObjectParameter("RangedDefence", rangedDefence) :
                new ObjectParameter("RangedDefence", typeof(bool));
    
            var cavalryDefenceParameter = cavalryDefence.HasValue ?
                new ObjectParameter("CavalryDefence", cavalryDefence) :
                new ObjectParameter("CavalryDefence", typeof(bool));
    
            var overallAttackDebuffParameter = overallAttackDebuff.HasValue ?
                new ObjectParameter("OverallAttackDebuff", overallAttackDebuff) :
                new ObjectParameter("OverallAttackDebuff", typeof(bool));
    
            var infantryAttackDebuffParameter = infantryAttackDebuff.HasValue ?
                new ObjectParameter("InfantryAttackDebuff", infantryAttackDebuff) :
                new ObjectParameter("InfantryAttackDebuff", typeof(bool));
    
            var rangedAttackDebuffParameter = rangedAttackDebuff.HasValue ?
                new ObjectParameter("RangedAttackDebuff", rangedAttackDebuff) :
                new ObjectParameter("RangedAttackDebuff", typeof(bool));
    
            var cavalryAttackDebuffParameter = cavalryAttackDebuff.HasValue ?
                new ObjectParameter("CavalryAttackDebuff", cavalryAttackDebuff) :
                new ObjectParameter("CavalryAttackDebuff", typeof(bool));
    
            var overallHealthDebuffParameter = overallHealthDebuff.HasValue ?
                new ObjectParameter("OverallHealthDebuff", overallHealthDebuff) :
                new ObjectParameter("OverallHealthDebuff", typeof(bool));
    
            var infantryHealthDebuffParameter = infantryHealthDebuff.HasValue ?
                new ObjectParameter("InfantryHealthDebuff", infantryHealthDebuff) :
                new ObjectParameter("InfantryHealthDebuff", typeof(bool));
    
            var rangedHealthDebuffParameter = rangedHealthDebuff.HasValue ?
                new ObjectParameter("RangedHealthDebuff", rangedHealthDebuff) :
                new ObjectParameter("RangedHealthDebuff", typeof(bool));
    
            var cavalryHealthDebuffParameter = cavalryHealthDebuff.HasValue ?
                new ObjectParameter("CavalryHealthDebuff", cavalryHealthDebuff) :
                new ObjectParameter("CavalryHealthDebuff", typeof(bool));
    
            var overallDefenceDebuffParameter = overallDefenceDebuff.HasValue ?
                new ObjectParameter("OverallDefenceDebuff", overallDefenceDebuff) :
                new ObjectParameter("OverallDefenceDebuff", typeof(bool));
    
            var infantryDefenceDebuffParameter = infantryDefenceDebuff.HasValue ?
                new ObjectParameter("InfantryDefenceDebuff", infantryDefenceDebuff) :
                new ObjectParameter("InfantryDefenceDebuff", typeof(bool));
    
            var rangedDefenceDebuffParameter = rangedDefenceDebuff.HasValue ?
                new ObjectParameter("RangedDefenceDebuff", rangedDefenceDebuff) :
                new ObjectParameter("RangedDefenceDebuff", typeof(bool));
    
            var cavalryDefenceDebuffParameter = cavalryDefenceDebuff.HasValue ?
                new ObjectParameter("CavalryDefenceDebuff", cavalryDefenceDebuff) :
                new ObjectParameter("CavalryDefenceDebuff", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FilterCores", otherParameter, overallAttackParameter, infantryAttackParameter, rangedAttackParameter, cavalryAttackParameter, overallHealthParameter, infantryHealthParameter, rangedHealthParameter, cavalryHealthParameter, overallDefenceParameter, infantryDefenceParameter, rangedDefenceParameter, cavalryDefenceParameter, overallAttackDebuffParameter, infantryAttackDebuffParameter, rangedAttackDebuffParameter, cavalryAttackDebuffParameter, overallHealthDebuffParameter, infantryHealthDebuffParameter, rangedHealthDebuffParameter, cavalryHealthDebuffParameter, overallDefenceDebuffParameter, infantryDefenceDebuffParameter, rangedDefenceDebuffParameter, cavalryDefenceDebuffParameter);
        }
    }
}
