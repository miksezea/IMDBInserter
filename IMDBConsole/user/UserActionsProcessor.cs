using System.Data.SqlClient;

namespace IMDBConsole.user
{
    public class UserActionsProcessor
    {
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";

        public void TitleSearch() // TODO: Add pagination, add option to go back to menu
        {
            Console.WriteLine("Enter a title to search for:");
            string? input = Console.ReadLine();
            Console.WriteLine("");

            if (input == null)
            {
                Console.WriteLine("Search can't be empty");
                Console.WriteLine();
                TitleSearch();
            }
            else
            {
                SqlConnection sqlConn = new(connString);
                sqlConn.Open();

                SqlCommand countCmd = new($"SELECT COUNT(*) FROM [Titles] WHERE primaryTitle LIKE @input", sqlConn);
                countCmd.Parameters.AddWithValue("@input", "%" + input + "%");
                int totalCount = (int)countCmd.ExecuteScalar();

                int displayNumberMin = 1;
                int displayNumberMax = 10;

                SqlCommand cmd = new($"SELECT TOP ({displayNumberMax}) [titleType], [primaryTitle], [originalTitle], [isAdult], [startYear], [endYear], [runtimeMinutes]" +
                    " FROM [Titles] WHERE primaryTitle LIKE @input", sqlConn);

                cmd.Parameters.AddWithValue("@input", "%" + input + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Title: {reader["primaryTitle"]}");
                    if (reader["originalTitle"] != reader["primaryTitle"])
                    {
                        Console.WriteLine($"Original Title: {reader["originalTitle"]}");
                    }
                    Console.WriteLine($"Type: {reader["titleType"]}");
                    if (reader["isAdult"] is bool isAdult && isAdult)
                    {
                        Console.WriteLine($"Adult movie");
                    }
                    Console.WriteLine($"Release date: {reader["startYear"]}");

                    if (reader["endYear"] != DBNull.Value)
                    {
                        Console.WriteLine($"End Year: {reader["endYear"]}");
                    }
                    if (reader["runtimeMinutes"] != DBNull.Value)
                    {
                        Console.WriteLine($"Length: {reader["runtimeMinutes"]}");
                    }
                    else
                    {
                        Console.WriteLine($"Length: Unknown");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"Showing {displayNumberMin}-{displayNumberMax} out of {totalCount} titles");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        public void NameSearch()
        {
            throw new NotImplementedException();
        }

        public void AddTitle()
        {
            throw new NotImplementedException();
        }

        public void AddName()
        {
            throw new NotImplementedException();
        }

        public void UpdateTitle()
        {
            throw new NotImplementedException();
        }

        public void DeleteTitle()
        {
            throw new NotImplementedException();
        }
    }
}
