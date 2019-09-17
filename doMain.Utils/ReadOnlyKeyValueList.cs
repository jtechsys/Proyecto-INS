using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="TKey">The type of the key.</typeparam>
    ///// <typeparam name="TValue">The type of the value.</typeparam>
    //public interface IReadOnlyKeyValueList<TKey,TValue>
    //{

    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <typeparam name="TKey">The type of the key.</typeparam>
    ///// <typeparam name="TValue">The type of the value.</typeparam>
    ///// <seealso cref="doMain.Utils.IReadOnlyKeyValueList{TKey, TValue}" />
    //public class ReadOnlyKeyValueList<TKey, TValue> : IReadOnlyKeyValueList<TKey, TValue>
    //{
    //    public TKey Key { get; set; }
    //    public TValue Value { get; set; }


    //}

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IKeyValue<TKey, TValue>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="doMain.Utils.IReadOnlyKeyValueList{TKey, TValue}" />
    public class KeyValue<TKey, TValue> : IKeyValue<TKey, TValue>
    {
        public KeyValue()
        {
            
        }
        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public TKey Key { get; set; }
        public TValue Value { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="doMain.Utils.IKeyValue{TKey, TValue}" />
    public class KeyValue : KeyValue<int, string>
    {

    }

}
