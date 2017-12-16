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
        }
    }
}
