using Sys.Pss.QsApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using System.Web.Mvc;

namespace Sys.Pss.QsApi.Filters
{
    public class LogFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new AppLog());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(actionContext.Request, "Controller : " + actionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + actionContext.ActionDescriptor.ActionName, "JSON", actionContext.ActionArguments);
            //base.OnActionExecuting(actionContext);
        }
    }

}