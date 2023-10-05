using IMDBConsole.TitleInserter;

namespace IMDBConsole
{
    public static class Program
    {
        // TODO: Connectionstring should be hidden from public repo.
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";
        private static readonly string tsvPath = @"C:\Users\mikke\OneDrive\Desktop\4Semester\IMDBTSV";

        private static void Main(string[] args)
        {
            Console.WriteLine("Select dataset:");

            Console.WriteLine("1: Title Basics");
            Console.WriteLine("2: Name Basics");
            Console.WriteLine("3: Title Crew");
            Console.WriteLine("4: Title Akas");
            Console.WriteLine("5: Title Principals");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    RunTitleInserter();
                    break;
                case "2":
                    Console.Clear();
                    Main(null); // TODO: Make NameInserter
                    break;
                case "3":
                    Console.Clear();
                    Main(null); // TODO: Make TitleCrewInserter
                    break;
                case "4":
                    Console.Clear();
                    Main(null); // TODO: Make TitleAkasInserter
                    break;
                case "5":
                    Console.Clear();
                    Main(null); // TODO: Make TitlePrincipalsInserter
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    Main(null);
                    break;
            }
        }

        private static void RunTitleInserter()
        {
            string path = tsvPath + @"\title.basics.tsv";

            Console.WriteLine("Title Basics");
            Console.WriteLine();

            Console.WriteLine("Choose action:");
            Console.WriteLine("0: Check amount of titles");
            Console.WriteLine("1: Delete all");
            Console.WriteLine("2: Normal insert");
            Console.WriteLine("3: Prepared insert");
            Console.WriteLine("4: Bulked insert");
            Console.WriteLine("5: Go back");

            string input = Console.ReadLine();


            switch (input)
            {
                case "0":
                    Console.Clear();
                    Console.WriteLine("Title Basics");
                    Console.WriteLine();
                    Console.WriteLine("Checking amount of titles...");
                    
                    break;
                case "1":
                    Console.Clear();
                    Console.WriteLine("Title Basics");
                    Console.WriteLine();
                    Console.WriteLine("Deleting all...");
                    
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Title Basics");
                    Console.WriteLine();
                    Console.WriteLine("How many lines do you want to insert?");
                    int lineAmount = int.Parse(Console.ReadLine());

                    
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Title Basics");
                    Console.WriteLine();

                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Title Basics");
                    Console.WriteLine();

                    break;
                case "5":
                    Console.Clear();
                    Main(null);
                    break;
            }

            InsertersProcessor titleInserter = new();
            titleInserter.TitleData(path, connString, 1000);
        }

    }
}