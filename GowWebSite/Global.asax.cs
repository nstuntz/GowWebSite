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
                IQueryable<GowWebSite.Models.Alliance> userAlliances;
                if (filterContext.HttpContext.User.IsInRole("Admin"))
                {
                    userAlliances = db.Alliances;
                }
                else
                {
                    var userAl = db.UserAlliances.Where(x => x.Email == filterContext.HttpContext.User.Identity.Name).Select(x => x.AllianceID);
                    userAlliances = db.Alliances.Where(x => userAl.Contains(x.AllianceID));
                }

                SelectList alliances;
                if (filterContext.RequestContext.HttpContext.Request.Cookies["SelectedAlliance"] != null && filterContext.RequestContext.HttpContext.Request.Cookies["SelectedAlliance"].Value != null)
                {
                    int allianceID = Int32.Parse(filterContext.RequestContext.HttpContext.Request.Cookies["SelectedAlliance"].Value);
                    alliances = new SelectList(userAlliances, "AllianceID", "Name", allianceID);
                }
                else
                {
                    //If the user isn't admin then set the cookie to the first value in the list
                    if (userAlliances.Count() > 0)
                    {
                        filterContext.RequestContext.HttpContext.Response.Cookies["SelectedAlliance"].Value = userAlliances.FirstOrDefault().AllianceID.ToString();
                        filterContext.RequestContext.HttpContext.Response.Cookies["SelectedAlliance"].Expires = DateTime.Now.AddYears(1);
                    }

                    alliances = new SelectList(userAlliances, "AllianceID", "Name");
                }

                filterContext.Controller.ViewBag.AllianceID = alliances;
            }
        }
    }
}
