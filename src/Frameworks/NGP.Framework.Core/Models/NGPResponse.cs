/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPResponse Description:
 * ngp返回对象
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/


using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace NGP.Framework.Core
{
    /// <summary>
    /// ngp返回对象
    /// </summary>
    [DataContract]
    public class NGPResponse
    {
        /// <summary>
        /// 操作状态
        /// </summary>
        [DataMember(Name = "status")]
        public StatusCode Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        private string _message;

        /// <summary>
        /// 消息
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_message))
                {
                    return Status.Message();
                }
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// 创建response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static NGPResponse Create(StatusCode status = StatusCode.Success, params object[] formatParameters)
        {
            return new NGPResponse
            {
                Message = status.Message(formatParameters),
                Status = status
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>();
            dictionary["status"] = Status;
            dictionary["message"] = Message;
            return dictionary;
        }
    }

    /// <summary>
    /// ngp分页查询返回
    /// </summary>
    [DataContract]
    public class NGPDataResponse : NGPResponse
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        [DataMember(Name = "data")]
        public dynamic Data { get; set; }

        /// <summary>
        /// 创建response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="formatParameters"></param>
        /// <returns></returns>
        public static NGPDataResponse Create(dynamic data,
            StatusCode status = StatusCode.Success,
            params object[] formatParameters)
        {
            return new NGPDataResponse
            {
                Message = status.Message(formatParameters),
                Status = status,
                Data = data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// 查询操作结果返回
    /// </summary>
    [DataContract]
    public class NGPDataResponse<T> : NGPDataResponse
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember(Name = "data")]
        public new T Data { get; set; }

        /// <summary>
        /// 创建response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="formatParameters"></param>
        /// <returns></returns>
        public static NGPDataResponse<T> Create(T data, StatusCode status = StatusCode.Success,
            params object[] formatParameters)
        {
            return new NGPDataResponse<T>
            {
                Message = status.Message(formatParameters),
                Status = status,
                Data = data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// ngp分页查询返回
    /// </summary>
    [DataContract]
    public class NGPDataPageResponse : NGPDataResponse
    {
        /// <summary>
        /// total count
        /// </summary>
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        [DataMember(Name = "data")]
        public new List<dynamic> Data { get; set; }

        /// <summary>
        /// 创建response
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="formatParameters"></param>
        /// <returns></returns>
        public static NGPDataPageResponse Create(int totalCount, List<dynamic> data, StatusCode status = StatusCode.Success,
            params object[] formatParameters)
        {
            return new NGPDataPageResponse
            {
                TotalCount = totalCount,
                Message = status.Message(formatParameters),
                Status = status,
                Data = data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// ngp分页查询返回
    /// </summary>
    [DataContract]
    public class NGPDataPageResponse<T> : NGPDataPageResponse
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        [DataMember(Name = "data")]
        public new List<T> Data { get; set; }

        /// <summary>
        /// 创建response
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <param name="formatParameters"></param>
        /// <returns></returns>
        public static NGPDataPageResponse<T> Create(int totalCount, List<T> data, StatusCode status = StatusCode.Success,
            params object[] formatParameters)
        {
            return new NGPDataPageResponse<T>
            {
                TotalCount = totalCount,
                Message = status.Message(formatParameters),
                Status = status,
                Data = data
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
