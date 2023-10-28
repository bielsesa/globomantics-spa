public interface IHouseRepository
{
    Task<List<HouseDto>> GetAll();
}