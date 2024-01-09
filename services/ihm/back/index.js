const express = require("express");
const bodyParser = require("body-parser");
const path = require("path");
const axios = require("axios");

const app = express();
const port = 3000;

app.use(bodyParser.json());
app.use(express.static("front"));

app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname + "/../front/index.html"));
});

app.post("/io", async (req, res) => {
  try {
    const response = await axios.post("http://io:5000/io", req.body);
    res.json(response.data);
  } catch (error) {
    console.error(error);
    res.status(500).send("Internal Server Error");
  }
});

app.listen(port, () => {
  console.log(`Le serveur est lancé sur http://localhost:${port}`);
});
