using Demo.DB;
using Demo.Games;
using Demo.Models;
using Demo.UI;

namespace Demo
{
    public class Game
    {
        public static List<GuessTheNumberUser> guessTheNumberUsers;
        public static List<BlackjackUser> blackjackUsers;
        private static GameMenu menu;

        static Game()
        {
            menu = new GameMenu([
             "1 - Start game Blackjack" ,
             "2 - Start game GuessTheNumber",
             "3 - Quit"],
            new List<string> { "1", "2", "3" },
            new Dictionary<string, Action>
            {
             { "1", () => Blackjack.StartPlayBlackjack() },
             { "2", () => GuessTheNumber.StartPlayGuessTheNumber() },
             { "3", () => Environment.Exit(0) },
            });
        }

        public static async Task Main(string[] args)
        {
            guessTheNumberUsers = await DBAPI.GetData<GuessTheNumberUser>("GuessTheNumber");
            blackjackUsers = await DBAPI.GetData<BlackjackUser>("Blackjack");
            menu.ShowMenu();
        }
        public static void showMainMenu()
        { menu.ShowMenu(); }
    }
}