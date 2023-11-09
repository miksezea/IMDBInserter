using IMDBLib.titleBasics;

namespace IMDBConsole.user
{
    public class AddTitleValues
    {
        public string? PrimaryTitle()
        {
            Console.WriteLine("What is the title called?");
            string? input = Console.ReadLine();
            string? primaryTitle = null;
            if (input != null)
            {
                primaryTitle = input.Trim();
            }
            return primaryTitle;
        }

        public string? OriginalTitle()
        {
            Console.WriteLine("What is the original title?");
            string? input = Console.ReadLine();
            string? originalTitle = null;
            if (input != null)
            {
                originalTitle = input.Trim();
            }
            return originalTitle;
        }

        public string? TitleType()
        {
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
            string? input = Console.ReadLine();

            string? titleType = null;
            switch (input)
            {
                case "1":
                    titleType = "movie";
                    return titleType;
                case "2":
                    titleType = "short";
                    return titleType;
                case "3":
                    titleType = "tvSeries";
                    return titleType;
                case "4":
                    titleType = "tvMovie";
                    return titleType;
                case "5":
                    titleType = "tvMiniSeries";
                    return titleType;
                case "6":
                    titleType = "tvShort";
                    return titleType;
                case "7":
                    titleType = "tvSpecial";
                    return titleType;
                case "8":
                    titleType = "video";
                    return titleType;
                case "9":
                    titleType = "videoGame";
                    return titleType;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    return TitleType();
            }
        }

        public bool IsAdult()
        {
            Console.WriteLine("Is the title an adult title?");
            Console.WriteLine("1: Yes");
            Console.WriteLine("2: No");
            string? input = Console.ReadLine();

            bool isAdult = false;

            switch (input)
            {
                case "1":
                    isAdult = true;
                    return isAdult;
                case "2":
                    isAdult = false;
                    return isAdult;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    return IsAdult();
            }
        }

        public int? StartYear()
        {
            Console.WriteLine("What is the release year? (empty if no release year)");
            string? input = Console.ReadLine();
            int? startYear = null;

            if (!string.IsNullOrEmpty(input))
            {
                startYear = int.Parse(input);
                return startYear;
            }
            else if (int.TryParse(input, out int result))
            {
                startYear = result;
                return startYear;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                return StartYear();
            }
        }

        public int? EndYear()
        {
            Console.WriteLine("What is the end year? (empty if no end year)");
            string? input = Console.ReadLine();
            int? endYear;

            if (!string.IsNullOrEmpty(input))
            {
                endYear = int.Parse(input);
                return endYear;
            }
            else if (int.TryParse(input, out int result))
            {
                endYear = result;
                return endYear;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                return EndYear();
            }
        }

        public int? RuntimeMinutes()
        {
            Console.WriteLine("What is the runtime in minutes? (empty if no runtime)");
            string? input = Console.ReadLine();
            int? runtimeMinutes;

            if (!string.IsNullOrEmpty(input))
            {
                runtimeMinutes = int.Parse(input);
                return runtimeMinutes;
            }
            else if (int.TryParse(input, out int result))
            {
                runtimeMinutes = result;
                return runtimeMinutes;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                return RuntimeMinutes();
            }
        }

        public List<Genre>? Genres()
        {
            Console.WriteLine("Which genres does the title have? (max 3)");
            Console.WriteLine("1: Documentary   |   2: Short      |   3: Animation");
            Console.WriteLine("4: Comedy        |   5: Romance    |   6: Sport");
            Console.WriteLine("7: News          |   8: Drama      |   9: Fantasy");
            Console.WriteLine("10: Horror       |   11: Biography |   12: Music");
            Console.WriteLine("13: War          |   14: Crime     |   15: Western");
            Console.WriteLine("16: Family       |   17: Adventure |   18: Action");
            Console.WriteLine("19: History      |   20: Mystery   |   21: Sci-Fi");
            Console.WriteLine("22: Musical      |");
            Console.WriteLine("23: No more genres");

            List<Genre>? genres = new List<Genre>();
            string? input = Console.ReadLine();
            int genresCount = 0;

            while (genresCount != 3)
            {
                switch (input)
                {
                    case "1":
                        genres.Add(new Genre("Documentary", 1));
                        genresCount++;
                        break;
                    case "2":
                        genres.Add(new Genre("Short", 2));
                        genresCount++;
                        break;
                    case "3":
                        genres.Add(new Genre("Animation", 3));
                        genresCount++;
                        break;
                    case "4":
                        genres.Add(new Genre("Comedy", 4));
                        genresCount++;
                        break;
                    case "5":
                        genres.Add(new Genre("Romance", 5));
                        genresCount++;
                        break;
                    case "6":
                        genres.Add(new Genre("Sport", 6));
                        genresCount++;
                        break;
                    case "7":
                        genres.Add(new Genre("News", 7));
                        genresCount++;
                        break;
                    case "8":
                        genres.Add(new Genre("Drama", 8));
                        genresCount++;
                        break;
                    case "9":
                        genres.Add(new Genre("Fantasy", 9));
                        genresCount++;
                        break;
                    case "10":
                        genres.Add(new Genre("Horror", 10));
                        genresCount++;
                        break;
                    case "11":
                        genres.Add(new Genre("Biography", 11));
                        genresCount++;
                        break;
                    case "12":
                        genres.Add(new Genre("Music", 12));
                        genresCount++;
                        break;
                    case "13":
                        genres.Add(new Genre("War", 13));
                        genresCount++;
                        break;
                    case "14":
                        genres.Add(new Genre("Crime", 14));
                        genresCount++;
                        break;
                    case "15":
                        genres.Add(new Genre("Western", 15));
                        genresCount++;
                        break;
                    case "16":
                        genres.Add(new Genre("Family", 16));
                        genresCount++;
                        break;
                    case "17":
                        genres.Add(new Genre("Adventure", 17));
                        genresCount++;
                        break;
                    case "18":
                        genres.Add(new Genre("Action", 18));
                        genresCount++;
                        break;
                    case "19":
                        genres.Add(new Genre("History", 19));
                        genresCount++;
                        break;
                    case "20":
                        genres.Add(new Genre("Mystery", 20));
                        genresCount++;
                        break;
                    case "21":
                        genres.Add(new Genre("Sci-Fi", 21));
                        genresCount++;
                        break;
                    case "22":
                        genres.Add(new Genre("Musical", 22));
                        genresCount++;
                        break;
                    case "23":
                        genresCount = 3;
                        break;
                    default:
                        Console.WriteLine($"{input} is not a valid option.");
                        Console.WriteLine();
                        return Genres();
                }
                if (genresCount != 3)
                {
                    Console.WriteLine("Next genre:");
                    input = Console.ReadLine();
                }
            }
            return genres;
        }
    }
}
