using Microsoft.EntityFrameworkCore;

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext context;

    public HouseRepository(HouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<HouseDto>> GetAll()
    {
        //// this is to be able to see the cute plant loading gif :)
        Thread.Sleep(1500);

        return await this.context.Houses
        .Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price))
        .ToListAsync();
    }

    public async Task<HouseDetailDto?> Get(int id) 
    {
        var e = await context.Houses.SingleOrDefaultAsync(h => h.Id == id);

        if (e == null)
        {
            return null;
        }

        return EntityToDto(e);
    }

    public async Task<HouseDetailDto> Add(HouseDetailDto dto) 
    {
        var entity = new HouseEntity();
        DtoToEntity(dto, entity);
        context.Houses.Add(entity);
        await context.SaveChangesAsync();
         //// commits the work.
        return EntityToDto(entity);
    }

    public async Task<HouseDetailDto> Update(HouseDetailDto dto) 
    {
        var entity = await context.Houses.FindAsync(dto.Id) 
            ?? throw new ArgumentException($"Error updating house {dto.Id}");

        DtoToEntity(dto, entity);

        //// since we turned off tracking in EF, in this case we need to inform
        //// it that the entity has been modified, this way.
        context.Entry(entity).State = EntityState.Modified;
        //// why not this in the Add case?
        //// bc the DbContext automatically starts tracking when a command like
        //// Add is executed, regardles if tracking is enabled or not.
        //// No command like Update exists, that is why we need to tell EF
        //// manually the State change.

        await context.SaveChangesAsync();
        return EntityToDto(entity);
    }

    public async Task Delete(int id) 
    {
        var entity = await context.Houses.FindAsync(id)
            ?? throw new ArgumentException($"Error updating house {id}");

        context.Houses.Remove(entity);
        await context.SaveChangesAsync();
    }

    private static void DtoToEntity(HouseDetailDto dto,
        HouseEntity e) 
    {
        e.Address = dto.Address;
        e.Country = dto.Country;
        e.Description = dto.Description;
        e.Price = dto.Price;
        e.Photo = dto.Photo;
    }

    private static HouseDetailDto EntityToDto(HouseEntity e)
    {
        return new HouseDetailDto(e.Id, e.Address, e.Country,
            e.Price, e.Description, e.Photo);
    }
}