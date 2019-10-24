using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class SysLog_TaskCommandMap : BaseDBEntityMap<SysLog_TaskCommand>, INGPEntityMap
    {
        /// <summary>
        /// 系统异常日志关联映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysLog_TaskCommand> builder)
        {
            // Table & Column Mappings
            builder.ToTable("Sys_Log_TaskCommand");

            builder.Property(t => t.ConfigKey)
                .HasMaxLength(50)
                .HasColumnName("ConfigKey");
            builder.Property(t => t.TaskStratTime)
                .HasColumnName("TaskStratTime");
            builder.Property(t => t.TaskEndTime)
                .HasColumnName("TaskEndTime");
            builder.Property(t => t.ExcuteTimeS)
                .HasColumnName("ExcuteTimeS");
            base.PostConfigure(builder);
        }
    }
}
