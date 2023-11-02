﻿using System.Data;
using System.Data.SqlClient;

namespace IMDBConsole.admin
{
    public class GlobalFunctions
    {
        public void CountTable(string table, SqlConnection sqlConn)
        {
            SqlCommand cmd = new($"SELECT COUNT(*) FROM {table}", sqlConn);
            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine($"There are {count} rows in {table}.");
            Console.WriteLine();
        }
        public void DeleteRows(string table, SqlConnection sqlConn)
        {
            SqlCommand cmd = new($"DELETE FROM {table}", sqlConn);
            cmd.ExecuteNonQuery();

            Console.WriteLine($"Deleted all rows in {table}.");
            Console.WriteLine();
        }
        public int GetMaxId(string tableID, string tableName, SqlConnection sqlConn)
        {
            SqlCommand maxCmd = new($"SELECT MAX({tableID}) FROM [dbo].[{tableName}]", sqlConn);
            object result = maxCmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }
            else
            {
                // Object not found
                return -1;
            }
        }
        public int GetID(string tableID, string tableName, string columnName, string name, SqlConnection sqlConn)
        {
            SqlCommand sqlCmd = new($"SELECT [{tableID}] FROM [dbo].[{tableName}] WHERE [{columnName}] = '{name}'", sqlConn);
            object result = sqlCmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }
            else
            {
                // Object not found
                return -1;
            }
        }
        public void TconstFromDBToList(List<string> tconstList, SqlConnection sqlConn)
        {
            SqlCommand sqlCmd = new($"SELECT [tconst] FROM [dbo].[Titles]", sqlConn);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                string tconst = reader.GetString(0);
                tconstList.Add(tconst);
            }
            reader.Close();
        }
        public void NconstFromDBToList(List<string> nconstList, SqlConnection sqlConn)
        {
            SqlCommand sqlCmd = new($"SELECT [nconst] FROM [dbo].[Names]", sqlConn);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                string nconst = reader.GetString(0);
                nconstList.Add(nconst);
            }
            reader.Close();
        }

        public bool ConvertToBool(string input)
        {
            if (input == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
            throw new ArgumentException("Ikke 0 eller 1: " + input);
        }
        public int? ConvertToInt(string input)
        {
            if (input == @"\N")
            {
                return null;
            }
            else
            {
                return int.Parse(input);
            }
        }
        public string CheckIntForNull(int? input)
        {
            if (input == null)
            {
                return "NULL";
            }
            else
            {
                return "" + input;
            }
        }
        public string ConvertToSqlString(string input)
        {
            return input.Replace("'", "''");
        }

        public void FillParameterPrepared(SqlParameter parameter, object? value)
        {
            if (value != null)
            {
                parameter.Value = value;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }
        public void FillParameterBulked(DataRow row, string columnName, object? value)
        {
            if (value != null)
            {
                row[columnName] = value;
            }
            else
            {
                row[columnName] = DBNull.Value;
            }
        }
    }
}