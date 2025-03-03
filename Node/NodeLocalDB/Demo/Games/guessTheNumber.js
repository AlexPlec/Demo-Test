const { insertData } = require('../Db/dbInteractions.js');
const { createRlQuestion, rlClose } = require('../library/rLInteractions.js');
const { setUpGame } = require('../library/gameMenu.js');
const { displayGameInfoStatistics, displayGameInfoHistory } = require('../library/gameInfo.js');

let randomNumber;
function createGuess() {
  randomNumber = Math.floor(Math.random() * (100 - 1 + 1) + 1);
}

let attempts = 0;

const GuessMenuConfig = {
  prompt: '1 - Play game\n2 - View info\n3 - Quit\n4 - Main Menu',
  options: ['1', '2', '3', '4'],
  actions: {
    1: () => {createGuess(), guessNumber()},
    2: () => setUpGame(infoMenuConfig),
    3: () => {
      console.log('Quitting the game.');
      rlClose();
    },
    4: () => setUpGame(global.MenuConfig)
  }
};

const infoMenuConfig = {
  prompt: '1 - View History\n2 - View Statistics\n3 - Guess The Number Main Menu',
  options: ['1', '2', '3'],
  actions: {
    1: () => displayGameInfoHistory('guessTheNumber', 'Guess the Number', ['attempts', 'number'], infoMenuConfig),
    2: () => displayGameInfoStatistics('guessTheNumber', ['attempts', 'number'], false, infoMenuConfig),
    3: () => setUpGame(GuessMenuConfig)
  }
};

async function guessNumber() {
  attempts++;
  let answer = await createRlQuestion(`Guess the number (1-100): `);
  if (isNaN(answer) || answer < 1 || answer > 100) {
    console.log('Please enter a valid number between 1 and 100.');
    guessNumber();
    attempts--;
    return;
  }

  answer = parseInt(answer);

  if (answer == randomNumber) {
    console.log(`Congratulations! You guessed the number in ${attempts} attempts.`);
    insertData('guessTheNumber', [attempts, answer]);
    setUpGame(GuessMenuConfig);
    attempts = 0;
    createGuess();
  } else if (answer > randomNumber) {
    console.log('Too high. Try again.');
    guessNumber();
  } else {
    console.log('Too low. Try again.');
    guessNumber();
  }

}

setUpGame(GuessMenuConfig);

module.exports = { GuessMenuConfig };