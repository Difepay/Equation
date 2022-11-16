using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equations
{
    internal class Polynomial
    {
        private List<Monomial> firstEquation;
        private List<Monomial> equation;
        private List<Tuple<double, string>> solutions;
        private uint length;
        private uint highestPower;

        // Constructors
        public Polynomial(Polynomial pol)
        {
            this.firstEquation = pol.equation;
            this.equation = pol.equation;
            this.length = pol.length;
            this.solutions = pol.solutions; 
            this.highestPower = pol.highestPower;
        }

        public Polynomial(string equation)
        {
            equation = equation.Replace("-", "+-");
            string[] monomials = equation.Split('+');

            this.length = (uint)monomials.Length;
            this.equation = new List<Monomial>(monomials.Length);

            char name = Program.FindName(equation);

            for (int i = 0; i < this.length; ++i)
            {
                uint power = 0;
                double factor = 0;

                if (!monomials[i].Contains('^'))
                {
                    if (monomials[i].Contains(name))
                    {
                        power = 1;

                        if (monomials[i].Length == 1)
                            factor = 1;
                        else if (monomials[i] == "-x")
                            factor = -1;
                        else
                            factor = double.Parse(monomials[i].Substring(0, monomials[i].IndexOf(name)));
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

                    if (monComp[0].Length == 1)
                        factor = 1;
                    else if (monComp[0] == "-x")
                        factor = -1;
                    else
                        factor = double.Parse(monComp[0].Substring(0, monComp[0].Length - 1));
                    power = uint.Parse(monComp[1]);
                }

                Monomial item = new Monomial(factor, power, name);
                this.equation.Add(item);
            }
            this.firstEquation = new List<Monomial>((int)this.length);

            for (int i = 0; i < this.length; ++i)
                this.firstEquation[i] = new Monomial(this.equation[i]);

            this.highestPower = this.length > 0 ? this.equation[0].power : 0;
            this.solutions = new List<Tuple<double, string>>();
        }


        // Methods
        public void Print()
        {
            foreach (Monomial mon1 in this.equation)
                mon1.Print();
        }

        
        public void Sort()
        {
            uint n = this.length - 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    if (this.equation[j] < this.equation[j + 1])
                    {
                        var tmp = this.equation[j];
                        this.equation[j] = this.equation[j + 1];
                        this.equation[j + 1] = tmp;
                    }
                }
            }
        }

        
        public void Insert()
        {
            for (int i = 0; i < this.length - 1; ++i)
            {
                if (this.equation[i].power == this.equation[i + 1].power)
                {
                    this.equation[i] = (this.equation[i] + this.equation[i + 1])[0];
                    --this.length;
                    this.equation.RemoveAt(i + 1);
                }
            }
        }
    
        
        public void GetSimpleSolution()
        {
            double evenPower = 0, oddPower = 0;

            foreach (var item in this.equation)
            {
                if (item.power % 2 == 0)
                    evenPower += item.factor;
                else
                    oddPower += item.factor;
            }

            if (evenPower + oddPower == 0)
                solutions.Add(new Tuple<double, string>(1, "(x - 1)"));
            if  (evenPower == oddPower)
                solutions.Add(new Tuple<double, string>(-1, "(x + 1)"));

            if (this.equation.Last().power != 0)
                solutions.Add(new Tuple<double, string>(0, "x"));
        }


        public void DivideByPolinomial(double solution)
        {
            // this.equation(solution) = 0
            if (solution == 0)
            {
                uint minPower = this.equation.Last().power;

                for (int i = 0; i < this.length; i++)
                    this.equation[i].power -= minPower;
            }
        }


        // Getters
        public List<Monomial> GetfirstEquation() { return this.firstEquation; }

        public List<Monomial> GetEquation() { return this.equation; }

        public List<Tuple<double, string>> GetSolutions() { return this.solutions; }

        public uint GetLength() { return this.length; }

        public uint GetHighestPower() { return this.highestPower; }
    }
}
