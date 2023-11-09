using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.admin.titleActions
{
    public class TitleNormal : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre>
    {
        readonly AdminFunctions f = new();
        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {
            foreach (Title title in titles)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Titles]" +
                    "([tconst],[titleType],[primaryTitle],[originalTitle], " +
                    "[isAdult],[startYear],[endYear],[runtimeMinutes])VALUES " +
                    $"('{title.tconst}', " +
                    $"'{title.titleType}', " +
                    $"'{f.ConvertToSqlString(title.primaryTitle)}', " +
                    $"'{f.ConvertToSqlString(title.originalTitle)}', " +
                    $"'{title.isAdult}', " +
                    $"{f.CheckIntForNull(title.startYear)}, " +
                    $"{f.CheckIntForNull(title.endYear)}, " +
                    $"{f.CheckIntForNull(title.runtimeMinutes)})", sqlConn);
                try
                {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlCmd.CommandText);
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Titles have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<Genre> genres)
        {
            foreach (Genre genre in genres)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Genres]" +
                    "([genreName])VALUES " +
                    $"('{genre.genreName}')", sqlConn);
                try
                {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlCmd.CommandText);
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Genres have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<TitleGenre> titleGenres)
        {
            foreach (TitleGenre titleGenre in titleGenres)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[TitlesGenres]" +
                    "([tconst],[genreID])VALUES " +
                    $"('{titleGenre.tconst}',{titleGenre.genreID})", sqlConn);
                try
                {
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(sqlCmd.CommandText);
                    Console.ReadKey();
                }
            }
            Console.WriteLine("TitleGenres have been inserted.");
        }
    }
}
