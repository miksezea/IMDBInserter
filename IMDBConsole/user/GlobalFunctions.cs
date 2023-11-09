using System.Data.SqlClient;

namespace IMDBConsole.user
{
    public class GlobalFunctions
    {
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
    }
}
