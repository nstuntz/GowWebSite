using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GowWebSite.Models
{
    public partial class Login
    {
        public double RunSince
        {
            get
            {
                return DateTime.Now.Subtract(this.LastRun).TotalMinutes;
            }
        }
    }
}