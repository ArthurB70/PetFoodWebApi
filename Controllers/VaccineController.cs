using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/vaccine")]
public class VaccineController : ControllerBase
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IPetRepository _petRepository;
    public VaccineController(IVaccineRepository vaccineRepository, IPetRepository petRepository){
        _vaccineRepository = vaccineRepository;
        _petRepository = petRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var vaccines = await _vaccineRepository.GetAllAsync();
        
        if(vaccines.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(vaccines.Select(v => v.ToVaccineDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var vaccine = await _vaccineRepository.GetByIdAsync(id);

        if(vaccine == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(vaccine.ToVaccineDTO());
    }
    [HttpGet("bypetid/{petId}")]
    public async Task<IActionResult> GetByPetId([FromRoute] int petId){
        var vaccines = await _vaccineRepository.GetAllByPetIdAsync(petId);
        if(vaccines.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(vaccines?.Select(v => v.ToVaccineDTO()));
    }

    [HttpPost("{petId}")]
    public async Task<IActionResult> Create([FromRoute] int petId, [FromBody] CreateVaccineDTO vaccineDTO){
        if(!await _petRepository.PetExists(petId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var vaccine = vaccineDTO.ToVaccineFromCreateVaccineDTO(petId);
        await _vaccineRepository.CreateAsync(vaccine);

        return CreatedAtAction(nameof(GetById), new {id = vaccine.Id}, vaccine.ToVaccineDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateVaccineDTO updateVaccineDTO){
        var vaccine = await _vaccineRepository.UpdateAsync(id, updateVaccineDTO);

        if(vaccine == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(vaccine.ToVaccineDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var vaccine = await _vaccineRepository.DeleteAsync(id);
        if(vaccine == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }
}
