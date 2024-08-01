using Microsoft.EntityFrameworkCore;

namespace api;

public class MedicineRepository : IMedicineRepository
{
    private readonly ApplicationDBContext _context;

    public MedicineRepository(ApplicationDBContext context){
        _context = context;
    }

    public async Task<Medicine> CreateAsync(Medicine medicine)
    {
        await _context.Medicine.AddAsync(medicine);
        await _context.SaveChangesAsync();
        
        return medicine;
    }

    public async Task<List<Medicine>?> DeleteAllByPetIdAsync(int petId)
    {
        var medicines = await GetAllByPetIdAsync(petId);
        medicines?.ForEach(c => _context.Medicine.Remove(c));
        await _context.SaveChangesAsync();
        return medicines;
    }

    public async Task<Medicine?> DeleteAsync(int id)
    {
        var medicine = await _context.Medicine.FirstOrDefaultAsync(m => m.Id == id);

        if(medicine == null){
            return null;
        }

        _context.Medicine.Remove(medicine);
        await _context.SaveChangesAsync();
        return medicine;
    }

    public async Task<List<Medicine>> GetAllAsync()
    {
       return await _context.Medicine.ToListAsync();
    }

    public async Task<List<Medicine>?> GetAllByPetIdAsync(int petId)
    {
        var medicines = await _context.Medicine.Where(m => m.PetId == petId).ToListAsync();
        
        return medicines;
    }

    public async Task<Medicine?> GetByIdAsync(int id)
    {
        return await _context.Medicine.FindAsync(id);
    }

    public async Task<Medicine?> UpdateAsync(int id, UpdateMedicineDTO updateMedicineDTO)
    {
        var medicine = await _context.Medicine.FirstOrDefaultAsync(m => m.Id == id);
        if(medicine == null){
            return null;
        }
        medicine.Name = updateMedicineDTO.Name;
        medicine.Dousage = updateMedicineDTO.Dousage;
        medicine.Type = updateMedicineDTO.Type;
        medicine.MedicineSchedule = updateMedicineDTO.MedicineSchedule;

        await _context.SaveChangesAsync();
        return medicine;
    }
}
