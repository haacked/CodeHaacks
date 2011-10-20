using System;
using System.Web;
using System.Web.Routing;
using WebApiHaack.DemoWeb.Api;
using WebApiHaack.DemoWeb.AppStart;

namespace WebApiHaack.DemoWeb
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapServiceRoute<CommentsApi>("comments", new CommentsConfiguration());
        }
    }
}