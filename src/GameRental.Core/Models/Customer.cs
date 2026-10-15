namespace GameRental.Core.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public Customer()
    {

    }

    public Customer(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}