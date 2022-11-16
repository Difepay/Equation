using System;
using System.Collections.Generic;
using System.Linq;


namespace Equations
{
    internal class Polynomial
    {
        // Lists of the monomials
        private List<Monomial> equation;
        private List<Monomial> currEquation;

        // List of Tuples of solutions. (first Tuple value - solution, second - string with math expression)
        private List<Tuple<double, string>> solutions;
        private uint length;
        private uint highestPower;


        // Constructors
        // Copy constructor. (Deep copy)
        public Polynomial(Polynomial pol)
        {
            this.length = pol.length;
            this.highestPower = pol.highestPower;

            this.equation = new List<Monomial>((int)this.length);
            foreach (var monomial in pol.equation)
                this.equation.Add(new Monomial(monomial));

            this.currEquation = new List<Monomial>((int)this.length);
            foreach (var monomial in pol.currEquation)
                this.currEquation.Add(new Monomial(monomial));
            
            this.solutions = new List<Tuple<double, string>>(pol.solutions.Count);
            foreach (var solution in pol.solutions)
                this.solutions.Add(new Tuple<double, string>(solution.Item1, solution.Item2));
        }


        // Default constructor
        public Polynomial(string equation)
        {
            equation = equation.Replace("-", "+-");

            string[] monomials = equation.Split('+');                   // All string monomials
            List<string> clearList = monomials.ToList();
            clearList.RemoveAll(s => s == String.Empty || s == null);
            monomials = clearList.ToArray();

            this.length = (uint)monomials.Length;
            this.equation = new List<Monomial>(monomials.Length);

            char name = Program.FindName(equation);                     // Finds our variable

            for (int i = 0; i < this.length; ++i)
            {
                uint power = 0;
                double factor = 0;

                if (monomials[i].StartsWith("x") || monomials[i].StartsWith("+x"))
                    factor = 1;
                else if (monomials[i].StartsWith("-x"))
                    factor = -1;

                if (!monomials[i].Contains('^'))
                {
                    if (monomials[i].Contains(name))
                    {
                        power = 1;
                        factor = factor == 0 ? double.Parse(monomials[i].Substring(0, monomials[i].IndexOf(name))) : factor;
                    }
                    else
                    {
                        power = 0;
                        factor = double.Parse(monomials[i]);
                    }
                }
                else
                {
                    string[] monComp = monomials[i].Split('^');
                    factor = factor == 0 ? double.Parse(monComp[0].Substring(0, monComp[0].Length - 1)) : factor;
                    power = uint.Parse(monComp[1]);
                }

                Monomial item = new Monomial(factor, power, name);
                this.equation.Add(item);
            }
            this.currEquation = new List<Monomial>((int)this.length);

            for (int i = 0; i < this.length; ++i)
                this.currEquation.Add(new Monomial(this.equation[i]));

            this.highestPower = this.length > 0 ? this.equation[0].GetPower() : 0;
            this.solutions = new List<Tuple<double, string>>();
        }


        // Methods
        public void Print()
        {
            foreach (Monomial mon1 in this.equation)
                mon1.Print();
            Console.WriteLine();
        }

        
        public void Sort()
        {
            int n = this.equation.Count;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(this.equation, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                var tmp = this.equation[0];
                this.equation[0] = this.equation[i];
                this.equation[i] = tmp;

                Heapify(this.equation, i, 0);
            }
        }


        public void Insert()
        {
            for (int i = 0; i < this.length - 1; ++i)
            {
                if (this.equation[i].GetPower() == this.equation[i + 1].GetPower())
                {
                    this.equation[i] = (this.equation[i] + this.equation[i + 1])[0];
                    --this.length;
                    this.equation.RemoveAt(i + 1);
                }
            }
            this.equation[0].SetHead(true);
        }
    
        
        public void GetSimpleSolution()
        {
            double evenPower = 0, oddPower = 0;

            foreach (var item in this.equation)
            {
                if (item.GetPower() % 2 == 0)
                    evenPower += item.GetFactor();
                else
                    oddPower += item.GetFactor();
            }

            uint lastMonomialPower = this.equation.Last().GetPower();
            if (lastMonomialPower != 0)
            {
                solutions.Add(new Tuple<double, string>(0, lastMonomialPower == 1 ? "x" : $"x^{lastMonomialPower}"));
                this.DivideByPolinomial(0);
            }

            if (evenPower + oddPower == 0)
            {
                solutions.Add(new Tuple<double, string>(1, "(x - 1)"));
                this.DivideByPolinomial(1);
            }

            if  (evenPower == oddPower)
            {
                solutions.Add(new Tuple<double, string>(-1, "(x + 1)"));
                this.DivideByPolinomial(-1);
            }
        }


        public double GetValue(double x)
        {
            double sum = 0;

            foreach (var item in this.equation)
                sum += item.GetFactor() * Math.Pow(x, item.GetPower());
 
            return sum;
        }


        public void PrintSolutions()
        {
            foreach (var solution in this.solutions)
                Console.Write(solution.Item2);
            Console.WriteLine("(...)");
        }


        // Helper methods
        private void Heapify(List<Monomial> list, int n, int i)
        {
            int smallest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && list[l] < list[smallest])
                smallest = l;

            if (r < n && list[r] < list[smallest])
                smallest = r;

            if (smallest != i)
            {
                var tmp = list[i];
                list[i] = list[smallest];
                list[smallest] = tmp;

                Heapify(list, n, smallest);
            }
        }

        private public void DivideByPolinomial(double solution)
        {
            if (solution == 0)
            {
                uint minPower = this.currEquation.Last().GetPower();

                for (int i = 0; i < this.length; i++)
                    this.currEquation[i].SetPower(this.currEquation[i].GetPower() - minPower);
            }
        }


        // Getters
        public List<Monomial> GetCurrEquation() { return this.currEquation; }

        public List<Monomial> GetEquation() { return this.equation; }

        public List<Tuple<double, string>> GetSolutions() { return this.solutions; }

        public uint GetLength() { return this.length; }

        public uint GetHighestPower() { return this.highestPower; }
    }
}
