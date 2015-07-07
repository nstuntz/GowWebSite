using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GowWebSite.Models
{
    [MetadataType(typeof(CityPayItemsMetadata))]
    public partial class CityPayItems { }

    public class CityPayItemsMetadata
    {
        [DefaultValue(false)]
        public bool Paid { get; set; }
    }
}