using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StaticAsyncLocalDictionary
{
    public static class AsyncLocalDictionary
    {

        private static IDictionary<string, AsyncLocal<object>> asyncLocalDictionary = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static T GetLogicalValue<T>(string key)
        {
            return (T)asyncLocalDictionary[key].Value;
        }

        public static void SetLogicalValue<T>(string key, T value)
        {
            if (!asyncLocalDictionary.ContainsKey(key))
            {
                asyncLocalDictionary.Add(key, new AsyncLocal<object>());
            }

            asyncLocalDictionary[key].Value = value;

        }

    }

    

}
