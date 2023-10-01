namespace IMDBConsole
{
    public static class Program
    {
        // TODO: Connectionstring should be hidden from public repo.
        private static readonly string connString = "server=localhost; database=MyIMDB;" +
            "user id=sa; password=bibliotek; TrustServerCertificate=True";
        private static readonly string tsvPath = @"C:\Users\mikke\OneDrive\Desktop\4Semester\IMDBTSV";

        private static void Main(string[] args)
        {
            RunTitleInserter(1000);
        }

        private static void RunTitleInserter(int lineAmount)
        {
            string path = tsvPath + @"\title.basics.tsv\data.tsv";
            TitleInserter titleInserter = new();
            titleInserter.TitleData(path, connString, lineAmount);
        }

    }
}