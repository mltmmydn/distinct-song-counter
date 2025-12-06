using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputPath = "exhibit-a.txt";
        string outputPath = "output.txt";

        DateTime targetDate = new DateTime(2016, 8, 10);

        var userSongs = new Dictionary<string, HashSet<string>>();

        foreach (var line in File.ReadLines(inputPath).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue; 
          
            var parts = line.Split(new char[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 4) continue;

            string songId = parts[1].Trim();
            string clientId = parts[2].Trim();
            string timestamp = parts[3].Trim();

            if (!DateTime.TryParseExact(timestamp,
                    new string[] { "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime playTime))
                continue;

            if (playTime.Date != targetDate.Date)
                continue;

            if (!userSongs.ContainsKey(clientId))
                userSongs[clientId] = new HashSet<string>();

            userSongs[clientId].Add(songId);
        }

        var distribution = userSongs
            .GroupBy(kv => kv.Value.Count)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

        using var writer = new StreamWriter(outputPath);
        writer.WriteLine("DISTINCT_PLAY_COUNT\tCLIENT_COUNT");
        foreach (var kv in distribution)
            writer.WriteLine($"{kv.Key}\t{kv.Value}");

        Console.WriteLine("Processing completed. Results written to: " + outputPath);

        int maxDistinct = distribution.Keys.Any() ? distribution.Keys.Max() : 0;
        int usersWith346 = distribution.ContainsKey(346) ? distribution[346] : 0;
        Console.WriteLine("Users with 346 songs (Q2): " + usersWith346);
        Console.WriteLine("Max distinct songs (Q3): " + maxDistinct);
    }
}
