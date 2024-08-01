using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/hardware")]
public class HardwareController : ControllerBase
{
    private readonly IHardwareRepository _hardwareRepository;
    private readonly IUserRepository _userRepository;
    public HardwareController(IHardwareRepository hardwareRepository, IUserRepository userRepository){
        _hardwareRepository = hardwareRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var hardwares = await _hardwareRepository.GetAllAsync();
        
        if(hardwares.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(hardwares.Select(h => h.ToHardwareDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var hardware = await _hardwareRepository.GetByIdAsync(id);

        if(hardware == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(hardware.ToHardwareDTO());
    }
    [HttpGet("byuserid/{userId}")]
    public async Task<IActionResult> GetByUserId([FromRoute] int userId){
        var hardwares = await _hardwareRepository.GetAllByUserIdAsync(userId);
        if(hardwares.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(hardwares?.Select(p => p.ToHardwareDTO()));
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] CreateHardwareDTO createHardwareDTO){
        if(!await _userRepository.UserExists(userId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var hardware = createHardwareDTO.ToHardwareFromCreateHardwareDTO(userId);
        await _hardwareRepository.CreateAsync(hardware);

        return CreatedAtAction(nameof(GetById), new {id = hardware.Id}, hardware.ToHardwareDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateHardwareDTO updateHardwareDTO){
        var hardware = await _hardwareRepository.UpdateAsync(id, updateHardwareDTO);

        if(hardware == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(hardware.ToHardwareDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var hardware = await _hardwareRepository.DeleteAsync(id);
        if(hardware == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }
}
