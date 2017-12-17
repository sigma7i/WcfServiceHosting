using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ServiceClient.ServiceTopshelfReference;
using ServiceClient.WcfWithoutConfig;

namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWpfServices();
            ParallelWorkingTest();
            ThrowServiceExeption();

            Console.ReadKey();
        }

        private static void ThrowServiceExeption()
        {
            var topShelfService = new CalculatorClient();

            try
            {
                // topShelfService can not be reuse
                topShelfService.DoAnyExeption();
            }
            catch (Exception e)
            {
            }

            var secondClient = new CalculatorClient();
            var result = secondClient.Divide(4, 2);

            try
            {
                // Sevice crush
                var thirdClient = new CalculatorClient();
                topShelfService.DoStackOverflow();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private static void TestWpfServices()
        {
            var serv = new ServiceReference.CalculatorClient();

            var result = serv.AddAsync(3, 4).Result;
            Console.WriteLine(result);

            var servTopShelf = new CalculatorClient();
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
