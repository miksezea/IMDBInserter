
namespace IMDBLib.nameBasicsAndCrew
{
    public class PrimaryProfession
    {
        public string nconst { get; set; }
        public string professionName { get; set; }

        public PrimaryProfession(string nconst, string professionName)
        {
            this.nconst = nconst;
            this.professionName = professionName;
        }
    }
}
