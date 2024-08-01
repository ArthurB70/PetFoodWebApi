using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IPetRepository _petRepository;
    private readonly IAllergyRepository _allergyRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IMedicineRepository _medicineRepository;
    private readonly IVaccineRepository _vaccineRepository;
    private readonly IUserRepository _userRepository;
    private readonly IReminderRepository _reminderRepository;
    private readonly IHardwareRepository _hardwareRepository;
    public UserController(IPetRepository petRepository, 
                        IUserRepository userRepository,
                        IAllergyRepository allergyRepository,
                        ICommentRepository commentRepository,
                        IMedicineRepository medicineRepository,
                        IVaccineRepository vaccineRepository,
                        IReminderRepository reminderRepository,
                        IHardwareRepository hardwareRepository){

        _petRepository = petRepository;
        _allergyRepository = allergyRepository;
        _commentRepository = commentRepository;
        _medicineRepository = medicineRepository;
        _vaccineRepository = vaccineRepository;
        _userRepository = userRepository;
        _reminderRepository = reminderRepository;
        _hardwareRepository = hardwareRepository;

    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(){
        var users = await _userRepository.GetAllAsync();

        if(users.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }

        return Ok(users.Select(u => u.ToUserDTO()));
    }
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id){

        var user = await _userRepository.GetByIdAsync(id);
        if(user == null){
            return NotFound(new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                });
        }
        return Ok(user.ToUserDTO());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO){
        var user = createUserDTO.ToUserFromCreateUserDTO();
        var userEmail = await _userRepository.GetByEmailAsync(user.Email);

        if(userEmail != null){
            return BadRequest(new ErrorDTO{
                ErrorMessage = "Outro usuário já foi cadastrado com o e-mail informado." + userEmail
            });
        }
        await _userRepository.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new {id = user.Id}, user.ToUserDTO());
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDTO updateUserDTO){
        var user = await _userRepository.UpdateAsync(id, updateUserDTO);
        if(user == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok(user.ToUserDTO());
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var pets = await _petRepository.GetAllByUserIdAsync(id);
        if(pets?.Count > 0){
            foreach (var pet in pets)
            {
                await _allergyRepository.DeleteAllByPetIdAsync(pet.Id);
                await _commentRepository.DeleteAllByPetIdAsync(pet.Id);
                await _medicineRepository.DeleteAllByPetIdAsync(pet.Id);
                await _vaccineRepository.DeleteAllByPetIdAsync(pet.Id);
                await _petRepository.DeleteAsync(pet.Id);
            }
            
        }
        
        await _reminderRepository.DeleteAllByUserIdAsync(id);
        await _hardwareRepository.DeleteAllByUserIdAsync(id);
        var user = await _userRepository.DeleteAsync(id);
        if(user == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        
        return Ok();
    }
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO){
        var user  = await _userRepository.GetAuthenticationAsync(loginDTO.Email);
        
        if(user == null || user.Password != loginDTO.Password){
            return BadRequest(
                new ErrorDTO{ErrorMessage = "E-mail ou senha inválidos. Tente novamente."}
            );
        }
        return Ok(new LoginReturnDTO{Message = "Login bem sucedido."});
    }

}
