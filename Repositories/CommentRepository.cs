
using Microsoft.EntityFrameworkCore;

namespace api;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comment.AddAsync(comment);
        await _context.SaveChangesAsync();
        
        return comment;
    }

    public async Task<List<Comment>?> DeleteAllByPetIdAsync(int petId)
    {
        var comments = await GetAllByPetIdAsync(petId);
        comments?.ForEach(c => _context.Comment.Remove(c));
        await _context.SaveChangesAsync();
        return comments;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);

        if(comment == null){
            return null;
        }

        _context.Comment.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
       return await _context.Comment.ToListAsync();
    }

    public async Task<List<Comment>?> GetAllByPetIdAsync(int petId)
    {
        var comment = await _context.Comment.Where(c => c.PetId == petId).ToListAsync();
        
        return comment;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comment.FindAsync(id);
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentDTO updateAllergyDTO)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(a => a.Id == id);
        if(comment == null){
            return null;
        }
        comment.Name = updateAllergyDTO.Name;
        
        await _context.SaveChangesAsync();
        return comment;
    }
}
