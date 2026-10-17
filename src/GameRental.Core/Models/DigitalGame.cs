namespace GameRental.Core.Models;

public class DigitalGame : Game
{
    public string Platform { get; set; } = string.Empty;

    public DigitalGame()
    {

    }

    public DigitalGame(int id, string title, string genre, int ageLimit, decimal dailyPrice, string platform) : base(id, title, genre, ageLimit, dailyPrice)
    {
        Platform = platform;
    }
}