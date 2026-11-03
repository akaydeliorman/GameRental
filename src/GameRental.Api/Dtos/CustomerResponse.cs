using System;
using GameRental.Core.Models;

namespace GameRental.Api.Dtos;

public record CustomerResponse(
    int Id,
    string Name,
    int Age)
{
    public static CustomerResponse From(Customer customer)
    {
        return new CustomerResponse(customer.Id, customer.Name, customer.Age);
    }
}