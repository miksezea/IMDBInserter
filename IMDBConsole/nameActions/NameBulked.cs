using IMDBLib.nameBasics;
using System.Data;
using System.Data.SqlClient;

namespace IMDBConsole.nameActions
{
    public class NameBulked : IInserter<Name>, IInserter<Profession>, IInserter<PrimaryProfession>, IInserter<KnownForTitle>
    {
        readonly GlobalFunctions f = new();

        public void InsertData(SqlConnection sqlConn, List<Name> names)
        {
            DataTable nameTable = new("Names");

            nameTable.Columns.Add("nconst", typeof(string));
            nameTable.Columns.Add("primaryName", typeof(string));
            nameTable.Columns.Add("birthYear", typeof(int));
            nameTable.Columns.Add("deathYear", typeof(int));

            foreach (Name name in names)
            {
                DataRow nameRow = nameTable.NewRow();
                f.FillParameterBulked(nameRow, "nconst", name.nconst);
                f.FillParameterBulked(nameRow, "primaryName", name.primaryName);
                f.FillParameterBulked(nameRow, "birthYear", name.birthYear);
                f.FillParameterBulked(nameRow, "deathYear", name.deathYear);
                nameTable.Rows.Add(nameRow);
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Names";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(nameTable);
            Console.WriteLine("Names have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<Profession> professions)
        {
            int professionID = f.GetMaxId("professionID", "Professions", sqlConn);
            if (professionID == -1)
            {
                professionID = 0;
            }
            else
            {
                professionID++;
            }

            DataTable genreTable = new("Professions");

            genreTable.Columns.Add("professionID", typeof(int));
            genreTable.Columns.Add("professionName", typeof(string));

            foreach (Profession profession in professions)
            {
                DataRow genreRow = genreTable.NewRow();
                f.FillParameterBulked(genreRow, "professionID", professionID);
                f.FillParameterBulked(genreRow, "professionName", profession.professionName);
                genreTable.Rows.Add(genreRow);
                professionID++;
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "Professions";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(genreTable);
            Console.WriteLine("Professions have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<PrimaryProfession> primaryProfessions)
        {
            DataTable primaryProfessionsTable = new("PrimaryProfessions");

            primaryProfessionsTable.Columns.Add("nconst", typeof(string));
            primaryProfessionsTable.Columns.Add("professionID", typeof(int));

            foreach (PrimaryProfession primaryProfession in primaryProfessions)
            {
                DataRow primaryProfessionRow = primaryProfessionsTable.NewRow();
                f.FillParameterBulked(primaryProfessionRow, "nconst", primaryProfession.nconst);
                f.FillParameterBulked(primaryProfessionRow, "professionID", primaryProfession.professionID);
                primaryProfessionsTable.Rows.Add(primaryProfessionRow);
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "PrimaryProfessions";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(primaryProfessionsTable);
            Console.WriteLine("Primary professions have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<KnownForTitle> knownForTitles)
        {
            DataTable knownForTitlesTable = new("KnownForTitles");

            knownForTitlesTable.Columns.Add("nconst", typeof(string));
            knownForTitlesTable.Columns.Add("tconst", typeof(string));

            foreach (KnownForTitle knownForTitle in knownForTitles)
            {
                DataRow knownForTitleRow = knownForTitlesTable.NewRow();
                f.FillParameterBulked(knownForTitleRow, "nconst", knownForTitle.nconst);
                f.FillParameterBulked(knownForTitleRow, "tconst", knownForTitle.tconst);
                knownForTitlesTable.Rows.Add(knownForTitleRow);
            }
            SqlBulkCopy bulkCopy = new(sqlConn, SqlBulkCopyOptions.KeepNulls, null);
            bulkCopy.DestinationTableName = "KnownForTitles";
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.WriteToServer(knownForTitlesTable);
            Console.WriteLine("Known for titles have been inserted.");
        }
    }
}
