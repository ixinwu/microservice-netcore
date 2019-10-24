/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPFileService Description:
 * 文件扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-7    hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core;
using System.Linq;

namespace NGP.Framework.Core
{

    /// <summary>
    /// 上传类型
    /// </summary>
    public enum UploadType
    {
        /// <summary>
        /// 附件
        /// </summary>
        Attachment,

        /// <summary>
        /// 导入
        /// </summary>
        Import,
    }

    /// <summary>
    /// 文件扩展
    /// </summary>
    public static class NGPFileExtensions
    {
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="comanpyId"></param>
        /// <param name="uploadType"></param>
        /// <returns></returns>
        public static string VirtualFilePath(string areaId, string comanpyId, UploadType uploadType = UploadType.Attachment)
        {
            var index = 0;
            var path = areaId.Aggregate(string.Empty, (areaPath, next) =>
             {
                 areaPath += next;
                 index++;
                 if (index % 2 == 0 && index < 7)
                 {
                     areaPath += "/";
                 }
                 return areaPath;
             });
            if (!string.IsNullOrWhiteSpace(comanpyId))
            {
                path += string.Format("/{0}", comanpyId);
            }

            if (uploadType == UploadType.Attachment)
                return "wwwroot/attachments/" + path;
            else if (uploadType == UploadType.Import)
                return "wwwroot/import/" + path;
            else
                return "wwwroot/attachments/" + path;
        }

        /// <summary>
        /// 获取文件url
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FileUrl(string filePath,string fileName)
        {
            var webHelper = Singleton<IEngine>.Instance.Resolve<IWebHelper>();
            return string.Format("{0}/{1}", filePath, fileName).Replace("wwwroot", webHelper.GetStoreHost(false));
        }
    }
}
