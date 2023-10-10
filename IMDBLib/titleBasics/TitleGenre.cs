namespace IMDBLib.titleBasics
{
    public class TitleGenre
    {
        public string tconst { get; set; }
        public string genreName { get; set; }

        public TitleGenre(string tconst, string genreName)
        {
            this.tconst = tconst;
            this.genreName = genreName;
        }
    }
}
