using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

class Program
{
    static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("Début d'éxecution du C#");
        Console.WriteLine("--------------------------------------------------------------------");
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            IgnoreBlankLines = true
        };

        using (var reader = new StreamReader("data.csv"))
        using (var csv = new CsvReader(reader, configuration))
        {
            foreach (var line in csv.GetRecords<Line>())
            {
                Console.WriteLine(line.etudid.GetType());
            }
        }
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("C# executé !");
        Console.WriteLine("--------------------------------------------------------------------");
    }

}
public class Line
{
    public object? etudid { get; set; }
    public string? Prenom { get; set; }
}

