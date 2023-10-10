namespace IMDBConsole
{
    public static class Program
    {
        public static void Main(string[]? args)
        {
            ActionsProcessor processor = new();
            Console.WriteLine("Select dataset:");

            Console.WriteLine("1: Title Basics");
            Console.WriteLine("2: Name Basics (NOT IMPLEMENTED)");
            Console.WriteLine("3: Title Crew (NOT IMPLEMENTED)");
            Console.WriteLine("4: Title Akas (NOT IMPLEMENTED)");
            Console.WriteLine("5: Title Principals (NOT IMPLEMENTED)");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    processor.DatasetSelector("Title.Basics");
                    break;
                case "2":
                    Console.Clear();
                    processor.DatasetSelector("Name.Basics");
                    break;
                case "3":
                    Console.Clear();
                    processor.DatasetSelector("Title.Crew");
                    break;
                case "4":
                    Console.Clear();
                    processor.DatasetSelector("Title.Akas");
                    break;
                case "5":
                    Console.Clear();
                    processor.DatasetSelector("Title.Principals");
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