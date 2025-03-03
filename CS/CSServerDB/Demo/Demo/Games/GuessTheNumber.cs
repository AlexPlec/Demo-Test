using Demo.DB;
using Demo.Models;
using Demo.UI;
namespace Demo.Games
{
    public class GuessTheNumber
    {
        private static GameMenu mainMenu;
        private static GameMenu infoMenu;
        private static int randomNumber;
        private static int attempts;

        static GuessTheNumber()
        {
            mainMenu = new GameMenu([
            "1 - Play game" ,
            "2 - View info" ,
            "3 - Quit",
            "4 - Main Menu"],
            new List<string> { "1", "2", "3", "4" },
            new Dictionary<string, Action>
            {
             { "1", () => StartGame(randomNumber) },
             { "2", () =>  infoMenu.ShowMenu() },
             { "3", () => Environment.Exit(0) },
             { "4", () => Game.showMainMenu() }
            });
            infoMenu = new GameMenu([
            "1 - View History" ,
            "2 - View Statistic",
            "3 - Guess The Number Main Menu"],
            new List<string> { "1", "2", "3" },
            new Dictionary<string, Action>
            {
             { "1", () => ShowDataAsync() },
             { "2", () => ShowDataStatisticAsync() },
             { "3", () => mainMenu.ShowMenu() }
            });
        }

        public static void StartPlayGuessTheNumber()
        {
            CreateGuess();
            mainMenu.ShowMenu();
        }

        public static void CreateGuess()
        {
            Random random = new Random();
            randomNumber = random.Next(1, 101);
        }

        public static void StartGame(int randomNumber)
        {
            int guess;

            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out guess))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                StartGame(randomNumber);
            }

            attempts++;

            if (guess < randomNumber)
            {
                Console.WriteLine("The number is higher.");
                StartGame(randomNumber);
            }
            else if (guess > randomNumber)
            {
                Console.WriteLine("The number is lower.");
                StartGame(randomNumber);
            }
            else
            {
                Console.WriteLine($"Congratulations! You guessed the number in {attempts} attempts.");
                SaveDataAsync(attempts, randomNumber);
            }
            attempts = 0;
            CreateGuess();
            mainMenu.ShowMenu();
        }

        public static async Task SaveDataAsync(int attempts, int randomNumber)
        {
            await DBAPI.InsertData<GuessTheNumberUser>("GuessTheNumber", attempts, randomNumber);
            Game.guessTheNumberUsers = await DBAPI.GetData<GuessTheNumberUser>("GuessTheNumber");
        }

        public static async Task ShowDataAsync()
        {
            await DBAPI.ShowDataAsync(Game.guessTheNumberUsers);
            infoMenu.ShowMenu();
        }

        public static async Task ShowDataStatisticAsync()
        {
            await DBAPI.ShowDataStatisticAsync(Game.guessTheNumberUsers);
            infoMenu.ShowMenu();
        }
    }
}