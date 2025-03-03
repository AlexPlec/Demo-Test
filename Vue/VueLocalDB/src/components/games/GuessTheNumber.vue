<script>
import dbInteractions from '../../../../VueLocalDB/Db/dbInteractionsApi.js';

export default {
  name: 'GuessTheNumber',
  data() {
    return {
      randomNumber: Math.floor(Math.random() * (100 - 1 + 1) + 1), // Random number between 1 and 100
      guess: null, // User's current guess
      attempts: 0, // Number of attempts made
      message: '', // Feedback message to display
      invalidInput: false, // Flag for invalid input
    };
  },
  methods: {
    async makeGuess() {
      this.attempts++; // Increment attempts counter

      // Validate the input
      if (isNaN(this.guess) || this.guess < 1 || this.guess > 100) {
        this.message = 'Please enter a valid number between 1 and 100.';
        this.invalidInput = true;
        this.attempts--; // Decrement attempts for invalid input
        return;
      }

      this.invalidInput = false; // Reset invalid input flag
      const answer = parseInt(this.guess);

      // Check the guess
      if (answer === this.randomNumber) {
        this.message = `Congratulations! You guessed the number in ${this.attempts} attempts.`;

        // Save the result to the database
        await dbInteractions.insertData('guessTheNumber', [this.attempts, this.randomNumber]);

        // Reset the game
        this.resetGame();
      } else if (answer > this.randomNumber) {
        this.message = 'Too high. Try again.';
      } else {
        this.message = 'Too low. Try again.';
      }

      // Clear the input field
      this.guess = null;
    },
    resetGame() {
      this.randomNumber = Math.floor(Math.random() * (100 - 1 + 1) + 1); // Generate a new random number
      this.attempts = 0; // Reset attempts
      this.message = ''; // Clear the feedback message
    },
    backToMainMenu() {
      this.$emit('back-to-main-menu'); // Emit event to go back to main menu
    },
  },
};
</script>

<template>
  <div class="game-container">
    <h1>Guess the Number</h1>
    <p v-if="message">{{ message }}</p>
    <form @submit.prevent="makeGuess">
      <input 
        v-model="guess" 
        type="number" 
        placeholder="Enter your guess (1-100)" 
        :class="{ error: invalidInput }"
      />
      <button type="submit">Submit</button>
    </form>
    <p v-if="attempts > 0">Attempts: {{ attempts }}</p>
    <button @click="backToMainMenu">Back to main menu</button>
  </div>
 
</template>

<style scoped>
.game-container {
  text-align: center;
  margin-top: 50px;
}
input {
  padding: 10px;
  font-size: 16px;
  border: 2px solid #ccc;
  border-radius: 4px;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  margin-left: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
button:hover {
  background-color: #0056b3;
}
.error {
  border-color: red;
}
</style>