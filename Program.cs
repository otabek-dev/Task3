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

            var duplicates = args.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
            
            if (duplicates.Count > 0)
            {
                Console.WriteLine($"Invalid arguments.");
                Console.WriteLine($"The arguments have duplicates ({string.Join(", ", duplicates)})");
                Console.WriteLine("Please pass the arguments without duplicates!");
                return;
            }

            var game = new Game(args);
            game.Start();
        }
    }
}