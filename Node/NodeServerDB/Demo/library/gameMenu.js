const { createRlQuestion } = require('../library/rLInteractions.js');

async function setUpGame(config) {
  const fullPrompt = `${config.prompt}\nEnter an option: `;
  let answer = await createRlQuestion(fullPrompt);
  const option = answer.toLowerCase();
  if (config.options.includes(option)) {
    // Execute the corresponding game action
    config.actions[option]();
  } else {
    console.log(`Invalid input. Please try again.`);
    await setUpGame(config); // Recursively call setUpGame for invalid input
  }
}

module.exports = { setUpGame };