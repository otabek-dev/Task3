using System.Security.Cryptography;

namespace Task3
{
    public class CryptoGenerator
    {
        public static byte[] GenerateKey(int length)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                var key = new byte[length / 8];
                rng.GetBytes(key);
                return key;
            }
        }

        public static string GenerateHMAC(string move, byte[] key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                var moveBytes = System.Text.Encoding.UTF8.GetBytes(move);
                var hash = hmac.ComputeHash(moveBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }

}