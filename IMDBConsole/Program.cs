namespace IMDBConsole
{
    public static class Program
    {
        public static void Main(string[]? args)
        {
            InsertersProcessor processor = new();
            Console.WriteLine("Select dataset:");

            Console.WriteLine("1: Title Basics");
            Console.WriteLine("2: Name Basics");
            Console.WriteLine("3: Title Crew");
            Console.WriteLine("4: Title Akas");
            Console.WriteLine("5: Title Principals");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    processor.InserterSelector("Title.Basics");
                    break;
                case "2":
                    Console.Clear();
                    processor.InserterSelector("Name.Basics");
                    break;
                case "3":
                    Console.Clear();
                    processor.InserterSelector("Title.Crew");
                    break;
                case "4":
                    Console.Clear();
                    processor.InserterSelector("Title.Akas");
                    break;
                case "5":
                    Console.Clear();
                    processor.InserterSelector("Title.Principals");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    Main(null);
                    break;
            }
        }
    }
}