using System;
using GameRental.Core.Models;
using GameRental.Core.Results;

namespace GameRental.Core.Services;

public interface IGameRentalService
{
    ServiceResult<Game> AddPhysicalGame(string title, string genre, int ageLimit, decimal dailyPrice, int stockCount);
    ServiceResult<Game> AddDigitalGame(string title, string genre, int ageLimit, decimal dailyPrice, string platform);
    ServiceResult<Customer> AddCustomer(string name, int age);
    ServiceResult<Rental> RentGame(int customerId, int gameId);
    ServiceResult ReturnGame(int rentalId);
    IReadOnlyList<Game> GetGames();
    IReadOnlyList<Customer> GetCustomers();
    IReadOnlyList<Rental> GetRentals();
    void SeedSampleGames();

}
