
using Microsoft.EntityFrameworkCore;

namespace api;

public class AllergyRepository : IAllergyRepository
{
    private readonly ApplicationDBContext _context;

    public AllergyRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Allergy> CreateAsync(Allergy allergy)
    {
        await _context.Allergy.AddAsync(allergy);
        await _context.SaveChangesAsync();
        
        return allergy;
    }

    public async Task<List<Allergy>?> DeleteAllByPetIdAsync(int petId)
    {
        var allergies = await GetAllByPetIdAsync(petId);
        allergies?.ForEach(a => _context.Allergy.Remove(a));
        await _context.SaveChangesAsync();
        return allergies;
    }

    public async Task<Allergy?> DeleteAsync(int id)
    {
        var allergy = await _context.Allergy.FirstOrDefaultAsync(a => a.Id == id);

        if(allergy == null){
            return null;
        }

        _context.Allergy.Remove(allergy);
        await _context.SaveChangesAsync();
        return allergy;
    }

    public async Task<List<Allergy>> GetAllAsync()
    {
       return await _context.Allergy.ToListAsync();
    }

    public async Task<List<Allergy>?> GetAllByPetIdAsync(int petId)
    {
        var allergies = await _context.Allergy.Where(a => a.PetId == petId).ToListAsync();
        
        return allergies;
    }

    public async Task<Allergy?> GetByIdAsync(int id)
    {
        return await _context.Allergy.FindAsync(id);
    }

    public async Task<Allergy?> UpdateAsync(int id, UpdateAllergyDTO updateAllergyDTO)
    {
        var allergy = await _context.Allergy.FirstOrDefaultAsync(a => a.Id == id);
        if(allergy == null){
            return null;
        }
        allergy.Name = updateAllergyDTO.Name;
        allergy.Vaccinated = updateAllergyDTO.Vaccinated;

        await _context.SaveChangesAsync();
        return allergy;
    }
}
