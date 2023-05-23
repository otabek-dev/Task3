using System.Security.Cryptography;

namespace Task3
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3 || args.Length % 2 == 0)
            {
                Console.WriteLine("Invalid arguments.\nPlease restart the program with valid arguments!");
                return;
            }
            var game = new Game(args);
            game.Start();

            Console.ReadKey();
            //args.Max(x => x.Length);
        }
    }

    public class Game
    {
        private string[] moves;
        private byte[] key;

        public Game(string[] moves)
        {
            this.moves = moves;
            this.key = CryptoGenerator.GenerateKey(256);
        }

        public void Start()
        {
            var computerMove = GenerateComputerMove();
            var hmac = CryptoGenerator.GenerateHMAC(moves[computerMove], key);

            Console.WriteLine($"HMAC: {hmac}");
            PrintMenu();
            
            var userMove = GetUserMove();

            Console.WriteLine($"Your move: {moves[userMove]}");
            Console.WriteLine($"Computer move: {moves[computerMove]}");
            Console.WriteLine($"Key: {BitConverter.ToString(key).Replace("-", "")}");

            GameRules.DetermineWinner(userMove, computerMove, moves);
        }

        private void PrintMenu()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Length; i++)
                Console.WriteLine($"{i + 1} - {moves[i]}");

            Console.WriteLine("0 - Exit");
            Console.WriteLine("? - help");
        }

        public int GetUserMove()
        {
            int userMove;
            while (true)
            {
                Console.Write("Enter your move: ");

                var tryParse = Console.ReadLine();

                if (int.TryParse(tryParse, out userMove) && userMove >= 0 && userMove <= moves.Length)
                    break;

                Console.WriteLine("Invalid input. Please enter a valid move number.");
            }
            return userMove - 1;
        }

        public int GenerateComputerMove()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[1];
                rng.GetBytes(randomNumber);
                return randomNumber[0] % moves.Length;
            }
        }
    }
}