// Calculator calcualtor = new(10, 10);
// Console.WriteLine($"Addition: {calcualtor.Add()}");
// Console.WriteLine($"Subtraction: {calcualtor.Subtract()}");
// Console.WriteLine($"Multiplication: {calcualtor.Multiply()}");
// Console.WriteLine($"Division: {calcualtor.Divide()}\n");

// Calculator calcualtor2 = new(10, 0);
// Console.WriteLine($"Addition: {calcualtor2.Add()}");
// Console.WriteLine($"Subtraction: {calcualtor2.Subtract()}");
// Console.WriteLine($"Multiplication: {calcualtor2.Multiply()}");
// Console.WriteLine($"Division: {calcualtor2.Divide()}");

Console.WriteLine("SIMPLE CALCULATOR PROGRAM");
Console.WriteLine("-------------------------\n");

bool check = false;
string firstNumber = "0";
string secondNumber = "0";
string chooseOperator = "";

while (!check)
{
    Console.Write("Masukkan angka pertama: ");
    firstNumber = Console.ReadLine() ?? "0";
    Console.Write("Masukkan angka kedua: ");
    secondNumber = Console.ReadLine() ?? "0";
    Console.Write("Pilih operator (+, -, *, /): ");
    chooseOperator = Console.ReadLine() ?? "";

    if (firstNumber.Length > 0 && secondNumber.Length > 0 && chooseOperator.Length > 0)
    {
        check = true;
    }
}

Calculator calculator = new(int.Parse(firstNumber), int.Parse(secondNumber));

if (chooseOperator == "+")
    Console.WriteLine($"Hasil penjumlahan adalah: {calculator.Add()}");
else if (chooseOperator == "-")
    Console.WriteLine($"Hasil pengurangan adalah: {calculator.Subtract()}");
else if (chooseOperator == "*")
    Console.WriteLine($"Hasil perkalian adalah: {calculator.Multiply()}");
else if (chooseOperator == "/")
{
    if (int.Parse(secondNumber) == 0)
    {
        Console.WriteLine("Tidak dapat dibagi dengan angka 0, Harap coba lagi!");
        return;
    }

    Console.WriteLine($"Hasil pembagian adalah: {calculator.Divide()}");
}
else
{
    Console.WriteLine("Operator tidak sesuai, Harap coba lagi!");
}

public class Calculator(int x, int y)
{
    private readonly int _x = x;
    private readonly int _y = y;

    public int Add() => _x + _y;

    public int Subtract() => _x - _y;

    public int Multiply() => _x * _y;

    public double Divide()
    {
        if (_y == 0)
            throw new DivideByZeroException("Cannot Divided by Zero");

        return (double)_x / _y;
    }
}
