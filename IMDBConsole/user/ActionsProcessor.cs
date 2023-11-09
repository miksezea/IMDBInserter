using IMDBLib.nameBasics;
using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.user
{
    public class ActionsProcessor
    {
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";
        private readonly UserActions ua = new();
        private readonly GlobalFunctions f = new();
        private readonly AddTitleValues atv = new();
        private readonly AddNameValues anv = new();

        public void TitleSearch() // TODO: Add pagination
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
                Console.WriteLine("What do you want to do next?");
                Console.ReadLine();
            }
        }

        public void NameSearch()
        {
            Console.WriteLine("Enter a name to search for:");
            string? input = Console.ReadLine();
            Console.WriteLine("");

            if (input == null)
            {
                Console.WriteLine("Search can't be empty");
                Console.WriteLine();
                NameSearch();
            }
            else
            {
                SqlConnection sqlConn = new(connString);
                sqlConn.Open();

                SqlCommand countCmd = new($"SELECT COUNT(*) FROM [Names] WHERE primaryName LIKE @input", sqlConn);
                countCmd.Parameters.AddWithValue("@input", "%" + input + "%");
                int totalCount = (int)countCmd.ExecuteScalar();

                int displayNumberMin = 1;
                int displayNumberMax = 10;

                SqlCommand cmd = new($"SELECT TOP ({displayNumberMax}) [primaryName], [birthYear], [deathYear]" +
                                       " FROM [Names] WHERE primaryName LIKE @input", sqlConn);

                cmd.Parameters.AddWithValue("@input", "%" + input + "%");

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["primaryName"]}");
                    Console.WriteLine($"Birth Year: {reader["birthYear"]}");
                    if (reader["deathYear"] != DBNull.Value)
                    {
                        Console.WriteLine($"Death Year: {reader["deathYear"]}");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine($"Showing {displayNumberMin}-{displayNumberMax} out of {totalCount} names");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
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
                        } else
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
            while (titleTypeFirst != null || titleTypeFirst != "")
            {
                Console.WriteLine("Title cannot be empty");
                Console.WriteLine();
                atv.TitleType();
            }
            string titleType = titleTypeFirst;
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

            Console.WriteLine("Is the title info correct?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            input = Console.ReadLine();

            while (input != "1" || input != "2")
            {
                switch (input)
                {
                    case "1":
                        SqlConnection sqlConn = new(connString);
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

                        Title title = new(tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear, runtimeMinutes);

                        SqlCommand addTitleCmd = new("" +
                            "INSERT INTO [dbo].[Titles]" +
                            "([tconst], [titleType], [primaryTitle], [originalTitle]," +
                            "[isAdult], [startYear], [endYear], [runtimeMinutes])VALUES " +
                            $"(@tconst, @titleType, @primaryTitle, @originalTitle," +
                            $"@isAdult, @startYear, @endYear, @runtimeMinutes)", sqlConn);

                        SqlParameter tconstParameter = new("@tconst", System.Data.SqlDbType.VarChar, 10);
                        addTitleCmd.Parameters.Add(tconstParameter);

                        SqlParameter titleTypeParameter = new("@titleType", System.Data.SqlDbType.VarChar, 50);
                        addTitleCmd.Parameters.Add(titleTypeParameter);

                        SqlParameter primaryTitleParameter = new("@primaryTitle", System.Data.SqlDbType.VarChar, 8000);
                        addTitleCmd.Parameters.Add(primaryTitleParameter);

                        SqlParameter originalTitleParameter = new("@originalTitle", System.Data.SqlDbType.VarChar, 8000);
                        addTitleCmd.Parameters.Add(originalTitleParameter);

                        SqlParameter isAdultParameter = new("@isAdult", System.Data.SqlDbType.Bit);
                        addTitleCmd.Parameters.Add(isAdultParameter);

                        SqlParameter startYearParameter = new("@startYear", System.Data.SqlDbType.Int);
                        addTitleCmd.Parameters.Add(startYearParameter);

                        SqlParameter endYearParameter = new("@endYear", System.Data.SqlDbType.Int);
                        addTitleCmd.Parameters.Add(endYearParameter);

                        SqlParameter runtimeMinutesParameter = new("@runtimeMinutes", System.Data.SqlDbType.Int);
                        addTitleCmd.Parameters.Add(runtimeMinutesParameter);

                        addTitleCmd.Prepare();

                        f.FillParameterPrepared(tconstParameter, title.tconst);
                        f.FillParameterPrepared(titleTypeParameter, title.titleType);
                        f.FillParameterPrepared(primaryTitleParameter, title.primaryTitle);
                        f.FillParameterPrepared(originalTitleParameter, title.originalTitle);
                        f.FillParameterPrepared(isAdultParameter, title.isAdult);
                        f.FillParameterPrepared(startYearParameter, title.startYear);
                        f.FillParameterPrepared(endYearParameter, title.endYear);
                        f.FillParameterPrepared(runtimeMinutesParameter, title.runtimeMinutes);

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

                        Console.WriteLine("Title added");
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Title not added");
                        Console.WriteLine();
                        ua.UserMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine($"{input} is not a valid option.");
                        Console.WriteLine();
                        Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}, Runtime: {runtimeMinutes}");
                        Console.WriteLine();
                        break;
                }
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

            int? deathYear = null;
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
            while (input != "1" || input != "2")
            {
                switch (input)
                {
                    case "1":
                        SqlConnection sqlConn = new(connString);
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

                        Name name = new(nconst, primaryName, birthYear, deathYear);

                        SqlCommand addNameCmd = new("" +
                            "INSERT INTO [dbo].[Names]" +
                            "([nconst], [primaryName], [birthYear], [deathYear])VALUES " +
                            $"(@nconst, @primaryName, @birthYear, @deathYear)", sqlConn);

                        SqlParameter nconstParameter = new("@nconst", System.Data.SqlDbType.VarChar, 10);
                        addNameCmd.Parameters.Add(nconstParameter);

                        SqlParameter primaryNameParameter = new("@primaryName", System.Data.SqlDbType.VarChar, 8000);
                        addNameCmd.Parameters.Add(primaryNameParameter);

                        SqlParameter birthYearParameter = new("@birthYear", System.Data.SqlDbType.Int);
                        addNameCmd.Parameters.Add(birthYearParameter);

                        SqlParameter deathYearParameter = new("@deathYear", System.Data.SqlDbType.Int);
                        addNameCmd.Parameters.Add(deathYearParameter);

                        addNameCmd.Prepare();

                        f.FillParameterPrepared(nconstParameter, name.nconst);
                        f.FillParameterPrepared(primaryNameParameter, name.primaryName);
                        f.FillParameterPrepared(birthYearParameter, name.birthYear);
                        f.FillParameterPrepared(deathYearParameter, name.deathYear);

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

                        Console.WriteLine("Name added");
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Name not added");
                        Console.WriteLine();
                        ua.UserMenu();
                        break;
                    default:
                        Console.WriteLine($"{input} is not a valid option.");
                        Console.WriteLine();
                        Console.WriteLine("Is the name info correct?");
                        Console.WriteLine("1: Yes");
                        Console.WriteLine("2: No");
                        input = Console.ReadLine();
                        break;
                }
            }
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
