const sqlite3 = require('sqlite3').verbose();
const path = require('path');
const dbPath = path.join(__dirname, './demo.db');
const db = new sqlite3.Database(dbPath);

function insertData(table, data) {

  const placeholders = data.map(() => '?').join(',');
  const sql = `INSERT INTO ${table} VALUES (NULL, ${placeholders})`;

  db.run(sql, data, function (err) {
  });
}

function deleteData(table, whereClause) {
  const sql = `DELETE FROM ${table} WHERE ${whereClause}`;

  db.run(sql, function (err) {
    if (err) {
      console.error(err.message);
    } else {
      console.log(`${this.changes} row(s) deleted from ${table}`);
    }
  });
}

function updateData(table, setClause, whereClause) {
  const sql = `UPDATE ${table} SET ${setClause} WHERE ${whereClause}`;

  db.run(sql, function (err) {
    if (err) {
      console.error(err.message);
    } else {
      console.log(`${this.changes} row(s) updated in ${table}`);
    }
  });
}

async function getAllDataFromTable(table) {

  return new Promise((resolve, reject) => {
    db.all(`SELECT * FROM ${table}`, (err, rows) => {
      if (err) {
        reject(err);
      } else {
        resolve(rows);
      }
    });
  });
}

module.exports = { insertData, getAllDataFromTable };
// deleteData('guessTheNumber', 'id = 16'); // delete single row
// deleteData('guessTheNumber', `id BETWEEN ${17} AND ${24}`); // delete multiple rows
// updateData('guessTheNumber', 'number = 21', 'id = 14');