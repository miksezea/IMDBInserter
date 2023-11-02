using IMDBConsole.titleActions;
using IMDBLib.nameBasicsAndCrew;
using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.nameCrewActions
{
    public class NameCrewInserter
    {
        readonly List<Name> names = new();
        readonly List<PrimaryProfession> primaryProfessions = new();
        readonly List<Profession> professions = new();
        readonly List<KnownForTitle> knownForTitles = new();
        readonly List<Director> directors = new();
        readonly List<Writer> writers = new();
        int _lineAmount = 0;
        int _lineAmount2 = 0;
        string _path = "";
        string _path2 = "";
        SqlConnection sqlConn = new();
        readonly HashSet<string> existingProfessions = new();
        readonly GlobalFunctions f = new();

        public void InsertNameCrewData(string connString, int inserterType, string path, int lineAmount, string path2, int lineAmount2)
        {
            DateTime before = DateTime.Now;

            _lineAmount = lineAmount;
            _path = path;
            _lineAmount2 = lineAmount2;
            _path2 = path2;
            sqlConn = new(connString);
            sqlConn.Open();

            MakeLists();

            IInserter<Name>? nameInsert = null;
            IInserter<PrimaryProfession>? primaryProfessionInsert = null;
            IInserter<Profession>? professionInsert = null;
            IInserter<KnownForTitle>? knownForTitleInsert = null;
            IInserter<Director>? directorInsert = null;
            IInserter<Writer>? writerInsert = null;

            switch (inserterType)
            {
                case 1:
                    nameInsert = new NameCrewNormal();
                    primaryProfessionInsert = new NameCrewNormal();
                    professionInsert = new NameCrewNormal();
                    knownForTitleInsert = new NameCrewNormal();
                    directorInsert = new NameCrewNormal();
                    writerInsert = new NameCrewNormal();
                    break;
                case 2:
                    nameInsert = new NameCrewPrepared();
                    primaryProfessionInsert = new NameCrewPrepared();
                    professionInsert = new NameCrewPrepared();
                    knownForTitleInsert = new NameCrewPrepared();
                    directorInsert = new NameCrewPrepared();
                    writerInsert = new NameCrewPrepared();
                    break;
                case 3:
                    nameInsert = new NameCrewBulked();
                    primaryProfessionInsert = new NameCrewBulked();
                    professionInsert = new NameCrewBulked();
                    knownForTitleInsert = new NameCrewBulked();
                    directorInsert = new NameCrewBulked();
                    writerInsert = new NameCrewBulked();
                    break;
            }
            nameInsert?.InsertData(sqlConn, names);
            primaryProfessionInsert?.InsertData(sqlConn, primaryProfessions);
            professionInsert?.InsertData(sqlConn, professions);
            knownForTitleInsert?.InsertData(sqlConn, knownForTitles);
            directorInsert?.InsertData(sqlConn, directors);
            writerInsert?.InsertData(sqlConn, writers);

            sqlConn.Close();

            DateTime after = DateTime.Now;

            Console.WriteLine("Tid: " + (after - before));
        }

        public void MakeLists()
        {
            IEnumerable<string> lines = File.ReadLines(_path).Skip(1);
            if (_lineAmount > 0)
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
                            
                            this.knownForTitles.Add(new KnownForTitle(values[0], knownForTitle));
                        }
                    }
                }
            }

            IEnumerable<string> lines2 = File.ReadLines(_path2).Skip(1);
            if (_lineAmount2 > 0)
            {
                lines2 = lines2.Take(_lineAmount2);
            }

            foreach (string line in lines2)
            {
                string[] values = line.Split("\t");

                else if (values.Length == 4)
                {
                    // Directors table
                    if (values[3] != @"\N")
                    {
                        string[] directors = values[3].Split(",");

                        foreach (string director in directors)
                        {
                            this.directors.Add(new Director(values[0], director));
                        }
                    }

                    // Writers table
                    if (values[2] != @"\N")
                    {
                        string[] writers = values[2].Split(",");

                        foreach (string writer in writers)
                        {
                            this.writers.Add(new Writer(values[0], writer));
                        }
                    }
                }   
            }

            Console.WriteLine("Amount of Names: " + names.Count);
        }
    }
}
