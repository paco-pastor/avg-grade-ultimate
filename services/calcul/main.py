from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/calcul', methods=['POST'])
def handle_io():
    print(request.data)
    #TODO process JSON and send average grades (ultimate)
    return "200 OK"


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001)
