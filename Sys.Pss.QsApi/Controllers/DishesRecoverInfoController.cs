using Sys.Pss.QsApi.Code;
using Sys.Pss.Server.webApi.Qs;
using Sys.Pss.View;
using Sys.Pss.View.qs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Sys.Pss.QsApi.Controllers
{
    public class DishesRecoverInfoController : BaseApiController
    {
        string tokenvalue = System.Configuration.ConfigurationManager.AppSettings["token"].ToString();
        [HttpPost]
        public HttpResponseMessage SearchNoRecoverOrderlist([FromBody] SearchNoRecoverOrderlistPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                DishesRecoverInfoServer obj = new DishesRecoverInfoServer();
                value = obj.SearchNoRecoverOrderlist(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
    }
}
