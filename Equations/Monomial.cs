using System;


namespace Equations
{
    internal class Monomial
    {
        private double factor;
        private uint power;
        private char name;
        private bool head;


        // Constructors

        // Default constructor
        public Monomial()
        {
            this.factor = 0;
            this.power = 0;
            this.name = '\0';
            this.head = false;
        }

        // Parameter constructor
        public Monomial(double factor, uint power, char name)
        {
            this.factor = factor;
            this.power = power;
            this.name = name;
            this.head = false;
        }
        
        // Copy constructor
        public Monomial(Monomial a)
        {
            this.factor = a.factor;
            this.power = a.power;
            this.name = a.name;
        }


        // Methods
        public void Print()
        {
            string factorStr = "";
            if (this.head)
                factorStr = this.factor < 0 ? "-" : "";
            else
                factorStr = this.factor > 0 ? $"+ " : $"- ";

            if (Math.Abs(this.factor) != 1)
                factorStr += $"{Math.Abs(this.factor)}";

            if (this.power == 0)
                Console.Write(factorStr);
            else if (this.power == 1)
                Console.Write($"{factorStr}{this.name} ");
            else
                Console.Write($"{factorStr}{this.name}^{this.power} ");
        }


        // Getters
        public uint GetPower() { return this.power; }

        public double GetFactor() { return this.factor; }

        public char GetName() { return this.name; }


        // Setters
        public void SetPower(uint power) { this.power = power; }

        public void SetFactor(double factor) { this.factor = factor; }

        public void SetName(char name) { this.name = name; }

        public void SetHead(bool head) { this.head = head; }


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
