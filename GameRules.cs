using System.Security.Cryptography;

namespace Task3
{
    public static class GameRules
    {
        public static void DetermineWinner(int userMove, int computerMove, string[] moves)
        {
            var halfMoves = moves.Length / 2;
            var distance = (userMove - computerMove + moves.Length) % moves.Length;

            if (distance == 0)
                Console.WriteLine("It's a draw!");
            else if (distance <= halfMoves)
                Console.WriteLine("You win!");
            else
                Console.WriteLine("Computer win!");
        }
    }
}