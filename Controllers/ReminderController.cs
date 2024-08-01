using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;
[ApiController]
[Route("api/reminder")]

public class ReminderController : ControllerBase
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IUserRepository _userRepository;
    public ReminderController(IReminderRepository reminderRepository, IUserRepository userRepository){
        _reminderRepository = reminderRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var reminders = await _reminderRepository.GetAllAsync();
        
        if(reminders.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(reminders.Select(r => r.ToReminderDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var reminder = await _reminderRepository.GetByIdAsync(id);

        if(reminder == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(reminder.ToReminderDTO());
    }
    [HttpGet("byuserid/{userId}")]
    public async Task<IActionResult> GetByUserId([FromRoute] int userId){
        var reminders = await _reminderRepository.GetAllByUserIdAsync(userId);
        if(reminders.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(reminders?.Select(r => r.ToReminderDTO()));
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] CreateReminderDTO createReminderDTO){
        if(!await _userRepository.UserExists(userId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var reminder = createReminderDTO.ToReminderFromCreateReminderDTOR(userId);
        await _reminderRepository.CreateAsync(reminder);

        return CreatedAtAction(nameof(GetById), new {id = reminder.Id}, reminder.ToReminderDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateReminderDTO updateReminderDTO){
        var reminder = await _reminderRepository.UpdateAsync(id, updateReminderDTO);

        if(reminder == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(reminder.ToReminderDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var reminder = await _reminderRepository.DeleteAsync(id);
        if(reminder == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }
}
