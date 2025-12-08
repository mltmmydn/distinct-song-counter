using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        const string input = "exhibitA-input.csv";
        const string output = "output.csv";
        var target = new DateTime(2016, 8, 10);

        var userSongs = File.ReadLines(input)
            .Skip(1)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.Split(new[] { '\t', ',' }))
            .Where(p => p.Length >= 4 && DateTime.TryParseExact(
                p[3].Trim(),
                "dd/MM/yyyy HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            .Select(p => new
            {
                Song = p[1].Trim(),
                Client = p[2].Trim(),
                Date = DateTime.ParseExact(p[3].Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
            })
            .Where(x => x.Date.Date == target.Date)
            .GroupBy(x => x.Client)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Song).Distinct().Count());

        var distribution = userSongs
            .GroupBy(x => x.Value)
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Count());

        using var writer = new StreamWriter(output);
        writer.WriteLine("DISTINCT_PLAY_COUNT,CLIENT_COUNT");
        foreach (var kv in distribution)
            writer.WriteLine($"{kv.Key},{kv.Value}");

        Console.WriteLine("Done");
        Console.WriteLine("Q2 (346 songs): " + distribution.GetValueOrDefault(346));
        Console.WriteLine("Q3 (max): " + (distribution.Any() ? distribution.Keys.Max() : 0));
    }
}
