using IMDBLib.titleCrew;
using System.Data.SqlClient;

namespace IMDBConsole.crewActions
{
    public class CrewInserter
    {
        readonly List<Director> directors = new();
        readonly List<Writer> writers = new();
        int _lineAmount = 0;
        string _path = "";
        SqlConnection sqlConn = new();
        readonly GlobalFunctions f = new();

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
                    //directorInsert = new CrewNormal();
                    //writerInsert = new CrewNormal();
                    break;
                case 2:
                    //directorInsert = new CrewPrepared();
                    //writerInsert = new CrewPrepared();
                    break;
                case 3:
                    //directorInsert = new CrewBulked();
                    //writerInsert = new CrewBulked();
                    break;
            }
            directorInsert?.InsertData(sqlConn, directors);
            writerInsert?.InsertData(sqlConn, writers);

            DateTime after = DateTime.Now;
            TimeSpan ts = after - before;
            Console.WriteLine($"Time taken: {ts}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public void MakeLists()
        {

        }
    }
}
