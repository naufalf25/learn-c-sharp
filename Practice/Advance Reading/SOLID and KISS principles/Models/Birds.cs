using SOLID_and_KISS_principles.Interfaces;

namespace SOLID_and_KISS_principles.Models;

public abstract class Bird
{
    public string Name { get; set; } = string.Empty;

    public Bird(string name)
    {
        Name = name;
    }

    public virtual void Eat()
    {
        Console.WriteLine($"{Name} is eating");
    }

    public abstract void MakeSound();
}

public class Sparrow : Bird, IFlyable
{
    public Sparrow() : base("Sparrow") { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} chirps: Tweet tweet!");
    }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying gracefully");
    }
}

public class Penguin : Bird, ISwimmable
{
    public Penguin() : base("Penguin") { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} make noice: Honk honk!");
    }

    public void Swim()
    {
        Console.WriteLine($"{Name} is swimming like a torpedo");
    }
}

public class Duck : Bird, IFlyable, ISwimmable
{
    public Duck() : base("Duck") { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} quacks: Quack quack!");
    }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying over the pond");
    }

    public void Swim()
    {
        Console.WriteLine($"{Name} is swimming on the water surface");
    }
}
