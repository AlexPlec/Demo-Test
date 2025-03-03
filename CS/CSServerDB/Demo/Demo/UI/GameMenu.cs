namespace Demo.UI
{
    public class GameMenu
    {
        private readonly string prompt;
        private readonly List<string> options;
        private readonly Dictionary<string, Action> actions;

        public GameMenu(string[] promptLines, List<string> options, Dictionary<string, Action> actions)
        {
            prompt = string.Join(Environment.NewLine, promptLines);
            this.options = options;
            this.actions = actions;
        }
        public async Task ShowMenu()
        {
            Console.WriteLine(prompt);
            Console.WriteLine("Enter an option:");

            while (true)
            {
                string choice = Console.ReadLine().ToLower();
                if (options.Contains(choice))

                {
                    actions[choice]();
                    break;
                }
                else
                { Console.WriteLine("Invalid input. Please try again."); }
            }
        }
    }
}

