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
    
    public partial class PayItem
    {
        public PayItem()
        {
            this.CityPayItems = new HashSet<CityPayItem>();
        }
    
        public int PayItemID { get; set; }
        public int ItemType { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<CityPayItem> CityPayItems { get; set; }
    }
}
