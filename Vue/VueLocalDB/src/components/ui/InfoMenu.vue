<script>
export default {
  name: 'InfoMenu',
  props: {
    gameName: {
      type: String,
      required: true, // Ensure a game name is provided
    },
  },
  methods: {
    backToGameMenu() {
      this.$emit('back-to-game-menu', this.gameName); // Emit event to go back to GameMenu with the current game name
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
  <div class="info-menu-container">
    <h1>Info Menu for {{ gameName }}</h1>
    <button @click="viewHistory">View History</button>
    <button @click="viewStatistics">View Statistics</button>
    <button @click="backToGameMenu">Back to Game Menu</button>
  </div>
</template>

<style scoped>
.info-menu-container {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  text-align: center;
  color: #ffffff;
  margin-top: 50px;
}

button {
  padding: 14px 24px;
  font-size: 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  background-color: #007bff;
  color: white;
  transition: background-color 0.3s ease;
  margin: 10px 0;
}

button:hover {
  background-color: #0056b3;
}
</style>