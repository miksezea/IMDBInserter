using System.Data.SqlClient;

namespace IMDBConsole
{
    public interface IInserter<T>
    {
        void InsertData(SqlConnection sqlConn, List<T> datas);
    }
}
