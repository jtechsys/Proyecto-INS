using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumUtils
    {

        
        /// <summary>
        /// To the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// To the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return ToEnum<T>(name);
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescription<T>(int value)
        {
            var toenum = ToEnum<T>(value);
            return GetDescription(toenum);
        }

        /// <summary>
        /// To the dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IReadOnlyDictionary<int, string> ToDictionary<T>() where T : struct
        {
            var dictionary = new Dictionary<int, string>();

            var values = Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                int key = (int)value;

                dictionary.Add(key, value.ToString());
            }

            return dictionary;
        }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IReadOnlyList<KeyValue> ToList<T>() where T : struct
        {
            var dictionary = new List<KeyValue>();

            var values = Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                int key = (int)value;

                dictionary.Add(new KeyValue() { Key = key, Value = StringUtils.FormatCaptionDisplay(value.ToString())});
            }

            return dictionary;
        }

        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string GetDescription<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        /// <summary>
        /// Gets the enum from description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ArgumentException">Not found. - description</exception>
        public static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }


    }

   

}
