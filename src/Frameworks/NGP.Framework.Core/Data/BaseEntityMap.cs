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
    public class BaseEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        #region Utilities

        /// <summary>
        /// 开发人员可以在自定义分部类中重写此方法，以便添加一些自定义配置代码
        /// </summary>
        /// <param name="builder">用于配置实体的生成器</param>
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
            // 主键
            builder.HasKey(s => s.Id);

            // 表id
            builder.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(50);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 配置实体
        /// </summary>
        /// <param name="builder">用于配置实体的生成器</param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //add custom configuration
            PostConfigure(builder);
        }

        /// <summary>
        /// 应用此映射配置
        /// </summary>
        /// <param name="modelBuilder">用于为数据库上下文构造模型的生成器</param>
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        #endregion
    }
}
