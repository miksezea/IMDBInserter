﻿using IMDBLib.titleCrew;
using System.Data;
using System.Data.SqlClient;

namespace IMDBConsole.admin.crewActions
{
    public class CrewBulked : IInserter<Director>, IInserter<Writer>
    {
        readonly AdminFunctions f = new();

        public void InsertData(SqlConnection sqlConn, List<Director> directors)
        {
            DataTable directorsTable = new("Directors");

            directorsTable.Columns.Add("nconst", typeof(string));
            directorsTable.Columns.Add("tconst", typeof(string));

            foreach (Director director in directors)
            {
                DataRow directorsRow = directorsTable.NewRow();
                f.FillParameterBulked(directorsRow, "nconst", director.nconst);
                f.FillParameterBulked(directorsRow, "tconst", director.tconst);
                directorsTable.Rows.Add(directorsRow);
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Directors";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(directorsTable);
            Console.WriteLine("Directors have been inserted.");
        }

        public void InsertData(SqlConnection sqlConn, List<Writer> writers)
        {
            DataTable writersTable = new("Writers");

            writersTable.Columns.Add("nconst", typeof(string));
            writersTable.Columns.Add("tconst", typeof(string));

            foreach (Writer writer in writers)
            {
                DataRow writersRow = writersTable.NewRow();
                f.FillParameterBulked(writersRow, "nconst", writer.nconst);
                f.FillParameterBulked(writersRow, "tconst", writer.tconst);
                writersTable.Rows.Add(writersRow);
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Writers";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(writersTable);
            Console.WriteLine("Writers have been inserted.");
        }
    }
}
