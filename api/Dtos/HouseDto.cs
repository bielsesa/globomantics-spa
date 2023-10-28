/// <summary>
/// Data transfer object that represents a house.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="Address">The adress.</param>
/// <param name="Country">The country.</param>
/// <param name="Price">The price.</param>
public record HouseDto(int Id, string? Address, string? Country, int Price);

/// NOTES
/// why a record? 
/// - Immutable
/// - Easy to declare (no need to declare the props like a class)