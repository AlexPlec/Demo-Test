<script>
import dbInteractions from '../../../../VueLocalDB/Db/dbInteractionsApi.js';

export default {
  props: {
    gameName: {
      type: String,
      required: true, // Ensure a game name is provided
    },
  },
  data() {
    return {
      stats: {
        totalGames: 0,
        averageAttempts: 0,
        minAttempts: 0,
        maxAttempts: 0,
        playerWins: 0,
        dealerWins: 0,
        ties: 0,
      },
      AllData: [],
    };
  },
  async mounted() {
    try {
      const data = await dbInteractions.fetchData(this.gameName); // Fetch data based on the game name
      this.AllData = data || [];
      this.calculateStatistics(); // Calculate statistics after fetching data
    } catch (error) {
      console.error(`Error fetching ${this.gameName} statistics:`, error);
    }
  },
  methods: {
    calculateStatistics() {
      if (!this.AllData || this.AllData.length === 0) {
        this.resetStats();
        return;
      }
      this.stats.totalGames = this.AllData.length;
      if (this.gameName === 'guessTheNumber') {
        this.calculateGuessTheNumberStats();
      } else if (this.gameName === 'blackjack') {
        this.calculateBlackjackStats();
      }
    },
    calculateGuessTheNumberStats() {
      const totalAttempts = this.AllData.reduce((sum, game) => sum + game.attempts, 0);
      this.stats.averageAttempts = Math.round(totalAttempts / this.stats.totalGames * 100) / 100;
      this.stats.minAttempts = Math.min(...this.AllData.map(game => game.attempts));
      this.stats.maxAttempts = Math.max(...this.AllData.map(game => game.attempts));
    },
    calculateBlackjackStats() {
      this.stats.playerWins = this.AllData.filter(game => game.gameResult === 'win').length;
      this.stats.dealerWins = this.AllData.filter(game => game.gameResult === 'lose').length;
      this.stats.ties = this.AllData.filter(game => game.gameResult === 'tie').length;
    },
    resetStats() {
      this.stats.totalGames = 0;
      this.stats.averageAttempts = 0;
      this.stats.minAttempts = 0;
      this.stats.maxAttempts = 0;
      this.stats.playerWins = 0;
      this.stats.dealerWins = 0;
      this.stats.ties = 0;
    },
    backToGameMenu() {
      this.$emit('back-to-game-menu' , this.gameName); // Emit event to go back to main menu
    },
    viewHistory() {
      this.$emit('view-history'); // Emit event to view history
    },
    viewStatistics() {
      this.$emit('view-statistics'); // Emit event to view statistics
    },
  },
};
</script>

<template>
  <div class="statistics-container">
    <h1>{{ gameName }} Game Statistics</h1>
    <p>Total Games Played: {{ stats.totalGames }}</p>
    <ul v-if="stats.totalGames > 0">
      <li>Total Games Played: {{ stats.totalGames }}</li>
      <template v-if="gameName === 'guessTheNumber'">
        <li>Average Attempts: {{ stats.averageAttempts }}</li>
        <li>Minimum Attempts: {{ stats.minAttempts }}</li>
        <li>Maximum Attempts: {{ stats.maxAttempts }}</li>
      </template>
      <template v-else-if="gameName === 'blackjack'">
        <li>Player Wins: {{ stats.playerWins }}</li>
        <li>Dealer Wins: {{ stats.dealerWins }}</li>
        <li>Ties: {{ stats.ties }}</li>
      </template>
    </ul>
    <p v-else>No statistics available for {{ gameName }}.</p>

    <!-- Navigation buttons -->
    <button @click="viewHistory">View History</button>
    <button @click="viewStatistics">View Statistics</button>
    <button @click="backToGameMenu">Back to Game Menu</button>
  </div>
</template>

<style scoped>
.statistics-container {
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