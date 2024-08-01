namespace api;

public static class CommentMapper
{
    public static CommentDTO ToCommentDTO (this Comment comment){
        return new CommentDTO{
            Id = comment.Id,
            Name = comment.Name,
            PetId = comment.PetId
        };
    }
    public static Comment ToCommentFromCreateCommentDTO (this CreateCommentDTO createCommentDTO, int petId){
        return new Comment{
            Name = createCommentDTO.Name,
            PetId = petId
        };
    }
}
