using Sys.Pss.Common;
using Sys.Pss.Dal.Models;
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
    public class QsInfoController :BaseApiController
    {
         string tokenvalue = System.Configuration.ConfigurationManager.AppSettings["token"].ToString();
         [HttpPost]
         public HttpResponseMessage CourierApply([FromBody] CourierApplyPra pra)
         {
             Result value = new Result();
             if (pra.token != null && pra.token == tokenvalue)
             {
                 QsInfoServer obj = new QsInfoServer();
                 value=obj.CourierApply(pra);
             }
             else
             {
                 value.Bool = false;
                 value.ErrorMessage = "token验证失败";
             }
             return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
         }
        [HttpPost]
         public HttpResponseMessage PassWordLogin([FromBody] PassWordLoginPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                pra.PassWord = DESEncrypt.Encrypt(pra.PassWord);
                value = obj.PassWordLogin(pra);                
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage GetQsVerificationMsg([FromBody] GetQsVerificationMsgPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.GetQsVerificationMsg(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage VerificationLogin([FromBody] VerificationLoginPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.VerificationLogin(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage GetServiceSlogans([FromBody]GetServiceSlogansPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.GetServiceSlogans(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage UpdateServiceSlogans([FromBody]UpdateServiceSlogansPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.UpdateServiceSlogans(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage GetVerificationPhone([FromBody]GetVerificationPhonePra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.GetVerificationPhone(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage UpdatePhone([FromBody]UpdatePhonePra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.UpdatePhone(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage UpdatePassword([FromBody]UpdatePasswordPra pra)
        {
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                pra.OldPassword = DESEncrypt.Encrypt(pra.OldPassword);
                pra.Password= DESEncrypt.Encrypt(pra.Password);
                value = obj.UpdatePassword(pra);
            }
            else
            {
                value.Bool = false;
                value.ErrorMessage = "token验证失败";
            }
            return jsonMessage(Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }
        [HttpPost]
        public HttpResponseMessage UpdateHeadPhoto()
        {
      
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象                  
            UpdateHeadPhotoPra pra = new UpdateHeadPhotoPra();
            pra.token = request["token"];
            Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                try
                {
                    pra.CourierId = request["CourierId"];
                    QsInfoServer obj = new QsInfoServer();
                    value= obj.GetCourierModel(pra);
                    if (value.Bool)
                    {
                        F_Courier model = (F_Courier)value.Data;
                        if (model != null)
                        {
                            HttpFileCollectionBase imgFiles = request.Files;
                            string fileName = "";
                            HttpPostedFileBase file;
                            if (imgFiles != null && imgFiles.Count > 0)
                            {
                                file = imgFiles[0];
                                fileName = imgFiles[0].FileName;
                                fileName = model.CourierId.ToString() + System.IO.Path.GetExtension(fileName);
                                string FilePath = System.Web.HttpContext.Current.Server.MapPath("..\\Img\\Head\\") + fileName;
                                file.SaveAs(FilePath);
                                pra.file = fileName;
                                obj.UpdateHeadPhoto(pra);
                            }
                            else
                            {
                                value.Bool = false;
                                value.ErrorMessage = "更新头像失败,图片对象不存在";
                            }
                          
                        }
                        else
                        {
                            value.Bool = false;
                            value.ErrorMessage = "更新头像失败,骑手不存在";
                        }

                    }
                    else
                    {
                        value.Bool = false;
                        value.ErrorMessage = "更新头像失败,骑手不存在";
                    }

                }
                catch
                {
                    value.Bool = false;
                    value.ErrorMessage = "更新头像失败,异常";
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
        public HttpResponseMessage UpdateCourierState([FromBody]UpdateCourierStatePra pra)
        {

             Result value = new Result();
            if (pra.token != null && pra.token == tokenvalue)
            {
                QsInfoServer obj = new QsInfoServer();
                value = obj.UpdateCourierState(pra);
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
