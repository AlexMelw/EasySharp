namespace EasySharp.NHelpers.Utils.Cryptography
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;

    public static class RNGUtil
    {
        /// <summary>Returns a random integer that is within a specified range.</summary>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">
        ///     The exclusive upper bound of the random number returned. <paramref name="max" /> must be greater than or equal to
        ///     <paramref name="min" />.
        /// </param>
        /// <returns>
        ///     A 32-bit signed integer greater than or equal to <paramref name="min" /> and less than <paramref name="min" />;
        ///     that is, the range of return values includes <paramref name="min" /> but not <paramref name="max" />. If
        ///     <paramref name="min" /> equals <paramref name="max" />, <paramref name="min" /> is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="min" /> is greater than <paramref name="max" />.</exception>
        public static int Next(int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));

            if (min == max) return min;

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[4];
                rng.GetBytes(data);

                int generatedValue = Math.Abs(BitConverter.ToInt32(data, 0));

                int diff = max - min;
                int mod = generatedValue % diff;
                int normalizedNumber = mod + min;

                return normalizedNumber;
            }
        }

        /// <summary>
        ///     Generates a random number.
        /// </summary>
        /// <param name="size">Size of generated random number measured in bytes.</param>
        /// <returns>Array of <see cref="byte" /> representing the generated random number.</returns>
        public static byte[] GenerateRandomNumber(int size)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumbers = new byte[size];
                randomNumberGenerator.GetBytes(randomNumbers);

                return randomNumbers;
            }
        }


        public static byte GenerateRandomByte()
        {
            byte[] generatedRandomNumber = GenerateRandomNumber(1);
            byte generatedByte = generatedRandomNumber.Single();

            return generatedByte;
        }

        public static int GenerateRandomInt()
        {
            byte[] generatedRandomNumber = GenerateRandomNumber(4);
            int generatedInt = BitConverter.ToInt32(generatedRandomNumber, 0);

            return generatedInt;
        }
    }
}