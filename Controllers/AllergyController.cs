using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/allergy")]
public class AllergyController: ControllerBase
{
    private readonly IAllergyRepository _allergyRepository;
    private readonly IPetRepository _petRepository;
    public AllergyController(IAllergyRepository allergyRepository, IPetRepository petRepository){
        _allergyRepository = allergyRepository;
        _petRepository = petRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var allergies = await _allergyRepository.GetAllAsync();
        
        if(allergies.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(allergies.Select(p => p.ToAllergyDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var allergy = await _allergyRepository.GetByIdAsync(id);

        if(allergy == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(allergy.ToAllergyDTO());
    }
    [HttpGet("bypetid/{petId}")]
    public async Task<IActionResult> GetByPetId([FromRoute] int petId){
        var allergies = await _allergyRepository.GetAllByPetIdAsync(petId);
        if(allergies.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(allergies?.Select(p => p.ToAllergyDTO()));
    }

    [HttpPost("{petId}")]
    public async Task<IActionResult> Create([FromRoute] int petId, [FromBody] CreateAllergyDTO allergyDTO){
        if(!await _petRepository.PetExists(petId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var allergy = allergyDTO.ToAllergyFromAllergyDTO(petId);
        await _allergyRepository.CreateAsync(allergy);

        return CreatedAtAction(nameof(GetById), new {id = allergy.Id}, allergy.ToAllergyDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateAllergyDTO updateAllergyDTO){
        var allergy = await _allergyRepository.UpdateAsync(id, updateAllergyDTO);

        if(allergy == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(allergy.ToAllergyDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var allergy = await _allergyRepository.DeleteAsync(id);
        if(allergy == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }
}
