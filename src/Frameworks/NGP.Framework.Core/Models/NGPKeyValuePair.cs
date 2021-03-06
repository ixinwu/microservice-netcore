﻿/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * KeyValuePairInfo Description:
 * 键值对模型
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Runtime.Serialization;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 键值对模型
    /// </summary>
    public class NGPKeyValuePair<TKey, TValue> : INGPModel
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderIndex { get; set; }
    }

    /// <summary>
    /// 键值对模型
    /// </summary>
    public class NGPKeyValuePair : NGPKeyValuePair<string, string>
    {
    }

    /// <summary>
    /// 键值对模型
    /// </summary>
    public class NGPKeyValuePair<TValue> : NGPKeyValuePair<string, TValue>
    {
    }
}
