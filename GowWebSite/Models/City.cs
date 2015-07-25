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
    
    public partial class City
    {
        public City()
        {
            this.UserCities = new HashSet<UserCity>();
            this.CityPayItems = new HashSet<CityPayItem>();
        }
    
        public int CityID { get; set; }
        public int LoginID { get; set; }
        public string CityName { get; set; }
        public Nullable<int> Kingdom { get; set; }
        public Nullable<int> LocationX { get; set; }
        public Nullable<int> LocationY { get; set; }
        public bool Created { get; set; }
        public bool Placed { get; set; }
        public int ResourceTypeID { get; set; }
        public string Alliance { get; set; }
        public Nullable<int> AllianceID { get; set; }
    
        public virtual Login Login { get; set; }
        public virtual ResourceType ResourceType { get; set; }
        public virtual CityInfo CityInfo { get; set; }
        public virtual ICollection<UserCity> UserCities { get; set; }
        public virtual ICollection<CityPayItem> CityPayItems { get; set; }
    }
}
