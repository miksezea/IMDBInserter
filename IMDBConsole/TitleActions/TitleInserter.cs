using IMDBLib.titleBasics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBConsole.TitleActions
{
    public class TitleInserter
    {
        public void InsertTitleData(string connString, int inserterType, string path, int lineAmount)
        {
            DateTime before = DateTime.Now;

            List<Title> titles = new();
            List<Genre> genres = new();
            List<TitleGenre> titleGenres = new();

            MakeLists(path, lineAmount, titles, genres, titleGenres);

            SqlConnection sqlConn = new(connString);
            sqlConn.Open();

            IInserter<Title>? titleNormal = null;
            IInserter<Genre>? genreNormal = null;
            IInserter<TitleGenre>? titleGenreNormal = null;

            switch (inserterType)
            {
                case 1:
                    titleNormal = new TitleNormal();
                    genreNormal = new TitleNormal();
                    titleGenreNormal = new TitleNormal();
                    break;
            }

            if (titleNormal != null)
            {
                titleNormal.InsertData(sqlConn, titles);
                genreNormal.InsertData(sqlConn, genres);
                titleGenreNormal.InsertData(sqlConn, titleGenres);
            }
            
        }
        public void MakeLists(string path, int lineAmount, List<Title> titles, List<Genre> genres, List<TitleGenre> titleGenres)
        {
            ValuesProcessor v = new();
            HashSet<string> existingGenres = new();

            IEnumerable<string> lines = File.ReadLines(path).Skip(1);

            if (lineAmount > 0)
            {
                lines = lines.Take(lineAmount);
            }

            foreach (
                string line in lines
                )
            {
                string[] values = line.Split("\t");

                if (values.Length == 9)
                {
                    // Titles table
                    titles.Add(new Title(values[0], values[1], values[2], values[3],
                        v.ConvertToBool(values[4]), v.ConvertToInt(values[5]),
                        v.ConvertToInt(values[6]), v.ConvertToInt(values[7])
                        ));

                    // Genres table
                    if (values[8] != @"\N")
                    {
                        string[] genreNames = values[8].Split(",");

                        foreach (string genreName in genreNames)
                        {
                            if (!existingGenres.Contains(genreName))
                            {
                                genres.Add(new Genre(genreName));

                                existingGenres.Add(genreName);
                            }
                        }
                    }

                    // TitlesGenres table
                    string[] genreForTitle = values[8].Split(",");

                    foreach (string genreName in genreForTitle)
                    {
                        titleGenres.Add(new TitleGenre(values[0], v.ConvertToString(genreName)));
                    }
                }
            }
            Console.WriteLine("Amount of titles: " + titles.Count);
        }
    }
}
