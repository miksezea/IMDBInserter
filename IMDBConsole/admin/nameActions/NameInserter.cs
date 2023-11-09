using IMDBLib.nameBasics;
using System.Data.SqlClient;

namespace IMDBConsole.admin.nameActions
{
    public class NameInserter
    {
        readonly List<Name> names = new();
        readonly List<Profession> professions = new();
        readonly List<PrimaryProfession> primaryProfessions = new();
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
            IInserter<Profession>? professionInsert = null;
            IInserter<PrimaryProfession>? primaryProfessionInsert = null;
            IInserter<KnownForTitle>? knownForTitleInsert = null;

            switch (inserterType)
            {
                case 1:
                    nameInsert = new NameNormal();
                    professionInsert = new NameNormal();
                    primaryProfessionInsert = new NameNormal();
                    knownForTitleInsert = new NameNormal();
                    break;
                case 2:
                    nameInsert = new NamePrepared();
                    professionInsert = new NamePrepared();
                    primaryProfessionInsert = new NamePrepared();
                    knownForTitleInsert = new NamePrepared();
                    break;
                case 3:
                    nameInsert = new NameBulked();
                    professionInsert = new NamePrepared();
                    primaryProfessionInsert = new NameBulked();
                    knownForTitleInsert = new NameBulked();
                    break;
            }
            nameInsert?.InsertData(sqlConn, names);
            professionInsert?.InsertData(sqlConn, professions);
            primaryProfessionInsert?.InsertData(sqlConn, primaryProfessions);
            knownForTitleInsert?.InsertData(sqlConn, knownForTitles);

            sqlConn.Close();

            DateTime after = DateTime.Now;
            TimeSpan ts = after - before;

            Console.WriteLine($"Time taken: {ts}");
        }

        public void MakeLists()
        {
            List<string> tconsts = new();
            f.TconstFromDBToList(tconsts, sqlConn);

            IEnumerable<string> lines = File.ReadLines(_path).Skip(1);
            if (_lineAmount != 0)
            {
                lines = lines.Take(_lineAmount);
            }

            int professionID = f.GetMaxId("professionID", "Professions", sqlConn);

            foreach (string line in lines)
            {
                string[] values = line.Split("\t");

                if (values.Length == 6)
                {
                    // Name table
                    names.Add(new Name(values[0], values[1], f.ConvertToInt(values[2]), f.ConvertToInt(values[3])));

                    // Professions table and PrimaryProfessions table
                    if (values[4] != @"\N")
                    {
                        string[] professionNames = values[4].Split(",");

                        foreach (string professionName in professionNames)
                        {
                            if (!existingProfessions.Contains(professionName))
                            {
                                existingProfessions.Add(professionName);

                                if (professionID == -1)
                                {
                                    professionID = 1;
                                    professions.Add(new Profession(professionName, professionID));
                                }
                                else
                                {
                                    professionID++;
                                    professions.Add(new Profession(professionName, professionID));
                                }
                            }
                            primaryProfessions.Add(new PrimaryProfession(values[0], professionID));
                        }
                    }

                    // KnownForTitles table
                    if (values[5] != @"\N")
                    {
                        string[] knownForTitlesArray = values[5].Split(",");

                        foreach (string knownForTitle in knownForTitlesArray)
                        {
                            if (tconsts.Contains(knownForTitle))
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
