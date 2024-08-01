using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api;

[ApiController]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPetRepository _petRepository;
    public CommentController(ICommentRepository commentRepository, IPetRepository petRepository){
        _commentRepository = commentRepository;
        _petRepository = petRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var comments = await _commentRepository.GetAllAsync();
        
        if(comments.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado."
                }
            );
        }
        return Ok(comments.Select(c => c.ToCommentDTO()));
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var comment = await _commentRepository.GetByIdAsync(id);

        if(comment == null){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }
        return Ok(comment.ToCommentDTO());
    }
    [HttpGet("bypetid/{petId}")]
    public async Task<IActionResult> GetByPetId([FromRoute] int petId){
        var comments = await _commentRepository.GetAllByPetIdAsync(petId);
        if(comments.IsNullOrEmpty()){
            return NotFound(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        return Ok(comments?.Select(c => c.ToCommentDTO()));
    }

    [HttpPost("{petId}")]
    public async Task<IActionResult> Create([FromRoute] int petId, [FromBody] CreateCommentDTO commentDTO){
        if(!await _petRepository.PetExists(petId)){
            return BadRequest(
                new ErrorDTO{
                    ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
                }
            );
        }

        var comment = commentDTO.ToCommentFromCreateCommentDTO(petId);
        await _commentRepository.CreateAsync(comment);

        return CreatedAtAction(nameof(GetById), new {id = comment.Id}, comment.ToCommentDTO());
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentDTO updateCommentDTO){
        var comment = await _commentRepository.UpdateAsync(id, updateCommentDTO);

        if(comment == null){
            return  NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }

        return Ok(comment.ToCommentDTO());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var comment = await _commentRepository.DeleteAsync(id);
        if(comment == null){
            return NotFound(new ErrorDTO{
                ErrorMessage = "Nenhum registro foi encontrado com o Id informado."
            });
        }
        return Ok();
    }    
}
