using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExecutionContextCapture
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Only one instance of AsyncLocal
            var asyncLocal = new AsyncLocal<string>();

            var ecResult = await new Program().Method1(asyncLocal);
            var ec = ExecutionContext.Capture();

            Console.WriteLine($"Key Value [{asyncLocal.Value}] should be null");

            // Use ExecutionContext.Capture and .Run to manipulate the values of AsyncLocal
            // Only one instance of AsyncLocal - Lots of different values
            // Because AsyncLocal is just a tunnel to what's stored in ExecutionContext

            ExecutionContext.Run(ecResult.method1, o =>
            {
                Console.WriteLine($"Key Value [{asyncLocal.Value}] should be Method1");
            }, null);

            ExecutionContext.Run(ecResult.method2, o =>
            {
                Console.WriteLine($"Key Value [{asyncLocal.Value}] should be Method2");
            }, null);

            ExecutionContext.Run(ecResult.method3, o =>
            {
                Console.WriteLine($"Key Value [{asyncLocal.Value}] should be Method3");
            }, null);

            ExecutionContext.Run(ec, o =>
            {
                Console.WriteLine($"Key Value [{asyncLocal.Value}] should be null");
            }, null);

            Console.ReadKey();

        }


        async Task<(ExecutionContext method1, ExecutionContext method2, ExecutionContext method3)> Method1(AsyncLocal<string> al)
        {
            al.Value = "Method1";
            var ec1 = ExecutionContext.Capture();
            var ec2_3 = await Method2(al);
            Console.WriteLine($"Key Value [{al.Value}] should be Method1");
            return (ec1, ec2_3.ec2, ec2_3.ec3);
        }

        async Task<(ExecutionContext ec2, ExecutionContext ec3)> Method2(AsyncLocal<string> al)
        {
            al.Value = "Method2";
            var ec2 = ExecutionContext.Capture();
            var ec3 = await Method3(al);
            Console.WriteLine($"Key Value [{al.Value}] should be Method2");
            return (ec2, ec3);
        }

        async Task<ExecutionContext> Method3(AsyncLocal<string> al)
        {
            al.Value = "Method3";
            var ec3 = ExecutionContext.Capture();
            await Task.Delay(10);
            Console.WriteLine($"Key Value [{al.Value}] should be Method3");
            return ec3;

        }
    }
}
