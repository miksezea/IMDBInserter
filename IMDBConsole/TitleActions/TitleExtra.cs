using System.Data.SqlClient;

namespace IMDBConsole.titleActions
{
    public class TitleExtra
    {
        SqlConnection _sqlConn = new();
        public void DBTitleCount(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to check?");
            Console.WriteLine("1: Titles");
            Console.WriteLine("2: Genres");
            Console.WriteLine("3: TitlesGenres");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    CountTable("Titles");
                    break;
                case "2":
                    Console.Clear();
                    CountTable("Genres");
                    break;
                case "3":
                    Console.Clear();
                    CountTable("TitlesGenres");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBTitleCount(_sqlConn);
                    break;
            }
        }
        private void CountTable(string table)
        {
            SqlCommand cmd = new($"SELECT COUNT(*) FROM {table}", _sqlConn);
            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine($"There are {count} rows in {table}.");
            Console.WriteLine();
        }

        public void DBTitleDeleteRows(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
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
                    DeleteRows("Titles");
                    break;
                case "2":
                    Console.Clear();
                    DeleteRows("Genres");
                    break;
                case "3":
                    Console.Clear();
                    DeleteRows("TitlesGenres");
                    break;
                case "4":
                    Console.Clear();
                    DeleteRows("TitlesGenres");
                    DeleteRows("Titles");
                    DeleteRows("Genres");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBTitleDeleteRows(_sqlConn);
                    break;
            }
        }
        private void DeleteRows(string table)
        {
            SqlCommand cmd = new($"DELETE FROM {table}", _sqlConn);
            cmd.ExecuteNonQuery();
            if (table == "Genres")
            {
                SqlCommand reseedCmd = new("DBCC CHECKIDENT ('Genres', RESEED, 0)", _sqlConn);
                reseedCmd.ExecuteNonQuery();
            }
            Console.WriteLine($"Deleted all rows in {table}.");
            Console.WriteLine();
        }
        public int GetGenreMaxId(SqlConnection sqlConn)
        {
            SqlCommand maxCmd = new("SELECT MAX(genreID) FROM [dbo].[Genres]", sqlConn);
            object result = maxCmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }
            else
            {
                // Genre not found
                return -1;
            }
        }
        public int GetGenreID(SqlConnection sqlConn, string genreName)
        {
            SqlCommand sqlCmd = new($"SELECT [genreID] FROM [dbo].[Genres] WHERE [genreName] = '{genreName}'", sqlConn);
            object result = sqlCmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }
            else
            {
                // Genre not found
                return -1;
            }
        }
    }
}
