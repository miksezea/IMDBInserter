using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole
{
    internal interface IInserter
    {
        void InsertData(SqlConnection sqlConn, List<Title> titles);
    }
}
