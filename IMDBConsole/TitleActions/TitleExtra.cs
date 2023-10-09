using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.TitleActions
{
    public class TitleExtra
    {
        public void CheckTitleData(string tsv, string connString)
        {
            Console.WriteLine("Which Table do you want to check?");
            Console.WriteLine("1: Titles");
            Console.WriteLine("2: Genres");
            Console.WriteLine("3: TitlesGenres");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    WhichTable(connString, "Titles");
                    break;
                case "2":
                    Console.Clear();
                    WhichTable(connString, "Genres");
                    break;
                case "3":
                    Console.Clear();
                    WhichTable(connString, "TitlesGenres");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    CheckTitleData(tsv, connString);
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            InsertersProcessor insertersProcessor = new();
            insertersProcessor.InserterSelector(tsv);
        }

        private void WhichTable(string connString, string table)
        {
            using (SqlConnection sqlConn = new(connString))
            {
                sqlConn.Open();
                SqlCommand cmd = new($"SELECT COUNT(*) FROM {table}", sqlConn);
                int count = (int)cmd.ExecuteScalar();
                Console.WriteLine($"There are {count} rows in {table}.");
                Console.WriteLine();
            }
        }

        public void DeleteTitleData(string tsv, string connString)
        {
               Console.WriteLine("Which Table do you want to delete?");
            Console.WriteLine("1: Titles");
            Console.WriteLine("2: Genres");
            Console.WriteLine("3: TitlesGenres");
            Console.WriteLine("4: All of the above");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    DeleteTable(connString, "Titles");
                    break;
                case "2":
                    Console.Clear();
                    DeleteTable(connString, "Genres");
                    break;
                case "3":
                    Console.Clear();
                    DeleteTable(connString, "TitlesGenres");
                    break;
                case "4":
                    Console.Clear();
                    DeleteTable(connString, "Titles");
                    DeleteTable(connString, "Genres");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DeleteTitleData(tsv, connString);
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            InsertersProcessor insertersProcessor = new();
            insertersProcessor.InserterSelector(tsv);
        }

        private void DeleteTable(string connString, string table)
        {
            using (SqlConnection sqlConn = new(connString))
            {
                sqlConn.Open();
                SqlCommand cmd = new($"DELETE FROM {table}", sqlConn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Deleted all rows in {table}.");
                Console.WriteLine();
            }
        }
    }
}
