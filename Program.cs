using System.Security.Cryptography;

namespace Task3
{
    public class Program
    {
        private static string[] moves;
        private static byte[] key;

        static void Main(string[] args)
        {
            if (args.Length < 3 || args.Length % 2 == 0)
            {
                Console.WriteLine("Invalid arguments.\nPlease restart the program with valid arguments!");
                return;
            }

            moves = args;
            key = CryptoGenerator.GenerateKey(256);
            string hmac = CryptoGenerator.GenerateHMAC(moves[0], key);
            Console.WriteLine(hmac + $"\n{moves[0]}");
        }

    }

    public class CryptoGenerator
    {
        public static byte[] GenerateKey(int length)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[length / 8];
                rng.GetBytes(key);
                return key;
            }
        }

        public static string GenerateHMAC(string move, byte[] key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] moveBytes = System.Text.Encoding.UTF8.GetBytes(move);
                byte[] hash = hmac.ComputeHash(moveBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }

}