namespace GameRental.Core.Models;

public abstract class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int AgeLimit { get; set; }
    public decimal DailyPrice { get; set; }

    public virtual bool CanBeRented()
    {
        return true;
    }
    public Game()
    {

    }
    public Game(int id, string title, string genre, int ageLimit, decimal dailyPrice)
    {
        Id = id;
        Title = title;
        Genre = genre;
        AgeLimit = ageLimit;
        DailyPrice = dailyPrice;

    }
}