using Demo.DB;
using Demo.Models;
using Demo.UI;

namespace Demo.Games
{
    public class Blackjack
    {
        private static GameMenu mainMenu;
        private static GameMenu infoMenu;

        private static List<string> suits = new List<string> { "Hearts", "Diamonds", "Clubs", "Spades" };
        private static List<string> values = new List<string> { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        private static List<Card> deck;
        private static List<Card> playerHand;
        private static List<Card> dealerHand;

        public class Card
        {
            public string Suit { get; set; }
            public string Value { get; set; }
        }

        static Blackjack()
        {
            mainMenu = new GameMenu([
             "1 - Play game" ,
             "2 - View info" ,
             "3 - Quit",
             "4 - Main Menu"],
            new List<string> { "1", "2", "3", "4" },
            new Dictionary<string, Action>
            {
             { "1", () => { SetUp(); PlayBlackjack(); } },
             { "2", () => infoMenu.ShowMenu() },
             { "3", () => Environment.Exit(0) },
             { "4", () => Game.showMainMenu()}
            });
            infoMenu = new GameMenu([
             "1 - View History" ,
             "2 - View Statistic",
             "3 - Blackjack Main Menu"],
            new List<string> { "1", "2", "3" },
            new Dictionary<string, Action>
            {
             { "1", () => ShowDataAsync() },
             { "2", () => ShowDataStatisticAsync() },
             { "3", () => mainMenu.ShowMenu() }
            });
        }

        public static void StartPlayBlackjack()
        { mainMenu.ShowMenu(); }

        private static void SetUp()
        {
            const int numDecks = 2; // Adjust the number of decks as needed
            deck = CreateDeck(numDecks);
            ShuffleDeck(deck);

            playerHand = new List<Card> { DealCard(), DealCard() };
            dealerHand = new List<Card> { DealCard(), DealCard() };

            Console.WriteLine("Your hand: " + DisplayHand(playerHand));
        }

        private static List<Card> CreateDeck(int numDecks)
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < numDecks; i++)
            {
                foreach (string suit in suits)
                {
                    foreach (string value in values)
                    { deck.Add(new Card { Suit = suit, Value = value }); }
                }
            }
            return deck;
        }

        private static void ShuffleDeck(List<Card> deck)
        {
            Random random = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }
        }

        private static Card DealCard()
        {
            Card card = deck.Last();
            deck.RemoveAt(deck.Count - 1);
            return card;
        }

        private static int CalculateHandValue(List<Card> hand)
        {
            int value = 0;
            int aceCount = 0;

            foreach (Card card in hand)
            {
                if (card.Value == "Ace")
                {
                    aceCount++;
                    value += 11;
                }
                else if (new List<string> { "Jack", "Queen", "King" }.Contains(card.Value))
                { value += 10; }
                else
                { value += int.Parse(card.Value); }
            }

            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }

            return value;
        }

        private static string DisplayHand(List<Card> hand)
        { return string.Join(", ", hand.Select(card => $"{card.Value} of {card.Suit}")); }

        private static void PlayBlackjack()
        {
            Console.Write("Hit or stand? (h/s): ");
            string answer = Console.ReadLine().ToLower();

            if (answer == "h")
            {
                playerHand.Add(DealCard());
                Console.WriteLine("Your hand: " + DisplayHand(playerHand));
                PlayerWinConditions(CalculateHandValue(playerHand));
            }
            else if (answer == "s")
            {
                while (CalculateHandValue(dealerHand) < 17)
                { dealerHand.Add(DealCard()); }
                Console.WriteLine("Dealer's hand: " + DisplayHand(dealerHand));
                DealerWinConditions(CalculateHandValue(playerHand), CalculateHandValue(dealerHand));
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter \"h\" or \"s\".");
                PlayBlackjack();
            }
        }

        private static void PlayerWinConditions(int playerValue)
        {
            if (playerValue > 21)
            {
                Console.WriteLine("Dealer wins!");
                SaveDataAsync("lose", playerValue);
            }
            else if (playerValue == 21)
            {
                Console.WriteLine("You win!");
                SaveDataAsync("win", playerValue);
            }
            else
            { PlayBlackjack(); }
            mainMenu.ShowMenu();
        }

        private static void DealerWinConditions(int playerValue, int dealerValue)
        {
            if (dealerValue > 21)
            {
                Console.WriteLine("Dealer busts! You win!");
                SaveDataAsync("win", playerValue, dealerValue);
            }
            else if (playerValue > dealerValue)
            {
                Console.WriteLine("You win!");
                SaveDataAsync("win", playerValue, dealerValue);
            }
            else if (playerValue < dealerValue)
            {
                Console.WriteLine("Dealer wins!");
                SaveDataAsync("lose", playerValue, dealerValue);
            }
            else
            {
                Console.WriteLine("Push. It's a tie.");
                SaveDataAsync("tie", playerValue, dealerValue);
            }
            mainMenu.ShowMenu();
        }

        public static async Task SaveDataAsync(string gameResult, int playerValue, int dealerValue = 0)
        {
            await DBAPI.InsertData<BlackjackUser>("Blackjack", gameResult, playerValue, dealerValue);
            Game.blackjackUsers = await DBAPI.GetData<BlackjackUser>("Blackjack");
        }

        public static async Task ShowDataAsync()
        {
            await DBAPI.ShowDataAsync(Game.blackjackUsers);
            infoMenu.ShowMenu();
        }

        public static async Task ShowDataStatisticAsync()
        {
            await DBAPI.ShowDataStatisticAsync(Game.blackjackUsers);
            infoMenu.ShowMenu();
        }
    }
}
