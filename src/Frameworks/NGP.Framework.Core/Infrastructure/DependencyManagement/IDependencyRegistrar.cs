/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * IDependencyRegistrar Description:
 * 依赖注入接口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-28   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Autofac;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        /// <summary>
        /// 注入顺序
        /// </summary>
        int Order { get; }
    }
}
