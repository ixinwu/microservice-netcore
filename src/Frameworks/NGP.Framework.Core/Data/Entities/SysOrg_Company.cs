/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_Company Description:
 * 平台实体
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 部门实体
    /// </summary>
    public class SysOrg_Company : BaseDBEntity
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 地区id
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        public string USCC { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public string Corporation { get; set; }
        /// <summary>
        /// 平台地址
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        public Decimal? Capital { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 平台状态
        /// </summary>
        public string CompanyStatus { get; set; }
        /// <summary>
        /// 营业期限
        /// </summary>
        public string BusinessTerm { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        public DateTime? FoundDate { get; set; }
        /// <summary>
        /// 受理机关
        /// </summary>
        public string AcceptMatter { get; set; }
        /// <summary>
        /// 登记机关
        /// </summary>
        public string RegisteDepart { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string BusinessScope { get; set; }
        /// <summary>
        /// 工商登记证书
        /// </summary>
        public string CompanyFile { get; set; }
        /// <summary>
        /// 是否无限期
        /// </summary>
        public bool? IsIndefinite { get; set; }
    }
}
