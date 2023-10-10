using IMDBLib.titleBasics;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace IMDBConsole.TitleActions
{
    public class TitleNormal : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre> {
        readonly ValuesProcessor v = new();
        readonly TitleExtra e = new();
        public void InsertData(SqlConnection sqlConn, List<Title> titles) {
            foreach (Title title in titles) {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Titles]" +
                    "([tconst],[titleType],[primaryTitle],[originalTitle]," +
                    "[isAdult],[startYear],[endYear],[runtimeMinutes])VALUES " +
                    $"('{title.tconst}','{title.titleType}','{v.ConvertToSqlString(title.primaryTitle)}'," +
                    $"'{v.ConvertToSqlString(title.originalTitle)}','{title.isAdult}'," +
                    $"{v.CheckIntForNull(title.startYear)},{v.CheckIntForNull(title.endYear)}," +
                    $"{v.CheckIntForNull(title.runtimeMinutes)})", sqlConn);

                try {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlCmd.CommandText);
                    Console.ReadKey();
                }
            }
        }

        public void InsertData(SqlConnection sqlConn, List<Genre> genres) {
            foreach (Genre genre in genres) {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Genres]" +
                    "([genreName])VALUES " +
                    $"('{genre.genreName}')", sqlConn);

                try {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlCmd.CommandText);
                    Console.ReadKey();
                }
            }
        }

        public void InsertData(SqlConnection sqlConn, List<TitleGenre> titleGenres)
        {
            foreach (TitleGenre titleGenre in titleGenres) {
                int genreID = e.GetGenreID(sqlConn, titleGenre.genreName);

                if (genreID != -1) {
                    // Insert the data into the TitlesGenres table
                    SqlCommand sqlCmd = new("INSERT INTO [dbo].[TitlesGenres]" +
                        "([tconst],[genreID])VALUES " +
                        $"('{titleGenre.tconst}',{genreID})", sqlConn);

                    try {
                        sqlCmd.ExecuteNonQuery();
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(sqlCmd.CommandText);
                        Console.ReadKey();
                    }
                } else {
                    Console.WriteLine($"Genre '{titleGenre.genreName}' not found.");
                }
            }
        }
    }
}
