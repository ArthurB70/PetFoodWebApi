using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/medicine")]
public class MedicineController : ControllerBase
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IPetRepository _petRepository;
    public MedicineController(IMedicineRepository medicineRepository, IPetRepository petRepository){
        _medicineRepository = medicineRepository;
        _petRepository = petRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var medicines = await _medicineRepository.GetAllAsync();
        
        if(medicines.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(medicines.Select(m => m.ToMedicineDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var medicine = await _medicineRepository.GetByIdAsync(id);

        if(medicine == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(medicine.ToMedicineDTO());
    }
    [HttpGet("bypetid/{petId}")]
    public async Task<IActionResult> GetByPetId([FromRoute] int petId){
        var medicines = await _medicineRepository.GetAllByPetIdAsync(petId);
        if(medicines.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(medicines?.Select(m => m.ToMedicineDTO()));
    }

    [HttpPost("{petId}")]
    public async Task<IActionResult> Create([FromRoute] int petId, [FromBody] CreateMedicineDTO medicineDTO){
        if(!await _petRepository.PetExists(petId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var medicine = medicineDTO.ToMedicineFromCreateMedicineDTO(petId);
        await _medicineRepository.CreateAsync(medicine);

        return CreatedAtAction(nameof(GetById), new {id = medicine.Id}, medicine.ToMedicineDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateMedicineDTO updateMedicineDTO){
        var medicine = await _medicineRepository.UpdateAsync(id, updateMedicineDTO);

        if(medicine == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(medicine.ToMedicineDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var medicine = await _medicineRepository.DeleteAsync(id);
        if(medicine == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }
}
