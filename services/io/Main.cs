using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("Début d'éxecution du C#");
        Console.WriteLine("--------------------------------------------------------------------");

        List<List<string>> csv = ReadCSV("data.csv");

        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("C# executé !");
        Console.WriteLine("--------------------------------------------------------------------");
    }

    static List<List<string>> ReadCSV(string path)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            List<List<string>> csv = new List<List<string>>();
            while ((line = sr.ReadLine()) != null)
            {
                List<string> lineAsList = line.Split(';').ToList();
                csv.Add(lineAsList);
            }
            return csv;
        }
    }

}
