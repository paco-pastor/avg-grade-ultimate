using System;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("Début d'éxecution du C#");
        Console.WriteLine("--------------------------------------------------------------------");

        List<List<string>> csv = ReadCSV("data.csv");

        // TODO Dictionnaire NomChamp -> IndexColonne pour rendre dynamique le tout

        foreach (List<string> line in csv)
        {
            if (int.TryParse(line[0], out int id))
            {
                Student student = new Student // TODO : utiliser indexes dynamiques selon header colonne
                {
                    Id = id,
                    Surname = line[5],
                    Name = line[6],
                    TU = line[8],
                };
                string json = JsonConvert.SerializeObject(student);
                Console.WriteLine(json);
            }
        }

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

    public class Student
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string TU { get; set; }
    }
}
