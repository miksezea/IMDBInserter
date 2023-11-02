namespace IMDBLib.titleBasics
{
    public class TitleGenre
    {
        public string tconst { get; set; }
        public int genreID { get; set; }

        public TitleGenre(string tconst, int genreID)
        {
            this.tconst = tconst;
            this.genreID = genreID;
        }
    }
}
