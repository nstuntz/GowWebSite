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
    
    public partial class UserAlliance
    {
        public int AllianceID { get; set; }
        public string Email { get; set; }
    
        public virtual Alliance Alliance { get; set; }
    }
}
