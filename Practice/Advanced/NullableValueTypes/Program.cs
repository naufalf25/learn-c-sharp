Console.WriteLine("===== Nullable Value Types =====\n");

// Basic Nullable Value Types
Console.WriteLine("--- Basic Nullable Value Types ---");

int regularInt = default; // 0
bool regularBool = default; // false
DateTime regularDate = default; // 1/1/0001 12:00:00 AM

Console.WriteLine($"Regular int default: {regularInt}");
Console.WriteLine($"Regular bool default: {regularBool}");
Console.WriteLine($"Regular DateTime default: {regularDate}");

// Nullable value types CAN represent the absence of a value
int? nullableInt = null;
bool? nullableBool = null;
DateTime? nullableDate = null;

Console.WriteLine($"Nullable int: {nullableInt?.ToString() ?? "null"}");
Console.WriteLine($"Nullable bool: {nullableBool?.ToString() ?? "null"}");
Console.WriteLine($"Nullable DateTime: {nullableDate?.ToString() ?? "null"}");

// Verifying they're actually null
Console.WriteLine("\nVerifying null status: ");
Console.WriteLine($"nullableInt == null: {nullableInt == null}");
Console.WriteLine($"nullableBool == null: {nullableBool == null}");
Console.WriteLine($"nullableDate == null: {nullableDate == null}");

// Let's assign some actual values
nullableInt = 42;
nullableBool = true;
nullableDate = new DateTime(2025, 9, 15);

Console.WriteLine("\nAfter assigning real values:");
Console.WriteLine($"Nullable int: {nullableInt}");
Console.WriteLine($"Nullable bool: {nullableBool}");
Console.WriteLine($"Nullable DateTime: {nullableDate}");

Console.WriteLine("\nNull status after assignments: ");
Console.WriteLine($"nullableInt == null: {nullableInt == null}");
Console.WriteLine($"nullableBool == null: {nullableBool == null}");
Console.WriteLine($"nullableDate == null: {nullableDate == null}");

Console.WriteLine("\n-------------------------------------------\n");

// Nullable<T> Struct Internals
Console.WriteLine("--- Nullable<T> Struct ---");

Nullable<int> explicitNullable = new Nullable<int>(100);
int? shothandNullable = 100;

Console.WriteLine($"Explicit Nullable<int>: {explicitNullable}");
Console.WriteLine($"Shorthand init: {shothandNullable}");
Console.WriteLine($"Are they the same type? {explicitNullable.GetType() == shothandNullable.GetType()}");

// The struct has two key properties: HasValue and Value
int? testValue = 50;

Console.WriteLine("\nExamining the internal structure:");
Console.WriteLine($"testValue = {testValue}");
Console.WriteLine($"testValue.HasValue = {testValue.HasValue}");

if (testValue.HasValue)
{
    Console.WriteLine($"testValue.Value = {testValue.Value}");
}

// Let's see what happens when we set it to null
testValue = null;
Console.WriteLine("\nAfter setting to null:");
Console.WriteLine($"testValue = {testValue}");
Console.WriteLine($"testValue.HasValue = {testValue.HasValue}");

// Dangerous part - accessing Value when HasValue is false
Console.WriteLine("\nDemonstrating the danger of accessing Value when null");
try
{
#pragma warning disable CS8629 // Nullable value type may be null
    int dangerousAccess = testValue.Value; // this will throw InvalidOperationException!
#pragma warning restore CS8629
    Console.WriteLine($"This won't print: {dangerousAccess}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Exception caught: {ex.Message}");
}

// Safe alternatives for getting values
Console.WriteLine("\nSafe ways to extract values:");
Console.WriteLine($"GetValueOrDefault(): {testValue.GetValueOrDefault()}");
Console.WriteLine($"GetValueOrDefault(999): {testValue.GetValueOrDefault(999)}");

// Default value behavior
int? defaultNullable = default;
Console.WriteLine($"\nDefault value of int?: {defaultNullable}");
Console.WriteLine($"Default HasValue: {defaultNullable.HasValue}");

Console.WriteLine("\n-------------------------------------------\n");


// Implicit and Explicit Conversions
Console.WriteLine("--- Understanding Nullable Conversions ---");

// T to T?
Console.WriteLine("\nRULE 1: Regular value type ➡ Nullable type (IMPLICIT - always safe)");
int regularInt2 = 25;
int? nullableFromRegular = regularInt2; // No cast needed!

Console.WriteLine($"Regular int: {regularInt}");
Console.WriteLine($"Implicity converted to nullable: {nullableFromRegular}");

// T? to T
Console.WriteLine("\nRULE 2: Nullable type ➡ Regular value type (EXPLICIT - potentially dangerous)");
int? nullableInt2 = 75;
int backToRegular = (int)nullableInt2; // Explicit cast required

Console.WriteLine($"Nullable int: {nullableInt2}");
Console.WriteLine($"Explicity converted back to regular: {backToRegular}");

// Dangerous scenarios - null to regular type
Console.WriteLine("The DANGEROUS scenario - converting null to regular type:");
int? nullValue = null;
try
{
    int willThrow = (int)nullValue;

    Console.WriteLine($"This won't print: {willThrow}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"💥 Boom! {ex.Message}");
}

// Safe Alternatives
Console.WriteLine("\nSAFE alternatives for nullable ➡ regular conversions:");

// Option 1: Check HasValue first
Console.WriteLine("Option 1: Check HasValue first");
if (nullValue.HasValue)
{
    int safeConversion = (int)nullValue;
    Console.WriteLine($"Safe conversion: {safeConversion}");
}
else
{
    Console.WriteLine("Cannot convert -  value is null, avoiding the explosion!");
}

// Option 2: Use GetValueOrDefault
Console.WriteLine("\nOption 2: Use GetValueOrDefault");
int defaultValue = nullValue.GetValueOrDefault(-1);
Console.WriteLine($"Using GetValueOrDefault(-1): {defaultValue}");

// Option 3: Use null-coalescing operator (we'll cover this more later)
Console.WriteLine("\nOption 3: Use null-coalescing operator (??)");
int coalescedValue = nullValue ?? -999;
Console.WriteLine($"Using ?? operator: {coalescedValue}");

Console.WriteLine("\n-------------------------------------------\n");


// Boxing and Unboxing
Console.WriteLine("--- Boxing and Unboxing ---");

// Boxing nullable types
int? nullableValue = 123;
object boxedValue = nullableValue; // Boxing

Console.WriteLine($"Original nullable: {nullableValue}");
Console.WriteLine($"Boxed object: {boxedValue}");
Console.WriteLine($"Boxed type: {boxedValue.GetType().Name}");

// Boxing a null nullable type results in null reference
int? nullNullable = null;
object? boxedNull = nullNullable;

Console.WriteLine($"Null nullable: {nullNullable}");
Console.WriteLine($"Boxed null: {boxedNull}");
Console.WriteLine($"Boxed null == null: {boxedNull == null}");

// Unboxing back
int? unboxedValue = (int?)boxedValue;
Console.WriteLine($"Unboxed back to nullable: {unboxedValue}");

// Safe unboxing using 'as' operator
object stringObject = "Not a number";
int? safeUnbox = stringObject as int?;

Console.WriteLine($"Safe unboxing from string: {safeUnbox}");
Console.WriteLine($"Safe unboxing HasValue: {safeUnbox.HasValue}");

// Direct unboxing to regular value type
object anotherBoxed = 456;
int directUnbox = (int)anotherBoxed;
Console.WriteLine($"Direct unboxing: {directUnbox}");

Console.WriteLine("\n-------------------------------------------\n");


// Operator Lifting
Console.WriteLine("--- Operator Lifting ---");

int? a = 10;
int? b = 20;
int? c = null;

Console.WriteLine($"Our test values: a = {a}, b = {b}, c = {c}");

Console.WriteLine("\nArithmetic operations with valid values work as expected:");
Console.WriteLine($"a + b = {a + b}");
Console.WriteLine($"a * 2 = {a * 2}");

Console.WriteLine("\nBut here's where operations with null:");
Console.WriteLine($"a + c = {a + c}");
Console.WriteLine($"c / 5 = {c / 5}");

Console.WriteLine("\nThe rule: If ANY operand is null, arithmetic result is null");

Console.WriteLine("\n Comparison operations have their own rules:");
Console.WriteLine($"a < b = {a < b}");
Console.WriteLine($"a > b = {a > b}");

Console.WriteLine("\nBut comparisons with null are always false:");
Console.WriteLine($"a < c = {a < c}");
Console.WriteLine($"c > b = {c < b}");

Console.WriteLine("\nKey insight: null is icomparable - neither greater, less, nor equal to anything");
Console.WriteLine("(except for equality, where null == null is true)");

Console.WriteLine("\n-------------------------------------------\n");


// Equality Operator
Console.WriteLine("--- Equality Operator ---");

int? x = 5;
int? y = 5;
int? z = 10;
int? nullValue1 = null;
int? nullValue2 = null;

Console.WriteLine($"x = {x}, y = {y}, z = {z}");
Console.WriteLine($"nullValue1 = {nullValue1}, nullValue2 = {nullValue2}");

Console.WriteLine($"x == y: {x == y}");
Console.WriteLine($"x == null: {x == null}");
Console.WriteLine($"nullValue1 == null: {nullValue1 == null}");
Console.WriteLine($"nullValue1 == nullValue2: {nullValue1 == nullValue2}");
Console.WriteLine($"x == nullValue1: {x == nullValue1}");

int regularInt3 = 5;
Console.WriteLine($"x == regularInt3: {x == regularInt3}");

Console.WriteLine("\n-------------------------------------------\n");


// Boolean Logical Operators
Console.WriteLine("--- Boolean Logical Operators ---");

bool? n = null;
bool? f = false;
bool? t = true;

Console.WriteLine($"n = {n}, f = {f}, t = {t}");
Console.WriteLine("\nTesting logical OR (|) operator:");

// OR operations - null trated as "unknown"
Console.WriteLine($"n | n = {n | n}"); // null
Console.WriteLine($"n | f = {n | f}"); // null
Console.WriteLine($"n | t = {n | t}"); // true
Console.WriteLine($"f | n = {f | n}"); // null
Console.WriteLine($"t | n = {t | n}"); // true

// AND operations
Console.WriteLine($"n & n = {n & n}"); // null
Console.WriteLine($"n & f = {n & f}"); // false
Console.WriteLine($"n & t = {n & t}"); // null
Console.WriteLine($"f & n = {f & n}"); // false
Console.WriteLine($"t & n = {t & n}"); // null

Console.WriteLine("\nKey insights:");
Console.WriteLine(" - true OR anything = true (even null)");
Console.WriteLine(" - false AND anything = false (even null)");
Console.WriteLine(" - Other combinations with null remain null");

Console.WriteLine("\n-------------------------------------------\n");


// Null-Coalescing Operator
Console.WriteLine("--- Null-Coalescing Operator ---");

int? nullableValue1 = null;
int? anotherValue = 42;

// Basic null-coalescing
int result1 = nullableValue1 ?? 100;
int result2 = anotherValue ?? 100;

Console.WriteLine($"nullableValue1 = {nullableValue1}");
Console.WriteLine($"anotherValue = {anotherValue}");
Console.WriteLine($"nullableValue1 ?? 100 = {result1}");
Console.WriteLine($"anotherValue ?? 100 == {result2}");

// Chaining null-coalescing operators
int? first = null;
int? second = null;
int? third = 999;
int? fourth = 888;

int chainedResult = first ?? second ?? third ?? fourth ?? 0;
Console.WriteLine("\nChaining example:");
Console.WriteLine($"first = {first}, second = {second}, third = {third}, fourth = {fourth}");
Console.WriteLine($"first ?? second ?? third ?? fourth ?? 0 = {chainedResult}");

// Null-coalescing with different types
string? nullString = null;
string defaultString = nullString ?? "Default Value";
Console.WriteLine("\nWith strings:");
Console.WriteLine($"nullString ?? \"Default Value\" = \"{defaultString}\"");

// Practical examples
Console.WriteLine("\nPractical examples:");

// Configuration values
int? configTimeout = GetConfigTimeout(); // Migh return null
int actualTimeout = configTimeout ?? 30;
Console.WriteLine($"Configuration timeout: {configTimeout}");
Console.WriteLine($"Actual timetout used: {actualTimeout}");

// User input validation
int? userAge = GetUserAge();
string ageDisplay = $"Age: {userAge?.ToString() ?? "Not specified"}";
Console.WriteLine($"User age display: {ageDisplay}");

// Null-coalescing assignment (C# 8++)
int? score = null;
score ??= 0; // Assign 0 if score is null
Console.WriteLine($"Score after ??= operator: {score}");

Console.WriteLine("\n-------------------------------------------\n");

// Helper methods for practical exams
static int? GetConfigTimeout()
{
    // Simulate reading from config file that might not have this value
    return null;
}

static int? GetUserAge()
{
    // Simulate user input that might be invalid
    return null;
}


// Nullable Types vs Magic Values
Console.WriteLine("--- Nullable Types vs Magic Values ---");

Console.WriteLine("\nOLD APPROACH - Magic Values:");

string text = "Hello, World!";
int indexOfZ = text.IndexOf('z');
int indexOfH = text.IndexOf('H');

Console.WriteLine($"Looking for 'z' in '{text}': {indexOfZ}");
Console.WriteLine($"Looking for 'H' in '{text}': {indexOfH}");
Console.WriteLine("Not good practices using Magic Values");

Console.WriteLine("\nInconsistent magic values:");
Console.WriteLine($"String 'not found': {-1}");
Console.WriteLine($"Invalid date: {DateTime.MinValue}");
Console.WriteLine($"Invalid number: {double.NaN}");

Console.WriteLine("\nNEW APPROACH - Nullable Types:");

int? findIndex = FindCharacterIndex(text, 'z');
int? findValidIndex = FindCharacterIndex(text, 'H');

Console.WriteLine($"Looking for 'z': {findIndex?.ToString() ?? "Not Found"}");
Console.WriteLine($"Looking for 'H': {findValidIndex?.ToString() ?? "Not Found"}");

// Type safety
if (findIndex.HasValue)
    Console.WriteLine($"Character found at index: {findIndex.Value}");
else
    Console.WriteLine("Character not found - no ambiguity!");

// Helper method nullable approach
static int? FindCharacterIndex(string text, char character)
{
    int index = text.IndexOf(character);
    return index >= 0 ? index : null; // Return null instead of -1
}

Console.WriteLine("\n-------------------------------------------\n");


// Real World Scenario
Console.WriteLine("--- REAL WORLD SCENARIO ---");

var employees = new[]
{
    new Employee("John Doe", 30, 75000.50m),
    new Employee("Jane Smith", null, 82000.00m),
    new Employee("Bob Wilson", 45, null),
    new Employee("Alice Brown", null, null),
    new Employee("Charlie Davis", 28, 45000.00m),
};

Console.WriteLine("Employee Management System - Complete Employee List:");
Console.WriteLine("==============================================");

foreach (var emp in employees)
{
    emp.DisplayInfo();
    Console.WriteLine();
}

// Demonstrate nullable arithmetic in action
Console.WriteLine("BONUS CALCULATION DEMO:");
Console.WriteLine("=======================");
decimal? bonusPercentage = 5.5m; // 5.5% bonus

foreach (var emp in employees)
{
    decimal? bonus = emp.CalculateBonus(bonusPercentage);
    string bonusDisplay = bonus?.ToString("C") ?? "Cannot calculate (salary unknown)";
    Console.WriteLine($"{emp.Name}: Bonus = {bonusDisplay}");
}

Console.WriteLine("\nRETIREMENT ELIGIBILITY CHECK:");
Console.WriteLine("===============================");
foreach (var emp in employees)
{
    bool eligible = emp.IsRetirementEligible();
    string status = eligible ? "Eligible for retirement" : "Not eligible (or age unknown)";
    Console.WriteLine($"{emp.Name}: {status}");
}

Console.WriteLine("\nSTATISTICAL ANALYSIS:");
Console.WriteLine("=======================");

var stats = EmployeeStatistics.Calculate(employees);
Console.WriteLine($"Total employees: {stats.TotalEmployees}");
Console.WriteLine($"Employees with known age: {stats.EmployeesWithAge}");
Console.WriteLine($"Employees with known salary: {stats.EmployeesWithSalary}");
Console.WriteLine($"Average age: {stats.AverageAge?.ToString("F1") ?? "Cannot calculate (insufficient data)"}");
Console.WriteLine($"Average salary: {stats.AverageSalary?.ToString("C") ?? "Cannot calculate (insufficient data)"}");
Console.WriteLine($"Total payroll: {stats.TotalPayroll?.ToString("C") ?? "Cannot calculate (some salaries unknown)"}");

Console.WriteLine("\nKey Takeaways from this real-world example:");
Console.WriteLine("===========================================");
Console.WriteLine("1. Nullable types elegantly handle missing/unknown data");
Console.WriteLine("2. Operations propagate null appropriately (bonus calculation)");
Console.WriteLine("3. GetValueOrDefault provides safe fallbacks (retirement eligibility)");
Console.WriteLine("4. Null-coalescing operators create user-friendly displays");
Console.WriteLine("5. Statistical calculations handle partial data gracefully");

Console.WriteLine();

// Real World Classes
public class Employee
{
    public string Name { get; }
    public int? Age { get; } // Nullable - age might be private/unknown
    public decimal? Salary { get; } // Nullable - salary might be confidental

    public Employee(string name, int? age, decimal? salary)
    {
        Name = name;
        Age = age;
        Salary = salary;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Employee: {Name}");

        // Using null-coalescing for display
        string ageDisplay = Age?.ToString() ?? "Unknown";
        string salaryDisplay = Salary?.ToString() ?? "Confidential";

        // Determine benefits eligibility (requires both age and salary)
        bool? eligibleForBenefits = IsEligibleForBenefits();
        string eligibilityStatus = eligibleForBenefits?.ToString() ?? "Cannot determine";
        Console.WriteLine($"  Benefits eligible: {eligibilityStatus}");
    }

    public bool? IsEligibleForBenefits()
    {
        // Need both age and salary to determine eligibility
        if (!Age.HasValue || !Salary.HasValue)
            return null; // Cannot determine without complete information

        // Eligible if age >= 21 and salary >= 40000
        return Age >= 21 && Salary >= 40000;
    }

    public decimal? CalculateBonus(decimal? bonusPercentage)
    {
        // Using nullable arithmetic - if any value is null, result is null
        return Salary * (bonusPercentage / 100);
    }

    public bool IsRetirementEligible()
    {
        // Using GetValueOrDefault for safe comparison
        return Age.GetValueOrDefault(0) >= 65;
    }
}

public class EmployeeStatistics
{
    public int TotalEmployees { get; set; }
    public int EmployeesWithAge { get; set; }
    public int EmployeesWithSalary { get; set; }
    public double? AverageAge { get; set; }
    public decimal? AverageSalary { get; set; }
    public decimal? TotalPayroll { get; set; }

    public static EmployeeStatistics Calculate(Employee[] employees)
    {
        var stats = new EmployeeStatistics
        {
            TotalEmployees = employees.Length
        };

        // Calculate statistics for non-null values only
        var knownAges = new List<int>();
        var knowSalaries = new List<decimal>();
        decimal? totalPayroll = 0;
        bool allSalariesKnown = true;

        foreach (var emp in employees)
        {
            if (emp.Age.HasValue)
            {
                knownAges.Add(emp.Age.Value);
                stats.EmployeesWithAge++;
            }

            if (emp.Salary.HasValue)
            {
                knowSalaries.Add(emp.Salary.Value);
                stats.EmployeesWithSalary++;
                totalPayroll += emp.Salary.Value;
            }
            else
            {
                allSalariesKnown = false;
            }
        }

        // Calculate averages (nullable results)
        stats.AverageAge = knownAges.Count > 0 ? knownAges.Average() : null;
        stats.AverageSalary = knowSalaries.Count > 0 ? knowSalaries.Average() : null;
        stats.TotalPayroll = allSalariesKnown ? totalPayroll : null;

        return stats;
    }
}
