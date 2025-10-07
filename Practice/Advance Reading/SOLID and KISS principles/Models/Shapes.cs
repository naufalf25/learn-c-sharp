namespace SOLID_and_KISS_principles.Models;

public abstract class Shape
{
    public abstract double Area();
    public abstract string GetShapeInfo();
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double Area()
    {
        return Width * Height;
    }

    public override string GetShapeInfo()
    {
        return $"Rectangle: {Width} x {Height}, Area: {Area()}";
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }

    public override string GetShapeInfo()
    {
        return $"Circle: radius {Radius}, Area: {Area():F2}";
    }
}

public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public Triangle(double baseLength, double height)
    {
        Base = baseLength;
        Height = height;
    }

    public override double Area()
    {
        return 0.5 * Base * Height;
    }

    public override string GetShapeInfo()
    {
        return $"Triangle: base {Base}, height {Height}, Area: {Area():F2}";
    }
}