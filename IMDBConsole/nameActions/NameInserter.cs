using IMDBLib.nameBasics;
using System.Data.SqlClient;

namespace IMDBConsole.nameActions
{
    public class NameInserter
    {
        readonly List<Name> names = new();
        readonly List<PrimaryProfession> primaryProfessions = new();
        readonly List<Profession> professions = new();
        readonly List<KnownForTitle> knownForTitles = new();
        int _lineAmount = 0;
        string _path = "";
        SqlConnection sqlConn = new();
        readonly HashSet<string> existingProfessions = new();
        readonly GlobalFunctions f = new();

        public void InsertNameData(string connString, int inserterType, string path, int lineAmount)
        {
            DateTime before = DateTime.Now;

            _lineAmount = lineAmount;
            _path = path;

            sqlConn = new(connString);
            sqlConn.Open();

            MakeLists();

            IInserter<Name>? nameInsert = null;
            IInserter<PrimaryProfession>? primaryProfessionInsert = null;
            IInserter<Profession>? professionInsert = null;
            IInserter<KnownForTitle>? knownForTitleInsert = null;

            switch (inserterType)
            {
                case 1:
                    nameInsert = new NameNormal();
                    primaryProfessionInsert = new NameNormal();
                    professionInsert = new NameNormal();
                    knownForTitleInsert = new NameNormal();
                    break;
                case 2:
                    nameInsert = new NamePrepared();
                    primaryProfessionInsert = new NamePrepared();
                    professionInsert = new NamePrepared();
                    knownForTitleInsert = new NamePrepared();
                    break;
                case 3:
                    nameInsert = new NameBulked();
                    primaryProfessionInsert = new NameBulked();
                    professionInsert = new NameBulked();
                    knownForTitleInsert = new NameBulked();
                    break;
            }
            nameInsert?.InsertData(sqlConn, names);
            primaryProfessionInsert?.InsertData(sqlConn, primaryProfessions);
            professionInsert?.InsertData(sqlConn, professions);
            knownForTitleInsert?.InsertData(sqlConn, knownForTitles);

            sqlConn.Close();

            DateTime after = DateTime.Now;

            Console.WriteLine("Tid: " + (after - before));
        }

        public void MakeLists()
        {
            IEnumerable<string> lines = File.ReadLines(_path).Skip(1);
            if (_lineAmount != 0)
            {
                lines = lines.Take(_lineAmount);
            }

            foreach (string line in lines)
            {
                string[] values = line.Split("\t");

                if (values.Length == 6)
                {
                    // Name table
                    names.Add(new Name(values[0], values[2], f.ConvertToInt(values[3]), f.ConvertToInt(values[4])));

                    // Professions table and PrimaryProfessions table
                    if (values[5] != @"\N")
                    {
                        string[] professionNames = values[8].Split(",");

                        foreach (string professionName in professionNames)
                        {
                            if (!existingProfessions.Contains(professionName))
                            {
                                existingProfessions.Add(professionName);

                                int professionID = f.GetMaxId("professionID", "Professions", sqlConn);

                                if (professionID == -1)
                                {
                                    SqlCommand insertProfessionCmd = new("INSERT INTO [dbo].[Professions]" +
                                        "([professionName])VALUES " +
                                        $"('{professionName}')", sqlConn);
                                    insertProfessionCmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    professions.Add(new Profession(professionName));
                                }
                            }
                            primaryProfessions.Add(new PrimaryProfession(values[0], professionName));
                        }
                    }

                    // KnownForTitles table
                    if (values[6] != @"\N")
                    {
                        string[] knownForTitles = values[6].Split(",");

                        foreach (string knownForTitle in knownForTitles)
                        {
                            bool tconstExists = f.CheckForTconst(knownForTitle, sqlConn);
                            if (tconstExists)
                            {
                                this.knownForTitles.Add(new KnownForTitle(values[0], knownForTitle));
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Amount of Names: " + names.Count);
        }
    }
}
