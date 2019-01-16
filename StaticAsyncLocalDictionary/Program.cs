using System;
using System.Threading.Tasks;

namespace StaticAsyncLocalDictionary
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await new Program().Method1();
            Console.WriteLine($"Key Value [{AsyncLocalDictionary.GetLogicalValue<string>(key)}] should be null");
            Console.ReadKey();
        }

        static string key = "key";
        async Task Method1()
        {
            AsyncLocalDictionary.SetLogicalValue(key, "Method1");
            await Method2();
            Console.WriteLine($"Key Value [{AsyncLocalDictionary.GetLogicalValue<string>(key)}] should be Method1");
        }

        async Task Method2()
        {
            AsyncLocalDictionary.SetLogicalValue(key, "Method2");
            await Method3();
            Console.WriteLine($"Key Value [{AsyncLocalDictionary.GetLogicalValue<string>(key)}] should be Method2");
        }

        async Task Method3()
        {
            AsyncLocalDictionary.SetLogicalValue(key, "Method3");
            await Task.Delay(10);
            Console.WriteLine($"Key Value [{AsyncLocalDictionary.GetLogicalValue<string>(key)}] should be Method3");

        }
    }
}
