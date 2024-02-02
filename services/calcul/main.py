from flask import Flask, request, jsonify
import json
app = Flask(__name__)

@app.route('/calcul', methods=['POST'])
def handle_io():
    
    data = json.loads(request.data.decode('utf-8')) # Décodage des bytes vers un fichier json.

    if 'Coeffs' not in data or 'StudentList' not in data : # Contrôle des données
        return jsonify({'error': 'Données invalides'}), 400

    coefList = data["Coeffs"]

    StudentList = data["StudentList"]
    
    sommeCoef = 0 # Somme des coef pour le calcul de la moyenne.
    for coef in coefList:
        sommeCoef = sommeCoef + coef

    resultats = []
    for student in StudentList: # Parcours de la liste des étudiants.
        sommeUE = 0
        i = 0

        if len(student["Grades"]) > len(coefList):
            return jsonify({'error ': 'Le nombre de notes est plus grand que le nombre de coefficients.'})

        for note in student["Grades"].values(): 
            sommeUE = sommeUE + float(note) * coefList[i] #calcul de la somme des notes avec les coefficients.
            i = i + 1

        moyenneEleve = sommeUE / sommeCoef #calcul de la moyenne.
        resultats.append({'Id': student['Id'], 'nom': student['Name'], 'moyenne_notes': moyenneEleve})
        print("ELEVE : ", student["Name"], ", MOYENNE :", moyenneEleve)


    #response = {
    #    'id': data['id'],
    #    'nom': data['nom'],
    #    'surnom': data['surnom'],
    #    'moyenne_notes': moyenne
    #}

    return jsonify(resultats)
    #return "200 OK"


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001, debug=True)


