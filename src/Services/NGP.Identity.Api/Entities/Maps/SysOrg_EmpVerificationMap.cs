using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Identity.Api.Entities.Map
{
    /// <summary>
    /// 用户验证码信息表
    /// </summary>
    public class SysOrg_EmpVerificationMap : BaseDBEntityMap<SysOrg_EmpVerification>, INGPEntityMap
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysOrg_EmpVerification> builder)
        {

            // Table & Column Mappings
            builder.ToTable("Sys_Org_EmpVerification");
            builder.Property(t => t.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpId");

            builder.Property(t => t.SendTime)
                .HasColumnName("SendTime");

            builder.Property(t => t.VerificationCode)
                .HasMaxLength(50)
                .HasColumnName("VerificationCode");
            base.PostConfigure(builder);
        }
    }
}
