using IMDBConsole.titleActions;
using IMDBConsole.nameCrewActions;
using System.Data.SqlClient;

namespace IMDBConsole
{
    public class ActionsProcessor
    {
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";
        private static readonly string tsvPath = @"C:\Users\mikke\OneDrive\Desktop\4Semester\IMDBTSV\";
        private string _tsv = "";
        private string _tsv2 = "";
        public void DatasetSelector(string tsv, string? tsv2) 
        {
            _tsv = tsv;
            if (tsv2 != null)
            {
                _tsv2 = tsv2;

                Console.WriteLine(_tsv + " and " + _tsv2);
                Console.WriteLine();

                Console.WriteLine("Choose action:");
                Console.WriteLine("0: Check amount for " + _tsv + " or " + _tsv2 + " data");
                Console.WriteLine("1: Delete all of " + _tsv + " or " + _tsv2 + " In DB");
                Console.WriteLine("2: Normal insert of " + _tsv + " or " + _tsv2);
                Console.WriteLine("3: Prepared insert of " + _tsv + " or " + _tsv2);
                Console.WriteLine("4: Bulked insert of " + _tsv + " or " + _tsv2);
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
                        DatasetSelector(_tsv, _tsv2);
                        break;
                }
            }
            else
            {
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
                        DatasetSelector(_tsv, null);
                        break;
                }
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
                    NameCrewExtra nameCrewExtra = new();
                    nameCrewExtra.DBNameCrewCount(sqlConn);
                    break;
                case "Title.Akas": // TODO
                    DatasetSelector(_tsv, null);
                    break;
                case "Title.Principals": // TODO
                    DatasetSelector(_tsv, null);
                    break;
            }
            sqlConn.Close();
            DateTime after = DateTime.Now;
            Console.WriteLine("Tid: " + (after - before));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            if (_tsv2 != "")
            {
                DatasetSelector(_tsv, _tsv2);
            }
            else
            {
                DatasetSelector(_tsv, null);
            }
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
                case "Name.Basics": // TODO
                    NameCrewExtra nameCrewExtra = new();
                    nameCrewExtra.DBNameCrewDeleteRows(sqlConn);
                    break;
                case "Title.Akas": // TODO

                    break;
                case "Title.Principals": // TODO

                    break;
            }
            sqlConn.Close();
            DateTime after = DateTime.Now;
            Console.WriteLine("Tid: " + (after - before));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            if (_tsv2 != "")
            {
                DatasetSelector(_tsv, _tsv2);
            }
            else
            {
                DatasetSelector(_tsv, null);
            }
        }

        public void InsertData(int inserterType)
        {
            string path = tsvPath + _tsv + @".tsv\data.tsv";
            
            if (_tsv2 != "")
            {
                string path2 = tsvPath + _tsv2 + @".tsv\data.tsv";

                Console.WriteLine($"How many lines do you want to add from {_tsv}? No input for all lines");
                string? input = Console.ReadLine();

                int lineAmount = 0;
                if (input != null)
                {
                    lineAmount = Convert.ToInt32(input);
                    Console.WriteLine($"{input} lines selected");
                }
                else
                {
                    Console.WriteLine("All lines selected");
                }

                Console.WriteLine($"How many lines do you want to add from {_tsv2}? No input for all lines");
                string? input2 = Console.ReadLine();

                int lineAmount2 = 0;
                if (input2 != null)
                {
                    lineAmount2 = Convert.ToInt32(input2);
                    Console.WriteLine($"{input2} lines selected");
                }
                else
                {
                    Console.WriteLine("All lines selected");
                }

                NameCrewInserter nameCrewInserter = new();
                nameCrewInserter.InsertNameCrewData(connString, inserterType, path, lineAmount, path2, lineAmount2);
            }
            else
            {
                Console.WriteLine("How many lines do you want to add? No input for all lines");
                string? input = Console.ReadLine();
                int lineAmount = 0;
                if (input != null)
                {
                    lineAmount = Convert.ToInt32(input);
                }

                switch (_tsv)
                {
                    case "Title.Basics":
                        TitleInserter titleInserter = new();
                        titleInserter.InsertTitleData(connString, inserterType, path, lineAmount);
                        break;
                    case "Name.Basics": // TODO

                        break;
                    case "Title.Crew": // TODO

                        break;
                    case "Title.Akas": // TODO

                        break;
                    case "Title.Principals": // TODO

                        break;
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            if (_tsv2 != "")
            {
                DatasetSelector(_tsv, _tsv2);
            }
            else
            {
                DatasetSelector(_tsv, null);
            }
        }
    }
}
