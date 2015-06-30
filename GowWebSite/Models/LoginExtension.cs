using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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

        public AllowDelays DelayTier
        {
            get
            {
                if (this.LoginDelayMin < 90)
                    return AllowDelays.Min60; 
                else if (this.LoginDelayMin < 270)
                    return AllowDelays.Min180; 
                else
                    return AllowDelays.Min360;
            }
            set
            {
                this.LoginDelayMin = (int)value;
            }
            
        }
        public enum AllowDelays
        {
            [Display(Name="1 Hour")]
            Min60 = 60,
            [Display(Name = "3 Hours")]
            Min180 = 180,
            [Display(Name = "6 Hours")]
            Min360 = 360
        }
    }
}