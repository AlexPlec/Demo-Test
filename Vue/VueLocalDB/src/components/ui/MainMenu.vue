<script>
export default {
  name: 'MainMenu',
  methods: {
    openGameMenu(gameName) {
      this.$emit('open-game-menu', gameName); // Emit event to open GameMenu with a specific game name
    },
  },
};
</script>

<template>
  <div class="main-menu-container">
    <h1>Main Menu</h1>
    <button @click="openGameMenu('Blackjack')">Open Blackjack Menu</button>
    <button @click="openGameMenu('GuessTheNumber')">Open GuessTheNumber Menu</button>
  </div>
</template>

<style scoped>
.main-menu-container {
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