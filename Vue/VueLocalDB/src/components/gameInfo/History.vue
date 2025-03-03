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
      history: [], // Stores the fetched history
    };
  },
  async mounted() {
    if (!this.gameName) {
      console.error('Game name is not defined in History.vue');
      return;
    }

    try {
      const data = await dbInteractions.fetchData(this.gameName); // Fetch data based on the game name
      this.history = data || []; // Handle empty data gracefully
    } catch (error) {
      console.error(`Error fetching ${this.gameName} history:`, error);
    }
  },
  methods: {
    backToGameMenu() {
      this.$emit('back-to-game-menu', this.gameName); // Emit event to go back to main menu
    },
    viewHistory() {
      this.$emit('view-history', this.gameName); // Emit event to view history with the current game name
    },
    viewStatistics() {
      this.$emit('view-statistics', this.gameName); // Emit event to view statistics with the current game name
    },
  },
};
</script>

<template>
  <div class="history-container">
    <h1>{{ gameName }} Game History</h1>
    <ul v-if="history.length > 0">
      <li v-for="(entry, index) in history" :key="index">
        <!-- Dynamically display fields based on the game -->
        <template v-if="gameName === 'GuessTheNumber'">
          Attempts: {{ entry.attempts }}, Number: {{ entry.number }}
        </template>
        <template v-else-if="gameName === 'Blackjack'">
          Result: {{ entry.gameResult }}, Player Value: {{ entry.playerValue }}, Dealer Value: {{ entry.dealerValue }}
        </template>
      </li>
    </ul>
    <p v-else>No history available for {{ gameName }}.</p>
    <!-- Navigation buttons -->
    <button @click="viewHistory">View History</button>
    <button @click="viewStatistics">View Statistics</button>
    <button @click="backToGameMenu">Back to Game Menu</button>
  </div>
</template>

<style scoped>
.history-container {
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