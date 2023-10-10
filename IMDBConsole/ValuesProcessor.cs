using System.Data.SqlClient;

namespace IMDBConsole
{
    public class ValuesProcessor
    {
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

        public string? ConvertToString(string input)
        {
            if (input == @"\N")
            {
                return null;
            }
            else
            {
                return input;
            }
        }
        public void FillParameter(SqlParameter parameter, object? value)
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
    }
}
