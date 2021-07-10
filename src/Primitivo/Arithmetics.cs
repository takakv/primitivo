namespace Primitivo
{
    public class Arithmetics
    {
        // Euclid's GCD algorithm using subtraction.
        // Assumes the left-hand parameter to be the larger one.
        // No checks!
        public static ulong EuclidGcd(ulong a, ulong b)
        {
            if (b == 0) return a;

            while (a != b)
            {
                if (a > b) a -= b;
                else b -= a;
            }

            return a;
        }
        
        // Euclid's GCD algorithm using Euclidean division.
        // Since the input parameters can only be positive,
        // there are no differences between the least positive remainder (LPR)
        // and least absolute remainder (LAR) versions.
        // Assumes the left-hand parameter to be the larger one.
        // No checks!
        public static ulong EuclideanGcd(ulong a, ulong b)
        {
            while (b != 0)
            {
                ulong tmp = a % b;
                a = b;
                b = tmp;
            }

            return a;
        }
    }
}