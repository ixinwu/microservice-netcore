/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_EmployeeMap Description:
 * 雇员实体映射
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
    /// 雇员实体
    /// </summary>
    public class SysOrg_EmployeeMap : BaseDBEntityMap<SysOrg_Employee>, INGPEntityMap
    {
        /// <summary>
        /// 雇员实体映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysOrg_Employee> builder)
        {
            // 表
            builder.ToTable("Sys_Org_Employee");

            builder.Property(t => t.DeptId)
                .HasMaxLength(50);

            builder.Property(t => t.CompanyIds);

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.LoginName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.EmployeeName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.PhoneNumber)
                .HasMaxLength(50);

            builder.Property(t => t.EmployeeTypes)
                .HasMaxLength(100);

            builder.Property(t => t.LevelType)
                .HasMaxLength(100);

            builder.Property(t => t.IdentityType)
                .HasMaxLength(100);

            builder.Property(t => t.Position)
                .HasMaxLength(100);

            builder.Property(t => t.LoginTime);
            builder.Property(t => t.FailureTimes);
            builder.Property(t => t.SuccessTimes);
            builder.Property(t => t.IsLocking);
            builder.Property(t => t.IsWhiteList);
            builder.Property(t => t.IsShow);
            builder.Property(t => t.AccountStatus);
            builder.Property(t => t.EmplAccountType)
                .HasMaxLength(50);


            base.PostConfigure(builder);
        }
    }
}
