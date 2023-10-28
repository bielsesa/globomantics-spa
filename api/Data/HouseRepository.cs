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
}