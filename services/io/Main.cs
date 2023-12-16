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
        // Chemin vers le fichier CSV d'entrée
        string cheminFichierCSV = "data.csv";

        // Chemin vers le fichier JSON de sortie
        string cheminFichierJSON = "data.json";

        // Convertit le CSV en JSON
        ConvertirCSVEnJSON(cheminFichierCSV, cheminFichierJSON);

        Console.WriteLine("Conversion terminée. JSON généré avec succès!");
    }

    static void ConvertirCSVEnJSON(string cheminFichierCSV, string cheminFichierJSON)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);

        using (var reader = new StreamReader(cheminFichierCSV))
        using (var csv = new CsvReader(reader, configuration))
        {

            foreach (var i in csv.GetRecords<Line>())
            {
                Console.WriteLine(i.Nom);
            }
            // var json = Newtonsoft.Json.JsonConvert.SerializeObject(listeEnregistrements, Newtonsoft.Json.Formatting.Indented);

            // File.WriteAllText(cheminFichierJSON, json);
        }
    }
    public class Line
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }
}
