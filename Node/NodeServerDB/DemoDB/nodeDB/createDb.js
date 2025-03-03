const sqlite3 = require('sqlite3').verbose();
const db = new sqlite3.Database('demo.db');

db.serialize(() => {
  db.run(`CREATE TABLE guessTheNumber (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        attempts INTEGER DEFAULT 0,
        number INTEGER DEFAULT 0
      )`);

  db.run(`CREATE TABLE blackjack (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        gameResult TEXT,
        playerValue INTEGER DEFAULT 0,
        dealerValue INTEGER DEFAULT 0
      )`);
});

db.close();