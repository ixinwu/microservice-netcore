/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_AreaMap Description:
 * 地区映射
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;
using System;

namespace NGP.Framework.DataAccess
{
    /// <summary>
    /// 地区映射
    /// </summary>
    public class SysOrg_AreaMap : BaseDBEntityMap<SysOrg_Area>, INGPEntityMap
    {
        /// <summary>
        /// 雇员实体映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysOrg_Area> builder)
        {
            // 表
            builder.ToTable("Sys_Org_Area");

            builder.Property(t => t.AreaName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.ParentId)
                .HasMaxLength(50);

            builder.Property(t => t.LevelType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.IsLevelArea);
            builder.Property(t => t.IsFunctionPark);
            builder.Property(t => t.IsCountyCity);

            base.PostConfigure(builder);
        }
    }
}
