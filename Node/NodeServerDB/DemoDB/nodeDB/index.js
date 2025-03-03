const { insertData, getAllDataFromTable } = require('./dbInteractions');
const express = require('express');
const cors = require('cors'); // Import the cors middleware

const app = express();
const port = 3000;

// Enable CORS for all routes
app.use(cors()); // This allows all origins, methods, and headers by default

// Parse incoming JSON requests
app.use(express.json());

// POST route to insert data
app.post('/insertData', async (req, res) => {
  try {
    const { tableName, dataBody } = req.body;

    // Insert data into the database
    await insertData(tableName, Object.values(dataBody));

    // Respond with success message
    res.status(201).json({ message: 'Data received successfully' });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: 'Failed to insert data' });
  }
});

// GET route to fetch all data from a table
app.get('/getAllDataFromTable', async (req, res) => {
  try {
    const tableName = req.query.tableName;
    const data = await getAllDataFromTable(tableName);

    // Send the data as a JSON response
    res.status(200).json({ data });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: 'Failed to fetch data' });
  }
});

// Start the server
app.listen(port, () => {
  console.log(`Server listening on port ${port} http://localhost:${port}`);
});