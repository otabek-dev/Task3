namespace Task3
{
    class View
    {
        private readonly string[,] table;
        private readonly int maxLength = 0;
        private readonly string[] moves;
        private readonly int movesCount;

        public View(string[] moves)
        {
            this.movesCount = moves.Length;
            this.table = new string[movesCount + 1, movesCount + 1];
            this.maxLength = moves.Max(x => x.Length);
            this.moves = moves;
            GenerateTable();
        }

        private void GenerateTable()
        {
            GameResult result;
            table[0, 0] = string.Empty;
            for (int i = 0; i < movesCount; i++)
            {
                table[0, i + 1] = moves[i];
                table[i + 1, 0] = moves[i];
            }

            for (int i = 1; i <= movesCount; i++)
            {
                for (int j = 1; j <= movesCount; j++)
                {
                    result = GameRules.DetermineWinner(j, i, moves);
                    if (result == GameResult.Draw)
                        table[i, j] = "Draw";
                    else if (result == GameResult.YouWin)
                        table[i, j] = "Win";
                    else if (result == GameResult.ComputerWin)
                        table[i, j] = "Lose";
                }
            }
        }

        public void PrintMenu()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Length; i++)
                Console.WriteLine($"{i + 1} - {moves[i]}");

            Console.WriteLine("0 - Exit");
            Console.WriteLine("? - help");
        }

        public static void PrintResult(GameResult result)
        {
            if (result == GameResult.Draw)
                Console.WriteLine("It's a draw!");
            else if (result == GameResult.YouWin)
                Console.WriteLine("You win!");
            else if (result == GameResult.ComputerWin)
                Console.WriteLine("Computer wins!");
        }

        public void PrintTable()
        {
            var tableSize = table.GetLength(0);
            var maxWidthLength = (maxLength * tableSize) + tableSize * 2;
            
            Console.WriteLine($"Blue line you and redline computer");
            Console.WriteLine("Rules Table:");
            
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    var widthLength = 1 + maxLength - table[i, j].Length;

                    if (i == 0 && j > 0)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    else if (i > 0 && j == 0)
                        Console.BackgroundColor = ConsoleColor.Red;

                    if (table[i, j] == "Win")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (table[i, j] == "Lose")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (table[i, j] == "Draw")
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write($"{table[i, j]}{new string(' ', widthLength)}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write('|');   
                }
                Console.WriteLine();
                
                for (int k = 0; k < maxWidthLength; k++)
                    Console.Write("—");

                Console.WriteLine();
            }
        }
    }
}