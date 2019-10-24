/* ---------------------------------------------------------------------    
 * Copyright:
 * Wuxi Efficient Technology Co., Ltd. All rights reserved. 
 * 
 * Class Description:
 * 
 *
 * Comment 					        Revision	        Date                 Author
 * -----------------------------    --------         --------            -----------
 * Created							1.0		    2019/3/4 15:37:37   rock@xcloudbiz.com
 *
 * ------------------------------------------------------------------------------*/


namespace NGP.Framework.Core
{
    /// <summary>
    /// 系统异常日志
    /// </summary>
    public class SysLog_Error: BaseDBEntity
    {
        /// <summary>
        /// api路径
        /// </summary>
        public string ApiUrl { get;set; }
        /// <summary>
        /// api提交参数
        /// </summary>
        public string ApiPostParameter { get;set; }
        /// <summary>
        /// 业务方法
        /// </summary>
        public string BusinessMethod { get;set; }
        /// <summary>
        /// 异常内容
        /// </summary>
        public string ExceptionContent { get;set; }
        /// <summary>
        /// 人员Id
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 平台Id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 登录Ip
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public string AreaId { get; set; }
    }
}