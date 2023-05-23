namespace Task3
{
    class RulesTable
    {
        private readonly string[,] table;
        private readonly int maxLength = 0;
        private readonly string[] moves;
        private readonly int movesCount;

        public RulesTable(string[] moves)
        {
            this.movesCount = moves.Length;
            this.table = new string[movesCount + 1, movesCount + 1];
            this.maxLength = moves.Max(x => x.Length);
            GenerateTable();
        }

        private void GenerateTable()
        {
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
                    int halfMoves = movesCount / 2;
                    int distance = (j - i + movesCount) % movesCount;

                    if (distance == 0)
                    {
                        table[i, j] = "Draw";
                    }
                    else if (distance <= halfMoves)
                    {
                        table[i, j] = "Win";
                    }
                    else
                    {
                        table[i, j] = "Lose";
                    }
                }
            }
        }

        public void PrintTable()
        {
            var tableSize = table.GetLength(0);
            var maxWidthLength = (maxLength * tableSize) + tableSize * 2;

            Console.WriteLine("Rules Table:");
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    var widthLength = 1 + maxLength - table[i, j].Length;

                    if (table[i, j] == "Win")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (table[i, j] == "Lose")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (table[i, j] == "Draw")
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write($"{table[i, j]}{new string(' ', widthLength)}");
                    Console.ForegroundColor = ConsoleColor.White;
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