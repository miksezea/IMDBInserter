using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.TitleInserter
{
    public class InsertersProcessor
    {
        readonly ValuesProcessor v = new();
        public void TitleData(string path, string connString, int lineAmount)
        {
            List<Title> titles = new();
            List<Genre> genres = new();
            List<TitleGenre> titlesGenres = new();
            MakeLists(path, lineAmount, titles, genres, titlesGenres);
            InsertTitleData(titles, connString);
            InsertGenreData(genres, connString);
            InsertTitleGenreData(titlesGenres, connString);
        }

        public void MakeLists(string path, int lineAmount, List<Title> titles, List<Genre> genres, List<TitleGenre> titlesGenres)
        {
            HashSet<string> existingGenres = new();
            foreach (
                string line in File.ReadLines(path).Skip(1).Take(lineAmount)
                )
            {
                string[] values = line.Split("\t");

                if (values.Length == 9)
                {
                    // Titles table
                    titles.Add(new Title(values[0], values[1], values[2], values[3],
                        v.ConvertToBool(values[4]), v.ConvertToInt(values[5]),
                        v.ConvertToInt(values[6]), v.ConvertToInt(values[7])
                        ));

                    // Genres table
                    if (values[8] != @"\N")
                    {
                        string[] genreNames = values[8].Split(",");

                        foreach (string genreName in genreNames)
                        {
                            if (!existingGenres.Contains(genreName))
                            {
                                genres.Add(new Genre(genreName));

                                existingGenres.Add(genreName);
                            }
                        }
                    }

                    // TitlesGenres table
                    string[] genreForTitle = values[8].Split(",");

                    foreach (string genreName in genreForTitle)
                    {
                        titlesGenres.Add(new TitleGenre(values[0], v.ConvertToString(genreName)));
                    }
                }
            }
            Console.WriteLine("Amount of titles: " + titles.Count);
        }

        public void InsertTitleData(List<Title> titles, string connString)
        {
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            foreach (Title title in titles)
            {
                SqlCommand sqlCommand = new("INSERT INTO [dbo].[Titles]" +
                    "([tconst],[titleType],[primaryTitle],[originalTitle]," +
                    "[isAdult],[startYear],[endYear],[runtimeMinutes])VALUES " +
                    $"('{title.tconst}','{title.titleType}','{v.ConvertToSqlString(title.primaryTitle)}'," +
                    $"'{v.ConvertToSqlString(title.originalTitle)}','{title.isAdult}'," +
                    $"{v.CheckIntForNull(title.startYear)},{v.CheckIntForNull(title.endYear)}," +
                    $"{v.CheckIntForNull(title.runtimeMinutes)})", sqlConn);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine(sqlCommand.CommandText);
                }
            }
        }

        public void InsertGenreData(List<Genre> genres, string connString)
        {
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            foreach (Genre genre in genres)
            {
                SqlCommand sqlCommand = new("INSERT INTO [dbo].[Genres]" +
                    "([genreName])VALUES " +
                    $"('{genre.genreName}')", sqlConn);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine(sqlCommand.CommandText);
                }
            }
        }

        public void InsertTitleGenreData(List<TitleGenre> titlesGenres, string connString)
        {
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            foreach (TitleGenre titleGenre in titlesGenres)
            {
                SqlCommand sqlCommand = new("INSERT INTO [dbo].[TitlesGenres]" +
                    "([tconst],[genreName])VALUES " +
                    $"('{titleGenre.tconst}',{v.CheckStringForNull(titleGenre.genreName)})", sqlConn);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine(sqlCommand.CommandText);
                }
            }
        }
    }
}
