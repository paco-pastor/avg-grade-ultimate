from flask import Flask, request, jsonify
from flask_cors import CORS
import json

app = Flask(__name__)
CORS(app, resources={r"/*": {"origins": "http://localhost:3000"}})

@app.route('/calcul', methods=['POST'])
def handle_io():

    data = json.loads(request.data.decode('utf-8')) # Décodage des bytes vers un fichier json.
    if 'Coeffs' not in data or 'StudentList' not in data : # Contrôle des données
        return jsonify({'error': 'Données invalides'}), 400

    coefList = data["Coeffs"] # Récupération des coefficients.

    StudentList = data["StudentList"] # Récupération de la liste des étudiants.
    
    sommeCoef = 0 # Somme des coef pour le calcul de la moyenne.
    for coef in coefList:
        sommeCoef = sommeCoef + coef

    resultats = []
    for student in StudentList: # Parcours de la liste des étudiants.
        sommeUE = 0
        i = 0

        if len(student["Grades"]) > len(coefList): # Contrôle du nombre de notes et de coefficients.
            return jsonify({'error ': 'Le nombre de notes est plus grand que le nombre de coefficients.'}),400

        for note in student["Grades"].values(): 
            sommeUE = sommeUE + float(note) * coefList[i] #calcul de la somme des notes avec les coefficients.
            i = i + 1

        moyenneEleve = round(sommeUE / sommeCoef,2) #calcul de la moyenne.
        resultats.append({'Id': student['Id'], 'nom': student['Name'],'surnom':student['Surname'], 'moyenne': moyenneEleve}) # Ajout des résultats dans la liste.

    return jsonify(resultats),200 # Renvoi des résultats.



if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001, debug=True)


