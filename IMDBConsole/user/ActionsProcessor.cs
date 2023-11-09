using IMDBLib.titleBasics;
using System.Data;
using System.Data.SqlClient;

namespace IMDBConsole.user
{
    public class ActionsProcessor
    {
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=IMDBUser; password=password; TrustServerCertificate=True";
        private readonly UserFunctions f = new();
        private readonly AddTitleValues atv = new();
        private readonly AddNameValues anv = new();
        SqlConnection sqlConn = new();

        public void TitleSearch()
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
                sqlConn = new(connString);
                sqlConn.Open();

                SqlCommand countCmd = new($"SELECT COUNT(*) FROM [Titles] WHERE primaryTitle LIKE @input", sqlConn);
                countCmd.Parameters.AddWithValue("@input", "%" + input + "%");
                int totalCount = (int)countCmd.ExecuteScalar();

                SqlCommand SelectCmd = new($"SELECT TOP (5) *" +
                    " FROM [Titles] WHERE primaryTitle LIKE @input", sqlConn);
                SelectCmd.Parameters.AddWithValue("@input", "%" + input + "%");

                SqlDataReader reader = SelectCmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader["tconst"]}");
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

                sqlConn.Close();

                Console.WriteLine($"Showing 5 out of {totalCount} titles");
                Console.WriteLine();
            }
        }

        public void NameSearch()
        {
            Console.WriteLine("Enter a name to search for:");
            string? input = Console.ReadLine();
            Console.WriteLine();

            if (input == null)
            {
                Console.WriteLine("Search can't be empty");
                Console.WriteLine();
                NameSearch();
            }
            else
            {
                sqlConn = new(connString);
                sqlConn.Open();

                SqlCommand countCmd = new($"SELECT COUNT(*) FROM [Names] WHERE primaryName LIKE @input", sqlConn);
                countCmd.Parameters.AddWithValue("@input", "%" + input + "%");
                int totalCount = (int)countCmd.ExecuteScalar();

                SqlCommand cmd = new($"SELECT TOP (5) *" +
                                       " FROM [Names] WHERE primaryName LIKE @input", sqlConn);

                cmd.Parameters.AddWithValue("@input", "%" + input + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["nconst"]}");
                    Console.WriteLine($"Name: {reader["primaryName"]}");
                    Console.WriteLine($"Birth Year: {reader["birthYear"]}");
                    if (reader["deathYear"] != DBNull.Value)
                    {
                        Console.WriteLine($"Death Year: {reader["deathYear"]}");
                    }
                    Console.WriteLine();
                }

                sqlConn.Close();

                Console.WriteLine($"Showing 5 out of {totalCount} names");
                Console.WriteLine();
            }
        }

        public void AddTitle()
        {
            string? primaryTitleFirst = atv.PrimaryTitle();
            while (primaryTitleFirst == null || primaryTitleFirst == "")
            {
                Console.WriteLine("Title cannot be empty");
                Console.WriteLine();
                atv.PrimaryTitle();
            }
            string primaryTitle = primaryTitleFirst;
            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}");
            Console.WriteLine();

            string? originalTitleFirst = null;
            string originalTitle = "";

            Console.WriteLine("Is original title the same as the title");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            string? input = Console.ReadLine();
            while (originalTitleFirst == null)
            {
                switch (input)
                {
                    case "1":
                        originalTitle = primaryTitle;
                        break;
                    case "2":
                        originalTitleFirst = atv.OriginalTitle();
                        if (originalTitleFirst == null || originalTitleFirst == "")
                        {
                            Console.WriteLine("Title cannot be empty");
                            Console.WriteLine();
                            atv.OriginalTitle();
                        }
                        else
                        {
                            originalTitle = originalTitleFirst;
                        }
                        break;
                    default:
                        Console.WriteLine($"{input} is not a valid option.");
                        Console.WriteLine();
                        Console.WriteLine("Is original title the same as the title");
                        Console.WriteLine("1: Yes");
                        Console.WriteLine("2: No");
                        input = Console.ReadLine();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}");
            Console.WriteLine();

            string? titleTypeFirst = atv.TitleType();
            string titleType = "";
            while (titleTypeFirst != null && titleTypeFirst != "")
            {
                Console.WriteLine("Title cannot be empty");
                Console.WriteLine();
                atv.TitleType();
                titleType = titleTypeFirst;
            }
            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}");
            Console.WriteLine();

            bool isAdult = atv.IsAdult();

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}");
            Console.WriteLine();

            int? startYear = atv.StartYear();

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}");
            Console.WriteLine();

            int? endYear = atv.EndYear();

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}");
            Console.WriteLine();

            int? runtimeMinutes = atv.RuntimeMinutes();

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}, Runtime: {runtimeMinutes}");
            Console.WriteLine();

            List<Genre>? genreList = atv.Genres();

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}, Runtime: {runtimeMinutes}");

            string genreText = "Genres: ";

            if (genreList != null)
            {
                string genresNames = string.Join(", ", genreList.Select(g => g.genreName));
                genreText += genresNames;
            }
            else
            {
                genreText += "No genres selected";
            }
            Console.WriteLine(genreText);

            Console.WriteLine("Is the title info correct?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            input = Console.ReadLine();

            while (input != "1" && input != "2")
            {
                Console.Clear();
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}, Runtime: {runtimeMinutes}");
                Console.WriteLine(genreText);
                Console.WriteLine();
                Console.WriteLine("Is the title info correct?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    sqlConn = new(connString);
                    sqlConn.Open();

                    SqlCommand findTconstCmd = new("SELECT TOP 1 [tconst] FROM [dbo].[Titles] ORDER BY [tconst] DESC", sqlConn);
                    SqlDataReader reader = findTconstCmd.ExecuteReader();

                    string? tconstFound = null;

                    while (reader.Read())
                    {
                        tconstFound = reader.GetString(0);
                    }
                    reader.Close();

                    if (tconstFound == null)
                    {
                        tconstFound = "tt0000000";
                    }

                    string tconstNumber = tconstFound[2..];

                    int tconstInt = int.Parse(tconstNumber);
                    tconstInt++;

                    string tconst = "tt" + tconstInt.ToString("D7");

                    SqlCommand addTitleCmd = new("InsertTitle", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addTitleCmd.Parameters.AddWithValue("@tconst", tconst);
                    addTitleCmd.Parameters.AddWithValue("@primaryTitle", f.ConvertToSqlString(primaryTitle));
                    addTitleCmd.Parameters.AddWithValue("@originalTitle", f.ConvertToSqlString(originalTitle));
                    addTitleCmd.Parameters.AddWithValue("@titleType", f.ConvertToSqlString(titleType));
                    addTitleCmd.Parameters.AddWithValue("@isAdult", isAdult);
                    addTitleCmd.Parameters.AddWithValue("@startYear", f.CheckIntForNull(startYear));
                    addTitleCmd.Parameters.AddWithValue("@endYear", f.CheckIntForNull(endYear));
                    addTitleCmd.Parameters.AddWithValue("@runtimeMinutes", f.CheckIntForNull(runtimeMinutes));

                    try
                    {
                        addTitleCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(addTitleCmd.CommandText);
                        Console.ReadKey();
                    }

                    if (genreList != null)
                    {
                        foreach (Genre genre in genreList)
                        {
                            SqlCommand addTitleGenreCmd = new("InsertTitleGenres", sqlConn)
                            {
                                CommandType = CommandType.StoredProcedure
                            };

                            addTitleGenreCmd.Parameters.AddWithValue("@tconst", tconst);
                            addTitleGenreCmd.Parameters.AddWithValue("@genre", genre.genreID);

                            try
                            {
                                addTitleGenreCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine(addTitleGenreCmd.CommandText);
                                Console.ReadKey();
                            }
                        }
                    }

                    sqlConn.Close();

                    Console.WriteLine("Title added");
                    Console.WriteLine();
                    break;
                case "2":
                    Console.WriteLine("Title not added");
                    Console.WriteLine();
                    break;
            }
        }

        public void AddName()
        {
            string? primaryNameFirst = anv.PrimaryName();
            while (primaryNameFirst == null || primaryNameFirst == "")
            {
                Console.WriteLine("Name cannot be empty");
                Console.WriteLine();
                anv.PrimaryName();
            }
            string primaryName = primaryNameFirst;
            Console.Clear();
            Console.WriteLine($"Name: {primaryName}");
            Console.WriteLine();

            int? birthYear = null;
            while (true)
            {
                birthYear = anv.BirthYear();
                if (birthYear != null)
                {
                    if (birthYear > DateTime.Now.Year)
                    {
                        Console.WriteLine("Birth year cannot be in the future");
                        Console.WriteLine();
                        anv.BirthYear();
                    }
                    else if (birthYear < 0)
                    {
                        Console.WriteLine("Birth year cannot be negative");
                        Console.WriteLine();
                        anv.BirthYear();
                    }
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine($"Name: {primaryName}, Birth year: {birthYear}");
            Console.WriteLine();

            int? deathYear;
            while (true)
            {
                deathYear = anv.DeathYear();
                if (deathYear != null)
                {
                    if (deathYear > DateTime.Now.Year)
                    {
                        Console.WriteLine("Death year cannot be in the future");
                        Console.WriteLine();
                        anv.DeathYear();
                    }
                    else if (deathYear < 0)
                    {
                        Console.WriteLine("Death year cannot be negative");
                        Console.WriteLine();
                        anv.DeathYear();
                    }
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine($"Name: {primaryName}, Birth year: {birthYear}, Death year: {deathYear}");
            Console.WriteLine();



            Console.WriteLine("Is the name info correct?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            string? input = Console.ReadLine();

            while (input != "1" && input != "2")
            {
                Console.Clear();
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                Console.WriteLine($"Name: {primaryName}, Birth year: {birthYear}, Death year: {deathYear}");
                Console.WriteLine();
                Console.WriteLine("Is the name info correct?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    sqlConn = new(connString);
                    sqlConn.Open();

                    SqlCommand findNconstCmd = new("SELECT TOP 1 [nconst] FROM [dbo].[Names] ORDER BY [nconst] DESC", sqlConn);
                    SqlDataReader reader = findNconstCmd.ExecuteReader();

                    string? nconstFound = null;

                    while (reader.Read())
                    {
                        nconstFound = reader.GetString(0);
                    }
                    reader.Close();

                    if (nconstFound == null)
                    {
                        nconstFound = "nm0000000";
                    }

                    string nconstNumber = nconstFound[2..];

                    int nconstInt = int.Parse(nconstNumber);
                    nconstInt++;

                    string nconst = "nm" + nconstInt.ToString("D7");


                    SqlCommand addNameCmd = new("InsertName", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    addNameCmd.Parameters.AddWithValue("@nconst", nconst);
                    addNameCmd.Parameters.AddWithValue("@primaryName", f.ConvertToSqlString(primaryName));
                    addNameCmd.Parameters.AddWithValue("@birthYear", f.CheckIntForNull(birthYear));
                    addNameCmd.Parameters.AddWithValue("@deathYear", f.CheckIntForNull(deathYear));

                    try
                    {
                        addNameCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(addNameCmd.CommandText);
                        Console.ReadKey();
                    }

                    sqlConn.Close();

                    Console.WriteLine("Name added");
                    Console.WriteLine();
                    break;
                case "2":
                    Console.WriteLine("Name not added");
                    Console.WriteLine();
                    break;
            }
        }

        public void UpdateTitle()
        {
            Console.WriteLine("Which title should be updated? (tconst)");
            string? input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Clear();
                Console.WriteLine("tconst can't be null");
                Console.WriteLine("Which title should be updated? (tconst)");
                input = Console.ReadLine();
            }

            sqlConn = new(connString);
            sqlConn.Open();

            SqlCommand findTitleCmd = new($"SELECT * FROM [Titles] WHERE tconst = {input}", sqlConn);

            SqlDataReader reader = findTitleCmd.ExecuteReader();

            string tconst = "";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tconst = reader.GetString(0);
                    Console.WriteLine($"Primary title: {reader["primaryTitle"]}");
                    Console.WriteLine($"Original title: {reader["originalTitle"]}");
                    Console.WriteLine($"Type: {reader["titleType"]}");
                    Console.WriteLine($"Adult: {reader["isAdult"]}");
                    Console.WriteLine($"Release year: {reader["startYear"]}");
                    Console.WriteLine($"End year: {reader["endYear"]}");
                    Console.WriteLine($"Runtime: {reader["runtimeMinutes"]}");
                    Console.WriteLine();
                }
                reader.Close();

                Console.WriteLine("Enter the updated information (leave empty if no changes): ");

                SqlCommand updateTitleCmd = new("UpdateTitle", sqlConn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                updateTitleCmd.Parameters.AddWithValue("@tconst", tconst);

                Console.WriteLine("Enter new primary title: ");
                string? primaryTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(primaryTitle))
                {
                    updateTitleCmd.Parameters.AddWithValue("@primaryTitle", primaryTitle);
                }

                Console.WriteLine("Enter new original title: ");
                string? originalTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(originalTitle))
                {
                    updateTitleCmd.Parameters.AddWithValue("@originalTitle", originalTitle);
                }

                Console.WriteLine("Enter new title type: ");
                string? titleType = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(titleType))
                {
                    updateTitleCmd.Parameters.AddWithValue("@titleType", titleType);
                }

                Console.WriteLine("Is it an adult movie? (true/false): ");
                string? isAdult = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(isAdult))
                {
                    bool isAdultBool = bool.Parse(isAdult);
                    updateTitleCmd.Parameters.AddWithValue("@isAdult", isAdultBool);
                }

                Console.WriteLine("Enter new release year (empty if no release year): ");
                string? startYear = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(startYear))
                {
                    int startYearInt = int.Parse(startYear);
                    updateTitleCmd.Parameters.AddWithValue("@startYear", startYearInt);
                }

                Console.WriteLine("Enter new end year (empty if no end year): ");
                string? endYear = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(endYear))
                {
                    int endYearInt = int.Parse(endYear);
                    updateTitleCmd.Parameters.AddWithValue("@endYear", endYearInt);
                }

                Console.WriteLine("Enter new runtime (empty if no runtime): ");
                string? runtimeMinutes = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(runtimeMinutes))
                {
                    int runtimeMinutesInt = int.Parse(runtimeMinutes);
                    updateTitleCmd.Parameters.AddWithValue("@runtimeMinutes", runtimeMinutesInt);
                }

                try
                {
                    updateTitleCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(updateTitleCmd.CommandText);
                    Console.ReadKey();
                }
                sqlConn.Close();
            }
            else
            {
                Console.WriteLine($"Title with ID: {tconst} does not exist");
                Console.WriteLine("Press any button to go back...");
                sqlConn.Close();
                Console.ReadKey();
            }
        }

        public void DeleteTitle()
        {
            Console.WriteLine("Which title should be deleted? (tconst)");
            string? input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Clear();
                Console.WriteLine("tconst can't be null");
                Console.WriteLine("Which title should be deleted? (tconst)");
                input = Console.ReadLine();
            }

            sqlConn = new(connString);
            sqlConn.Open();

            SqlCommand findTitleCmd = new($"SELECT * FROM [Titles] WHERE tconst = {input}", sqlConn);

            SqlDataReader reader = findTitleCmd.ExecuteReader();

            string tconst = "";

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tconst = reader.GetString(0);
                    Console.WriteLine($"Primary title: {reader["primaryTitle"]}");
                    Console.WriteLine($"Original title: {reader["originalTitle"]}");
                    Console.WriteLine($"Type: {reader["titleType"]}");
                    Console.WriteLine($"Adult: {reader["isAdult"]}");
                    Console.WriteLine($"Release year: {reader["startYear"]}");
                    Console.WriteLine($"End year: {reader["endYear"]}");
                    Console.WriteLine($"Runtime: {reader["runtimeMinutes"]}");
                    Console.WriteLine();
                }
                reader.Close();

                Console.WriteLine("Are you sure you want to delete this title?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                string? deleteInput = Console.ReadLine();

                while (deleteInput != "1" && deleteInput != "2")
                {
                    Console.Clear();
                    Console.WriteLine($"{deleteInput} is not a valid option.");
                    Console.WriteLine();
                    Console.WriteLine("Are you sure you want to delete this title?");
                    Console.WriteLine("1: Yes");
                    Console.WriteLine("2: No");
                    deleteInput = Console.ReadLine();
                }

                switch (deleteInput)
                {
                    case "1":
                        SqlCommand deleteTitleCmd = new("DeleteTitle", sqlConn)
                        {
                            CommandType = CommandType.StoredProcedure
                        };

                        deleteTitleCmd.Parameters.AddWithValue("@tconst", tconst);

                        try
                        {
                            deleteTitleCmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(deleteTitleCmd.CommandText);
                            Console.ReadKey();
                        }
                        sqlConn.Close();
                        Console.WriteLine("Title deleted");
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Title not deleted");
                        Console.WriteLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Title with ID: {tconst} does not exist");
                Console.WriteLine("Press any button to go back...");
                sqlConn.Close();
                Console.ReadKey();
            }
        }
    }
}
