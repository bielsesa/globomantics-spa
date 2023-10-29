public class BidEntity
{
    public int Id { get; set; }
    public int HouseId { get; set; }
    public HouseEntity? House { get; set; } //// this is a navigation property, 
                                            //// so that EF can automatically detect that 'HouseId' is a foreign key
    public string Bidder { get; set; } = string.Empty;
    public int Amount { get; set; }
}