using System;
using System.IO;
using System.Threading.Tasks;

namespace DGP.Snap.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadKey();
        }

        public interface IAaa
        {
            void Init();
        }

        public class Aaa : IAaa
        {
            public async void Init()
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                throw new NotImplementedException();
            }
        }
    }
}
