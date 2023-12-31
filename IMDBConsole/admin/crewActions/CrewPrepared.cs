﻿using IMDBLib.titleCrew;
using System.Data.SqlClient;

namespace IMDBConsole.admin.crewActions
{
    public class CrewPrepared : IInserter<Director>, IInserter<Writer>
    {
        readonly AdminFunctions f = new();

        public void InsertData(SqlConnection sqlConn, List<Director> directors)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Directors] " +
                "([nconst],[tconst])VALUES (@nconst, @tconst)", sqlConn);

            SqlParameter nconstParameter = new("@nconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(nconstParameter);

            SqlParameter tconstParameter = new("@tconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(tconstParameter);
            sqlCmd.Prepare();

            foreach (Director director in directors)
            {
                f.FillParameterPrepared(nconstParameter, director.nconst);
                f.FillParameterPrepared(tconstParameter, director.tconst);
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
            Console.WriteLine("Directors have been inserted.");
        }

        public void InsertData(SqlConnection sqlConn, List<Writer> writers)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Writers] " +
                "([nconst],[tconst])VALUES (@nconst, @tconst)", sqlConn);

            SqlParameter nconstParameter = new("@nconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(nconstParameter);

            SqlParameter tconstParameter = new("@tconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(tconstParameter);

            sqlCmd.Prepare();

            foreach (Writer writer in writers)
            {
                f.FillParameterPrepared(nconstParameter, writer.nconst);
                f.FillParameterPrepared(tconstParameter, writer.tconst);
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
            Console.WriteLine("Writers have been inserted.");
        }
    }
}
