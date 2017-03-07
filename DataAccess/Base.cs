using System.Xml;
using System.IO;
using System.Configuration;
using System.Web;
using System;
using System.Globalization;

namespace DataAccess
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class Base
    {
        /// <summary>
        /// 获取配置文件配置
        /// Author:Baolin
        /// CreateDate:2015年6月30日15:46:28
        /// </summary>
        /// <param name="appsettingKey">名称</param>
        /// <returns></returns>
        public static string GetAppSetting(string appsettingKey)
        {
            string appsettingsValue;
            var appsettingsValueFromCache = (string)HttpRuntime.Cache.Get(appsettingKey);
            if (appsettingsValueFromCache == null)
            {//缓存为空，读取配置文件
                appsettingsValue = ConfigurationManager.AppSettings[appsettingKey];
                if (appsettingsValue != null)
                {
                    HttpRuntime.Cache.Insert(appsettingKey, appsettingsValue);
                }
            }
            else
            {
                appsettingsValue = appsettingsValueFromCache;
            }
            return appsettingsValue;
        }
        /// <summary>
        /// 获取配置文件值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="appsettingKey">配置Key</param>
        /// <returns></returns>
        public static T GetAppSetting<T>(string appsettingKey)
        {
            string appsettingsValue = string.Empty;
            var appsettingsValueFromCache = (string)HttpRuntime.Cache.Get(appsettingKey);
            if (appsettingsValueFromCache == null)
            {//缓存为空，读取配置文件
                appsettingsValue = ConfigurationManager.AppSettings[appsettingKey];
                if (appsettingsValue != null)
                {
                    HttpRuntime.Cache.Insert(appsettingKey, appsettingsValue);
                }
            }
            else
            {
                appsettingsValue = appsettingsValueFromCache;
            }
            return (T)Convert.ChangeType(appsettingsValue, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static void Logger(Exception ex)
        {
            bool writerLog = false;
            try
            {
                writerLog = GetAppSetting<bool>("SQLLog");
            }
            catch { }
            if (!writerLog) return;
            Action<Exception> log = (e) =>
            {
                string basePath = System.AppDomain.CurrentDomain.BaseDirectory + "SQL.Error";
                if (!System.IO.Directory.Exists(basePath))
                {
                    System.IO.Directory.CreateDirectory(basePath);
                }
                string logPath = basePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.AppendLine("*************************【时间：" + DateTime.Now + "】********************************");
                builder.AppendLine("【Message】" + e.Message);
                builder.AppendLine("【Source】" + e.Source);
                builder.AppendLine("【StackTrace】" + e.StackTrace);
                builder.AppendLine("*************************【结束】********************************");
                //using (var sw = System.IO.File.AppendText(logPath))
                //{
                //    sw.WriteLine(builder.ToString());
                //}
                WriteText(logPath, builder.ToString());
            };
            log.BeginInvoke(ex, null, null);
        }
        /// <summary>
        /// 非占用读取文件方式
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private static string ReaderText(string path)
        {
            string text = string.Empty;
            try
            {
                using (FileStream fsRead = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fsRead, System.Text.Encoding.UTF8))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                return text;
            }
            catch { }
            return text;
        }
        /// <summary>
        /// 非占用写入文件方式
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <returns></returns>
        private static void WriteText(string path, string text)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(text);
                    }
                }
            }
            catch { }
        }
    }
}
