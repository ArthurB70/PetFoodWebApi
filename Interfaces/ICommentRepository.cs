namespace api;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<List<Comment>?> GetAllByPetIdAsync(int petId);
    Task<List<Comment>?> DeleteAllByPetIdAsync(int petId);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, UpdateCommentDTO updateCommentDTO);
    Task<Comment?> DeleteAsync(int id);
}
