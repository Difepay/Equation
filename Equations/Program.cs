using System;

namespace Equations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the equation: ");
            string equation = GetEquation();

            if (!StringValid(equation))
            {
                Console.WriteLine("Your equation is incorrect!");
                Environment.Exit(0);
            }

            Polynomial polynomial = new Polynomial(equation);

            polynomial.Print();

            polynomial.Sort();
            Console.WriteLine("After sort:");
            polynomial.Print();

            polynomial.Insert();
            Console.WriteLine("After inserting:");
            polynomial.Print();

            polynomial.GetSimpleSolution();
            Console.WriteLine("After calculas:");
            polynomial.Print();

            Console.WriteLine("Monomial: ");
            polynomial.PrintSolutions();


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

        public static string GetEquation()
        {
            return Console.ReadLine().ToLower().Trim().Replace(" ", "").Replace(',', '.').Replace("**", "^");
        }
    }
}
