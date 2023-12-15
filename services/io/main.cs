using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CsvHelper;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: CsvToJson <chemin_du_fichier_csv>");
            return;
        }

        string csvFilePath = args[0];
        
        try
        {
            var jsonData = ConvertCsvToJson(csvFilePath);
            Console.WriteLine(jsonData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
        }
    }

    static string ConvertCsvToJson(string csvFilePath)
    {
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader))
        {
            // Utilisation de CsvHelper pour lire le fichier CSV
            var records = csv.GetRecords<dynamic>();

            // Conversion des enregistrements en liste de dictionnaires
            var dataList = new List<Dictionary<string, object>>();
            foreach (var record in records)
            {
                var recordDictionary = new Dictionary<string, object>();
                foreach (var property in record.GetType().GetProperties())
                {
                    recordDictionary[property.Name] = property.GetValue(record);
                }
                dataList.Add(recordDictionary);
            }

            // Sérialisation de la liste en JSON
            return JsonConvert.SerializeObject(dataList, Formatting.Indented);
        }
    }
}
