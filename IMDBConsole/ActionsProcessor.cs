using IMDBConsole.TitleActions;
using System.Data.SqlClient;

namespace IMDBConsole
{
    public class ActionsProcessor
    {
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";
        private static readonly string tsvPath = @"C:\Users\mikke\OneDrive\Desktop\4Semester\IMDBTSV\";
        public void DatasetSelector(string tsv) {
            Console.WriteLine(tsv);
            Console.WriteLine();

            Console.WriteLine("Choose action:");
            Console.WriteLine("0: Check amount for " + tsv + " data");
            Console.WriteLine("1: Delete all of " + tsv + " In DB");
            Console.WriteLine("2: Normal insert of " + tsv);
            Console.WriteLine("3: Prepared insert of " + tsv);
            Console.WriteLine("4: Bulked insert" + tsv);
            Console.WriteLine("5: Go back");

            string? input = Console.ReadLine();


            switch (input)
            {
                case "0":
                    Console.Clear();
                    DBCount(tsv);

                    break;
                case "1":
                    Console.Clear();
                    DBDeleteRows(tsv);

                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Normal insert chosen...");
                    Console.WriteLine();
                    InsertData(tsv, connString, 1);
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Prepared insert chosen...");
                    Console.WriteLine();
                    InsertData(tsv, connString, 2);
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Bulked insert chosen...");
                    Console.WriteLine();
                    InsertData(tsv, connString, 3);
                    break;
                case "5":
                    Console.Clear();
                    Program.Main(null);
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DatasetSelector(tsv);
                    break;
            }
        }

        public void DBCount(string tsv)
        {
            DateTime before = DateTime.Now;
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();
            switch (tsv)
            {
                case "Title.Basics":
                    TitleExtra titleExtra = new();
                    titleExtra.DBTitleCount(sqlConn);
                    break;
            }
            sqlConn.Close();
            DateTime after = DateTime.Now;
            Console.WriteLine("Tid: " + (after - before));
        }

        public void DBDeleteRows(string tsv)
        {
            DateTime before = DateTime.Now;
            SqlConnection sqlConn = new(connString);
            sqlConn.Open();
            switch (tsv)
            {
                case "Title.Basics":
                    TitleExtra titleExtra = new();
                    titleExtra.DBTitleDeleteRows(sqlConn);
                    break;
            }
            sqlConn.Close();
            DateTime after = DateTime.Now;
            Console.WriteLine("Tid: " + (after - before));
        }

        public void InsertData(string tsv, string connString, int inserterType)
        {
            string path = tsvPath + tsv + @".tsv\data.tsv";

            Console.WriteLine("How many lines do you want to add? No input for all lines");
            string? input = Console.ReadLine();
            int lineAmount = 0;
            if (input != null)
            {
                lineAmount = Convert.ToInt32(input);
            }

            TitleInserter titleInserter = new();

            switch (tsv)
            {
                case "Title.Basics":

                    titleInserter.InsertTitleData(connString, inserterType, path, lineAmount);
                    break;
            }
        }
    }
}
