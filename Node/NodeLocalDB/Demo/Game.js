const { setUpGame } = require('./library/gameMenu.js');
const { rlClose } = require('./library/rLInteractions.js');

global.MenuConfig = {
  prompt: '1 - Start game Blackjack\n2 - Start game GuessTheNumber\n3 - Quit',
  options: ['1', '2', '3'],
  actions: {
    1: () => setUpGame(global.BlackjackMenuConfig),
    2: () => setUpGame(global.GuessMenuConfig),
    3: () => {
      console.log('Quitting the game.');
      rlClose();
    }
  }
};

setUpGame(global.MenuConfig);

const { GuessMenuConfig } = require('./Games/guessTheNumber.js');
const { BlackjackMenuConfig } = require('./Games/blackjack.js');

global.GuessMenuConfig = GuessMenuConfig;
global.BlackjackMenuConfig = BlackjackMenuConfig;
