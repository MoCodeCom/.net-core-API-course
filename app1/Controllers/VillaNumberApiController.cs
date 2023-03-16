using app1.DTO;
using app1.Logger;
using app1.Model;
using app1.Repo;
using app1.Repository.RepositoryInterface;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace app1.Controllers
{
    [Route("api/villaNumber")]
    [ApiController]
    public class VillaNumberApiController : ControllerBase
    {

        private readonly IVillaRepository _repoVilla;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _repo;
        protected APIResponse _response;
        public VillaNumberApiController(ApplicationDbContext context, 
                                  IMapper mapper, 
                                  IVillaNumberRepository repo,
                                  IVillaRepository repoVilla)
        {
            _mapper = mapper;
            _repo = repo;
            _response = new();
            _repoVilla = repoVilla;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetAllVillaDTO()
        {
            try
            {
                IEnumerable<VillaNumber> VillaNumberList = await _repo.GetAllNotNull();
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(VillaNumberList);
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

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> GetVillaDto(int id)
        {
            try
            {


                if (id < 0)
                {

                    return BadRequest();
                    
                }


                var villa = await _repo.GetVillaNotNull(u => u.VillaNo == id);


                if (villa == null)
                {

                    return NotFound();
                }
                

                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] CreateVillaNumberDTO villaDto)
        {
            try
            {
                
                VillaNumber villa = _mapper.Map<VillaNumber>(villaDto);

                if (await _repo.GetVillaNotNull(u => u.VillaNo == villaDto.VillaNo) != null)
                {
                    ModelState.AddModelError("Custom Error", "Villa already Exists!");
                    return BadRequest(ModelState);
                }

                if (villa == null)

                {
                    return BadRequest(villaDto);
                }

                if (await _repoVilla.GetAllNotNull(u => u.Id == villaDto.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }


                await _repo.Create(villa);
                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;

                await _repo.Save();
                
                return CreatedAtRoute("GetVilla", new { id = villa.VillaNo }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpDelete("id:int", Name ="DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>>DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
 
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }


                
                var villa = await _repo.GetVillaNotNull(u => u.VillaNo == id);


                
                if (villa == null)
                {
                    return NotFound();
                }
                
                await _repo.Remove(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                
                _repo.Save();
                
                return _response;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }


            return _response;

        }


        [HttpPut("{id:int}", Name ="UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> PutVilla(int id, [FromBody] UpdateVillaNumberDTO villaDto)
        {
            try
            {
                
                VillaNumber villa = _mapper.Map<VillaNumber>(villaDto);

                if (id == null || villa == null)
                {
                    return BadRequest();
                }

                if (await _repoVilla.GetAllNotNull(u => u.Id == villaDto.VillaId) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }

                await _repo.Update(villa);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;


                await _repo.Save();

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpPatch("{id:int}", Name ="UpdatePartialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaNumberDTO>patchVilla )
        {
            try
            {
                if (id == null || patchVilla == null)
                {
                    return BadRequest();
                }

                var v = await _repo.GetVillaNotNull(u => u.VillaNo == id, true);

                VillaNumberDTO villa = _mapper.Map<VillaNumberDTO>(v);


                if (v == null)
                {
                    return BadRequest();
                }

                patchVilla.ApplyTo(villa, ModelState);

                VillaNumber model = _mapper.Map<VillaNumber>(villa);

                _repo.Update(model);
                await _repo.Save();

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                }
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
