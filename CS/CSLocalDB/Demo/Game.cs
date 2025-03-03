using Demo.Games;
using Demo.UI;

namespace Demo
{
    public class Game
    {
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
             { "2", () => GuessTheNumber.StartPlayGuesTheNumber() },
             { "3", () => Environment.Exit(0) }
            });
        }
        public static void Main(string[] args)
        { menu.ShowMenu(); }

        public static void showMainMenu()
        { menu.ShowMenu(); }
    }
}
