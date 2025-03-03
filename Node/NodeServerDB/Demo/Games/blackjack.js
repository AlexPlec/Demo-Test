const { insertData } = require('../Db/dbInteractionsApi.js');
const { createRlQuestion, rlClose } = require('../library/rLInteractions.js');
const { setUpGame } = require('../library/gameMenu.js');
const { displayGameInfoStatistics, displayGameInfoHistory } = require('../library/gameInfo.js');

const suits = ['Hearts', 'Diamonds', 'Clubs', 'Spades'];
const values = ['Ace', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'Jack', 'Queen', 'King'];

let cardDeck;
let playerHand;
let dealerHand;

const BlackjackMenuConfig = {
    prompt: '1 - Play game\n2 - View info\n3 - Quit\n4 - Main Menu',
    options: ['1', '2', '3', '4'],
    actions: {
        1: () => {
            setUp();
            playBlackjack();
        },
        2: () => setUpGame(infoMenuConfig),
        3: () => {
            console.log('Quitting the game.');
            rlClose();
        },
        4: () => setUpGame(global.MenuConfig)
    }
};

const infoMenuConfig = {
    prompt: '1 - View History\n2 - View Statistics\n3 - Blackjack Main Menu',
    options: ['1', '2', '3'],
    actions: {
        1: () => displayGameInfoHistory('blackjack', 'blackjack', ['gameResult', 'playerValue', 'dealerValue'], infoMenuConfig),
        2: () => displayGameInfoStatistics('blackjack', ['playerValue', 'dealerValue'], false, infoMenuConfig),
        3: () => setUpGame(BlackjackMenuConfig)
    }
};

function createDeck(numDecks) {
    const deck = [];
    for (let i = 0; i < numDecks; i++) {
        for (let suit of suits) {
            for (let value of values) {
                deck.push({ suit, value });
            }
        }
    }
    return deck;
}

function shuffleDeck(deck) {
    for (let i = deck.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [deck[i], deck[j]] = [deck[j], deck[i]];
    }
}

function dealCard(deck) {
    return deck.pop();
}

function calculateHandValue(hand) {
    let value = 0;
    let aceCount = 0;

    for (let card of hand) {
        if (card.value === 'Ace') {
            aceCount++;
            value += 11;
        } else if (['Jack', 'Queen', 'King'].includes(card.value)) {
            value += 10;
        } else {
            value += parseInt(card.value);
        }
    }

    while (value > 21 && aceCount > 0) {
        value -= 10;
        aceCount--;
    }

    return value;
}

function displayHand(hand) {
    let handString = '';
    for (let card of hand) {
        handString += `${card.value} of ${card.suit}, `;
    }

    return handString.slice(0, -2);
}

function setUp() {
    const numDecks = 2; // Adjust the number of decks as needed
    cardDeck = createDeck(numDecks);
    shuffleDeck(cardDeck);

    playerHand = [dealCard(cardDeck), dealCard(cardDeck)];
    dealerHand = [dealCard(cardDeck), dealCard(cardDeck)];

    console.log('Your hand:', displayHand(playerHand));
}

async function playBlackjack() {
    let answer = await createRlQuestion(`Hit or stand? (h/s): `);

    if (answer.toLowerCase() === 'h') {

        playerHand.push(dealCard(cardDeck));
        console.log('Your hand:', displayHand(playerHand));
        playerWinConditions(calculateHandValue(playerHand));
        setUpGame(BlackjackMenuConfig);

    } else if (answer.toLowerCase() === 's') {
        while (calculateHandValue(dealerHand) < 17) {
            dealerHand.push(dealCard(cardDeck));
        }
        console.log('Dealer\'s hand:', displayHand(dealerHand));

        dealerWinConditions(calculateHandValue(playerHand), calculateHandValue(dealerHand));
        setUpGame(BlackjackMenuConfig);

    } else {
        console.log('Invalid input. Please enter "h" or "s".');
        playBlackjack();
    }
}

function playerWinConditions(playerValue) {
    if (playerValue > 21) {
        console.log('Dealer wins!');
        writeGameResults('lose', playerValue);
        return;
    }

    if (playerValue == 21) {
        console.log('You win!');
        writeGameResults('win', playerValue);
        return;
    } else {
        playBlackjack();
    }
}

function dealerWinConditions(playerValue, dealerValue) {
    if (dealerValue > 21) {
        console.log('Dealer busts! You win!');
        writeGameResults('win', playerValue, dealerValue);
        return;
    }

    if (playerValue > dealerValue) {
        console.log('You win!');
        writeGameResults('win', playerValue, dealerValue);
        return;
    }

    if (playerValue < dealerValue) {
        console.log('Dealer wins!');
        writeGameResults('lose', playerValue, dealerValue);
        return;
    } else {
        console.log('Push. It\'s a tie.');
        writeGameResults('tie', playerValue, dealerValue);
    }
}

function writeGameResults(result, playerValue, dealerValue) {

    if (!dealerValue) {
        insertData('blackjack', [result, playerValue]);
    } else {
        insertData('blackjack', [result, playerValue, dealerValue]);
    }
}

setUpGame(BlackjackMenuConfig);

module.exports = { BlackjackMenuConfig };