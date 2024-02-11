﻿using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

class Program {
  public static async Task Main(string[] args) {

    var webHost = Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(webBuilder => {
        webBuilder.UseUrls("http://0.0.0.0:5000");
        webBuilder.ConfigureServices(services =>
        {
          services.AddCors(options =>
          {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                  builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
          });
        });
        webBuilder.Configure(app => {
          app.UseRouting();
          app.UseCors("AllowSpecificOrigin");
          app.UseEndpoints(endpoints => {
            endpoints.MapPost("/io", async context => { // Definition de la route /io
              string csvContent = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();

              List < List < string >> csv = ReadCSV(csvContent);
              List < string > TUs = ListTUs(csv);
              List < Student > studentList = new List < Student > ();
              List < double > coeffList = new List < double > ();

              foreach(List < string > line in csv) {
                if (int.TryParse(line[0], out int id)) // Si l'ID etudiant existe
                {
                  Dictionary < string, string > grades = new Dictionary < string, string > ();
                  foreach(string tu in TUs)
                  grades[tu] = line[csv[0].IndexOf(tu)].Replace(",", ".");

                  Student student = new Student {
                    Id = id,
                      Surname = line[csv[0].IndexOf("Nom", 4)].ToUpper(),
                      Name = line[csv[0].IndexOf("Prenom")],
                      Grades = grades
                  };
                  studentList.Add(student);
                } else { // Sinon on vérifie si la ligne contient les coefficients afin de les renvoyer egalement
                  if (line[csv[0].IndexOf("Nom")] == "Coef.") {
                    coeffList = ListCoeffs(line);
                  }

                }
              }
              await context.Response.WriteAsync(JsonConvert.SerializeObject(new Result {
                Coeffs = coeffList, StudentList = studentList
              }));
            });
          });
        });
      })
      .Build();

    await webHost.RunAsync();
  }

// Renvoi d'une liste de listes de strings correspondant aux lignes et colonnes du CSV
  static List < List < string >> ReadCSV(string csvContent) {
    using(StringReader sr = new StringReader(csvContent)) {
      string line;
      List < List < string >> csv = new List < List < string >> ();
      while ((line = sr.ReadLine()) != null) {
        List < string > lineAsList = line.Split(';').ToList();
        csv.Add(lineAsList);
      }
      return csv;
    }
  }

// TU = Teaching Unit (UE)
// Recuperation des en-tetes de colonnes correspondant aux UEs (S1101, S1102, ...)
  static List < string > ListTUs(List < List < string >> csv) {
    List < string > TUs = new List < string > ();
    foreach(string header in csv[0])
    if (header.StartsWith("S") && (header.Length == 5 || header.Length == 6))
      TUs.Add(header);
    return TUs;
  }

// Recuperation des coefficients
  static List < double > ListCoeffs(List < string > line) {
    List < double > coeffs = new List < double > ();
    foreach(string cell in line) {
      if ((double.TryParse(cell, NumberStyles.Float, new CultureInfo("fr-FR"), out double coeff))) {
        coeffs.Add(coeff);
      }
    }
    return coeffs;
  }

// Classes utiles pour la serialisation en JSON
  public class Student {
    public int Id {
      get;
      set;
    }
    public string Name {
      get;
      set;
    }
    public string Surname {
      get;
      set;
    }
    public Dictionary < string, string > Grades {
      get;
      set;
    }
  }

// On renvoie une liste de coefficients et une liste d'etudiants dans un objet Result
  public class Result {
    public List < double > Coeffs {
      get;
      set;
    }
    public List < Student > StudentList {
      get;
      set;
    }
  }
}