using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using ServiceClient.WcfWithoutConfig;

namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWpfServices();
            ParallelWorkingTest();

            Console.ReadKey();
        }

        private static void TestWpfServices()
        {
            var serv = new ServiceReference.CalculatorClient();

            var result = serv.AddAsync(3, 4).Result;
            Console.WriteLine(result);

            var servTopShelf = new ServiceTopshelfReference.CalculatorClient();
            var resultTopShelf = servTopShelf.Multiply(2.5, 5);
            Console.WriteLine($"TopSelf {resultTopShelf}");

            var servNoConfig = new MyServiceClient();
            var stringResult = servNoConfig.ConvertString(Environment.SystemDirectory);

            Console.WriteLine($"To Upper {stringResult}");
        }

        private static void ParallelWorkingTest()
        {
            var servNoConfig = new MyServiceClient();
            var watch = new Stopwatch();

            watch.Start();
            Parallel.For(0, 5, (i, loop) =>
            {
                var dowork = servNoConfig.DoLongWork10Second(2, 9);

                if (dowork != 11)
                    Console.WriteLine(dowork + " " + i);
                else
                {
                    Console.WriteLine($"{i} завершился успешно");
                }
            });

            watch.Stop();
            Console.WriteLine(watch.Elapsed.Seconds + "секунд");
        }
    }
}
