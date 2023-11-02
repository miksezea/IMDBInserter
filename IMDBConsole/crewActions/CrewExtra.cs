﻿using System.Data.SqlClient;

namespace IMDBConsole.crewActions
{
    public class CrewExtra
    {
        SqlConnection _sqlConn = new();
        readonly GlobalFunctions f = new();
        public void DBCrewCount(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to check?");
            Console.WriteLine("1: Directors");
            Console.WriteLine("2: Writers");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    f.CountTable("Directors", sqlConn);
                    break;
                case "2":
                    Console.Clear();
                    f.CountTable("Writers", sqlConn);
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBCrewCount(_sqlConn);
                    break;
            }
        }
        public void DBCrewDeleteRows(SqlConnection sqlConn)
        {
            _sqlConn = sqlConn;
            Console.WriteLine("Which Table do you want to delete?");
            Console.WriteLine("1: Directors");
            Console.WriteLine("2: Writers");
            Console.WriteLine("3: Both");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    f.DeleteRows("Directors", sqlConn);
                    break;
                case "2":
                    Console.Clear();
                    f.DeleteRows("Writers", sqlConn);
                    break;
                case "3":
                    Console.Clear();
                    f.DeleteRows("Directors", sqlConn);
                    f.DeleteRows("Writers", sqlConn);
                    break;
                default:
                    Console.WriteLine($"{input} is not a valid option.");
                    Console.WriteLine();
                    DBCrewDeleteRows(_sqlConn);
                    break;
            }
        }

    }
}
