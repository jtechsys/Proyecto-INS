using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class XmlUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public static string ObjectToXml<T>(T obj)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    string xml;

        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Encoding = new UnicodeEncoding(false, false);
        //    settings.Indent = true;
        //    settings.OmitXmlDeclaration = true;

        //    StringWriter textWriter = new StringWriter();
        //    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
        //    {
        //        serializer.Serialize(xmlWriter, obj);
        //    }
        //    xml = textWriter.ToString(); //This is the output as a string
        //    xml = "<?xml version='1.0' encoding='UTF-8' standalone='no'?>" + xml;

        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xml);
        //    //XmlNodeList elemList = doc.GetElementsByTagName("cbc:IssueTime");
        //    //elemList[0].InnerText = Convert.ToDateTime(elemList[0].InnerText).ToString("HH:mm:ss");

        //    xml = doc.InnerXml.ToString();
        //    return xml;
        //}

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSerialize">To serialize.</param>
        /// <returns></returns>
        public static string SerializeObjectToString<T>(T objectToSerialize)
        {
            
            var serializer = new XmlSerializer(objectToSerialize.GetType());
            string resultado = "";

            using (var memStr = new MemoryStream())
            {
                using (var stream = new StreamWriter(memStr))
                {
                    serializer.Serialize(stream, objectToSerialize);
                }
                var betterBytes = Encoding.Convert(Encoding.UTF8,
                                  Encoding.GetEncoding("UTF-8"),
                                  memStr.ToArray());

                resultado = System.Text.Encoding.UTF8.GetString(betterBytes);
                
            }
            return resultado;
        
        }

        public static byte[] SerializeObjectToBytes<T>(T objectToSerialize)
        {

            var serializer = new XmlSerializer(objectToSerialize.GetType());
            byte[] resultado = null;

            using (var memStr = new MemoryStream())
            {
                using (var stream = new StreamWriter(memStr))
                {
                    serializer.Serialize(stream, objectToSerialize);
                }
                var betterBytes = Encoding.Convert(Encoding.UTF8,
                                  Encoding.GetEncoding("UTF-8"),
                                  memStr.ToArray());

                resultado = betterBytes;
                //resultado = Convert.ToBase64String(betterBytes);
            }
            return resultado;

        }

        /// <summary>
        /// Deserializes the specified XML.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            T result;

            using (TextReader reader = new StringReader(xml))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result; 
        }
    }
}
