using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GowWebSite.Models
{
    [MetadataType(typeof(CityInfoMetadata))]
    public partial class CityInfo { }

    public class CityInfoMetadata
    {
        [Range(1, 21)]
        public int StrongHoldLevel { get; set; }
        [Range(1, 4)]
        public int RSSBankNum { get; set; }
        [Range(1, 4)]
        public int SilverBankNum { get; set; }
        [Range(0, 8)]
        public int RssMarches { get; set; }
        [Range(0, 8)]
        public int SilverMarches { get; set; }
        [Range(0, 510)]
        public Nullable<int> RallyX { get; set; }
        [Range(0, 1022)]
        public Nullable<int> RallyY { get; set; }
    }
}