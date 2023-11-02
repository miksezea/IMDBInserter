using System.Data.SqlClient;

namespace IMDBConsole.admin
{
    public interface IInserter<T>
    {
        void InsertData(SqlConnection sqlConn, List<T> datas);
    }
}
