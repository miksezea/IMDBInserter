
namespace IMDBLib.nameBasics
{
    public class Profession
    {
        public int professionID { get; set; }
        public string professionName { get; set; }

        public Profession(string professionName, int professionID)
        {
            this.professionName = professionName;
            this.professionID = professionID;
        }
    }
}
