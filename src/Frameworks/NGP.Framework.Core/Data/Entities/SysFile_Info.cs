/* ---------------------------------------------------------------------    
 * Copyright:
 * Wuxi Efficient Technology Co., Ltd. All rights reserved. 
 * 
 * Class Description:
 * 
 *
 * Comment 					        Revision	        Date                 Author
 * -----------------------------    --------         --------            -----------
 * Created							1.0		    2019/3/7 14:49:49   rock@xcloudbiz.com
 *
 * ------------------------------------------------------------------------------*/
using System;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 文件表
    /// </summary>
    public class SysFile_Info : BaseDBEntity
    {

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get;set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get;set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtensionName { get;set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get;set; }
    }
}