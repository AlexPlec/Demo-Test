<script>
import dbInteractions from '../../../../VueLocalDB/Db/dbInteractionsApi.js';

export default {
  name: 'Blackjack',
  data() {
    return {
      suits: ['Hearts', 'Diamonds', 'Clubs', 'Spades'],
      values: ['Ace', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'Jack', 'Queen', 'King'],
      cardDeck: [],
      playerHand: [],
      dealerHand: [],
      gameOver: false,
      dealerRevealed: false,
      message: '',
    };
  },
  computed: {
    playerValue() {
      return this.calculateHandValue(this.playerHand);
    },
    dealerValue() {
      return this.calculateHandValue(this.dealerHand);
    },
  },
  methods: {
    createDeck(numDecks) {
      const deck = [];
      for (let i = 0; i < numDecks; i++) {
        for (let suit of this.suits) {
          for (let value of this.values) {
            deck.push({ suit, value });
          }
        }
      }
      return deck;
    },
    shuffleDeck(deck) {
      for (let i = deck.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [deck[i], deck[j]] = [deck[j], deck[i]];
      }
    },
    dealCard(deck) {
      return deck.pop();
    },
    calculateHandValue(hand) {
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
    },
    setUp() {
      const numDecks = 2; // Adjust the number of decks as needed
      this.cardDeck = this.createDeck(numDecks);
      this.shuffleDeck(this.cardDeck);
      this.playerHand = [this.dealCard(this.cardDeck), this.dealCard(this.cardDeck)];
      this.dealerHand = [this.dealCard(this.cardDeck), this.dealCard(this.cardDeck)];
      this.gameOver = false;
      this.dealerRevealed = false;
      this.message = '';
    },
    hit() {
      this.playerHand.push(this.dealCard(this.cardDeck));
      this.checkPlayerWinConditions();
    },
    stand() {
      while (this.calculateHandValue(this.dealerHand) < 17) {
        this.dealerHand.push(this.dealCard(this.cardDeck));
      }
      this.dealerRevealed = true;
      this.checkDealerWinConditions();
    },
    checkPlayerWinConditions() {
      const playerValue = this.playerValue;
      if (playerValue > 21) {
        this.endGame('lose');
      } else if (playerValue === 21) {
        this.endGame('win');
      }
    },
    checkDealerWinConditions() {
      const playerValue = this.playerValue;
      const dealerValue = this.dealerValue;

      if (dealerValue > 21 || playerValue > dealerValue) {
        this.endGame('win');
      } else if (playerValue < dealerValue) {
        this.endGame('lose');
      } else {
        this.endGame('tie');
      }
    },
    endGame(result) {
      this.gameOver = true;
      this.writeGameResults(result);
      if (result === 'win') {
        this.message = 'You win!';
      } else if (result === 'lose') {
        this.message = 'Dealer wins!';
      } else {
        this.message = 'Push. It\'s a tie.';
      }
    },
    writeGameResults(result) {
      dbInteractions.insertData('blackjack', [result, this.playerValue, this.dealerValue])
        .then(() => console.log('Game result saved successfully'))
        .catch((error) => console.error('Error saving game result:', error));
    },
    resetGame() {
      this.setUp();
    },
    backToMainMenu() {
      this.$emit('back-to-main-menu'); // Emit event to go back to main menu
    },
  },
  mounted() {
    this.setUp(); // Start the game when the component is mounted
  },
};
</script>

<template>
  <div class="blackjack-container">
    <h1>Blackjack</h1>
    <p v-if="message">{{ message }}</p>
    <div v-if="!gameOver">
      <h2>Your Hand:</h2>
      <ul>
        <li v-for="(card, index) in playerHand" :key="index">
          {{ card.value }} of {{ card.suit }}
        </li>
      </ul>
      <p>Your Total: {{ playerValue }}</p>
      <button @click="hit">Hit</button>
      <button @click="stand">Stand</button>
    </div>
    <div v-if="dealerRevealed">
      <h2>Dealer's Hand:</h2>
      <ul>
        <li v-for="(card, index) in dealerHand" :key="index">
          {{ card.value }} of {{ card.suit }}
        </li>
      </ul>
      <p>Dealer's Total: {{ dealerValue }}</p>
    </div>
    <button v-if="gameOver" @click="resetGame">Play Again</button>
    <button @click="backToMainMenu">Back to Main Menu</button>
  </div>
</template>

<style scoped>
.blackjack-container {
  text-align: center;
  margin-top: 50px;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  margin: 10px 0;
}
button {
  margin-top: 20px;
  padding: 14px 24px;
  font-size: 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  background-color: #007bff;
  color: white;
  transition: background-color 0.3s ease;
}
button:hover {
  background-color: #0056b3;
}
</style>