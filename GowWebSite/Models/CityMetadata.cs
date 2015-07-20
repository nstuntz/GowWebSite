using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GowWebSite.Models
{
    [MetadataType(typeof(CityMetadata))]
    public partial class City {}

    public class CityMetadata
    {
        [Required]
        public string CityName { get; set; }
        [Range(1, 2000)]
        public int Kingdom { get; set; }
        [Range(1, 510)]
        public int LocationX { get; set; }
        [Range(1, 1022)]
        public int LocationY { get; set; }
        [MinLength(3)]
        [MaxLength(4)]
        public string Alliance { get; set; }
    }
}