using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: SortApp <filename>");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File {filePath} does not exist.");
            return;
        }

        try
        {
            // Read all text from the file
            string text = File.ReadAllText(filePath);

            // Use regex to find words (sequences of letters)
            var regex = new Regex(@"\b(?![0-9])[\p{L}\p{N}]+\b", RegexOptions.IgnoreCase);
            var matches = regex.Matches(text);

            // Extract words from matches
            var words = new List<string>();
            foreach (Match match in matches)
            {
                words.Add(match.Value);
            }

            // Remove duplicates, sort lexicographically and take first 5
            var sortedWords = words
                .Distinct(StringComparer.OrdinalIgnoreCase) // Case insensitive distinct
                .OrderBy(word => word, StringComparer.OrdinalIgnoreCase) // Case insensitive sort
                ;

            // Output the sorted words
            foreach (var word in sortedWords)
            {
                Console.WriteLine(word.ToUpper());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
