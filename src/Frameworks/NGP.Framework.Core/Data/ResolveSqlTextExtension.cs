/* ---------------------------------------------------------------------    
 * Copyright:
 * 
 * 
 * ResolveXmlHelper Description:
 * 
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2016/7/26 15:07:18 layne
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NGP.Framework.Core.Data
{
    /// <summary>
    /// 解析XML
    /// </summary>
    public static class ResolveSqlTextExtension
    {
        #region Methods
        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="xmlFilePath">完整路径</param>
        /// <returns>XElement</returns>
        private static XElement LoadXmlFile(string xmlFilePath)
        {
            if (!File.Exists(xmlFilePath))
            {
                throw new NGPException(StatusCode.FileException);
            }
            return XElement.Load(xmlFilePath);
        }

        /// <summary>
        /// 获取节点文本
        /// </summary>
        /// <param name="xElement">xml解析文档</param>
        /// <param name="xName">节点名称</param>
        /// <param name="elementName">子节点名称</param>
        /// <returns>获取的文本</returns>
        private static string GetElementText(this XElement xElement, string xName, string elementName)
        {
            var item = xElement.Descendants(xName).FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }
            return item.Element(elementName).Value;
        }

        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="xElement">xml解析文档</param>
        /// <param name="xName">节点名称</param>
        /// <param name="elementName">子节点名称</param>
        /// <returns>获取的节点列表</returns>
        private static IEnumerable<XElement> GetElementList(this XElement xElement, string xName, string elementName)
        {
            return xElement.Descendants(xName).FirstOrDefault().Descendants(elementName);
        }

        /// <summary>
        /// 获取操作的xml
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="paramValueList"></param>
        /// <returns></returns>
        public static (string sqlText, IDictionary<string, object> parameters) GetResolveXmlResult(string xmlId, 
            string sqlId,
            params object[] paramValueList)
        {
            var fullPath = CommonHelper.DefaultFileProvider.MapPath($"App_Data/SqlFiles/{xmlId}.xml");
            var xmlDocument = LoadXmlFile(fullPath);
            var sqlText = xmlDocument.GetElementText(sqlId, "Sql");
            var parameters = new Dictionary<string, object>();
            var paramNodeList = xmlDocument.GetElementList(sqlId, "Param").ToList();

            for (int i = 0; i < paramNodeList.Count; i++)
            {
                var key = paramNodeList[i].Attribute("name").Value;
                if (paramValueList[i] != null)
                {
                    parameters[key] = paramValueList[i];
                }
                else
                {
                    parameters[key] = DBNull.Value;
                }
            }

            return (sqlText, parameters);
        }
        #endregion
    }
}
