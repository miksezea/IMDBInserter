using IMDBConsole.admin.crewActions;
using IMDBConsole.admin.nameActions;
using IMDBConsole.admin.titleActions;
using System.Data.SqlClient;

namespace IMDBConsole.admin
{
    public class ActionsProcessor
    {
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";

#pragma warning disable S1075 // URIs should not be hardcoded
        private static readonly string tsvPath = @"C:\Users\mikke\OneDrive\Desktop\4Semester\IMDBTSV\";
#pragma warning restore S1075 // URIs should not be hardcoded

        private string _tsv = "";
        public void DatasetSelector(string tsv)
        {
            _tsv = tsv;

            Console.WriteLine(_tsv);
            Console.WriteLine();

            Console.WriteLine("Choose action:");
            Console.WriteLine("0: Check amount for " + _tsv + " data");
            Console.WriteLine("1: Delete all of " + _tsv + " In DB");
            Console.WriteLine("2: Normal insert of " + _tsv);
            Console.WriteLine("3: Prepared insert of " + _tsv);
            Console.WriteLine("4: Bulked insert of " + _tsv);
            Console.WriteLine("5: Go back");

            string? input = Console.ReadLine();


            switch (input)
            {
                case "0":
                    Console.Clear();
                    DBCount();

                    break;
                case "1":
                    Console.Clear();
                    DBDeleteRows();

                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Normal insert chosen...");
                    Console.WriteLine();
                    InsertData(1);
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Prepared insert chosen...");
                    Console.WriteLine();
                    InsertData(2);
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Bulked insert chosen...");
                    Console.WriteLine();
                    InsertData(3);
                    break;
                case "5":
                    Console.Clear();
                    Program.Main(null);
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DatasetSelector(_tsv);
                    break;
            }
        }

        public void DBCount()
        {
            DateTime before = DateTime.Now;
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();
            switch (_tsv)
            {
                case "Title.Basics":
                    TitleExtra titleExtra = new();
                    titleExtra.DBTitleCount(sqlConn);
                    break;
                case "Name.Basics":
                    NameExtra nameExtra = new();
                    nameExtra.DBNameCount(sqlConn);
                    break;
                case "Title.Crew":
                    CrewExtra crewExtra = new();
                    crewExtra.DBCrewCount(sqlConn);
                    break;
                case "Title.Akas": // Extra
                    DatasetSelector(_tsv);
                    break;
                case "Title.Principals": // Extra
                    DatasetSelector(_tsv);
                    break;
            }
            sqlConn.Close();
            DateTime after = DateTime.Now;
            Console.WriteLine("Tid: " + (after - before));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            DatasetSelector(_tsv);
        }

        public void DBDeleteRows()
        {
            DateTime before = DateTime.Now;
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();
            switch (_tsv)
            {
                case "Title.Basics":
                    TitleExtra titleExtra = new();
                    titleExtra.DBTitleDeleteRows(sqlConn);
                    break;
                case "Name.Basics":
                    NameExtra nameExtra = new();
                    nameExtra.DBNameDeleteRows(sqlConn);
                    break;
                case "Title.Crew":
                    CrewExtra crewExtra = new();
                    crewExtra.DBCrewDeleteRows(sqlConn);
                    break;
                case "Title.Akas": // Extra

                    break;
                case "Title.Principals": // Extra

                    break;
            }
            sqlConn.Close();
            DateTime after = DateTime.Now;
            Console.WriteLine("Tid: " + (after - before));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            DatasetSelector(_tsv);
        }

        public void InsertData(int inserterType)
        {
            string path = tsvPath + _tsv + @".tsv\data.tsv";

            Console.WriteLine("How many lines do you want to add? No input for all lines");
            string? input = Console.ReadLine();
            int lineAmount = 0;
            if (input != null)
            {
                lineAmount = Convert.ToInt32(input);
            }
            if (lineAmount < 0)
            {
                Console.WriteLine("Negative numbers are not allowed");
                Console.WriteLine();
                InsertData(inserterType);
            }

            switch (_tsv)
            {
                case "Title.Basics":
                    TitleInserter titleInserter = new();
                    titleInserter.InsertTitleData(connString, inserterType, path, lineAmount);
                    break;
                case "Name.Basics":
                    NameInserter nameInserter = new();
                    nameInserter.InsertNameData(connString, inserterType, path, lineAmount);
                    break;
                case "Title.Crew":
                    CrewInserter crewInserter = new();
                    crewInserter.InsertCrewData(connString, inserterType, path, lineAmount);
                    break;
                case "Title.Akas": // Extra

                    break;
                case "Title.Principals": // Extra

                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            DatasetSelector(_tsv);
        }
    }
}
