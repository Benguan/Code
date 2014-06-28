using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Resources;
using System.Web;

using System.Web.Mvc;

namespace NEG.Website.Controls.Common
{
    public static class LocalizationHelpers
    {
        

        /// <summary>
        /// 在外边的 Html 中直接使用
        /// </summary>
        /// <param name="htmlhelper"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Lang(this HtmlHelper htmlhelper, string key)
        {
            string FilePath = htmlhelper.ViewContext.HttpContext.Server.MapPath("/") + "Resource\\";
            return GetLangString(htmlhelper.ViewContext.HttpContext, key, FilePath);
        }
        /// <summary>
        /// 在外边的 Html 中直接使用，对 JS 进行输出字符串
        /// </summary>
        /// <param name="htmlhelper"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string LangOutJsVar(this HtmlHelper htmlhelper, string key)
        {
            string FilePath = htmlhelper.ViewContext.HttpContext.Server.MapPath("/") + "Resource\\";
            string langstr = GetLangString(htmlhelper.ViewContext.HttpContext, key, FilePath);
            return string.Format("var {0} = '{1}'", key, langstr);
        }
        /// <summary>
        /// 在 C# 中使用
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string InnerLang(HttpContextBase httpContext, string key)
        {
            string FilePath = httpContext.Server.MapPath("/") + "Resource\\";
            return GetLangString(httpContext, key, FilePath);
        }

        private static string GetLangString(HttpContextBase httpContext, string key, string FilePath)
        {
            LangType langtype = LangType.en;

            if (httpContext.Session["Lang"] != null)
            {
                langtype = (LangType)httpContext.Session["Lang"];
            }
            return LangResourceFileProvider.GetLangString(key, langtype, FilePath);
        }
    }

    public static class LangResourceFileProvider
    {
        private static IDictionary<string, string> dataCollection = new Dictionary<string, string>();

        public static string GetLangString(string Key, LangType langtype, string FilePath)
        {
            if (dataCollection.ContainsKey(Key))
            {
                return dataCollection[Key] as string;
            }


            string filename;
            switch (langtype)
            {
                case LangType.cn:
                    filename = "zh-CN.resx";
                    break;
                case LangType.en:
                    filename = "en-US.resx";
                    break;
                default:
                    filename = "en-US.resx";
                    break;
            }

            //System.Resources.ResourceReader reader = new System.Resources.ResourceReader(FilePath + filename);
            ResXResourceReader reader = new ResXResourceReader(FilePath + filename);


            try
            {
                foreach (DictionaryEntry d in reader)
                {
                    dataCollection.Add(new KeyValuePair<string, string>(d.Key.ToString(), d.Value.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }

            if (dataCollection.ContainsKey(Key))
            {
                return dataCollection[Key] as string;
            }

            return string.Empty;
        }
    }

    public enum LangType
    {
        cn,
        en
    }
}
