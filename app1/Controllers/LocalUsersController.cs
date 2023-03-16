using app1.DTO;
using app1.Repository.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace app1.Controllers
{
    //without multi versioning
    //[Route("api/UserAuth")]
    //with multi versioning
    [Route("api/v{version:apiVersion}/UserAuth")]
    //[ApiVersion("1.0")]
    [ApiVersionNeutral]
    [ApiController]
    public class LocalUsersController:Controller
    {
        
        private readonly ILocalUsers _userRepo;
        protected APIResponse _response;
        public LocalUsersController(ILocalUsers userRepo)
        {
            _userRepo = userRepo;
            this._response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestLocalUsersDTO model)
        {
            try
            {
                var loginResponse = await _userRepo.Login(model);
                await Console.Out.WriteLineAsync(loginResponse.Tocken + "  --  " + loginResponse.User);


                if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Tocken))
                {
                    //the Apiresopnse can give us the reuslt in many useful phases
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Username or password is incorrect");
                    return BadRequest(_response);
                }
                //in success case:
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponse;
                return Ok(_response);
            }
            catch (Exception)
            {

                throw;
            }
            

        }




        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrerationRequestLocalUsersDTO model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
