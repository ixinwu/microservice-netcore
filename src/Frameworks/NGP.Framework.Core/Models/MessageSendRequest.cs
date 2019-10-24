namespace NGP.Framework.Core
{
    /// <summary>
    /// 短信发送模型
    /// </summary>
    public class MessageSendRequest : INGPModel
    {
        /// <summary>
        /// 短信接收号码
        /// </summary>
        public string[] Mobiles { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }
    }
}
