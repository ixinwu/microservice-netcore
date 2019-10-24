using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.DataAccess
{
    /// <summary>
    /// 系统接口API调用日志Map
    /// </summary>
    public class SysLog_ApiInterfaceMap : BaseDBEntityMap<SysLog_ApiInterface>, INGPEntityMap
    {
        /// <summary>
        /// 系统接口API调用日志关联映射        
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysLog_ApiInterface> builder)
        {
            // Table & Column Mappings
            builder.ToTable("Sys_Log_ApiInterface");

            builder.Property(t => t.ApiUrl)
                .HasMaxLength(200)
                .HasColumnName("ApiUrl");
            builder.Property(t => t.ApiPostParameter)
                .HasColumnName("ApiPostParameter");
            builder.Property(t => t.BusinessMethod)
                .HasMaxLength(100)
                .HasColumnName("BusinessMethod");
            builder.Property(t => t.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpId");
            builder.Property(t => t.LoginName)
                .HasMaxLength(50)
                .HasColumnName("LoginName");
            builder.Property(t => t.RoleId)
                .HasMaxLength(50)
                .HasColumnName("RoleId");
            builder.Property(t => t.CompanyId)
                .HasMaxLength(50)
                .HasColumnName("CompanyId");
            builder.Property(t => t.AreaId)
                .HasMaxLength(50)
                .HasColumnName("AreaId");
            builder.Property(t => t.LoginIP)
               .HasColumnName("LoginIP");
            builder.Property(t => t.CallStratTime)
               .HasColumnName("CallStratTime");
            builder.Property(t => t.CallEndTime)
              .HasColumnName("CallEndTime");
            builder.Property(t => t.CallTimeS)
              .HasColumnName("CallTimeS");
            builder.Property(t => t.ApiResponseBody)
                .HasColumnName("ApiResponseBody");


            base.PostConfigure(builder);
        }
    }
}
