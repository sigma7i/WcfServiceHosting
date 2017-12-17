using System;

namespace ServiceTopshelf
{
    // Implement the ICalculator service contract in a service class.
    public class CalculatorService : ICalculator
    {
        // Implement the ICalculator methods.
        public double Add(double n1, double n2)
        {
            double result = n1 + n2;
            return result;
        }

        public double Subtract(double n1, double n2)
        {
            double result = n1 - n2;
            return result;
        }

        public double Multiply(double n1, double n2)
        {
            double result = n1 * n2;
            return result;
        }

        public double Divide(double n1, double n2)
        {
            double result = n1 / n2;
            return result;
        }

        public void DoStackOverflow()
        {
            DoStackOverflow();
        }

        public void DoAnyExeption()
        {
            throw new Exception("Simple example throws exeption");
        }
    }
}
