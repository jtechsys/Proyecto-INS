using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class SerializeUtils
    {


        //public static IList<T> DeserializeFile<T>(string filename)
        //{
        //    XmlSerializer deserializer = new XmlSerializer(typeof(List<Object>));

        //    TextReader reader = new StreamReader(filename);

        //    try
        //    {
        //        object obj = deserializer.Deserialize(reader);
        //        reader.Close();

        //        return (List<T>)obj;
        //    }
        //    catch(Exception ex)
        //    {
        //        reader.Close();
        //        return null;
        //    }


        //}

        /// <summary>
        /// XMLs the serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="filename">The filename.</param>
        public static void XmlSerialize<T>(IList<T> list, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            using (var stream = File.OpenWrite(filename))
            {
                serializer.Serialize(stream, list);
            }
            
        }

        /// <summary>
        /// XMLs the serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The object.</param>
        /// <param name="filename">The filename.</param>
        public static void XmlSerialize<T>(T @object, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var stream = File.OpenWrite(filename))
            {
                serializer.Serialize(stream, @object);
            }
        }

        /// <summary>
        /// XMLs the serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The object.</param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T @object)
        {
            var serializer = new XmlSerializer(@object.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, @object);
            }

            return sb.ToString();

        }

        /// <summary>
        /// Populates the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="target">The target.</param>
        public static void PopulateObject(string value,object target)
        {
            JsonConvert.PopulateObject(value, target);
        }

        /// <summary>
        /// Jsons the deserialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectString">The object string.</param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string objectString)
        {
            //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy hh:mm:ss" };
            
            return JsonConvert.DeserializeObject<T>(objectString, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None , TypeNameHandling = TypeNameHandling.Auto , NullValueHandling = NullValueHandling.Ignore   } );
        }

      

        public static string JsonSerializer(object T)
        {
            return JsonSerializer(T, true);
        }



        public static string JsonSerializer(object T, bool references)
        {


            if (references)
            {
                return JsonConvert.SerializeObject(T, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //DateFormatString = "dd/MM/yyyy hh:mm:ss"
                    //TypeNameHandling = TypeNameHandling.All
                    //ContractResolver  = new OnlyPropertiesContractResolver()
                });

                //return JsonConvert.SerializeObject(T);
            }
            else
            {
                return JsonConvert.SerializeObject(T, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    //TypeNameHandling = TypeNameHandling.All,
                    ContractResolver = new OnlyPropertiesContractResolver()
                });
            }




            //}

            //public static string JsonSerializer(object obj)
            //{

            //    var jss = new JavaScriptSerializer();
            //    return jss.Serialize(obj);

            //    //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            //    //MemoryStream ms = new MemoryStream();
            //    //ser.WriteObject(ms, t);
            //    //string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            //    //ms.Close();
            //    //return jsonString;
            //}
            ///// <summary>
            ///// JSON Deserialization
            ///// </summary>
            //public static T JsonDeserialize<T>(string jsonText)
            //{
            //    //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            //    //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            //    //T obj = (T)ser.ReadObject(ms);
            //    //return obj;

            //    var jss = new JavaScriptSerializer();
            //    return jss.Deserialize<T>(jsonText);
            //}


        }

        public class OnlyPropertiesContractResolver : DefaultContractResolver
        {
            protected override List<MemberInfo> GetSerializableMembers(Type objectType)
            {
                // whait do i do here ??? 
                List<PropertyInfo> props = new List<PropertyInfo>();

                var properties = objectType.GetProperties()
           .Where(p =>
                       !typeof(IEnumerable).IsAssignableFrom(p.PropertyType));

                foreach (var p in properties)
                {
                    //if(p.GetAccessors())
                    if (p.GetGetMethod().IsVirtual)
                        continue;



                    props.Add(p);


                }

                return new List<MemberInfo>(props);

            }
        }

        public class WithChildsContractResolver : DefaultContractResolver
        {
            protected override List<MemberInfo> GetSerializableMembers(Type objectType)
            {
                // whait do i do here ??? 
                List<PropertyInfo> props = new List<PropertyInfo>();

                var properties = objectType.GetProperties();


                foreach (var p in properties)
                {
                    //if(p.GetAccessors())
                    if (p.GetGetMethod().IsVirtual)
                        if (!typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                            continue;

                    props.Add(p);
                }

                return new List<MemberInfo>(props);

            }
        }
    }


}
