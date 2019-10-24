/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * BasePageQueryInfo Description:
 * 分页查询模型
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core.Models;
using System.Runtime.Serialization;
using System.Linq;
using Newtonsoft.Json;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 分页查询模型
    /// </summary>
    public class NGPPageQueryRequest<T> where T : INGPModel
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        private int _pageSize = 20;
        public int? PageSize
        {
            get { return this._pageSize; }
            set
            {
                if (value != null)
                {
                    this._pageSize = value.Value;
                }
            }
        }

        private int _pageNumber = 1;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int? PageNumber
        {
            get { return this._pageNumber; }
            set
            {
                if (value != null)
                {
                    this._pageNumber = value.Value;
                }
            }
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        public T RequestData { get; set; }


        /// <summary>
        /// 查询条件(文本框输入的模糊查询条件)
        /// </summary>
        public string LikeValue { get; set; }

        /// <summary>
        /// 排序列名
        /// </summary>
        public string SortField { get; set; }
        /// <summary>
        /// 排序asc顺序，desc逆序
        /// </summary>
        public string SortDirection { get; set; }

        public NGPResponse Validat()
        {
            var valudation = new NGPPageQueryRequestValidator<T>();
            var result = valudation.Validate(this);
            if (!result.IsValid)
            {
                var e = result.Errors.FirstOrDefault().ErrorMessage;

                return JsonConvert.DeserializeObject<NGPResponse>(e);
            }

            return null;
        }
    }
}
