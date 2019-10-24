/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_RoleMap Description:
 * 部门实体映射
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;

namespace NGP.Framework.DataAccess
{
    /// <summary>
    /// 部门实体映射
    /// </summary>
    public class SysOrg_RoleMap : BaseDBEntityMap<SysOrg_Role>, INGPEntityMap
    {
        /// <summary>
        /// 部门实体映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysOrg_Role> builder)
        {
            // 表
            builder.ToTable("Sys_Org_Role");

            builder.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.RoleType)
                .IsRequired()
                .HasMaxLength(100);

            base.PostConfigure(builder);
        }
    }
}
