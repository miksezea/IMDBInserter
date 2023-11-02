using IMDBConsole;
using System.Data.SqlClient;

namespace IMDBConsole.nameActions
{
    public class NameExtra
    {
        SqlConnection _sqlConn = new();
        readonly GlobalFunctions f = new();
        public void DBNameCount(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to check?");
            Console.WriteLine("1: Names");
            Console.WriteLine("2: KnownForTitles");
            Console.WriteLine("3: PrimaryProfessions");
            Console.WriteLine("4: Professions");
            Console.WriteLine("5: Go Back");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    f.CountTable("Names", sqlConn);
                    break;
                case "2":
                    Console.Clear();
                    f.CountTable("KnownForTitles", sqlConn);
                    break;
                case "3":
                    Console.Clear();
                    f.CountTable("PrimaryProfessions", sqlConn);
                    break;
                case "4":
                    Console.Clear();
                    f.CountTable("Professions", sqlConn);
                    break;
                case "5":
                    Console.Clear();
                    ActionsProcessor ap = new();
                    ap.DatasetSelector("Name.Basics");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBNameCount(_sqlConn);
                    break;
            }
        }
        public void DBNameDeleteRows(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to delete?");
            Console.WriteLine("1: Names");
            Console.WriteLine("2: KnownForTitles");
            Console.WriteLine("3: PrimaryProfessions");
            Console.WriteLine("4: Professions");
            Console.WriteLine("5: All of the above");
            Console.WriteLine("6: Go Back");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    f.DeleteRows("Names", sqlConn);
                    break;
                case "2":
                    Console.Clear();
                    f.DeleteRows("KnownForTitles", sqlConn);
                    break;
                case "3":
                    Console.Clear();
                    f.DeleteRows("PrimaryProfessions", sqlConn);
                    break;
                case "4":
                    Console.Clear();
                    f.DeleteRows("Professions", sqlConn);

                    SqlCommand reseedCmd = new("DBCC CHECKIDENT ('Professions', RESEED, 0)", _sqlConn);
                    reseedCmd.ExecuteNonQuery();
                    break;
                case "5":
                    Console.Clear();
                    f.DeleteRows("Names", sqlConn);
                    f.DeleteRows("KnownForTitles", sqlConn);
                    f.DeleteRows("PrimaryProfessions", sqlConn);
                    f.DeleteRows("Professions", sqlConn);

                    SqlCommand reseedCmd2 = new("DBCC CHECKIDENT ('Professions', RESEED, 0)", _sqlConn);
                    reseedCmd2.ExecuteNonQuery();
                    break;
                case "6":
                    Console.Clear();
                    ActionsProcessor ap = new();
                    ap.DatasetSelector("Name.Basics");
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBNameDeleteRows(_sqlConn);
                    break;
            }
        }
    }
}
