using IMDBLib.titleBasics;
using System.Data;
using System.Data.SqlClient;

namespace IMDBConsole.titleActions
{
    public class TitleBulked : IInserter<Title>, IInserter<Genre>, IInserter<TitleGenre>
    {
        readonly ValuesProcessor v = new();
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
                v.FillParameterBulked(titleRow, "tconst", title.tconst);
                v.FillParameterBulked(titleRow, "titleType", title.titleType);
                v.FillParameterBulked(titleRow, "primaryTitle", title.primaryTitle);
                v.FillParameterBulked(titleRow, "originalTitle", title.originalTitle);
                v.FillParameterBulked(titleRow, "isAdult", title.isAdult);
                v.FillParameterBulked(titleRow, "startYear", title.startYear);
                v.FillParameterBulked(titleRow, "endYear", title.endYear);
                v.FillParameterBulked(titleRow, "runtimeMinutes", title.runtimeMinutes);
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
            int genreID = e.GetGenreMaxId(sqlConn);
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
                v.FillParameterBulked(genreRow, "genreID", genreID);
                v.FillParameterBulked(genreRow, "genreName", genre.genreName);
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
                int genreID = e.GetGenreID(sqlConn, titleGenre.genreName);

                if (genreID != -1)
                {
                    DataRow titleGenreRow = titleGenreTable.NewRow();
                    v.FillParameterBulked(titleGenreRow, "tconst", titleGenre.tconst);
                    v.FillParameterBulked(titleGenreRow, "genreID", genreID);
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
