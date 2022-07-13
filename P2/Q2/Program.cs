using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// testCase40 sqrt(ln(exp(5.6 ^ 2) + 9.73 - 6 * 7)) //out 5.60
// testCase41 ln(exp(4.0)) //out 4.00
// testCase42 exp(ln(4)) //out 4.00
// testCase43 exp(sin(53) + (cos(3) / 2)) // 0.90
// testCase44 sqrt(8) ^ (2 / 3) //2.00
namespace Q2
{

    public enum elementType
    {
        Number,
        Operator,
        Function
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); // it must be splitted by space
            Calculator calculator = new Calculator(input);
            try
            {
                double answer = calculator.Evaluate();
                System.Console.WriteLine(String.Format("{0:0.00}", answer));
            }
            catch
            {
                System.Console.WriteLine("INVALID");
            }
        }
    }
}

