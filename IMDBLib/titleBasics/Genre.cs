namespace IMDBLib.titleBasics
{
    public class Genre
    {
        public int genreID { get; set; }
        public string genreName { get; set; }

        public Genre(string genreName, int genreID)
        {
            this.genreName = genreName;
            this.genreID = genreID;
        }
    }
}
