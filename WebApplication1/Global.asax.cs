﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
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
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)

        {

            HttpContext context = HttpContext.Current;

            if (context != null && context.Request.Cookies != null)

            {

                HttpCookie cookie = HttpContext.Current.Request.Cookies["_lang"];

                if (cookie != null && cookie.Value != null)

                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);

                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);

                }

                else

                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("he-IL");

                }

            }

        }

    }
}
