using System.Data.SqlClient;

namespace IMDBConsole.nameCrewActions
{
    public class NameCrewExtra
    {
        SqlConnection _sqlConn = new();
        readonly GlobalFunctions f = new();
        public void DBNameCrewCount(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to check?");
            Console.WriteLine("1: Names");
            Console.WriteLine("2: KnownForTitles");
            Console.WriteLine("3: PrimaryProfessions");
            Console.WriteLine("4: Professions");
            Console.WriteLine("5: Directors");
            Console.WriteLine("6: Writers");

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
                    f.CountTable("Directors", sqlConn);
                    break;
                case "6":
                    Console.Clear();
                    f.CountTable("Writers", sqlConn);
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBNameCrewCount(_sqlConn);
                    break;
            }
        }
        public void DBNameCrewDeleteRows(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to delete?");
            Console.WriteLine("1: Names");
            Console.WriteLine("2: KnownForTitles");
            Console.WriteLine("3: PrimaryProfessions");
            Console.WriteLine("4: Professions");
            Console.WriteLine("5: Directors");
            Console.WriteLine("6: Writers");
            Console.WriteLine("7: All of the above");

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
                    f.DeleteRows("Directors", sqlConn);
                    break;
                case "6":
                    Console.Clear();
                    f.DeleteRows("Writers", sqlConn);
                    break;
                case "7":
                    Console.Clear();
                    f.DeleteRows("Names", sqlConn);
                    f.DeleteRows("KnownForTitles", sqlConn);
                    f.DeleteRows("PrimaryProfessions", sqlConn);
                    f.DeleteRows("Professions", sqlConn);
                    f.DeleteRows("Directors", sqlConn);
                    f.DeleteRows("Writers", sqlConn);

                    SqlCommand reseedCmd2 = new("DBCC CHECKIDENT ('Professions', RESEED, 0)", _sqlConn);
                    reseedCmd2.ExecuteNonQuery();
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
