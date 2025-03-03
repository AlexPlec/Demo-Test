<script>
export default {
  name: 'GameMenu',
  props: {
    gameName: {
      type: String,
      required: true, // Ensure a game name is provided
    },
  },
  methods: {
    startGame() {
      this.$emit('start-game', this.gameName); // Emit event to start the game with the current game name
    },
    viewInfoMenu() {
      this.$emit('view-info', this.gameName); // Emit event to view info menu with the current game name
    },
    backToMainMenu() {
      this.$emit('back-to-main-menu'); // Emit event to go back to main menu
    },
  },
};
</script>

<template>
  <div class="game-menu-container">
    <h1>Game Menu for {{ gameName }}</h1>
    <button @click="startGame">Play {{ gameName }}</button>
    <button @click="viewInfoMenu">View Info</button>
    <button @click="backToMainMenu">Back to Main Menu</button>
  </div>
</template>

<style scoped>
.game-menu-container {
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