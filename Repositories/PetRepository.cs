
using Microsoft.EntityFrameworkCore;

namespace api;

public class PetRepository : IPetRepository
{
    public ApplicationDBContext _context;
    public PetRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Pet> CreateAsync(Pet pet)
    {
        await _context.Pet.AddAsync(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task<Pet?> DeleteAsync(int id)
    {
        var pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id);
        if(pet == null){
            return null;
        }
        _context.Pet.Remove(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task<List<Pet>> GetAllAsync()
    {
        return await _context.Pet.ToListAsync();
    }

    public async Task<List<Pet>?> GetAllByUserIdAsync(int userId)
    {
        var pets =  await _context.Pet.Where(p => p.UserId == userId).ToListAsync();
        
        return pets;
    }

    public async Task<Pet?> GetByIdAsync(int id)
    {
        return await _context.Pet.FindAsync(id);
    }

    public async Task<bool> PetExists(int id)
    {
        return await _context.Pet.AnyAsync(a => a.Id == id);
    }

    public async Task<Pet?> UpdateAsync(int id, UpdatePetDTO updatePetDTO)
    {
        var pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id);
        if(pet == null){
            return null;
        }

        pet.Specie = updatePetDTO.Specie;
        pet.Name = updatePetDTO.Name;
        pet.Breed = updatePetDTO.Breed;
        pet.BirthDate = updatePetDTO.BirthDate;
        pet.Gender = updatePetDTO.Gender;
        pet.Size = updatePetDTO.Size;

        await _context.SaveChangesAsync();
        return pet;
    }
}
