using IMDBLib.titleBasics;
using System.Data.Common;
using System.Data.SqlClient;


namespace IMDBConsole.TitleActions
{
    public class TitleInserter
    {
        public void InsertTitleData(string connString, int inserterType, string path, int lineAmount) {
            DateTime before = DateTime.Now;

            List<Title> titles = new();
            List<Genre> genres = new();
            List<TitleGenre> titleGenres = new();

            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            MakeLists(sqlConn, path, lineAmount, titles, genres, titleGenres);

            IInserter<Title>? titleInsert = null;
            IInserter<Genre>? genreInsert = null;
            IInserter<TitleGenre>? titleGenreInsert = null;

            switch (inserterType) {
                case 1:
                    titleInsert = new TitleNormal();
                    genreInsert = new TitleNormal();
                    titleGenreInsert = new TitleNormal();
                    break;
                case 2:
                    //titleInsert = new TitlePrepared();
                    //genreInsert = new TitlePrepared();
                    //titleGenreInsert = new TitlePrepared();
                    break;
                case 3:
                    //titleInsert = new TitleBulked();
                    //genreInsert = new TitleBulked();
                    //titleGenreInsert = new TitleBulked();
                    break;
            }
            titleInsert?.InsertData(sqlConn, titles);
            genreInsert?.InsertData(sqlConn, genres);
            titleGenreInsert?.InsertData(sqlConn, titleGenres);

            sqlConn.Close();

            DateTime after = DateTime.Now;

            Console.WriteLine("Tid: " + (after - before));
        }
        public void MakeLists(SqlConnection sqlConn, string path, int lineAmount,
            List<Title> titles, List<Genre> genres, List<TitleGenre> titleGenres)
        {

            IEnumerable<string> lines = File.ReadLines(path).Skip(1);
            if (lineAmount > 0) {
                lines = lines.Take(lineAmount);
            }

            HashSet<string> existingGenres = new();
            ValuesProcessor v = new();
            TitleExtra extra = new();

            foreach (string line in lines) {
                string[] values = line.Split("\t");

                if (values.Length == 9) {
                    // Titles table
                    titles.Add(new Title(values[0], values[1], values[2], values[3],
                        v.ConvertToBool(values[4]), v.ConvertToInt(values[5]),
                        v.ConvertToInt(values[6]), v.ConvertToInt(values[7])));

                    // Genres table and TitlesGenres table
                    if (values[8] != @"\N") {
                        string[] genreNames = values[8].Split(",");                        

                        foreach (string genreName in genreNames) {
                            if (!existingGenres.Contains(genreName)) {
                                existingGenres.Add(genreName);

                                int genreID = extra.GetGenreID(sqlConn, genreName);

                                if (genreID == -1)
                                {
                                    SqlCommand insertGenreCmd = new("INSERT INTO [dbo].[Genres]" +
                                        "([genreName])VALUES " +
                                        $"('{genreName}')", sqlConn);
                                    insertGenreCmd.ExecuteNonQuery();

                                    genreID = extra.GetGenreID(sqlConn, genreName);
                                }
                                genres.Add(new Genre(genreID, genreName));
                            }
                            titleGenres.Add(new TitleGenre(values[0], genreName));
                        }
                    }
                }
            }
            Console.WriteLine("Amount of titles: " + titles.Count);
        }
    }
}
