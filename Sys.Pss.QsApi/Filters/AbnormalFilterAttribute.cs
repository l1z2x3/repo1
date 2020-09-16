using Sys.Pss.QsApi.Common;
using Sys.Pss.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

namespace Sys.Pss.QsApi.Filters
{
    public class AbnormalFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new AppLog());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Error(actionExecutedContext.Request, "Controller : " + actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + actionExecutedContext.ActionContext.ActionDescriptor.ActionName, actionExecutedContext.Exception);
            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(actionExecutedContext.Exception.Message), ReasonPhrase = "ValidationException" };
                throw new HttpResponseException(resp);
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(Result.getResult(false, actionExecutedContext.Exception));
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {

                    Content = new StringContent(result),
                    ReasonPhrase = "ValidationException"
                };
                throw new HttpResponseException(resp);
                // throw new HttpResponseException(actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
            //base.OnException(actionExecutedContext);
        }
    }

}