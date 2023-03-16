using app1.DTO;
using app1.Logger;
using app1.Model;
using app1.Repo;
using app1.Repository.RepositoryInterface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace app1.Controllers
{
    //without multi versioning
    //[Route("api/villa")]
    //with multi versioning
    [Route("api/v{version:apiVersion}/villa")]
    [ApiController]
    [ApiVersion("1.0", Deprecated =true)]
    [ApiVersion("2.0")]
    public class VillaApiController : ControllerBase
    {

        //Using Dependency Injection to show Logger when logging to application
        /*
        public readonly ILogger<VillaApiController> _Logger;
        public VillaApiController(ILogger<VillaApiController> _logger)
        {
            _Logger = _logger;
        }*/

        /*
        public readonly ILoggers _logger;
        public VillaApiController(ILoggers logger)
        {
            _logger = logger;
        }*/

        /*
        public readonly ILoggersV2 _logger;
        public VillaApiController(ILoggersV2 logger)
        {
            _logger = logger;
        }*/

        //Using dependency injection with DbContext
        //private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        //replacing DbContext to IRepository
        private readonly IVillaRepository _repo;
        protected APIResponse _response;
        public VillaApiController(ApplicationDbContext context, 
                                  IMapper mapper, 
                                  IVillaRepository repo)
        {
            /*_context = context;*/
            _mapper = mapper;
            _repo = repo;
            _response = new();
        }


        /*
        [HttpGet]
        public IEnumerable<VillaModel> GetVillas()
        {
            return new List<VillaModel>
            {
                new VillaModel{Id = 0, Name = "Pool View"},
                new VillaModel{Id = 1, Name = "Beach View"}
            };
        }*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[Authorize]
        [MapToApiVersion("1.0")]
        //public async Task<ActionResult<IEnumerable<VillaModelDTO>>> GetAllVillaDTO()
        public async Task<ActionResult<APIResponse>> GetAllVillaDTO()
        {
            //_Logger.LogInformation("Getting All Villas");
            //_logger.LoggersV2("not logging","Error");
            //return Ok(VillaRepo.villaList);

            //return Ok(await _context.Villas.ToListAsync());
            //By using Mapper:
            /*IEnumerable<VillaModel> villaModelList = await _context.Villas.ToListAsync();
            return Ok(_mapper.Map<VillaModelDTO>(villaModelList));*/
            //return Ok(_mapper.Map<VillaModelDTO>(await _context.Villas.ToListAsync()));

            //With repository pattern
            //return Ok(await _repo.GetAllNotNull());

            //With APIRepository
            try
            {
                IEnumerable<VillaModel> villaModelList = await _repo.GetAllNotNull();
                _response.Result = _mapper.Map<List<VillaModelDTO>>(villaModelList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
            
        }

        [HttpGet("getnothing")]
        [MapToApiVersion("2.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1","value2" };
            
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        /*
        [ProducesResponseType(200, Type=typeof(VillaModelDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]*/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles ="ADMIN")]
        //public async Task<ActionResult<VillaModelDTO>> GetVillaDto(int id)
        public async Task<ActionResult<APIResponse>> GetVillaDto(int id)
        {
            try
            {


                if (id < 0)
                {
                    //return Content("it's bad request YO");
                    return BadRequest();
                    //return Redirect("https://www.google.com");
                }


                //var villa = VillaRepo.villaList.FirstOrDefault(u => u.Id == id);

                //var villa = await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);
                //with repository pattern
                var villa = await _repo.GetVillaNotNull(u => u.Id == id);


                if (villa == null)
                {
                    //_Logger.LogError("The Villa Id is: " + villa.Id);
                    //_logger.LoggersV2("Error no data is null ","Error");
                    return NotFound();
                }
                //_Logger.LogWarning("The Villa Id is: " + villa.Id);
                //_logger.LoggersV2($"the information of logger with id: {id.ToString()}","Information");
                //_context.SaveChangesAsync();

                _response.Result = _mapper.Map<VillaModelDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                //_repo.Save();
                //return Ok(villa);
                //Using Auto mapper
                //return Ok(_mapper.Map<VillaModelDTO>(villa));

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<VillaModelDTO>> CreateVilla([FromBody] CreateVillaModelDTO villaDto)
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] CreateVillaModelDTO villaDto)
        {
            try
            {
                //Generate custom validation / ERROR
                /*
                VillaModel villa = new()
                {
                    //Id = villaDto.Id,
                    Name = villaDto.Name,
                    CreateData = villaDto.CreateData,
                    Description = villaDto.Description,
                    ImageUrl = villaDto.ImageUrl,
                    Rate = villaDto.Rate,
                    UpdateData = villaDto.UpdateData,
                    sqft = villaDto.sqft
                };*/

                //using auto mapper:
                VillaModel villa = _mapper.Map<VillaModel>(villaDto);

                //if(await _context.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villa.Name.ToLower()) != null)
                //if (VillaRepo.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)

                //with repository pattern
                if (await _repo.GetVillaNotNull(u => u.Name.ToLower() == villa.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom Error", "Villa already Exists!");
                    return BadRequest(ModelState);
                }
                /*
                //To fix validation code work without [ApiController] 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                */

                //1- to check parameter is not null:
                if (villa == null)
                //if (villaDto == null)
                {
                    return BadRequest(villaDto);
                }
                //2- to clear Id in parameter and add it again later to compatible with Id order numbers:
                //if(villa.Id > 0)
                //if (villaDto.Id > 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError);
                //}
                //3- addint new Id value:
                //villa.Id = _context.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                //villaDto.Id = VillaRepo.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
                //_context.Villas.Add(villa);

                //with repository pattren
                await _repo.Create(villa);
                _response.Result = _mapper.Map<VillaModelDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                //VillaRepo.villaList.Add(villaDto);

                //await _context.SaveChangesAsync();

                //with repository pattern
                await _repo.Save();
                //return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);

                //return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
                //return Ok(villaDto);
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpDelete("id:int", Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles ="CUSTOMER")]
        //public async Task<IActionResult> DeleteVilla(int id)
        public async Task<ActionResult<APIResponse>>DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    //return BadRequest();
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
                //TO select which property to delete
                //var villa = await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);

                //with repository
                var villa = await _repo.GetVillaNotNull(u => u.Id == id);


                //var villa = VillaRepo.villaList.FirstOrDefault(u => u.Id == id);
                //if there is no object selected
                if (villa == null)
                {
                    return NotFound();
                }
                //Removing object
                //_context.Villas.Remove(villa);

                //with repository pattern

                await _repo.Remove(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                //VillaRepo.villaList.Remove(villa);
                //Usually use no content with delete as action result
                //_context.SaveChangesAsync();

                //with repository pattern
                _repo.Save();
                //return NoContent();
                return _response;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }


            return _response;

        }


        [HttpPut("{id:int}", Name ="UpdateVilla")]
        //typing response status
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> PutVilla(int id, [FromBody]VillaModelDTO villaDto)
        public async Task<ActionResult<APIResponse>> PutVilla(int id, [FromBody] VillaModelDTO villaDto)
        {
            try
            {
                //To matching villaModel with villaDTO
                /*
                VillaModel villa = new()
                {
                    Id = villaDto.Id,
                    Name = villaDto.Name,
                    CreateData = villaDto.CreateData,
                    Description = villaDto.Description,
                    ImageUrl = villaDto.ImageUrl,
                    Rate = villaDto.Rate,
                    UpdateData = villaDto.UpdateData,
                    sqft = villaDto.sqft
                };*/

                //Using Auto Mapper
                VillaModel villa = _mapper.Map<VillaModel>(villaDto);
                //To check no null value in data
                if (id == null || villa == null)
                {
                    return BadRequest();
                }

                await _repo.Update(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;


                //To select data from list
                //var v = await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);

                //with repository pattern

                //var v = await _repo.GetVillaNotNull(u => u.Id == id);

                //var v = VillaRepo.villaList.FirstOrDefault(u => u.Id == id);
                //To make update for data
                //v.Name = villa.Name;
                //return for IAction update is NoContent
                //await _context.SaveChangesAsync();

                //with repository pattern
                await _repo.Save();
                //return NoContent();
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        //typing responses
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaModelDTO> patchVilla)
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaModelDTO>patchVilla )
        {
            try
            {


                //if there is no parameter
                if (id == null || patchVilla == null)
                {
                    return BadRequest();
                }

                //Adding not Tracking

                //var v = await _context.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

                //with repository pattren

                var v = await _repo.GetVillaNotNull(u => u.Id == id, true);

                //var v = VillaRepo.villaList.FirstOrDefault(u => u.Id == id);
                //if data with id is not exist
                /*
                VillaModel villa = new()
                {
                    Id = v.Id,
                    Name = v.Name,
                    CreateData = v.CreateData,
                    Description = v.Description,
                    ImageUrl = v.ImageUrl,
                    Rate = v.Rate,
                    UpdateData = v.UpdateData,
                    sqft = v.sqft
                };*/

                //Using Auto Mapper model -> DTO
                VillaModelDTO villa = _mapper.Map<VillaModelDTO>(v);


                if (v == null)
                {
                    return BadRequest();
                }
                //update data with id above:
                patchVilla.ApplyTo(villa, ModelState);
                /*
                VillaModel model = new VillaModel()
                {
                    Id = v.Id,
                    Name = v.Name,
                    CreateData = v.CreateData,
                    Description = v.Description,
                    ImageUrl = v.ImageUrl,
                    Rate = v.Rate,
                    UpdateData = v.UpdateData,
                    sqft = v.sqft
                };*/

                //Using auto mapper DTO -> model
                VillaModel model = _mapper.Map<VillaModel>(villa);
                //_context.Villas.Update(model);


                _repo.Update(model);

                //await _context.SaveChangesAsync();
                await _repo.Save();

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                if (!ModelState.IsValid)
                {
                    //return BadRequest(ModelState);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                }


                //return NoContent();
                return _response;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
