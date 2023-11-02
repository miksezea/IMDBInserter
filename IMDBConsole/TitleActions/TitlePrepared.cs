using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.titleActions
{
    public class TitlePrepared : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre>
    {
        readonly GlobalFunctions f = new();

        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Titles]" +
                "([tconst], [titleType], [primaryTitle], [originalTitle]," +
                "[isAdult], [startYear], [endYear], [runtimeMinutes])VALUES " +
                $"(@tconst, @titleType, @primaryTitle, @originalTitle, " +
                $"@isAdult, @startYear, @endYear, @runtimeMinutes)", sqlConn);

            SqlParameter tconstParameter = new("@tconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(tconstParameter);

            SqlParameter titleTypeParameter = new("@titleType", System.Data.SqlDbType.VarChar, 50);
            sqlCmd.Parameters.Add(titleTypeParameter);

            SqlParameter primaryTitleParameter = new("@primaryTitle", System.Data.SqlDbType.VarChar, 8000);
            sqlCmd.Parameters.Add(primaryTitleParameter);

            SqlParameter originalTitleParameter = new("@originalTitle", System.Data.SqlDbType.VarChar, 8000);
            sqlCmd.Parameters.Add(originalTitleParameter);

            SqlParameter isAdultParameter = new("@isAdult", System.Data.SqlDbType.Bit);
            sqlCmd.Parameters.Add(isAdultParameter);

            SqlParameter startYearParameter = new("@startYear", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(startYearParameter);

            SqlParameter endYearParameter = new("@endYear", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(endYearParameter);

            SqlParameter runtimeMinutesParameter = new("@runtimeMinutes", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(runtimeMinutesParameter);

            sqlCmd.Prepare();

            foreach (Title title in titles)
            {
                f.FillParameterPrepared(tconstParameter, title.tconst);
                f.FillParameterPrepared(titleTypeParameter, title.titleType);
                f.FillParameterPrepared(primaryTitleParameter, title.primaryTitle);
                f.FillParameterPrepared(originalTitleParameter, title.originalTitle);
                f.FillParameterPrepared(isAdultParameter, title.isAdult);
                f.FillParameterPrepared(startYearParameter, title.startYear);
                f.FillParameterPrepared(endYearParameter, title.endYear);
                f.FillParameterPrepared(runtimeMinutesParameter, title.runtimeMinutes);
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
        }

        public void InsertData(SqlConnection sqlConn, List<Genre> genres)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Genres]" +
                "([genreName])VALUES (@genreName)", sqlConn);

            SqlParameter genreNameParameter = new("@genreName", System.Data.SqlDbType.VarChar, 50);
            sqlCmd.Parameters.Add(genreNameParameter);

            sqlCmd.Prepare();

            foreach (Genre genre in genres)
            {
                f.FillParameterPrepared(genreNameParameter, genre.genreName);
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
        }

        public void InsertData(SqlConnection sqlConn, List<TitleGenre> titleGenres)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[TitlesGenres]" +
                "([tconst],[genreID])VALUES (@tconst, @genreID)", sqlConn);
            
            SqlParameter tconstParameter = new("@tconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(tconstParameter);

            SqlParameter genreIDParameter = new("@genreID", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(genreIDParameter);

            sqlCmd.Prepare();

            foreach (TitleGenre titleGenre in titleGenres)
            {
                int genreID = f.GetID("genreID", "Genres", "genreName", titleGenre.genreName, sqlConn);

                if (genreID != -1)
                {
                    f.FillParameterPrepared(tconstParameter, titleGenre.tconst);
                    f.FillParameterPrepared(genreIDParameter, genreID);

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
                else
                {
                    Console.WriteLine($"Genre '{titleGenre.genreName}' not found.");
                }
            }
        }
    }
}
