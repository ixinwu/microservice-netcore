/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * WorkContext Description:
 * 当前工作上下文
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core.Models;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 当前工作上下文
    /// </summary>
    public class WorkContext : IWorkContext
    {
        private WorkEmployee _current;

        /// <summary>
        /// 当前人员信息
        /// </summary>
        public WorkEmployee Current { get
            {
                if (_current == null)
                {
                    _current = WorkEmployee.SysDefaultEmployee();                    
                }
                return _current;
            }
            set
            {
                _current = value;
            }
        }
        /// <summary>
        /// 当前Api请求信息
        /// </summary>
        public WorkRequest CurrentRequest { get; set; }

        /// <summary>
        /// 工作语言
        /// </summary>
        public WorkLanguage WorkingLanguage { get; set; }
    }
}
