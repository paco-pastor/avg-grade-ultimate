const express = require("express");
const bodyParser = require('body-parser');
const path = require("path");
const { exec } = require("child_process");

const app = express();
const port = 3000;

app.use(bodyParser.json());

// Définit le dossier statique pour le front
app.use(express.static("front"));

app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname + "/../front/index.html"));
});

app.post("/io", (req, res) => {

  const file = req.body.file;
  const dockerExecCommand = `docker exec avg-grade-ultimate-io-1 dotnet Io.dll ${file}`;
  // BUG : docker n'existe pas dans ce contexte.

  exec(dockerExecCommand, (error, stdout, stderr) => {
    if (error) {
      console.error(
        `Erreur lors de l'exécution de la commande Docker exec: ${stderr}`
      );
      res
        .status(500)
        .send("Erreur lors de l'exécution de la commande Docker exec");
    } else {
      console.log(`Résultat de la commande Docker exec: ${stdout}`);
      res.status(200).send("Commande Docker exec exécutée avec succès");
    }
  });
});

app.listen(port, () => {
  console.log(`Le serveur est lancé sur http://localhost:${port}`);
});
