using System.Security.Cryptography;

namespace Task3
{
    public class Game
    {
        private string[] moves;
        private byte[] key;
        private View view;

        public Game(string[] moves)
        {
            this.moves = moves;
            this.key = CryptoGenerator.GenerateKey(256);
            this.view = new View(moves);
        }

        public void Start()
        {
            var computerMove = GenerateComputerMove();
            var hmac = CryptoGenerator.GenerateHMAC(moves[computerMove], key);

            Console.WriteLine($"HMAC: {hmac}");
            view.PrintMenu();
            
            var userMove = GetUserMove();

            Console.WriteLine($"Your move: {moves[userMove]}");
            Console.WriteLine($"Computer move: {moves[computerMove]}");
            Console.WriteLine($"Key: {BitConverter.ToString(key).Replace("-", "")}");

            var result = GameRules.DetermineWinner(userMove, computerMove, moves);
            View.PrintResult(result);
        }

        private int GetUserMove()
        {
            int userMove;
            while (true)
            {
                Console.Write("Enter your move: ");

                var tryParse = Console.ReadLine();

                if (int.TryParse(tryParse, out userMove) && userMove >= 0 && userMove <= moves.Length)
                    break;

                if (tryParse == "?")
                    view.PrintTable();
                else
                    Console.WriteLine("Invalid input. Please enter a valid move number.");
            }
            return userMove - 1;
        }

        private int GenerateComputerMove()
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