using IMDBLib.nameBasics;
using System.Data.SqlClient;

namespace IMDBConsole.nameActions
{
    public class NameNormal : IInserter<Name>, IInserter<Profession>, IInserter<PrimaryProfession>, IInserter<KnownForTitle>
    {
        readonly GlobalFunctions f = new();

        public void InsertData(SqlConnection sqlConn, List<Name> names)
        {
            foreach (Name name in names)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Names]" +
                    "([nconst],[primaryName],[birthYear],[deathYear])VALUES " +
                    $"('{name.nconst}'," +
                    $"'{f.ConvertToSqlString(name.primaryName)}'," +
                    $"'{f.CheckIntForNull(name.birthYear)}'," +
                    $"'{f.CheckIntForNull(name.deathYear)}'", sqlConn);

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
        public void InsertData(SqlConnection sqlConn, List<Profession> professions)
        {
            foreach (Profession profession in professions)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[Professions]" +
                    "([professionName])VALUES " +
                    $"('{profession.professionName}')", sqlConn);

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
        public void InsertData(SqlConnection sqlConn, List<PrimaryProfession> primaryProfessions)
        {
            foreach (PrimaryProfession primaryProfession in primaryProfessions)
            {
                int professionID = f.GetID("professionID", "Professions", "professionName", primaryProfession.professionName, sqlConn);

                if (professionID != -1)
                {
                    SqlCommand sqlCmd = new("INSERT INTO [dbo].[PrimaryProfessions]" +
                        "([nconst],[professionID])VALUES " +
                        $"('{primaryProfession.nconst}',{professionID})", sqlConn);

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
                    Console.WriteLine($"Profession '{primaryProfession.professionName}' not found.");
                }
            }
        }
        public void InsertData(SqlConnection sqlConn, List<KnownForTitle> knownForTitles)
        {
            foreach(KnownForTitle knownForTitle in knownForTitles)
            {
                SqlCommand sqlCmd = new("INSERT INTO [dbo].[KnownForTitles]" +
                    "([nconst],[tconst])VALUES " +
                    $"('{knownForTitle.nconst}','{knownForTitle.tconst}')", sqlConn);

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
    }
}
