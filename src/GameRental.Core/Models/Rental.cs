namespace GameRental.Core.Models;

public class Rental
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int GameId { get; set; }
    public bool IsReturned { get; set; } = false;

    public Rental()
    {

    }
    public Rental(int id, int customerId, int gameId)
    {
        Id = id;
        CustomerId = customerId;
        GameId = gameId;
    }
}
