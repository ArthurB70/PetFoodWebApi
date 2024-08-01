using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/pet")]
public class PetController : ControllerBase
{
    private readonly IPetRepository _petRepository;
    private readonly IAllergyRepository _allergyRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IMedicineRepository _medicineRepository;
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IUserRepository _userRepository;
    public PetController(IPetRepository petRepository, 
                        IUserRepository userRepository,
                        IAllergyRepository allergyRepository,
                        ICommentRepository commentRepository,
                        IMedicineRepository medicineRepository,
                        IVaccineRepository vaccineRepository){

        _petRepository = petRepository;
        _allergyRepository = allergyRepository;
        _commentRepository = commentRepository;
        _medicineRepository = medicineRepository;
        _vaccineRepository = vaccineRepository;
        _userRepository = userRepository;

    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var pets = await _petRepository.GetAllAsync();
        
        if(pets.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(pets.Select(p => p.ToPetDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var pet = await _petRepository.GetByIdAsync(id);

        if(pet == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(pet.ToPetDTO());
    }
    [HttpGet("byuserid/{userId}")]
    public async Task<IActionResult> GetByUserId([FromRoute] int userId){
        var pets = await _petRepository.GetAllByUserIdAsync(userId);
        if(pets.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(pets?.Select(p => p.ToPetDTO()));
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] CreatePetDTO createPetDTO){
        if(!await _userRepository.UserExists(userId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var pet = createPetDTO.ToPetFromCreatePetDTO(userId);
        await _petRepository.CreateAsync(pet);

        return CreatedAtAction(nameof(GetById), new {id = pet.Id}, pet.ToPetDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdatePetDTO updatePetDTO){
        var pet = await _petRepository.UpdateAsync(id, updatePetDTO);

        if(pet == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(pet.ToPetDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        
        await _allergyRepository.DeleteAllByPetIdAsync(id);
        await _commentRepository.DeleteAllByPetIdAsync(id);
        await _medicineRepository.DeleteAllByPetIdAsync(id);
        await _vaccineRepository.DeleteAllByPetIdAsync(id);
        
        var pet = await _petRepository.DeleteAsync(id);
        if(pet == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }
}
