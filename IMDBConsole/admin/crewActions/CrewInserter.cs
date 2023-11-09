using IMDBLib.titleCrew;
using System.Data.SqlClient;
using System.Linq;

namespace IMDBConsole.admin.crewActions
{
    public class CrewInserter
    {
        readonly List<Director> directors = new();
        readonly List<Writer> writers = new();
        int _lineAmount = 0;
        string _path = "";
        SqlConnection sqlConn = new();
        readonly AdminFunctions f = new();

        public void InsertCrewData(string connString, int inserterType, string path, int lineAmount)
        {
            DateTime before = DateTime.Now;

            _lineAmount = lineAmount;
            _path = path;

            sqlConn = new(connString);
            sqlConn.Open();

            MakeLists();

            IInserter<Director>? directorInsert = null;
            IInserter<Writer>? writerInsert = null;

            switch (inserterType)
            {
                case 1:
                    directorInsert = new CrewNormal();
                    writerInsert = new CrewNormal();
                    break;
                case 2:
                    directorInsert = new CrewPrepared();
                    writerInsert = new CrewPrepared();
                    break;
                case 3:
                    directorInsert = new CrewBulked();
                    writerInsert = new CrewBulked();
                    break;
            }
            directorInsert?.InsertData(sqlConn, directors);
            writerInsert?.InsertData(sqlConn, writers);

            DateTime after = DateTime.Now;
            TimeSpan ts = after - before;
            Console.WriteLine($"Time taken: {ts}");
        }

        public void MakeLists()
        {
            List<string> tconsts = new();
            List<string> nconsts = new();

            f.TconstFromDBToList(tconsts, sqlConn);
            f.NconstFromDBToList(nconsts, sqlConn);

            IEnumerable<string> lines = File.ReadLines(_path).Skip(1);
            if (_lineAmount != 0)
            {
                lines = lines.Take(_lineAmount);
            }

            foreach (string line in lines)
            {
                string[] values = line.Split("\t");

                if (values.Length == 3 && tconsts.Contains(values[0]))
                {
                    // Directors table
                    if (values[1] != @"\N")
                    {
                        string[] directorsArray = values[1].Split(",");
                        this.directors.AddRange(from string director in directorsArray
                                                where nconsts.Contains(director)
                                                select new Director(director, values[0]));
                    }

                    // Writers table
                    if (values[2] != @"\N")
                    {
                        string[] writersArray = values[2].Split(",");
                        this.writers.AddRange(from string writer in writersArray
                                              where nconsts.Contains(writer)
                                              select new Writer(writer, values[0]));
                    }
                }
            }
        }
    }
}
