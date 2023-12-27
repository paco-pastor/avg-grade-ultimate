using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    static void Main() // TODO : Use while loop to keep container alive
    {
        List<List<string>> csv = ReadCSV("data.csv");
        List<string> TUs = ListTUs(csv);
        List<Student> jsonList = new List<Student>();

        foreach (List<string> line in csv)
        {
            if (int.TryParse(line[0], out int id)) // If student id exists
            {
                Dictionary<string, string> grades = new Dictionary<string, string>();
                foreach (string tu in TUs)
                    grades[tu] = line[csv[0].IndexOf(tu)];

                Student student = new Student
                {
                    Id = id,
                    Surname = line[csv[0].IndexOf("Nom", 4)],
                    Name = line[csv[0].IndexOf("Prenom")],
                    Grades = grades
                };
                jsonList.Add(student);
            }
        }

        File.WriteAllText("data.json", JsonConvert.SerializeObject(jsonList));
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

    static List<string> ListTUs(List<List<string>> csv)
    {
        List<string> TUs = new List<string>();
        foreach (string header in csv[0])
            if (header.StartsWith("S") && (header.Length == 5 || header.Length == 6))
                TUs.Add(header);
        return TUs;
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Dictionary<string, string> Grades { get; set; }
    }
}
