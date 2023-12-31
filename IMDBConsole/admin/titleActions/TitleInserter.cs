﻿using IMDBLib.titleBasics;
using System.Data.SqlClient;

namespace IMDBConsole.admin.titleActions
{
    public class TitleInserter
    {
        readonly List<Title> titles = new();
        readonly List<Genre> genres = new();
        readonly List<TitleGenre> titleGenres = new();
        int _lineAmount = 0;
        string _path = "";
        SqlConnection sqlConn = new();
        readonly HashSet<string> existingGenres = new();
        readonly AdminFunctions f = new();

        public void InsertTitleData(string connString, int inserterType, string path, int lineAmount)
        {
            DateTime before = DateTime.Now;

            _lineAmount = lineAmount;
            _path = path;
            sqlConn = new(connString);
            sqlConn.Open();

            MakeLists();

            IInserter<Title>? titleInsert = null;
            IInserter<Genre>? genreInsert = null;
            IInserter<TitleGenre>? titleGenreInsert = null;

            switch (inserterType)
            {
                case 1:
                    titleInsert = new TitleNormal();
                    genreInsert = new TitleNormal();
                    titleGenreInsert = new TitleNormal();
                    break;
                case 2:
                    titleInsert = new TitlePrepared();
                    genreInsert = new TitlePrepared();
                    titleGenreInsert = new TitlePrepared();
                    break;
                case 3:
                    titleInsert = new TitleBulked();
                    genreInsert = new TitleBulked();
                    titleGenreInsert = new TitleBulked();
                    break;
            }
            titleInsert?.InsertData(sqlConn, titles);
            genreInsert?.InsertData(sqlConn, genres);
            titleGenreInsert?.InsertData(sqlConn, titleGenres);

            sqlConn.Close();

            DateTime after = DateTime.Now;
            TimeSpan ts = after - before;

            Console.WriteLine($"Time taken: {ts}");
        }

        public void MakeLists()
        {
            IEnumerable<string> lines = File.ReadLines(_path).Skip(1);
            if (_lineAmount != 0)
            {
                lines = lines.Take(_lineAmount);
            }

            int genreID = f.GetMaxId("genreID", "Genres", sqlConn);

            foreach (string line in lines)
            {
                string[] values = line.Split("\t");

                if (values.Length == 9)
                {
                    // Titles table
                    titles.Add(new Title(values[0], values[1], values[2], values[3],
                        f.ConvertToBool(values[4]), f.ConvertToInt(values[5]),
                        f.ConvertToInt(values[6]), f.ConvertToInt(values[7])));

                    // Genres table and TitlesGenres table
                    if (values[8] != @"\N")
                    {
                        string[] genreNames = values[8].Split(",");

                        foreach (string genreName in genreNames)
                        {

                            if (!existingGenres.Contains(genreName))
                            {
                                existingGenres.Add(genreName);

                                if (genreID == -1)
                                {
                                    genreID = 1;
                                    genres.Add(new Genre(genreName, genreID));
                                }
                                else
                                {
                                    genreID++;
                                    genres.Add(new Genre(genreName, genreID));
                                }
                            }
                            titleGenres.Add(new TitleGenre(values[0], genreID));
                        }
                    }
                }
            }
            Console.WriteLine("Amount of titles: " + titles.Count);
        }
    }
}
