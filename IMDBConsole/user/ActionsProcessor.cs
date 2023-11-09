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
            Console.WriteLine("What is the title called?");
            string? input = Console.ReadLine();
            string primaryTitle = "";
            if (input == null || input.Trim() == "")
            {
                Console.WriteLine("Name cannot be empty");
                Console.WriteLine();
                ua.UserMenu();
            }
            else
            {
                primaryTitle = input.Trim();
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}");
            Console.WriteLine();

            Console.WriteLine("Is original title the same as the title");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            input = Console.ReadLine();

            string originalTitle = "";
            switch (input)
            {
                case "1":
                    originalTitle = primaryTitle;
                    break;
                case "2":
                    Console.WriteLine("What is the original title?");
                    input = Console.ReadLine();
                    if (input == null || input.Trim() == "")
                    {
                        Console.WriteLine("Name cannot be empty");
                        Console.WriteLine();
                        ua.UserMenu();
                    }
                    else
                    {
                        originalTitle = input.Trim();
                    }
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    ua.UserMenu();
                    break;
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}");
            Console.WriteLine();

            Console.WriteLine("What is the type of the title?");
            Console.WriteLine("1: Movie");
            Console.WriteLine("2: Short");
            Console.WriteLine("3: TV Series");
            Console.WriteLine("4: TV Movie");
            Console.WriteLine("5: TV Mini Series");
            Console.WriteLine("6: TV Short");
            Console.WriteLine("7: TV Special");
            Console.WriteLine("8: Video");
            Console.WriteLine("9: Video Game");
            input = Console.ReadLine();

            string titleType = "";
            switch (input)
            {
                case "1":
                    titleType = "movie";
                    break;
                case "2":
                    titleType = "short";
                    break;
                case "3":
                    titleType = "tvSeries";
                    break;
                case "4":
                    titleType = "tvMovie";
                    break;
                case "5":
                    titleType = "tvMiniSeries";
                    break;
                case "6":
                    titleType = "tvShort";
                    break;
                case "7":
                    titleType = "tvSpecial";
                    break;
                case "8":
                    titleType = "video";
                    break;
                case "9":
                    titleType = "videoGame";
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    ua.UserMenu();
                    break;
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}");
            Console.WriteLine();

            Console.WriteLine("Is the title an adult title?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            input = Console.ReadLine();

            bool isAdult = false;

            switch (input)
            {
                case "1":
                    isAdult = true;
                    break;
                case "2":
                    isAdult = false;
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    ua.UserMenu();
                    break;
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}");
            Console.WriteLine();

            Console.WriteLine("What is the release year? (empty if no release year)");
            input = Console.ReadLine();

            int? startYear = null;

            if (!string.IsNullOrEmpty(input))
            {
                startYear = int.Parse(input);
            }
            else if (int.TryParse(input, out int result))
            {
                startYear = result;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                ua.UserMenu();
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}");
            Console.WriteLine();

            Console.WriteLine("What is the end year? (empty if no end year)");
            input = Console.ReadLine();

            int? endYear = null;

            if (!string.IsNullOrEmpty(input))
            {
                endYear = int.Parse(input);
            }
            else if (int.TryParse(input, out int result))
            {
                endYear = result;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                ua.UserMenu();
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}");
            Console.WriteLine();

            Console.WriteLine("What is the runtime in minutes? (empty if no runtime)");
            input = Console.ReadLine();

            int? runtimeMinutes = null;

            if (!string.IsNullOrEmpty(input))
            {
                runtimeMinutes = int.Parse(input);
            }
            else if (int.TryParse(input, out int result))
            {
                runtimeMinutes = result;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                ua.UserMenu();
            }

            Console.Clear();
            Console.WriteLine($"Title: {primaryTitle}, Original title: {originalTitle}, Type: {titleType}, Adult: {isAdult}, Release year: {startYear}, End year: {endYear}, Runtime: {runtimeMinutes}");
            Console.WriteLine();

            Console.WriteLine("Is the title info correct?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            input = Console.ReadLine();

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
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    ua.UserMenu();
                    break;
            }
        }

        public void AddName()
        {
            Console.WriteLine("What is the name of the person?");
            string? input = Console.ReadLine();

            string primaryName = "";
            if (input == null || input.Trim() == "")
            {
                Console.WriteLine("Name cannot be empty");
                ua.UserMenu();
            }
            else
            {
                primaryName = input.Trim();
            }

            Console.Clear();
            Console.WriteLine($"Name: {primaryName}");
            Console.WriteLine();

            Console.WriteLine("Which year was the person born in? (empty if no birth year)");
            input = Console.ReadLine();

            int? birthYear = null;

            if (!string.IsNullOrEmpty(input))
            {
                birthYear = int.Parse(input);
            }
            else if (int.TryParse(input, out int result))
            {
                birthYear = result;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                ua.UserMenu();
            }

            if (birthYear > DateTime.Now.Year)
            {
                Console.WriteLine("Birth year cannot be in the future");
                Console.WriteLine();
                ua.UserMenu();
            }
            if (birthYear < 0)
            {
                Console.WriteLine("Birth year cannot be negative");
                Console.WriteLine();
                ua.UserMenu();
            }

            Console.Clear();
            Console.WriteLine($"Name: {primaryName}, Birth year: {birthYear}");
            Console.WriteLine();

            Console.WriteLine("Which year did the person pass away in? (empty if no death year)");
            input = Console.ReadLine();

            int? deathYear = null;
            if (!string.IsNullOrEmpty(input))
            {
                deathYear = int.Parse(input);
            }
            else if (int.TryParse(input, out int result))
            {
                deathYear = result;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                ua.UserMenu();
            }

            if (deathYear > DateTime.Now.Year)
            {
                Console.WriteLine("Death year cannot be in the future");
                Console.WriteLine();
                ua.UserMenu();
            }
            if (deathYear < 0)
            {
                Console.WriteLine("Death year cannot be negative");
                Console.WriteLine();
                ua.UserMenu();
            }

            Console.Clear();
            Console.WriteLine($"Name: {primaryName}, Birth year: {birthYear}, Death year: {deathYear}");
            Console.WriteLine();

            Console.WriteLine("Is the name info correct?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            input = Console.ReadLine();

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
                    ua.UserMenu();
                    break;
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
