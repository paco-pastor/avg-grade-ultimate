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
        // Configuration CsvHelper (change si nécessaire)
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);

        // Utilisation de CsvHelper pour lire le CSV
        using (var reader = new StreamReader(cheminFichierCSV))
        using (var csv = new CsvReader(reader, configuration))
        {
            // Lis les enregistrements du CSV en tant qu'objets
            var enregistrements = csv.GetRecords<dynamic>();

            // Convertit les objets en liste pour sérialiser en JSON
            var listeEnregistrements = new List<dynamic>(enregistrements);

            // Sérialise la liste en JSON
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(listeEnregistrements, Newtonsoft.Json.Formatting.Indented);

            // Écrit le JSON dans un fichier
            File.WriteAllText(cheminFichierJSON, json);
        }
    }
}
