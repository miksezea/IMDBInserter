using IMDBLib.nameBasics;
using System.Data.SqlClient;

namespace IMDBConsole.nameActions
{
    public class NamePrepared : IInserter<Name>, IInserter<Profession>, IInserter<PrimaryProfession>, IInserter<KnownForTitle>
    {
        readonly GlobalFunctions f = new();

        public void InsertData(SqlConnection sqlConn, List<Name> names)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Names]" +
                "([nconst],[primaryName],[birthYear],[deathYear])VALUES " +
                "(@nconst, @primaryName, @birthYear, @deathYear", sqlConn);

            SqlParameter nconstParameter = new("@nconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(nconstParameter);

            SqlParameter primaryNameParameter = new("@primaryName", System.Data.SqlDbType.VarChar, 8000);
            sqlCmd.Parameters.Add(primaryNameParameter);

            SqlParameter birthYearParameter = new("@birthYear", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(birthYearParameter);

            SqlParameter deathYearParameter = new("@deathYear", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(deathYearParameter);

            sqlCmd.Prepare();

            foreach (Name name in names)
            {
                f.FillParameterPrepared(nconstParameter, name.nconst);
                f.FillParameterPrepared(primaryNameParameter, name.primaryName);
                f.FillParameterPrepared(birthYearParameter, name.birthYear);
                f.FillParameterPrepared(deathYearParameter, name.deathYear);
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
            Console.WriteLine("Names have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<Profession> professions)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[Professions]" +
                "([professionName])VALUES (@professionName)", sqlConn);

            SqlParameter professionNameParameter = new("@professionName", System.Data.SqlDbType.VarChar, 50);
            sqlCmd.Parameters.Add(professionNameParameter);

            sqlCmd.Prepare();

            foreach (Profession profession in professions)
            {
                f.FillParameterPrepared(professionNameParameter, profession.professionName);
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
            Console.WriteLine("Professions have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<PrimaryProfession> primaryProfessions)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[PrimaryProfessions] " +
                "([nconst],[professionID])VALUES (@nconst, @professionID)", sqlConn);

            SqlParameter professionNameParameter = new("@nconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(professionNameParameter);

            SqlParameter professionIDParameter = new("@professionID", System.Data.SqlDbType.Int);
            sqlCmd.Parameters.Add(professionIDParameter);

            sqlCmd.Prepare();

            foreach (PrimaryProfession primaryProfession in primaryProfessions)
            {
                f.FillParameterPrepared(professionNameParameter, primaryProfession.nconst);
                f.FillParameterPrepared(professionIDParameter, primaryProfession.professionID);
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
            Console.WriteLine("Primary professions have been inserted.");
        }
        public void InsertData(SqlConnection sqlConn, List<KnownForTitle> knownForTitles)
        {
            SqlCommand sqlCmd = new("" +
                "INSERT INTO [dbo].[KnownForTitles] " +
                "([nconst],[tconst])VALUES (@nconst, @tconst)", sqlConn);

            SqlParameter nconstParameter = new("@nconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(nconstParameter);

            SqlParameter tconstParameter = new("@tconst", System.Data.SqlDbType.VarChar, 10);
            sqlCmd.Parameters.Add(tconstParameter);

            sqlCmd.Prepare();

            foreach (KnownForTitle knownForTitle in knownForTitles)
            {
                f.FillParameterPrepared(nconstParameter, knownForTitle.nconst);
                f.FillParameterPrepared(tconstParameter, knownForTitle.tconst);
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
            Console.WriteLine("Known for titles have been inserted.");
        }
    }
}
