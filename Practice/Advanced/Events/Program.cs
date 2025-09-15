using System;

// Example of Event Handler
Console.WriteLine("\n---Stock Class Firing an Event---");
Stock s = new("TEST");
s.Price = 10000;
Console.WriteLine("The detail of Current Stock is:");
// Console.WriteLine(s.symbol); // Compile-time error: inaccessible due to its protection level
Console.Write("Price: ");
Console.WriteLine(s.Price);

Console.WriteLine("\n==================================================\n");

// Example of Standard Pattern
Console.WriteLine("---Example of Standard Pattern Events---");
Stock2 stock = new("THPW");
stock.Price = 27.10M;

// Register with the PriceChanged event (subscribe)
stock.PriceChanged += stock_PriceChanged;

stock.Price = 31.59M; // Trigger the event

void stock_PriceChanged(object? sender, PriceChangeEventArgs e) // Subscriber method
{
    if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
        Console.WriteLine("Alert, 10% stock price increase!");
}

Console.Write("The price of new current stock is: ");
Console.WriteLine(stock.Price);


// Example of Event Handler
public delegate void PriceChangeHandler(decimal oldPrice, decimal newPrice);

public class Stock
{
    string symbol;
    decimal price;

    public Stock(string symbol) => this.symbol = symbol;

    public event PriceChangeHandler PriceChanged; // Event declaration

    public decimal Price
    {
        get => price;
        set
        {
            if (price == value) return; // Exit if price hasn't changed

            decimal oldPrice = price;
            price = value;

            // Only invoke if there are subscribers
            if (PriceChanged != null)
                PriceChanged(oldPrice, price);
        }
    }
}

// Example of Standard Pattern
public class PriceChangeEventArgs : EventArgs
{
    public readonly decimal LastPrice;
    public readonly decimal NewPrice;
    public PriceChangeEventArgs(decimal lastPrice, decimal newPrice)
    {
        LastPrice = lastPrice;
        NewPrice = newPrice;
    }
}

public class Stock2
{
    string symbol;
    decimal price;

    public Stock2(string symbol) => this.symbol = symbol;

    public event EventHandler<PriceChangeEventArgs> PriceChanged;

    protected virtual void OnPriceChanged(PriceChangeEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }

    public decimal Price
    {
        get => price;
        set
        {
            if (price == value) return; // Exit if nothing has changed
            decimal oldPrice = price;
            price = value;
            OnPriceChanged(new PriceChangeEventArgs(oldPrice, price));
        }
    }
}
