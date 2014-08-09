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

            string currentLang = MVCContext.GetCurrentLang();


            return LangResourceFileProvider.GetLangString(key, currentLang, FilePath);
        }
    }

    public static class LangResourceFileProvider
    {
        public static string[] LangList = new string[] { "en-US", "zh-CN" };

        private static IDictionary<string, string> dataCollection = new Dictionary<string, string>();

        public static string GetLangString(string Key, string lang, string FilePath)
        {
            var assembleKey = AssmbleKey(Key, lang);

            if (dataCollection.ContainsKey(assembleKey))
            {
                return dataCollection[assembleKey] as string;
            }

            /*没有维护中文，就找英文*/
            if (lang != ResourceManager.LANG_DEFAULT)
            {
                var assembleDefaultKey = AssmbleKey(Key, ResourceManager.LANG_DEFAULT);
                if (dataCollection.ContainsKey(assembleDefaultKey))
                {
                    return dataCollection[assembleDefaultKey] as string;
                }
            }

            return string.Empty;
        }

        public static string AssmbleKey(string key, string lang)
        {
            return key + "-" + lang;
        }

        public static void InitResource()
        {
            string path = HttpContext.Current.Server.MapPath("/") + "Resource\\";

            foreach (string lang in LangList)
            {
                ResXResourceReader reader = new ResXResourceReader(path + lang + ".resx");

                try
                {
                    foreach (DictionaryEntry d in reader)
                    {
                        dataCollection.Add(new KeyValuePair<string, string>(AssmbleKey(d.Key.ToString(), lang), d.Value.ToString()));
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
            }
        }
    }

    public enum LangType
    {
        cn,
        en
    }
}
