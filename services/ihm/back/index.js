const express = require("express")
const bodyParser = require("body-parser")
const path = require("path")

const app = express()
const port = 3000

app.use(bodyParser.json())
app.use(express.static("front"))

app.get("/", (req, res) => {
    res.sendFile(path.join(__dirname + "/../front/index.html"))
})

app.listen(port, () => {
    console.log(`Le serveur est lancé sur http://localhost:${port}`)
})
