using Sys.Pss.Common;
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
    public class OrderInfoController : BaseApiController
    {
        string tokenvalue = System.Configuration.ConfigurationManager.AppSettings["token"].ToString();
        /// <summary>     
        /// 获取转单骑手列表
        /// </summary>
        /// <param name="pra"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage getCourierList([FromBody]getCourierListPra pra)
        {            
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.getCourierList(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage ChangeCourier([FromBody]ChangeCourierPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.ChangeCourier(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="pra"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SearchOrderlist([FromBody]SearchOrderlistPra pra)
        {            
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.SearchOrderlist(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 更新订单状态(接单确认、到店、取餐、确认送达)
        /// </summary>
        /// <param name="pra"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage updateOrderState([FromBody]updateOrderStatePra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.updateOrderState(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 获取订单状态列表
        /// </summary>
        /// <param name="pra"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetOrderStateList([FromBody] GetOrderStateListPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.GetOrderStateList(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// 报告异常订单
        /// </summary>
        [HttpPost]
        public HttpResponseMessage AddOrderTrouble()
        { 
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象                  
            AddOrderTroublePra pra=new AddOrderTroublePra();
            pra.token = request["token"];
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                try
                { 
                    HttpFileCollectionBase imgFiles = request.Files;
                    string fileName="" ;
                    HttpPostedFileBase file;
                    Guid id = Guid.NewGuid();
                    if (imgFiles != null && imgFiles.Count>0)
                    {
                        file = imgFiles[0];
                        fileName = imgFiles[0].FileName;  
                        fileName = id.ToString() + System.IO.Path.GetExtension(fileName) ;
                        string FilePath = System.Web.HttpContext.Current.Server.MapPath("..\\Img\\Trouble\\") + fileName; 
                        file.SaveAs(FilePath);                        
                    }
                    pra.id = id;
                    pra.OrderID= request["OrderID"];
                    pra.TroubleName = request["TroubleName"];
                    pra.Remarks = request["Remarks"];
                    pra.Img = fileName == "" ? null : fileName;
                    OrderInfoServer obj = new OrderInfoServer();
                    value = obj.AddOrderTrouble(pra);                           
               
                }
                catch
                {
                    //returnvalue.Bool = false;
                }
             }
              else
              {
                  value.Bool = false;
                  value.ErrorMessage = "token验证失败";
              }
              return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }      
        [HttpPost]
        public HttpResponseMessage GetChangeOrderlist([FromBody]GetChangeOrderlistPra pra)
        {            
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.GetChangeOrderlist(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage ChangeOrderAccept([FromBody]ChangeOrderAcceptPra pra)
        {            
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.ChangeOrderAccept(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        [HttpPost]
        public HttpResponseMessage SearchHistoryOrderlist([FromBody]SearchHistoryOrderlistPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {

                OrderInfoServer obj = new OrderInfoServer();
                value = obj.SearchHistoryOrderlist(pra);
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
