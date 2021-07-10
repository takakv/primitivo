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

        // Stein's GCD algorithm.
        public static ulong BinaryGcd(ulong a, ulong b)
        {
            // gcd(a, 0) = gcd(0, a) = a.
            if (a == 0 || b == 0) return a | b;

            // 2 as a common divisor count.
            // Two ways to count it:
            //  - g = 2^(count) | start with g = 1, then double, return b * g,
            //  - 2^g | start with g = 0, then increment, return b * 2^g.
            // Return values assume that a is being decremented.
            ulong g = 1;

            // This part alters the GCD by halving it each iteration.
            // gcd(2a, 2b) = 2 * gcd(a, b) for even a, b.
            while ((a & 1) == 0 && (b & 1) == 0)
            {
                a >>= 1;
                b >>= 1;
                g <<= 1;
            }

            // By now, a or b (or both) are odd and it stays this way.
            // The GCD is thus unaffected.
            while (a > 0)
            {
                // gcd(a, b) = gcd(a/2, b) for a even, b odd.
                if ((a & 1) == 0) a >>= 1;
                // gcd(a, b) = gcd(a, b/2) for b even, a odd.
                else if ((b & 1) == 0) b >>= 1;
                // For a and b both odd, a <= b, gcd(a, b) = gcd((a-b)/2, b).
                else
                {
                    // Swap early to avoid |a - b|.
                    if (a < b) (a, b) = (b, a);

                    // Both a and b are odd.
                    a = (a - b) >> 1;
                }
            }

            return b * g;
        }
    }
}