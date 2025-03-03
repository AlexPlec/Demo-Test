<script>
import GuessTheNumber from './components/games/GuessTheNumber.vue';
import History from './components/gameInfo/History.vue';
import Statistics from './components/gameInfo/Statistics.vue';
import Blackjack from './components/games/Blackjack.vue';
import GameMenu from './components/ui/GameMenu.vue';
import InfoMenu from './components/ui/InfoMenu.vue';
import MainMenu from './components/ui/MainMenu.vue';

export default {
  components: {
    GuessTheNumber,
    History,
    Statistics,
    Blackjack,
    GameMenu,
    InfoMenu,
    MainMenu,
  },
  data() {
    return {
      currentComponent: '', // Tracks the currently active component
      currentGame: '', // Tracks the currently selected game
    };
  },
  methods: {
    openGameMenu(gameName) {
      console.log('Opening GameMenu for:', gameName); // Debugging: Log the game name
      this.currentComponent = 'GameMenu'; // Navigate to GameMenu
      this.currentGame = gameName; // Set the current game
    },
    startGame(gameName) {
      console.log('Starting game:', gameName); // Debugging: Log the game name
      this.currentComponent = gameName; // Start the selected game
      this.currentGame = gameName; // Set the current game
    },
    viewHistory() {
      console.log('Viewing history for:', this.currentGame); // Debugging: Log the game name
      this.currentComponent = 'History'; // Navigate to History component
    },
    viewStatistics() {
      console.log('Viewing statistics for:', this.currentGame); // Debugging: Log the game name
      this.currentComponent = 'Statistics'; // Navigate to Statistics component
    },
    goBackToMainMenu() {
      console.log('Going back to Main Menu'); // Debugging: Log the action
      this.currentComponent = ''; // Reset currentComponent to show the main menu
      this.currentGame = ''; // Reset current game
    },
    backToGameMenu(gameName) {
      console.log('Going back to Game Menu for:', gameName); // Debugging: Log the action
      this.currentComponent = 'GameMenu'; // Navigate back to GameMenu
      this.currentGame = gameName; // Set the current game
    },
    viewInfoMenu(gameName) {
      console.log('Viewing InfoMenu for:', gameName); // Debugging: Log the game name
      this.currentComponent = 'InfoMenu'; // Navigate to InfoMenu
      this.currentGame = gameName; // Set the current game
    },
  },
};
</script>

<template>
  <div id="app">
    <!-- Dynamic menu rendering -->
    <component 
      :is="currentComponent || 'MainMenu'" 
      @open-game-menu="openGameMenu"
      @start-game="startGame"
      @view-history="viewHistory"
      @view-statistics="viewStatistics"
      @back-to-main-menu="goBackToMainMenu"
      @back-to-game-menu="backToGameMenu"
      @view-info="viewInfoMenu"
      :gameName=currentGame
    ></component>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  text-align: center;
  color: #ffffff;
  min-height: 100vh;
  display: flex;
  flex-direction: column; /* Stack children vertically */
  justify-content: center; /* Center the stack vertically */
  align-items: center; /* Center the stack horizontally */
}
</style>