using IMDBLib.titleBasics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBConsole.TitleActions
{
    public class TitlePrepared : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre>
    {
        readonly ValuesProcessor v = new();
        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Titles]" +
                "([tconst], [titleType], [primaryTitle], [originalTitle]," +
                "[isAdult], [startYear], [endYear], [runtimeMinutes])VALUES " +
                "$(@tconst, @titleType, @primaryTitle, @originalTitle, " +
                "$@isAdult, @startYear, @endYear, @runtimeMinutes)", sqlConn);

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
                v.FillParameter(tconstParameter, title.tconst);
                v.FillParameter(titleTypeParameter, title.titleType);
                v.FillParameter(primaryTitleParameter, title.primaryTitle);
                v.FillParameter(originalTitleParameter, title.originalTitle);
                v.FillParameter(isAdultParameter, title.isAdult);
                v.FillParameter(startYearParameter, title.startYear);
                v.FillParameter(endYearParameter, title.endYear);
                v.FillParameter(runtimeMinutesParameter, title.runtimeMinutes);
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
            throw new NotImplementedException();
        }

        public void InsertData(SqlConnection sqlConn, List<TitleGenre> titleGenres)
        {
            throw new NotImplementedException();
        }
    }
}
