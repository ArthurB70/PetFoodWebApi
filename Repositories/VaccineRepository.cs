using Microsoft.EntityFrameworkCore;

namespace api;

public class VaccineRepository : IVaccineRepository
{
    private readonly ApplicationDBContext _context;

    public VaccineRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Vaccine> CreateAsync(Vaccine vaccine)
    {
        await _context.Vaccine.AddAsync(vaccine);
        await _context.SaveChangesAsync();
        
        return vaccine;
    }

    public async Task<List<Vaccine>?> DeleteAllByPetIdAsync(int petId)
    {
        var vaccines = await GetAllByPetIdAsync(petId);
        vaccines?.ForEach(c => _context.Vaccine.Remove(c));
        await _context.SaveChangesAsync();
        return vaccines;
    }

    public async Task<Vaccine?> DeleteAsync(int id)
    {
        var vaccine = await _context.Vaccine.FirstOrDefaultAsync(a => a.Id == id);

        if(vaccine == null){
            return null;
        }

        _context.Vaccine.Remove(vaccine);
        await _context.SaveChangesAsync();
        return vaccine;
    }

    public async Task<List<Vaccine>> GetAllAsync()
    {
       return await _context.Vaccine.ToListAsync();
    }

    public async Task<List<Vaccine>?> GetAllByPetIdAsync(int petId)
    {
        var vaccines = await _context.Vaccine.Where(a => a.PetId == petId).ToListAsync();
        
        return vaccines;
    }

    public async Task<Vaccine?> GetByIdAsync(int id)
    {
        return await _context.Vaccine.FindAsync(id);
    }

    public async Task<Vaccine?> UpdateAsync(int id, UpdateVaccineDTO updateVaccineDTO)
    {
        var vaccine = await _context.Vaccine.FirstOrDefaultAsync(a => a.Id == id);
        if(vaccine == null){
            return null;
        }


        vaccine.Name = updateVaccineDTO.Name;
        vaccine.ApplicationDate = updateVaccineDTO.ApplicationDate;
        vaccine.ExpirationDate = updateVaccineDTO.ExpirationDate;
        
        await _context.SaveChangesAsync();
        return vaccine;
    }    
}
