using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

namespace DGP.Snap.Helper
{
    class Setting
    {
        public static readonly string LocalDataPath =
            Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"\Setting");
        /// <summary>
        /// 缓存设置项，使用普通的字典
        /// </summary>
        private static readonly Dictionary<string, XmlDocument> FileCache = new Dictionary<string, XmlDocument>();

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T Get<T>(string id, T defaultvalue = default(T), Assembly calling = null)
        {
            if (!typeof(T).IsSerializable && !typeof(ISerializable).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("正在序列化的类需要实现ISerializable接口");

            string file = Path.Combine(LocalDataPath,(calling ?? Assembly.GetCallingAssembly()).GetName().Name + ".config");

            XmlDocument xmlDocument = GetConfigFile(file);

            // try to get setting
            var setting = GetSettingFromXml(xmlDocument, id, defaultvalue);

            return setting != null ? setting : defaultvalue;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Set(string id, object value, Assembly calling = null)
        {
            if (!value.GetType().IsSerializable)
                throw new NotSupportedException("新的值不支持序列化.");

            var filepath = Path.Combine(LocalDataPath,(calling ?? Assembly.GetCallingAssembly()).GetName().Name + ".config");
            //也就是 DGP.Snap.config ？ 似乎可以适应多个程序集的情况，原作者为插件准备的吧

            WriteSettingToXml(GetConfigFile(filepath), id, value);
        }

        private static T GetSettingFromXml<T>(XmlDocument doc, string id, T failsafe)
        {
            var v = doc.SelectSingleNode($@"/Settings/{id}");

            try
            {
                var result = v == null ? failsafe : (T)Convert.ChangeType(v.InnerText, typeof(T));
                return result;
            }
            catch (Exception)
            {
                return failsafe;
            }
        }

        private static void WriteSettingToXml(XmlDocument doc, string id, object value)
        {
            var v = doc.SelectSingleNode($@"/Settings/{id}");

            if (v != null)
            {
                v.InnerText = value.ToString();
            }
            else
            {
                var node = doc.CreateNode(XmlNodeType.Element, id, doc.NamespaceURI);
                node.InnerText = value.ToString();
                doc.SelectSingleNode(@"/Settings")?.AppendChild(node);
            }

            doc.Save(new Uri(doc.BaseURI).LocalPath);
        }

        private static XmlDocument GetConfigFile(string file)
        {
            if (FileCache.ContainsKey(file))
                return FileCache[file];

            Directory.CreateDirectory(Path.GetDirectoryName(file));
            if (!File.Exists(file))
                CreateNewConfig(file);

            var doc = new XmlDocument();
            try
            {
                doc.Load(file);
            }
            catch (XmlException)
            {
                CreateNewConfig(file);
                doc.Load(file);
            }

            if (doc.SelectSingleNode(@"/Settings") == null)
            {
                CreateNewConfig(file);
                doc.Load(file);
            }

            FileCache.Add(file, doc);
            return doc;
        }

        private static void CreateNewConfig(string file)
        {
            using (var writer = XmlWriter.Create(file))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");
                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
            }
        }
    }
}
