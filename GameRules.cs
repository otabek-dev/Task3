using System.Security.Cryptography;

namespace Task3
{
    public static class GameRules
    {
        public static GameResult DetermineWinner(int userMove, int computerMove, string[] moves)
        {
            var halfMoves = moves.Length / 2;
            var distance = (userMove - computerMove + moves.Length) % moves.Length;

            if (distance == 0)
                return GameResult.Draw;
            else if (distance <= halfMoves)
                return GameResult.YouWin;
            else
                return GameResult.ComputerWin;
        }
    }
}