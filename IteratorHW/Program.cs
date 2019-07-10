using System;
class Program
{
    static void Main(string[] args)
    {
        Salon salon = new Salon();
        Reader reader = new Reader();
        reader.GetCars(salon);

        Console.Read();
    }
}

class Reader
{
    public void GetCars(Salon salon)
    {
        ICarIterator iterator = salon.CreateNumerator();
        while (iterator.HasNext())
        {
            Car book = iterator.Next();
            Console.WriteLine(book.Name);
        }
    }
}

interface ICarIterator
{
    bool HasNext();
    Car Next();
}
interface ICarNumerable
{
    ICarIterator CreateNumerator();
    int Count { get; }
    Car this[int index] { get; }
}
class Car
{
    public string Name { get; set; }
}

class Salon : ICarNumerable
{
    private Car[] cars;
    public Salon()
    {
        cars = new Car[]
        {
            new Car{Name="Audi R8"},
            new Car {Name="Audi A8"},
            new Car {Name="Audi Q8"}
        };
    }
    public int Count
    {
        get { return cars.Length; }
    }

    public Car this[int index]
    {
        get { return cars[index]; }
    }
    public ICarIterator CreateNumerator()
    {
        return new SalonNumerator(this);
    }
}
class SalonNumerator : ICarIterator
{
    ICarNumerable aggregate;
    int index = 0;
    public SalonNumerator(ICarNumerable a)
    {
        aggregate = a;
    }
    public bool HasNext()
    {
        return index < aggregate.Count;
    }

    public Car Next()
    {
        return aggregate[index++];
    }
}