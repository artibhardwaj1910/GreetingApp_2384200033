using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace HelloGreetingApplication.Controllers
{

    /// <summary>
    /// Class Providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGreetingBL _greetingBL;

        public HelloGreetingController(IGreetingBL greetingBL) // Use the interface
        {
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Handles the creation of a new greeting message.
        /// </summary>
        /// <param name="requestModel">The request containing the greeting message.</param>
        /// <returns>Returns a success response if the greeting is saved, or an error response if the input is invalid.</returns>
        [HttpPost("UC4")]
        public IActionResult SendGreeting(RequestModel requestModel)
        {
            ResponseModel<String> responseModel = new ResponseModel<string>();

            if (requestModel == null || string.IsNullOrWhiteSpace(requestModel.Value))
            {
                return BadRequest(new { Success = false, Message = "Invalid input. Message cannot be empty." });
            }

            var greeting = new GreetingEntity { Message = requestModel.Value };
            var savedGreeting = _greetingBL.AddGreeting(greeting);


            responseModel.Success = true;
            responseModel.Message = "Greeting saved successfully.";
            responseModel.Data = savedGreeting.Message;
            _logger.Info("SendGreeting Method Executed Successfully");
            return Ok(responseModel);
        }

        //UC5
        /// <summary>
        /// Retrieves a greeting message by its unique identifier.
        /// </summary>
        /// <param name = "id" > The unique identifier of the greeting message.</param>
        /// <returns>
        /// Returns an HTTP 200 response with the greeting message if found.
        /// Returns an HTTP 404 response if no greeting message is found.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.Info($"GetGreetingById called with id: {id}");

            ResponseModel<GreetingEntity?> responseModel = new ResponseModel<GreetingEntity?>();

            var greeting = _greetingBL.GetGreetingById(id);

            if (greeting == null)
            {
                responseModel.Success = false;
                responseModel.Message = "Greeting not found";
                responseModel.Data = null;
                _logger.Warn($"Greeting not found for id: {id}");
                return NotFound(responseModel);
            }

            responseModel.Success = true;
            responseModel.Message = "Greeting found successfully.";
            responseModel.Data = greeting;
            _logger.Info($"Greeting found: {greeting.Message}");

            return Ok(responseModel);
        }

        /// <summary>
        /// Retrieves a list of all greeting messages.
        /// </summary>
        /// <returns>A list of all stored greetings.</returns>
        [HttpGet("ListOfAll")]
        public IActionResult GetAllGreetings()
        {
            _logger.Info("GetAllGreetings method called.");

            var greetings = _greetingBL.GetAllGreetings();

            if (greetings == null || greetings.Count == 0)
            {
                _logger.Warn("No greetings found in the database.");
                return NotFound(new { Success = false, Message = "No greetings found." });
            }

            _logger.Info("All greetings retrieved successfully.");
            return Ok(new { Success = true, Data = greetings });
        }

        /// <summary>
        /// Patch a greeting message 
        /// </summary>
        /// <returns> returns a updated greeting message</returns>

        [HttpPatch("{id}")]
        public IActionResult PatchGreeting(int id, RequestGreetingModel updatedGreeting)
        {
            _logger.Info($"UpdateGreeting method called with id: {id}");
            try
            {
                var greeting = _greetingBL.UpdateGreeting(id, updatedGreeting.Message);
                if (greeting == null)
                {
                    _logger.Info("Greeting update failed: ID not found.");
                    return NotFound(new { Success = false, Message = "Greeting not found." });
                }
                _logger.Info("Greeting updated successfully.");
                return Ok(new { Success = true, Data = greeting.Message });
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while fetching greeting for id: {id}. Error: {ex.Message}");
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred." });
            }


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _logger.Info($"DELETE request received: Delete Greeting ID={id}");
                ResponseModel<GreetingEntity> response = new ResponseModel<GreetingEntity?>();
                var isDeleted = _greetingBL.DeleteGreeting(id);

                if (!isDeleted)
                {
                    response.Success = false;
                    response.Message = "Greeting not found!";
                    response.Data = null;
                    _logger.Info($"Greeting deletion failed: ID={id} not found.");
                    return NotFound(response);
                }

                response.Success = true;
                response.Message = "Greeting deleted successfully!";
                response.Data = null;
                _logger.Info($"Greeting with ID={id} deleted successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while deleting the greeting.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get method to get the Greeting Message
        /// </summary>
        /// <returns>"Hello World"</returns>
        [HttpGet]
        public IActionResult Get()
        {
           
            ResponseModel<String> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "API Endpoint Hit";
            responseModel.Data = "Hello, World!";
            _logger.Info("Get Method Executed");
            return Ok(responseModel);
        }



        /// <summary>
        /// Post method to accept a custom greeting message
        /// </summary>
        /// <param name="userModel">Greeting message from user</param>
        /// <returns>Confirmation response</returns>
        [HttpPost("greet")]
        public IActionResult Post(UsernameRequestModel userModel)
        {

            var response = _greetingBL.getGreetMessage(userModel);
            ResponseModel<String> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "API Endpoint Hit";
            responseModel.Data =response;
            _logger.Info("Post Method Executed");
            return Ok(responseModel);


        }

        /// <summary>
        /// Post method to accept a custom greeting message
        /// </summary>
        /// <param name="requestModel">Greeting message from user</param>
        /// <returns>Confirmation response</returns>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            ResponseModel<String> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "API Endpoint Hit";
            responseModel.Data = $"Key: {requestModel.Key} , Value : {requestModel.Value} ";
            _logger.Info("Post Method Executed");
            return Ok(responseModel);


        }

        /// <summary>
        /// Put method to accept a custom greeting message
        /// </summary>
        /// <param name="requestModel">Greeting message from user</param>
        /// <returns>Confirmation response</returns>
        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {
            ResponseModel<String> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "API Endpoint Hit in Put Method";
            responseModel.Data = $"Key: {requestModel.Key} , Value : {requestModel.Value} ";
            _logger.Info("Put Method Executed");
            return Ok(responseModel);
            
        }

        /// <summary>
        /// Patch method to accept a custom greeting message
        /// </summary>
        /// <param name="requestModel">Greeting message from user</param>
        /// <returns>Confirmation response</returns>
        [HttpPatch]
        public IActionResult Patch(RequestModel requestModel)
        {
            ResponseModel<String> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "API Endpoint Hit";
            responseModel.Data = $"Key: {requestModel.Key} , Value : {requestModel.Value} ";
            _logger.Info("Patch Method Executed");
            return Ok(responseModel);
        }

        /// <summary>
        /// Delete method to remove a greeting message
        /// </summary>
        [HttpDelete]
        public IActionResult Delete()
        {

            ResponseModel<String> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "API Endpoint Hit";
            responseModel.Data = null ;
            _logger.Info("Detele Method Executed");
            return Ok(responseModel);
        }
    }
}
