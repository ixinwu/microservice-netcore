using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class MobileMessageSend : IMobileMessageSend
    {

        #region private fields
        /// <summary>
        /// IWebHelper
        /// </summary>
        protected readonly IWebHelper _webHelper;

        /// <summary>
        /// IConfiguration
        /// </summary>
        protected readonly IConfiguration _configuration;

        #endregion

        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_webHelper"></param>
        /// <param name="_configuration"></param>
        public MobileMessageSend(IWebHelper webHelper, IConfiguration configuration)
        {
            _webHelper = webHelper;
            _configuration = configuration;
        }
        #endregion
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="context"></param>
        public void SendMessage(MobileSendContext context)
        {
            var host = _configuration.GetValue<string>("SMSHostServer");
            host = $"{host.TrimEnd('/')}/";
            //var addressInfo = _webHelper.GetStoreHost(false).Replace("5556", "5600");
            var client = new HttpClient();
            string url = string.Format(@"{0}api/extend/Values/SendMessage", host);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage messageRequest = new HttpRequestMessage(HttpMethod.Post, url);
            messageRequest.Content = new StringContent(JsonConvert.SerializeObject(context), Encoding.UTF8, "application/json");
            var postResult = client.PostAsync(url, messageRequest.Content).Result;
            var result = postResult.Content.ReadAsStringAsync().Result;

        }
    }
}
