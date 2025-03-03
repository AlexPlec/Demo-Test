using Demo.Models;
using System.Text;
using System.Text.Json;

namespace Demo.DB
{
    public class DBAPI
    {

        static HttpClient CreateHttpClient()
        { return new HttpClient(); }

        async static Task<string> SendGetRequest(HttpClient client, string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            { throw new Exception($"Error: {response.StatusCode}"); }
        }

        async static Task<HttpResponseMessage> SendPostRequest(HttpClient client, string url, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            return response;
        }

        public static async Task<List<T>?> GetData<T>(string endPoint, params object[] parameters) where T : class
        {
            using (HttpClient client = CreateHttpClient())
            {
                string url = $"http://localhost:7129/api/Tasks/{endPoint}";
                string data = await SendGetRequest(client, url);

                List<T> dataList = JsonSerializer.Deserialize<List<T>>(data);
                return dataList;
            }
        }

        public static async Task ShowDataAsync<T>(List<T> dataList)
        {

            foreach (var entity in dataList)
            {
                string output = "";
                foreach (var property in entity.GetType().GetProperties())
                {
                    if (property.Name.ToLower() != "id")
                    { output += $"{property.Name}: {property.GetValue(entity)} "; }
                }
                Console.WriteLine(output);
            }
        }

        public static async Task InsertData<T>(string endPoint, params object[] parameters) where T : class
        {
            using (HttpClient client = CreateHttpClient())
            {
                string url = $"http://localhost:7129/api/Tasks/{endPoint}";

                T data = (T)Activator.CreateInstance(typeof(T), parameters);

                string jsonData = JsonSerializer.Serialize(data);
                await SendPostRequest(client, url, jsonData);
            }
        }

        public static async Task ShowDataStatisticAsync<T>(List<T> dataList) where T : class
        {
            var entities = dataList;

            if (typeof(T) == typeof(BlackjackUser))
            {

                int totalGames = entities.Count();
                int wins = entities.OfType<BlackjackUser>().Count(entity => entity.GameResult == "win");
                int losses = entities.OfType<BlackjackUser>().Count(entity => entity.GameResult == "lose");
                double winRate = (double)wins / totalGames * 100;

                Console.WriteLine("Blackjack User Statistics:");
                Console.WriteLine($"- Total Games Played: {totalGames}");
                Console.WriteLine($"- Wins: {wins}");
                Console.WriteLine($"- Losses: {losses}");
                Console.WriteLine($"- Win Rate: {winRate:F2}%");
            }
            else if (typeof(T) == typeof(GuessTheNumberUser))
            {

                int totalGames = entities.Count();
                int averageAttempts = (int)entities.OfType<GuessTheNumberUser>().Average(entity => entity.Attempts);

                Console.WriteLine("GuessTheNumber User Statistics:");
                Console.WriteLine($"- Total Games Played: {totalGames}");
                Console.WriteLine($"- Average Attempts: {averageAttempts}");
            }
            else
            { Console.WriteLine("Statistics not available for this model type."); }
        }
    }
}
