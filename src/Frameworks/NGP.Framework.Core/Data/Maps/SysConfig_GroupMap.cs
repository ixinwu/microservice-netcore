﻿/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Config_GroupMap Description:
 * 雇员部门关联映射
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
    /// 系统大分类
    /// </summary>
    public class SysConfig_GroupMap : BaseDBEntityMap<SysConfig_Group>, INGPEntityMap
    {
        /// <summary>
        /// 雇员部门关联映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysConfig_Group> builder)
        {
            // 表
            builder.ToTable("Sys_Config_Group");

            builder.Property(t => t.GroupKey)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.GroupValue)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.GroupValue1)
                .HasMaxLength(200);

            builder.Property(t => t.GroupValue2)
                .HasMaxLength(200);

            builder.Property(t => t.GroupValue3)
                .HasMaxLength(200);

            builder.Property(t => t.GroupValue4)
                .HasMaxLength(200);

            base.PostConfigure(builder);
        }
    }
}
