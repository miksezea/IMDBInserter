namespace IMDBLib.titleBasics
{
    public class Genre
    {
        public int genreID { get; set; }
        public string genreName { get; set; }

        public Genre(int genreID, string genreName)
        {
            this.genreID = genreID;
            this.genreName = genreName;
        }
    }
}
