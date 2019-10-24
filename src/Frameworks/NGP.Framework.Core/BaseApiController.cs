/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPApiController Description:
 * api控制器基类
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-14   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGP.Framework.Core.Filters;
using System.Collections.Generic;

namespace NGP.Framework.Core
{
    /// <summary>
    /// api控制器基类
    /// </summary>
    [ApiController]
    [NGPValidator]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// 重写OK方法
        /// </summary>
        /// <returns></returns>
        protected OkObjectResult OkResult()
        {
            return Ok(NGPResponse.Create());
        }

        /// <summary>
        /// ok data result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected OkObjectResult OkDataResult<T>(T data)
        {
            return Ok(NGPDataResponse<T>.Create(data));
        }

        /// <summary>
        /// ok data result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected OkObjectResult OkDataResult(dynamic data)
        {
            return Ok(NGPDataResponse.Create(data));
        }

        /// <summary>
        /// ok page result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="totalCount"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected OkObjectResult OkPageDataResult<T>(int totalCount, List<T> data)
        {
            return Ok(NGPDataPageResponse<T>.Create(totalCount,data));
        }

        /// <summary>
        /// ok page result
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected OkObjectResult OkPageDataResult(int totalCount, List<dynamic> data)
        {
            return Ok(NGPDataPageResponse.Create(totalCount, data));
        }
    }
}
