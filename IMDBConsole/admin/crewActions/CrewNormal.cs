using IMDBLib.titleCrew;
using System.Data.SqlClient;

namespace IMDBConsole.admin.crewActions
{
    public class CrewNormal : IInserter<Director>, IInserter<Writer>
    {
        public void InsertData(SqlConnection sqlConn, List<Director> directors)
        {
            foreach (Director director in directors)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Directors]" +
                    "([nconst],[tconst])VALUES " +
                    $"('{director.nconst}','{director.tconst}')", sqlConn);

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
            foreach (Writer writer in writers)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Writers]" +
                    "([nconst],[tconst])VALUES " +
                    $"('{writer.nconst}','{writer.tconst}')", sqlConn);

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
