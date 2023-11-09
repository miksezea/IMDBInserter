namespace IMDBConsole.user
{
    public class UserFunctions
    {
        public string ConvertToSqlString(string input)
        {
            return input.Replace("'", "''");
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
    }
}
