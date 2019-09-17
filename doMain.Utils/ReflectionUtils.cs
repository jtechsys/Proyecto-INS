using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doMain.Utils
{

  

    /// <summary>
    /// 
    /// </summary>
    public class ReflectionUtils
    {

        //static bool IsClassProperty(Type type)
        //{
        //    if (type.FullName.Contains("Int"))
        //        return false;
        //    if (type.FullName.Contains("String"))
        //        return false;

        //    if(type.Name.Contains("IReadOnlyCollection"))
        //        return false;

        //    if (type.BaseType.FullName.Contains("Enum"))
        //        return false;

        //    if (type.Name.Contains("DateTime"))
        //        return true;

        //    if (type.Name.Contains("Boolean"))
        //        return true;

        //    return true;

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public static void ClearClassProperties(object obj)
        {
            if (obj == null)
                return;

            var dic = new Dictionary<string, object>();

            var props = obj.GetType().GetProperties();

            foreach (var p in props)
            {
                if (p.PropertyType.BaseType == null)
                    continue;

                if(!p.PropertyType.BaseType.FullName.StartsWith("System."))
                {
                    SetValue(obj, p.Name, null);
                }
            }

            
        }

        /// <summary>
        /// Gets the property clases.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetClassProperties(object obj)
        {
            var dic = new Dictionary<string, object>();

            var props = obj.GetType().GetProperties();

            foreach (var p in props)
            {
                if (p.PropertyType.BaseType == null)
                    continue;

                if (!p.PropertyType.BaseType.FullName.StartsWith("System."))
                {
                    dic.Add(p.Name, p.GetValue(obj));
                }
                                
            }

            return dic;
        }


        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyname">The assemblyname.</param>
        /// <param name="classname">The classname.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static I CreateInstance<I, T>(string assemblyname, string classname, params object[] args)
        {


            var clientAssembly = Assembly.LoadFrom(assemblyname);

            Type classType = clientAssembly.GetType(classname);

            Type[] typeArgs = { typeof(T) };
            Type constructed = classType.MakeGenericType(typeArgs);



            var obj = Activator.CreateInstance(constructed, args);

            return (I)obj;

        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <param name="assemblyfile">The assemblyfile.</param>
        /// <param name="namespaceclass">The namespaceclass.</param>
        /// <param name="methodname">The methodname.</param>
        /// <param name="pars">The pars.</param>
        /// <returns></returns>
        public static object GetMethod(string assemblyfile, string namespaceclass, string methodname, params object[] pars)
        {
            string dllpath = FileUtils.GetFolder(Application.ExecutablePath) + "\\" + assemblyfile + ".dll";


            Assembly assembly = Assembly.LoadFile(dllpath);
            Type type = assembly.GetType(namespaceclass);
            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod(methodname);
                if (methodInfo != null)
                {
                    object result = null;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(type, null);
                    if (parameters.Length == 0)
                    {
                        //This works fine
                        return methodInfo.Invoke(classInstance, null);
                    }
                    else
                    {


                        //The invoke does NOT work it throws "Object does not match target type"             
                        return methodInfo.Invoke(classInstance, pars);
                    }
                }
            }

            return null;

        }


        /// <summary>
        /// Lists the object collections.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public List<object> ListObjectCollections(object obj)
        {
            var objtype = obj.GetType();
            var list = new List<object>();
            foreach (var prop in objtype.GetProperties())
            {
                if (prop.PropertyType.Namespace == "System.Collections.Generic")
                {

                    list.Add(prop.GetValue(obj, null));
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">obj</exception>
        public static ICollection<T> GetCollection<T>(object obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            var type = obj.GetType();
            var res = new List<T>();
            foreach (var prop in type.GetProperties())
            {
                // is IEnumerable<T>?
                if (typeof(ICollection<T>).IsAssignableFrom(prop.PropertyType))
                {
                    var get = prop.GetGetMethod();
                    if (!get.IsStatic && get.GetParameters().Length == 0) // skip indexed & static
                    {
                        return (ICollection<T>)get.Invoke(obj, null);
                        
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Lists the object collections.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        //public static List<ICollection<T>> ListObjectCollections<T>(T obj)
        //{
        //    var objtype = obj.GetType();
        //    var list = new List<ICollection<T>>();
        //    foreach (var prop in objtype.GetProperties())
        //    {
        //        if (prop.PropertyType.Namespace == "System.Collections.Generic")
        //        {
        //            list.Add((ICollection<T>)prop.GetValue(obj, null));
        //        }
        //    }

        //    return list;
        //}

        /// <summary>
        /// Adds the item collection.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentNullException">obj</exception>
        public static void AddItemCollection(object obj,object item) 
        {
            if (obj == null) throw new ArgumentNullException("obj");
            var objtype = obj.GetType();
            var itemtype = item.GetType();
            
            foreach (var prop in objtype.GetProperties())
            {
                //prop.PropertyType.Namespace == "System.Collections.Generic" && 
                //    // is IEnumerable<T>?
                if (prop.PropertyType.FullName.Contains(string.Format("System.Collections.Generic.ICollection`1[[{0},", itemtype.FullName)))
                {
                    var get = prop.GetGetMethod();
                    if (!get.IsStatic && get.GetParameters().Length == 0) // skip indexed & static
                    {
                        var collection = get.Invoke(obj, null);
                        collection.GetType().GetMethod("Add").Invoke(collection, new[] { item } );

                      
                    }
                }
            }
              
        }

        /// <summary>
        /// Gets the column attribute value.
        /// </summary>
        /// <param name="objecttype">The objecttype.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static string GetColumnAttributeValue(object objecttype, string propertyName)
        {
            var proinfo = objecttype.GetType().GetProperties().Where(x => x.Name == propertyName).FirstOrDefault();

            //foreach (PropertyInfo property in objecttype.GetType().GetProperties())
            //{
            var attribute = Attribute.GetCustomAttribute(proinfo, typeof(ColumnAttribute))
                as ColumnAttribute;

            if (attribute != null) // This property has a KeyAttribute
            {
                // Do something, to read from the property:
                return attribute.Name; //.GetValue(objecttype);

            }
            //}

            return null;
        }

        //DisplayAttribute
        public static string GetAttribute<Attribute>(Type objecttype , Attribute attribute, string propertyname) where Attribute : class
        {

            var properties = objecttype.GetProperties()
                .Where(p => p.IsDefined(attribute.GetType(), false) &&  p.PropertyType.Name == propertyname)
                .Select(p => new
                {
                    //PropertyName = p.Name,
                    p.GetCustomAttributes(attribute.GetType(),
                            false).Cast<DisplayAttribute>().Single().Name
                });

            return properties.Select(x=>x.Name).FirstOrDefault();
        }


        


        public static object GetKeyAttributeValue(object objecttype) 
        {

            foreach (PropertyInfo property in objecttype.GetType().GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute))
                    as KeyAttribute;

                if (attribute != null) // This property has a KeyAttribute
                {
                    // Do something, to read from the property:
                    object val = property.GetValue(objecttype);
                    return val;
                }
            }

            return null; 
        }

        public static bool IsPropertyNulleable(Type prop)
        {
            if (Nullable.GetUnderlyingType(prop) != null)
            {
                // It's nullable
                return true;
            }

            return false;
        }

        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static object CreateInstance(object src, string propName)
        {
            Type t = GetPropertyType(src, propName);
            return Activator.CreateInstance(t);
        }

        public static Type GetPropertyType(object src, string propName)
        {
            foreach (var prop in src.GetType().GetProperties())
            {
                if(propName == prop.Name)
                    return prop.PropertyType;
            }

            return null;
            //return src.GetType().GetProperty(propName).GetType();
        }

        public static Dictionary<string,int> DicGetProperties(object mlist)
        {

            //if (mlist == null)
            //    return null;

            var item = mlist;

            if (item == null)
                return null;

            Type typeOfMyObject = item.GetType();
            PropertyInfo[] properties = typeOfMyObject.GetProperties();
            var names = new Dictionary<string,int>();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType.ToString().Contains("ExtensionDataObject"))
                    continue;

                names.Add(properties[i].Name, getProperty(properties[i].PropertyType.ToString()));
            }

            return names;

        }

        static int getProperty(string name)
        {
            if (name.Contains("String"))
                return 8;

            if (name.Contains("Boolean"))
                return 11;

            if (name.Contains("Date"))
                return 7;

            if (name.Contains("Byte"))
                return 5;

            return 8;
        }

        //public static List<string> GetProperties(IList objectproperties)
        //{
        //    var lista = objectproperties;
        //    object uniqueobject = null;
        //    foreach (var mObject in lista)
        //    {
        //        uniqueobject = mObject;
        //        break;
        //    }



        //    //Type myType = uniqueobject.GetType();

        //    var result = new List<string>();

        //    PropertyInfo[] properties = uniqueobject.GetType().GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        result.Add(property.Name);


        //        //if (property.Name == "MyProperty")
        //        //{
        //        //    object value = results.GetType().GetProperty(property.Name).GetValue(uniqueobject, null);
        //        //    if (value != null)
        //        //    {
        //        //        //assign the value
        //        //    }
        //        //}
        //    }

        //    //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

        //    //foreach (PropertyInfo prop in props)
        //    //{
        //    //    object propValue = prop.GetValue(uniqueobject, null);

        //    //    // Do something with propValue
        //    //}

        //    return result;

        //}

        /// <summary>
        /// Lists the properties value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Dictionary<string, object> ListPropertiesValue(object obj)
        {
           
            var result = new Dictionary<string, object>();

            PropertyInfo[] properties = (obj).GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj, null);
                string key = property.Name;

                if (property.PropertyType.FullName.Contains("ReadOnlyCollection")) 
                    continue;
                if (property.PropertyType.FullName.Contains("ICollection"))
                    continue;

               
                result.Add(key, value);
               
            }


            return result;

        }

        /// <summary>
        /// Lists the properties.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static List<Tuple<string,Type,object>> ListProperties(object obj)
        {

            var result = new List<Tuple<string, Type, object>>();

            PropertyInfo[] properties = (obj).GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj, null);
                string key = property.Name;

                if (property.PropertyType.FullName.Contains("ReadOnlyCollection"))
                    continue;
                if (property.PropertyType.FullName.Contains("ICollection"))
                    continue;


                result.Add(new Tuple<string, Type, object>(key, property.PropertyType,value));

            }

            return result;

        }

        public static Dictionary<string, Type> ListPropertiesType(object obj)
        {
            
            var result = new Dictionary<string, Type>();

            PropertyInfo[] properties = (obj).GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
              
                string key = property.Name;
               

                if (property.PropertyType.FullName.Contains("ReadOnlyCollection"))
                    continue;
                if (property.PropertyType.FullName.Contains("ICollection"))
                    continue;
                
               
                result.Add(key, property.PropertyType);
            }
           
            return result;

        }

        public static void SetValue(object entity,string propertyname, object value)
        {
            //PropertyInfo propertyInfo = entity.GetType().GetProperty(propertyname);


            var property = entity.GetType().GetProperty(propertyname);
            if (property != null)
            {
                Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue = null;

                if (value != null)
                    if (value.ToString().Trim() == "")
                    {
                        if (t.GetType() == typeof(Int32))
                            safeValue = 0;
                    }
                    else                     
                        safeValue = Convert.ChangeType(value, t);

                property.SetValue(entity, safeValue, null);
            }

            
            
        }

        public static bool Compare<T>(T Object1, T object2)
        {
            //Get the type of the object
            Type type = typeof(T);

            //return false if any of the object is false
            if (object.Equals(Object1, default(T)) || object.Equals(object2, default(T)))
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                if (property.Name != "ExtensionData")
                {
                    string Object1Value = string.Empty;
                    string Object2Value = string.Empty;
                    if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    if (Object1Value.Trim() != Object2Value.Trim())
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public static Type GetType(Type it)
        {
            Type columnType = Nullable.GetUnderlyingType(it) ?? it;
            return columnType;
        }

        public static string GetName<T>(T myInput) where T : class
        {
            if (myInput == null)
                return string.Empty;

            return typeof(T).Name;
        }

        public static List<Type> ListClass(string assemblypath)
        {
            var result = new List<Type>();
            Assembly assembly = Assembly.LoadFile(assemblypath);
            foreach (var type in assembly.GetTypes())
            {
                var a = type.GetTypeInfo().DeclaredProperties;
                result.Add(type);
            }

            return result;

        }

        //public static Dictionary<string, Type> ListProperties<T>() where T : class
        //{
            
        //    //var dd = obj.GetType().GetTypeInfo().DeclaredProperties;

        //    //var properties = obj.GetType().GetProperties();
        //    //PropertyInfo[] aaa = obj.GetType().GetProperties();
        //    return null;
        //}



        


        public static T CloneObj<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void CloneObj<T>(T source,ref T destination)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, destination))
            {
                return;
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                destination = (T)formatter.Deserialize(stream);
            }
        }

        public static void CloneObj<T>(T source, T destination)
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPI in destinationProperties)
            {
                PropertyInfo sourcePI = source.GetType().GetProperty(destinationPI.Name);

                destinationPI.SetValue(destination,
                                       sourcePI.GetValue(source, null),
                                       null);
            }

        }
    }
}
