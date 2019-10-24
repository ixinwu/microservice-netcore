/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ConsulServiceConfig Description:
 * Consul配置
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;

namespace NGP.Framework.Core.Consul
{
    /// <summary>
    /// Consul配置
    /// </summary>
    public class ConsulServiceConfig
    {
        /// <summary>
        /// 服务注册地址
        /// </summary>
        public Uri ServiceDiscoveryAddress { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public Uri ServiceAddress { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
    }
}
