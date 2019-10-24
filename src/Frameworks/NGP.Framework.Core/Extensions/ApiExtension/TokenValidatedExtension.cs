/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ApplicationBuilderExtensions Description:
 * TokenValidate扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NGP.Framework.Core
{
    /// <summary>
    /// TokenValidate扩展
    /// </summary>
    public static class TokenValidatedExtension
    {
        /// <summary>
        /// 解析token内容，设定工作上下文
        /// </summary>
        /// <param name="principal"></param>
        public static void SetWorkEmployee(this ClaimsPrincipal principal)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            // 设定上下文用户对象
            workContext.Current = new WorkEmployee();
            foreach (var item in principal.Claims)
            {
                switch (item.Type)
                {
                    case "ngp_dept_id":
                        workContext.Current.DeptId = item.Value;
                        break;
                    case "id":
                        workContext.Current.EmplId = item.Value;
                        break;
                    case "name":
                        workContext.Current.EmployeeName = item.Value;
                        break;
                    case "ngp_area_id":
                        workContext.Current.AreaId = item.Value;
                        break;
                    case "ngp_login_name":
                        workContext.Current.LoginName = item.Value;
                        break;
                    case "is_system_admin":
                        workContext.Current.IsSystemAdmin = CommonHelper.To<bool?>(item.Value);
                        break;
                    case "ngp_employee_types":
                        var EmployeeTypes = new List<string>(item.Value.Split(','));
                        workContext.Current.EmployeeTypes = EmployeeTypes;
                        break;
                    case "ngp_identity_type":
                        workContext.Current.IdentityType = item.Value;
                        break;
                    case "company_id":
                        workContext.Current.CompanyId = item.Value;
                        break;
                    case "role_id":
                        workContext.Current.RoleId = item.Value;
                        break;
                    case "role_type":
                        workContext.Current.RoleType = item.Value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}