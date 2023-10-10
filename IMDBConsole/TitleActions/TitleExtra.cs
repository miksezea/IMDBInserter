using System.Data.SqlClient;

namespace IMDBConsole.TitleActions
{
    public class TitleExtra
    {
        public void DBTitleCount(SqlConnection sqlConn)
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
                    CountTable(sqlConn, "Titles");
                    break;
                case "2":
                    Console.Clear();
                    CountTable(sqlConn, "Genres");
                    break;
                case "3":
                    Console.Clear();
                    CountTable(sqlConn, "TitlesGenres");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBTitleCount(sqlConn);
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ActionsProcessor insertersProcessor = new();
            insertersProcessor.DatasetSelector("Title.Basics");
        }
        private void CountTable(SqlConnection sqlConn, string table)
        {
            SqlCommand cmd = new($"SELECT COUNT(*) FROM {table}", sqlConn);
            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine($"There are {count} rows in {table}.");
            Console.WriteLine();
        }

        public void DBTitleDeleteRows(SqlConnection sqlConn)
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
                    DeleteRows(sqlConn, "Titles");
                    break;
                case "2":
                    Console.Clear();
                    DeleteRows(sqlConn, "Genres");
                    break;
                case "3":
                    Console.Clear();
                    DeleteRows(sqlConn, "TitlesGenres");
                    break;
                case "4":
                    Console.Clear();
                    DeleteRows(sqlConn, "Titles");
                    DeleteRows(sqlConn, "Genres");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBTitleDeleteRows(sqlConn);
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            ActionsProcessor actionsProcessor = new();
            actionsProcessor.DatasetSelector("Title.Basics");
        }
        private void DeleteRows(SqlConnection sqlConn, string table)
        {
            SqlCommand cmd = new($"DELETE FROM {table}", sqlConn);
            cmd.ExecuteNonQuery();
            Console.WriteLine($"Deleted all rows in {table}.");
            Console.WriteLine();
        }

        public int GetGenreID(SqlConnection sqlConn, string genreName)
        {
            SqlCommand sqlCmd = new("SELECT [genreID] FROM [dbo].[Genres] WHERE [genreName] = @GenreName", sqlConn);
            sqlCmd.Parameters.AddWithValue("@GenreName", genreName);
            object result = sqlCmd.ExecuteScalar();

            return (int)result;
        }
    }
}
