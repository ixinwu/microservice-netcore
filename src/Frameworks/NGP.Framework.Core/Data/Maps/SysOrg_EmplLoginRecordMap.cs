using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.DataAccess
{
    /// <summary>
    /// 用户登录履历信息表映射
    /// </summary>
    public class SysOrg_EmplLoginRecordMap : BaseDBEntityMap<SysOrg_EmplLoginRecord>, INGPEntityMap
    {
        /// <summary>
        /// 用户登录履历信息表映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysOrg_EmplLoginRecord> builder)
        {

            // Table & Column Mappings
            builder.ToTable("Sys_Org_EmplLoginRecord");
            //用户Id          
            builder.Property(t => t.EmpId)
                .HasMaxLength(50)
                .HasColumnName("EmpId");
            //角色Id                
            builder.Property(t => t.RoleId)
                .HasMaxLength(50)
                .HasColumnName("RoleId");
            //登录名 
            builder.Property(t => t.LoginName)
                .HasMaxLength(50)
                .HasColumnName("LoginName");
            //人员类别 
            builder.Property(t => t.EmployeeType)
                .HasMaxLength(100)
                .HasColumnName("EmployeeType");
            //地区Id 
            builder.Property(t => t.AreaId)
                .HasMaxLength(50)
                .HasColumnName("AreaId");
            //登录IP  
            builder.Property(t => t.LoginIP)
                .HasColumnName("LoginIP");
            //是否登录成功
            builder.Property(t => t.IsLoginSucess)
                .HasColumnName("IsLoginSucess");

            base.PostConfigure(builder);
        }
    }
}
