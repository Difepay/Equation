using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the equation: ");
            string equation = Console.ReadLine().ToLower().Trim().Replace(" ", "").Replace(',', '.').Replace("**", "^");

            
            if (!StringValid(equation))
            {
                Console.WriteLine("Your equation is incorrect!");
                Environment.Exit(0);
            }

            Polynomial polynomial = new Polynomial(equation);

           
            polynomial.Sort();
            polynomial.Insert();
            polynomial.Print();

            polynomial.GetSimpleSolution();
            polynomial.DivideByPolinomial(0);


            Console.ReadKey();

        }

        public static bool StringValid(string str)
        {
            if (str == null || str.Length == 0)
                return false;

            char targertChar = '\0';

            foreach (char c in str)
            {
                if (targertChar == '\0' && Char.IsLetter(c))
                    targertChar = c;

                if (!Char.IsDigit(c) && c != '^' && c != '.' && c != targertChar && c != '+' && c!= '-')
                    return false;
            }
            return targertChar != '\0';
        }

        public static char FindName(string str)
        {
            foreach (char c in str)
                if (Char.IsLetter(c))
                    return c;
            return '\0';
        }
    }
}
