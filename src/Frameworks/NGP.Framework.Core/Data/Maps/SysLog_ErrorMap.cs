/* ---------------------------------------------------------------------    
 * Copyright:
 * Wuxi Efficient Technology Co., Ltd. All rights reserved. 
 * 
 * Class Description:
 * 
 *
 * Comment 					        Revision	        Date                 Author
 * -----------------------------    --------         --------            -----------
 * Created							1.0		    2019/3/4 15:37:43   rock@xcloudbiz.com
 *
 * ------------------------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;

namespace NGP.Framework.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
	public class SysLog_ErrorMap : BaseDBEntityMap<SysLog_Error>, INGPEntityMap
    {
        /// <summary>
        /// 系统异常日志关联映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysLog_Error> builder)
        {

            // Table & Column Mappings
            builder.ToTable("Sys_Log_Error");

            builder.Property(t => t.ApiUrl)
                .HasMaxLength(200)
                .HasColumnName("ApiUrl");
            builder.Property(t => t.ApiPostParameter)
                .HasColumnName("ApiPostParameter");
            builder.Property(t => t.BusinessMethod)
                .HasMaxLength(100)
                .HasColumnName("BusinessMethod");
            builder.Property(t => t.ExceptionContent)
                .HasColumnName("ExceptionContent");
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

            base.PostConfigure(builder);
        }
    }
}
