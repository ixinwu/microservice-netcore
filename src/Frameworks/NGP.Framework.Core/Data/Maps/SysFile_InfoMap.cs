/* ---------------------------------------------------------------------    
 * Copyright:
 * Wuxi Efficient Technology Co., Ltd. All rights reserved. 
 * 
 * Class Description:
 * 
 *
 * Comment 					        Revision	        Date                 Author
 * -----------------------------    --------         --------            -----------
 * Created							1.0		    2019/3/7 14:50:07   rock@xcloudbiz.com
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
	public class SysFile_InfoMap : BaseDBEntityMap<SysFile_Info>, INGPEntityMap
    {
        /// <summary>
        /// 化债计划关联映射
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<SysFile_Info> builder)
        {
            // Table & Column Mappings
            builder.ToTable("Sys_File_Info ");
            builder.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("FileName");
            builder.Property(t => t.Size)
                .IsRequired()
                .HasColumnName("Size");
            builder.Property(t => t.ExtensionName)
                .HasMaxLength(50)
                .HasColumnName("ExtensionName");
            builder.Property(t => t.FilePath)
                .HasColumnName("FilePath");

            base.PostConfigure(builder);
        }
    }
}
