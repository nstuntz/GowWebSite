using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GowWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new MyPropertyActionFilter(), 0);
        }
    }


    public class MyPropertyActionFilter : ActionFilterAttribute
    {
        private GowWebSite.Models.GowEntities db = new GowWebSite.Models.GowEntities();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //IQueryable<GowWebSite.Models.UserDemographic> demographics;
                var demographics = db.UserDemographics
                    .OrderBy(y=>y.UserName)
                    .ToList()
                    .Select(x => new
                                {
                                    AspNetUserEmail = x.AspNetUserEmail,
                                    Description = string.Format("{0} ({1})", x.UserName, x.AspNetUserEmail)                                    
                                }
                            );

                demographics = demographics.Where(x => db.UserCities.Select(y => y.Email).Contains(x.AspNetUserEmail));

                if (!filterContext.HttpContext.User.IsInRole("Admin"))
                {
                    filterContext.RequestContext.HttpContext.Response.Cookies["SelectedUser"].Value = filterContext.HttpContext.User.Identity.Name;
                    filterContext.RequestContext.HttpContext.Response.Cookies["SelectedUser"].Expires = DateTime.Now.AddYears(1);
                    return;
                }
                //Only admins here

                SelectList userDD;
                if (filterContext.RequestContext.HttpContext.Request.Cookies["SelectedUser"] != null && filterContext.RequestContext.HttpContext.Request.Cookies["SelectedUser"].Value != null)
                {
                    string selectedUser = filterContext.RequestContext.HttpContext.Request.Cookies["SelectedUser"].Value;                    
                   // userDD = new SelectList(db.AspNetUsers, "Email", "Email", selectedUser);
                    userDD = new SelectList(demographics, "AspNetUserEmail", "Description", selectedUser);
                }
                else
                {
                    //userDD = new SelectList(users, "Email", "Email");
                    userDD = new SelectList(demographics, "AspNetUserEmail", "Description");
                }

                filterContext.Controller.ViewBag.UserIDs = userDD;
            }
        }
    }
}
