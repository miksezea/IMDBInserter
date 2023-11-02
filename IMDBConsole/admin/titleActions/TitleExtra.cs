using System.Data.SqlClient;

namespace IMDBConsole.admin.titleActions
{
    public class TitleExtra
    {
        SqlConnection _sqlConn = new();
        readonly GlobalFunctions f = new();
        public void DBTitleCount(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to check?");
            Console.WriteLine("1: Titles");
            Console.WriteLine("2: Genres");
            Console.WriteLine("3: TitlesGenres");
            Console.WriteLine("4: Go back");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    f.CountTable("Titles", sqlConn);
                    break;
                case "2":
                    Console.Clear();
                    f.CountTable("Genres", sqlConn);
                    break;
                case "3":
                    Console.Clear();
                    f.CountTable("TitlesGenres", sqlConn);
                    break;
                case "4":
                    Console.Clear();
                    ActionsProcessor ap = new();
                    ap.DatasetSelector("Title.Basics");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBTitleCount(_sqlConn);
                    break;
            }
        }

        public void DBTitleDeleteRows(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to delete?");
            Console.WriteLine("1: Titles");
            Console.WriteLine("2: Genres");
            Console.WriteLine("3: TitlesGenres");
            Console.WriteLine("4: All of the above");
            Console.WriteLine("5: Go back");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    f.DeleteRows("Titles", sqlConn);
                    break;
                case "2":
                    Console.Clear();
                    f.DeleteRows("Genres", sqlConn);

                    SqlCommand reseedCmd = new("DBCC CHECKIDENT ('Genres', RESEED, 0)", _sqlConn);
                    reseedCmd.ExecuteNonQuery();
                    break;
                case "3":
                    Console.Clear();
                    f.DeleteRows("TitlesGenres", sqlConn);
                    break;
                case "4":
                    Console.Clear();
                    f.DeleteRows("TitlesGenres", sqlConn);
                    f.DeleteRows("Titles", sqlConn);
                    f.DeleteRows("Genres", sqlConn);

                    SqlCommand reseedCmd2 = new("DBCC CHECKIDENT ('Genres', RESEED, 0)", _sqlConn);
                    reseedCmd2.ExecuteNonQuery();
                    break;
                case "5":
                    Console.Clear();
                    ActionsProcessor ap = new();
                    ap.DatasetSelector("Title.Basics");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBTitleDeleteRows(_sqlConn);
                    break;
            }
        }
    }
}
