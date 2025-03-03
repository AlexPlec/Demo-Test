async function fetchData(tableName) {
  try {
    const response = await fetch(`http://localhost:3000/getAllDataFromTable?tableName=${tableName}`, 
    );

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const data = await response.json();
    return data.data;
  } catch (error) {
    console.error('Error fetching data:', error);
  }
}

async function insertData(tableName, dataBody) {
  try {
    const response = await fetch('http://localhost:3000/insertData', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({tableName,dataBody
      })
    });
    if (!response.ok) {
      
      throw new Error(`HTTP error! status: ${response.status}`);
    }
  } catch (error) {
    console.error('Error:', error);
  }
}

module.exports = { fetchData, insertData };