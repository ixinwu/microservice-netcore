/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPValidatorAttribute Description:
 * ngp验证特性
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace NGP.Framework.Core.Filters
{
    /// <summary>
    /// ngp验证特性
    /// </summary>
    public class NGPValidatorAttribute : TypeFilterAttribute
    {
        #region Ctor

        /// <summary>
        /// Create instance of the filter attribute
        /// </summary>
        public NGPValidatorAttribute() : base(typeof(NGPValidatorFilter))
        {
        }

        #endregion

        #region Nested filter

        /// <summary>
        /// Represents a filter that validates
        /// </summary>
        private class NGPValidatorFilter : ResultFilterAttribute
        {
            #region Ctor
            /// <summary>
            /// ctor
            /// </summary>
            public NGPValidatorFilter()
            {
            }

            #endregion

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public override void OnResultExecuting(ResultExecutingContext context)
            {
                base.OnResultExecuting(context);

                //model valid not pass  
                if (!context.ModelState.IsValid)
                {
                    var entry = context.ModelState.Values.FirstOrDefault();
                    
                    var message = entry.Errors.FirstOrDefault().ErrorMessage;                    

                    //modify the result  
                    context.Result = new OkObjectResult(message);
                }
            }

            #endregion
        }

        #endregion
    }
}
