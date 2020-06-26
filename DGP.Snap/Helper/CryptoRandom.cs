using System;
using System.Security.Cryptography;

namespace DGP.Snap.Helper
{
    public class CryptoRandom : RandomNumberGenerator
    {
        private static RandomNumberGenerator r;
        /// <summary>
        /// 创建可用于生成随机数据的密码随机数生成器默认实现的实例
        /// </summary>
        public CryptoRandom()
        {
            r = Create();
        }
        /// <summary>
        /// 用随机数填充指定字节数组的元素
        /// </summary>
        /// <param name="buffer">包含随机数的字节数组</param>
        public override void GetBytes(byte[] buffer)
        {
            r.GetBytes(buffer);
        }
        /// <summary>
        /// 返回介于0.0和1.0之间的随机数
        /// </summary>
        /// <returns></returns>
        public double NextDouble()
        {
            byte[] b = new byte[4];
            r.GetBytes(b);
            return (double)BitConverter.ToUInt32(b, 0) / uint.MaxValue;
        }
        /// <summary>
        /// 返回指定范围内的随机数
        /// </summary>
        /// <param name="minPValue">随机数（含）下限</param>
        /// <param name="maxPValue">随机数的（不含）上限，maxValue必须大于或等于minValue。</param>
        /// <returns></returns>
        public int Next(int minPValue, int maxPValue)
        {
            return (int)Math.Round(NextDouble() * (maxPValue - minPValue - 1)) + minPValue;
        }
        /// <summary>
        /// 返回一个非负的随机数
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return Next(0, int.MaxValue);
        }
        /// <summary>
        /// 返回小于指定最大值的非负随机数
        /// </summary>
        /// <param name="maxValue">返回的随机数的（含）上限，maxValue必须大于或等于0</param>
        /// <returns></returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }
    }

}

