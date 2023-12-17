const express = require('express');
const path = require('path');

const app = express();
const port = 3000;

// Définit le dossier statique pour le front
app.use(express.static('front'));

// Route pour la page principale
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname + '/../front/index.html'));
});

// Écoute sur le port spécifié
app.listen(port, () => {
  console.log(`Le serveur est lancé sur http://localhost:${port} my G`);
});
