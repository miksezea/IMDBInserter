namespace IMDBConsole.user
{
    public class AddNameValues
    {
        public string? PrimaryName()
        {
            Console.WriteLine("What is the persons name?");
            string? input = Console.ReadLine();
            string? primaryName = null;
            if (input != null)
            {
                primaryName = input.Trim();
            }
            return primaryName;
        }

        public int? BirthYear()
        {
            Console.WriteLine("Which year was the person born in? (empty if no birth year)");
            string? input = Console.ReadLine();
            int? birthYear;

            if (!string.IsNullOrEmpty(input))
            {
                birthYear = int.Parse(input);
                return birthYear;
            }
            else if (int.TryParse(input, out int result))
            {
                birthYear = result;
                return birthYear;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                return BirthYear();
            }
        }

        public int? DeathYear()
        {
            Console.WriteLine("Which year did the person die in? (empty if no death year)");
            string? input = Console.ReadLine();
            int? deathYear;

            if (!string.IsNullOrEmpty(input))
            {
                deathYear = int.Parse(input);
                return deathYear;
            }
            else if (int.TryParse(input, out int result))
            {
                deathYear = result;
                return deathYear;
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option.");
                Console.WriteLine();
                return DeathYear();
            }
        }
    }
}
