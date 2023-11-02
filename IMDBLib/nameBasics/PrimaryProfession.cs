
namespace IMDBLib.nameBasics
{
    public class PrimaryProfession
    {
        public string nconst { get; set; }
        public int professionID { get; set; }

        public PrimaryProfession(string nconst, int professionID)
        {
            this.nconst = nconst;
            this.professionID = professionID;
        }
    }
}
