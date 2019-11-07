using System;
using System.Security.Cryptography;

namespace DGP.Snap.Helper
{
    public class CryptoRandom : RandomNumberGenerator
    {
        private static RandomNumberGenerator r;
        /// <summary>
        /// Creates an instance of the default implementation of a cryptographic random number generator that can be used to generate random data.
        /// </summary>
        public CryptoRandom()
        {
            r = RandomNumberGenerator.Create();
        }
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public override void GetBytes(byte[] buffer)
        {
            r.GetBytes(buffer);
        }
        /// <summary>
        /// Returns a random number between 0.0 and 1.0
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            byte[] b = new byte[4];
            r.GetBytes(b);
            return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
        }
        /// <summary>
        /// Returns a random number within the specified range.
        /// </summary>
        /// <param name="minPValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxPValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns></returns>
        public int Next(int minPValue, int maxPValue)
        {
            return (int)Math.Round(NextDouble() * (maxPValue - minPValue - 1)) + minPValue;
        }
        /// <summary>
        /// Returns a nonnegative random number
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return Next(0, Int32.MaxValue);
        }
        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum
        /// </summary>
        /// <param name="maxValue">The inclusive upper bound of the random number returned. maxValue must be greater than or equal 0</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }
    }

}

