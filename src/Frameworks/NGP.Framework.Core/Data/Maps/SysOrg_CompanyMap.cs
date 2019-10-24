/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_DepartmentMap Description:
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
    public class SysOrg_CompanyMap : BaseDBEntityMap<SysOrg_Company>, INGPEntityMap
    {
        /// <summary>
        /// 部门实体映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysOrg_Company> builder)
        {
            // 表
            builder.ToTable("Sys_Org_Company");

            builder.Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.AreaId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.USCC)
               .HasMaxLength(100);

            builder.Property(t => t.Corporation)
                .HasMaxLength(100)
                .HasColumnName("Corporation");
            
            builder.Property(t => t.CompanyAddress)
                .HasMaxLength(-1)
                .HasColumnName("CompanyAddress");
            
            builder.Property(t => t.Capital)
                .HasColumnType("decimal(18,6)")
                .HasColumnName("Capital");
            
            builder.Property(t => t.CompanyType)
                .HasMaxLength(100)
                .HasColumnName("CompanyType");
            
            builder.Property(t => t.CompanyStatus)
                .HasMaxLength(100)
                .HasColumnName("CompanyStatus");
            
            builder.Property(t => t.BusinessTerm)
                .HasMaxLength(100)
                .HasColumnName("BusinessTerm");
            
            builder.Property(t => t.FoundDate)
                .HasColumnName("FoundDate");
            
            builder.Property(t => t.AcceptMatter)
                .HasMaxLength(100)
                .HasColumnName("AcceptMatter");
            
            builder.Property(t => t.RegisteDepart)
                .HasMaxLength(100)
                .HasColumnName("RegisteDepart");
            
            builder.Property(t => t.BusinessScope)
                .HasColumnName("BusinessScope");
            
            builder.Property(t => t.CompanyFile)
                .HasColumnName("CompanyFile");

            builder.Property(t => t.IsIndefinite)
                .HasColumnName("IsIndefinite");

            base.PostConfigure(builder);
        }
    }
}
