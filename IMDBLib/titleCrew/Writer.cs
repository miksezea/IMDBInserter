namespace IMDBLib.titleCrew
{
    public class Writer
    {
        public string nconst { get; set; }
        public string tconst { get; set; }

        public Writer(string nconst, string tconst)
        {
            this.nconst = nconst;
            this.tconst = tconst;
        }
    }
}
