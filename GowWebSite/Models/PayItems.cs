﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GowWebSite.Models
{
    public class AllPayItems
    {
        public List<UserPayItem> UserItems = new List<UserPayItem>();
        public List<CityPayItem> CityItems = new List<CityPayItem>();
    }

    public enum PayItemEnum
    {
        Hour6 = 1,
        Hour3,
        Hour1,
        Upgrade,
        Treasury,
        BetaCities,
        InsaneGenieCities,
        AdminUserCities,
        BasicCity,
        PremiumCity,
        Basic5 = 1011,
        Basic10 = 1012,
        Basic25 = 1014,
        Basic50 = 1015,
        Premium5 = 1016,
        Premium10 = 1017,
        Premium25 = 1018,
        Premium50 = 1019
    }

    public enum PayItemTypeEnum
    {
        LoginDurations = 1,
        CityItems,
        BasicCityPackage,
        PremiumCityPackage,
        BasicCity,
        PremiumCity,
    }
}


//1	1	0.9000	6 Hour
//2	1	1.5000	3 Hour
//3	1	3.0000	1 Hour
//4	2	0.3000	Bank
//5	2	0.5000	Upgrade
//6	2	0.3500	Rally
//7	2	0.2000	Shield
//8	2	0.2000	Treasury