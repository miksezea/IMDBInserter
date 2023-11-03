namespace IMDBConsole
{
    public static class Program
    {
        public static void Main(string[]? args)
        {
            Console.WriteLine("Welcome to IMDB Console");
            Console.WriteLine();
            Console.WriteLine("Select action:");
            Console.WriteLine("1: Admin");
            Console.WriteLine("2: User");
            Console.WriteLine("3: Close program");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    AdminActions admin = new();
                    admin.AdminMenu();
                    break;
                case "2":
                    Console.Clear();
                    UserActions user = new();
                    user.UserMenu();
                    Main(null);
                    break;
                case "3":
                    Environment.Exit(0);
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