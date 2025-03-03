const { createInterface } = require('node:readline');

const rl = createInterface({
  input: process.stdin,
  output: process.stdout
});

function createRlQuestion(prompt) {
  return new Promise((resolve, reject) => {
    rl.question(prompt, (answer) => {
      resolve(answer);
    });
  });
}

function rlClose() {
  rl.close();
}

module.exports = { createRlQuestion, rlClose };