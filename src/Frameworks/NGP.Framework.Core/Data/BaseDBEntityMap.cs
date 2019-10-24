/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * BaseDBEntityMap Description:
 * DB映射基类
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NGP.Framework.Core
{
    /// <summary>
    /// DB映射基类
    /// </summary>
    public class BaseDBEntityMap<TEntity> : BaseEntityMap<TEntity> where TEntity : BaseDBEntity
    {
        #region Utilities

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder">用于配置实体的生成器</param>
        protected override void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
            base.PostConfigure(builder);

            // 删除标志
            builder.Property(t => t.IsDelete)
                .IsRequired();
            // 创建时间
            builder.Property(t => t.CreatedTime)
                .IsRequired();
            // 创建者
            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);
            //创建平台
            builder.Property(t => t.CreatedCompany)
                .HasMaxLength(50);
            //创建部门
            builder.Property(t => t.CreatedDept)
                .HasMaxLength(50);
            // 更新时间
            builder.Property(t => t.UpdatedTime)
               .IsRequired();
            // 更新者
            builder.Property(t => t.UpdatedBy)
                 .IsRequired()
                 .HasMaxLength(50);
            //创建平台
            builder.Property(t => t.UpdatedCompany)
                .HasMaxLength(50);
            //创建部门
            builder.Property(t => t.UpdatedDept)
                .HasMaxLength(50);
        }

        #endregion
    }
}
