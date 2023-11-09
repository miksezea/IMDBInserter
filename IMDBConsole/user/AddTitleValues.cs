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
    }
}
