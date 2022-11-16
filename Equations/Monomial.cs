using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equations
{
    internal class Monomial
    {
        public double factor;
        public uint power;
        public char name;

        // Constructors
        public Monomial()
        {
            this.factor = 0;
            this.power = 0;
            this.name = '\0';
        }
        public Monomial(double factor, uint power, char name)
        {
            this.factor = factor;
            this.power = power;
            this.name = name;
        }

        public Monomial(Monomial a)
        {
            this.factor = a.factor;
            this.power = a.power;
            this.name = a.name;
        }


        // Methods
        public void Print()
        {
            string factorStr = this.factor > 0 ? $"+ {this.factor}" : $"- {-this.factor}";
            if (this.power == 0)
                Console.Write(factorStr);
            else if (this.power == 1)
                Console.Write($"{factorStr}{this.name} ");
            else
                Console.Write($"{factorStr}{this.name}^{this.power} ");
        }


        // Operators
        public static Monomial[] operator +(Monomial m1, Monomial m2)
        {
            if (m1.power == m2.power)
                return new Monomial[] { new Monomial(m1.factor + m2.factor, m1.power, m1.name) };
            else
                return new Monomial[] { m1, m2 };
        }

        public static bool operator >(Monomial m1, Monomial m2)
        {
            return m1.power > m2.power;
        }

        public static bool operator <(Monomial m1, Monomial m2)
        {
            return m1.power < m2.power;
        }


    }
}
