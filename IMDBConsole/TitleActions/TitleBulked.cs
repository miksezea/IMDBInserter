using IMDBLib.titleBasics;
using System.Data;
using System.Data.SqlClient;

namespace IMDBConsole.titleActions
{
    public class TitleBulked : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre>
    {
        readonly GlobalFunctions f = new();
        readonly TitleExtra e = new();
        public void InsertData(SqlConnection sqlConn, List<Title> titles)
        {
            DataTable titleTable = new("Titles");

            titleTable.Columns.Add("tconst", typeof(string));
            titleTable.Columns.Add("titleType", typeof(string));
            titleTable.Columns.Add("primaryTitle", typeof(string));
            titleTable.Columns.Add("originalTitle", typeof(string));
            titleTable.Columns.Add("isAdult", typeof(bool));
            titleTable.Columns.Add("startYear", typeof(int));
            titleTable.Columns.Add("endYear", typeof(int));
            titleTable.Columns.Add("runtimeMinutes", typeof(int));

            foreach (Title title in titles)
            {
                DataRow titleRow = titleTable.NewRow();
                f.FillParameterBulked(titleRow, "tconst", title.tconst);
                f.FillParameterBulked(titleRow, "titleType", title.titleType);
                f.FillParameterBulked(titleRow, "primaryTitle", title.primaryTitle);
                f.FillParameterBulked(titleRow, "originalTitle", title.originalTitle);
                f.FillParameterBulked(titleRow, "isAdult", title.isAdult);
                f.FillParameterBulked(titleRow, "startYear", title.startYear);
                f.FillParameterBulked(titleRow, "endYear", title.endYear);
                f.FillParameterBulked(titleRow, "runtimeMinutes", title.runtimeMinutes);
                titleTable.Rows.Add(titleRow);
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Titles";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(titleTable);
        }
        // This function only sends the very first genre. I want it to send all genres.
        public void InsertData(SqlConnection sqlConn, List<Genre> genres)
        {
            int genreID = f.GetMaxId("genreID", "Genres", sqlConn);
            if (genreID == -1)
            {
                genreID = 0;
            }
            else
            {
                genreID++;
            }

            DataTable genreTable = new("Genres");

            genreTable.Columns.Add("genreID", typeof(int));
            genreTable.Columns.Add("genreName", typeof(string));

            foreach (Genre genre in genres)
            {
                DataRow genreRow = genreTable.NewRow();
                f.FillParameterBulked(genreRow, "genreID", genreID);
                f.FillParameterBulked(genreRow, "genreName", genre.genreName);
                genreTable.Rows.Add(genreRow);
                genreID++;
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Genres";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(genreTable);
        }

        public void InsertData(SqlConnection sqlConn, List<TitleGenre> titleGenres)
        {
            DataTable titleGenreTable = new("TitlesGenres");

            titleGenreTable.Columns.Add("tconst", typeof(string));
            titleGenreTable.Columns.Add("genreID", typeof(int));

            foreach (TitleGenre titleGenre in titleGenres)
            {
                int genreID = f.GetID("genreID", "Genres", "genreName", titleGenre.genreName, sqlConn);

                if (genreID != -1)
                {
                    DataRow titleGenreRow = titleGenreTable.NewRow();
                    f.FillParameterBulked(titleGenreRow, "tconst", titleGenre.tconst);
                    f.FillParameterBulked(titleGenreRow, "genreID", genreID);
                    titleGenreTable.Rows.Add(titleGenreRow);
                }
                else
                {
                    Console.WriteLine($"Genre '{titleGenre.genreName}' not found.");
                }
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "TitlesGenres";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(titleGenreTable);
        }
    }
}
