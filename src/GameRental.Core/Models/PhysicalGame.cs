namespace GameRental.Core.Models;

public class PhysicalGame : Game
{
    public int StockCount { get; set; }
    public PhysicalGame()
    {

    }
    public PhysicalGame(int id, string title, string genre, int ageLimit, decimal dailyPrice, int stockCount) : base(id, title, genre, ageLimit, dailyPrice)
    {
        StockCount = stockCount;
    }
    public override bool CanBeRented()
    {
        return StockCount > 0;
    }
}
