using DemoDB.DB;
using DemoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly GameDataManager _dataManager;

        public TasksController(GameDataManager dataManager)
        { _dataManager = dataManager; }

        [HttpGet("GuessTheNumber")]
        public async Task<IActionResult> GetGuessTheNumberData()
        {
            var users = await _dataManager.GetAllAsync<GuessTheNumberUser>();
            string jsonString = JsonSerializer.Serialize(users);

            return Ok(jsonString);
        }

        [HttpGet("Blackjack")]
        public async Task<IActionResult> GetBlackjackData()
        {
            var users = await _dataManager.GetAllAsync<BlackjackUser>();
            string jsonString = JsonSerializer.Serialize(users);

            return Ok(jsonString);
        }

        [HttpPost("GuessTheNumber")]
        public async Task<IActionResult> SaveGuessTheNumberResult([FromBody] GuessTheNumberUser user)
        {
            if (user == null)
            { return BadRequest("Invalid game result data"); }

            await _dataManager.SaveDataAsync(user);

            return Ok();
        }

        [HttpPost("Blackjack")]
        public async Task<IActionResult> SaveBlackjackResult([FromBody] BlackjackUser user)
        {
            if (user == null)
            { return BadRequest("Invalid game result data"); }

            await _dataManager.SaveDataAsync(user);

            return Ok();
        }
    }
}
