using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serv = new ServiceReference.CalculatorClient();

            var result = serv.AddAsync(3, 4).Result;
            Console.WriteLine(result);

            var servTopShelf = new ServiceTopshelfReference.CalculatorClient();
            var resultTopShelf = servTopShelf.Multiply(2.5, 5);
            Console.WriteLine($"TopSelf {resultTopShelf}");

            var servNoConfig = new WcfWithoutConfig.MyServiceClient();
            var stringResult = servNoConfig.ConvertString(Environment.SystemDirectory);

            Console.WriteLine($"To Upper {stringResult}");
        }
    }
}
