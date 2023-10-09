using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.TitleActions
{
    public class TitleNormal : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre>
    {
        readonly ValuesProcessor v = new();
        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {
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

        public void InsertData(SqlConnection sqlConn, List<Genre> genres)
        {
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

        public void InsertData(SqlConnection sqlConn, List<TitleGenre> titleGenres)
        {
            foreach (TitleGenre titleGenre in titleGenres)
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
