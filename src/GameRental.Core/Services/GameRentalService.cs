using GameRental.Core.Models;
using GameRental.Core.Results;
namespace GameRental.Core.Services;

public class GameRentalService : IGameRentalService
{
    private readonly List<Game> _games = new();
    private readonly Dictionary<int, Game> _gameById = new();
    private readonly List<Customer> _customer = new();
    private readonly Dictionary<int, Customer> _customerById = new();
    private readonly List<Rental> _rental = new();
    private readonly Dictionary<int, Rental> _rentalById = new();

    private int _nextGameId = 1;
    private int _nextCustomerId = 1;
    private int _nextRentalId = 1;

    public ServiceResult<Game> AddPhysicalGame(string title, string genre, int ageLimit, decimal dailyPrice, int stockCount)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Oyun adı boş olamaz.");
        }
        if (dailyPrice <= 0)
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Günlük fiyat pozitif olmalı.");
        }
        if (stockCount < 0)
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Stok negatif olamaz.");
        }
        
        PhysicalGame game = new PhysicalGame(_nextGameId, title, genre, ageLimit, dailyPrice, stockCount);
        
        _nextGameId++;
        _games.Add(game);
        _gameById[game.Id] = game;

        return ServiceResult<Game>.Success(game);
    }

    public ServiceResult<Game> AddDigitalGame(string title, string genre, int ageLimit, decimal dailyPrice, string platform)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Oyun adı boş olamaz.");
        }
        if (dailyPrice <= 0)
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Günlük fiyat pozitif olmalı.");
        }
        if (string.IsNullOrWhiteSpace(platform))
        {
            return ServiceResult<Game>.Fail(ErrorType.Validation, "Platform adı boş olamaz.");
        }

        DigitalGame game = new DigitalGame(_nextGameId, title, genre, ageLimit, dailyPrice, platform);
        
        _nextGameId++;
        _games.Add(game);
        _gameById[game.Id] = game;

        return ServiceResult<Game>.Success(game);
    }
    public ServiceResult<Customer> AddCustomer(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return ServiceResult<Customer>.Fail(ErrorType.Validation, "İsim boş olamaz.");
        }
        if (age <= 6)
        {
            return ServiceResult<Customer>.Fail(ErrorType.Validation, "Kullanıcının yaşı 7 veya daha büyük olmalı.");
        }

        Customer customer = new Customer(_nextCustomerId, name, age);
        _nextCustomerId++;
        _customer.Add(customer);
        _customerById[customer.Id] = customer;

        return ServiceResult<Customer>.Success(customer);
    }

    public ServiceResult<Rental> RentGame(int customerId, int gameId)
    {
        if (_customerById.TryGetValue(customerId, out Customer? customer) == false)
        {
            return ServiceResult<Rental>.Fail(ErrorType.NotFound, "Böyle bir müşteri yok.");
        }
        if (_gameById.TryGetValue(gameId, out Game? game) == false)
        {
            return ServiceResult<Rental>.Fail(ErrorType.NotFound, "Böyle bir oyun yok.");
        }
        if (customer.Age < game.AgeLimit)
        {
            return ServiceResult<Rental>.Fail(ErrorType.Validation, $"Bu oyun +{game.AgeLimit} yaş içindir, müşteri {customer.Age} yaşında.");
        }
        if (game.CanBeRented() == false)
        {
            return ServiceResult<Rental>.Fail(ErrorType.Conflict, "Oyun şu an kiralanamaz (stok yok).");
        }
        if (game is PhysicalGame physical)
        {
            physical.StockCount--;
        }
        Rental rental = new Rental(_nextRentalId, customerId, gameId);
        _nextRentalId++;
        _rental.Add(rental);
        _rentalById[rental.Id] = rental;

        return ServiceResult<Rental>.Success(rental);
    }
    public ServiceResult ReturnGame(int rentalId)
    {
        if (_rentalById.TryGetValue(rentalId, out Rental? rental) == false)
        {
            return ServiceResult.Fail(ErrorType.NotFound, "Böyle bir kiralama kaydı yok.");
        }
        if (rental.IsReturned)
        {
            return ServiceResult.Fail(ErrorType.Conflict, "Bu oyun zaten iade edilmiş.");
        }
        rental.IsReturned = true;
        if (_gameById.TryGetValue(rental.GameId, out Game? game) && game is PhysicalGame physical)
        {
            physical.StockCount++;
        }
        return ServiceResult.Success();
    }

    public IReadOnlyList<Game> GetGames()
    {
        return _games;
    }
    public IReadOnlyList<Customer> GetCustomers()
    {
        return _customer;
    }
    public IReadOnlyList<Rental> GetRentals()
    {
        return _rental;
    }
    public void SeedSampleGames()
    {
        AddPhysicalGame("FIFA 27", "Spor", 7, 30m, 3);
        AddPhysicalGame("The Witcher 3 Remastered", "Aksiyon", 18, 45m, 2);
        AddDigitalGame("Minecraft", "Macera", 7, 20m, "Steam");
        AddDigitalGame("Call of Duty", "Aksiyon", 18, 50m, "PlayStation Store");
        AddDigitalGame("Baldur's Gate 3", "RPG", 18, 70m, "Steam");
        AddDigitalGame("TEKKEN", "Dövüş", 13, 35m, "Steam");
    }
}
