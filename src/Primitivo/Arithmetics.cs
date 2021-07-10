using System;
using System.Numerics;

namespace Primitivo
{
    /// <summary>
    /// Provides basic arithmetic functions for unsigned integers.
    /// </summary>
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

        // Stein's GCD algorithm using textbook approach.
        // https://xlinux.nist.gov/dads/HTML/binaryGCD.html
        public static ulong TextbookBinaryGcd(ulong a, ulong b)
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

            // Make up for GCD halving.
            return b * g;
        }

        // Stein's GCD algorithm, compact version.
        // See textbook version for additional explanations.
        public static ulong BinaryGcd(ulong a, ulong b)
        {
            // gcd(a, 0) = gcd(0, a) = a.
            if (a == 0 || b == 0) return a | b;

            // 2 as a common divisor count. Makes up for
            // factoring out 2 x times: shift = 2^x.
            int shift = BitOperations.TrailingZeroCount(a | b);
            // Make b odd. It will remain odd.
            b >>= BitOperations.TrailingZeroCount(b);

            while (a > 0)
            {
                a >>= BitOperations.TrailingZeroCount(a);
                if (a < b) (a, b) = (b, a);
                a -= b;
            }

            return b << shift;
        }

        /// <summary>
        /// Computes the GCD of two unsigned integers.
        /// </summary>
        /// <param name="left">First value.</param>
        /// <param name="right">Second value.</param>
        /// <returns>The GCD of left and right.</returns>
        public static ulong Gcd(ulong left, ulong right)
        {
            return right > left ? BinaryGcd(right, left) : BinaryGcd(left, right);
        }

        /// <summary>
        /// Computes the LCM of two unsigned integers.
        /// </summary>
        /// <param name="left">First value.</param>
        /// <param name="right">Second value.</param>
        /// <returns>The LCM of left and right.</returns>
        /// <exception cref="OverflowException">
        /// If the LCM of <paramref name="left" /> and <paramref name="right" /> is larger than 64bits.
        /// </exception>
        /// <remarks>Returns 0 if either parameter or both are 0.</remarks>
        public static ulong Lcm(ulong left, ulong right)
        {
            if (left == 0 && right == 0) return 0;
            // Dividing first prevents some overflows.
            ulong lcm = left / Gcd(left, right) * right;
            if (lcm < left || lcm < right) throw new OverflowException("LCM was larger than 64bits.");
            return lcm;
        }
    }
}