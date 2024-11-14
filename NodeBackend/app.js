const express = require('express');
const app = express();

app.use(express.json());

app.post('/calculate', (req, res) => {

});

module.exports = app;