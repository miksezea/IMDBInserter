using IMDBConsole.user;

namespace IMDBConsole
{
    public class UserActions
    {
        public void UserMenu()
        {
            Console.WriteLine("Select action:");

            Console.WriteLine("1: Search for a title");
            Console.WriteLine("2: Search for a person");
            Console.WriteLine("3: Add a title to the database");
            Console.WriteLine("4: Add a person to the database");
            Console.WriteLine("5: Update a title");
            Console.WriteLine("6: Delete a title");
            Console.WriteLine("7: Back");

            string? input = Console.ReadLine();

            ActionsProcessor ap = new();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ap.TitleSearch();
                    break;
                case "2":
                    Console.Clear();
                    ap.NameSearch();
                    break;
                case "3":
                    Console.Clear();
                    ap.AddName();
                    break;
                case "4":
                    Console.Clear();
                    ap.AddTitle();
                    break;
                case "5":
                    Console.Clear();
                    ap.UpdateTitle();
                    break;
                case "6":
                    Console.Clear();
                    ap.DeleteTitle();
                    break;
                case "7":
                    Console.Clear();
                    Program.Main(null);
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    UserMenu();
                    break;
            }
        }
    }
}
