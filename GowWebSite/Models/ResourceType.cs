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
    
    public partial class ResourceType
    {
        public ResourceType()
        {
            this.Cities = new HashSet<City>();
        }
    
        public int ResourceTypeID { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<City> Cities { get; set; }
    }
}
