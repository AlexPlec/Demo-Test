namespace Demo.Models
{
    public class GuessTheNumberUser
    {
        public int Id { get; set; }
        public int Attempts { get; set; }
        public int Number { get; set; }

        public GuessTheNumberUser(int attempts, int number)
        {
            Attempts = attempts;
            Number = number;
        }
    }
}
