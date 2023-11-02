namespace IMDBLib.titleCrew
{
    public class Director
    {
        public string nconst { get; set; }
        public string tconst { get; set; }

        public Director(string nconst, string tconst)
        {
            this.tconst = tconst;
            this.nconst = nconst;
        }
    }
}
