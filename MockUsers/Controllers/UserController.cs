using Microsoft.AspNetCore.Mvc;
using MockUsers.DTO;
using MockUsers.Models;
using MockUsers.Services;
using MockUsers.UsersInteralSemiDb;
using MockUsers.Validation;
using static System.Net.WebRequestMethods;

namespace MockUsers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> p_logger;
        private readonly IRanomUserApiService p_apiService;
        private readonly IUserDb p_userDb;

        public UserController(IRanomUserApiService service, IUserDb userDb, ILogger<UserController> logger)
        {
            p_apiService = service;
            p_userDb = userDb;
            p_logger = logger;
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsersData([FromQuery] [GenderValidation] string gender)
        {
            try
            {
                List<User> users = await p_apiService.GetUsersData(gender, 10);
                return Ok(users);
            }
            catch (Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching user data.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("mostpopcountry")]
        public async Task<ActionResult> GetMostPopularCountry()
        {
            try
            {
                KeyValuePair<string, int> winningCountry = await p_apiService.GetMostPopularCountry(5000);
                return Ok($"Most Appeared Country is: {winningCountry.Key} with {winningCountry.Value} apperences ");
            }
            catch (Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching user data.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("listofmails")]
        public async Task<ActionResult> GetListOfMails()
        {
            try
            {
                List<string> mailsList = await p_apiService.RandomMails(30);
                return Ok(mailsList);
            }
            catch (Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching user data.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("staticuser")]
        public async Task<ActionResult> CreateStaticUser([FromBody] NewStaticUserDTO userDTO)
        {
            try
            {
                int newId = p_userDb.AddUser(userDTO);
                return Ok($"User with id:{newId} Created! ");
            }
            catch (Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching user data.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("staticuser/{userId}")]
        public async Task<ActionResult> GetStaticUser(int userId)
        {
            try
            {
                NewStaticUserDTO fetchedUser = p_userDb.GetUser(userId);
                return Ok(fetchedUser);
            }
            catch(Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching user data.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("staticuser")]
        public async Task<ActionResult> UpdateStaticUser([FromBody] UpdateStaticUserDTO userDTO)
        {
            try
            {
                int newId = p_userDb.UpdateUser(userDTO);
                if (newId != -1)
                {
                    return Ok($"User with id:{newId} Updated! ");
                }
                else
                    return NotFound($"User with ID: {userDTO.Id} Not Found!");
            }
            catch (Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching user data.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


    }
}
