using IMDBLib.models;

namespace IMDBConsole
{
    public static class Program
    {
        // TODO: Connectionstring should be hidden from public repo.
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";
        private static readonly string tsvPathLapTop = @"C:\Users\mikke\Desktop\Database\title.basics.tsv\data.tsv";
        private static readonly string tsvPathDesktop = @"C:\Users\mikke\Desktop\title.basics.tsv\data.tsv";

        private static void Main(string[] args)
        {
            RunTitleInserter(1000, tsvPathDesktop);
        }

        private static void RunTitleInserter(int lineAmount, string path)
        {
            TitleInserter titleInserter = new();
            titleInserter.TitleData(path, connString, lineAmount);
        }

    }
}