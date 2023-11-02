
namespace IMDBLib.nameBasicsAndCrew
{
    public class Name
    {
        public string nconst { get; set; }
        public string primaryName { get; set; }
        public int? birthYear { get; set; }
        public int? deathYear { get; set; }

        public Name(string nconst, string primaryName, int? birthYear, int? deathYear)
        {
            this.nconst = nconst;
            this.primaryName = primaryName;
            this.birthYear = birthYear;
            this.deathYear = deathYear;
        }
    }
}
