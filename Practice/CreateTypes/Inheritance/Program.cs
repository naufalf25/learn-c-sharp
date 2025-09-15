Student student = new Student { Name = "Fer", Email = "test@test.com", Age = 18, StudentId = 12345, Grade = 12, GPA = 3.68 };
student.DisplayUserDetail();
student.DisplayStudentInfo();
Console.WriteLine();

Employee employee = new Employee { Name = "Jun", Email = "test2@test.com", Age = 22, EmployeeId = 123354, Year = 2 };
employee.DisplayUserDetail();
employee.DisplayEmployeeInfo();
Console.WriteLine();

Display(student);
// Display(new User()); // Will be got error
Console.WriteLine();

static void Display(Student student)
{
    Console.WriteLine(student.Name);
}


// Upcasting
Console.WriteLine("Upcasting");
User a = student;
Console.WriteLine(a.Name);
Console.WriteLine();

// Downcasting
Console.WriteLine("Downcasting");
User b = student;
Student s = (Student)b;
// Student s2 = (Employee)b; // Will be got error
Console.WriteLine(s.StudentId);
Console.WriteLine(s == a);
Console.WriteLine(s == student);
Console.WriteLine();


User c = new User();
// as Operator
// Student s3 = c as Student; // Will be null

// is Operator
if (c is Student student1)
    Console.WriteLine(student1.StudentId);
else
    Console.WriteLine(false);
Console.WriteLine();


// Virtual Function Members
Console.WriteLine("---Virtual Function Members---");
House mansion = new House { Name = "McMansion", Mortgage = 2500000 };
Asset d = mansion;
Console.Write(mansion.Liability);
Console.WriteLine(" // House's overridden implementation");
Console.Write(d.Liability);
Console.WriteLine(" // Polymorphism: Calls House's implementation");
Console.WriteLine();

// Abstack Function Members
Console.WriteLine("---Abstract Function Members---");
Stock ownStock = new Stock { SharesOwner = 25000, CurrentPrice = 500000000 };
Console.Write(ownStock.NetValue);
Console.WriteLine(" // Must provide implementation from abstract Asset function");
Console.WriteLine();

// Hiding Inherited Members
Console.WriteLine("---Hiding Inherited Members---");
A newA = new B();
Console.WriteLine(newA.Counter);
B newB = new B();
Console.WriteLine(newB.Counter);
Console.WriteLine();


public class User
{
    public string Name = "";
    public string Email = "";
    public int Age;

    public void DisplayUserDetail()
    {
        Console.WriteLine("The detail of this user:");
        Console.WriteLine($"Name: {Name}, Email: {Email}, Age: {Age}");
    }
}

public class Student : User
{
    public int StudentId;
    public int Grade;
    public double GPA;

    public void DisplayStudentInfo()
    {
        Console.WriteLine("This user has student information:");
        Console.WriteLine($"ID: {StudentId}, Grade: {Grade}, GPA: {GPA}");
    }
}

public class Employee : User
{
    public int EmployeeId;
    public int Year;

    public void DisplayEmployeeInfo()
    {
        Console.WriteLine("This user has Employee Information:");
        Console.WriteLine($"ID: {EmployeeId}, Year in this company: {Year}");
    }
}

public abstract class Asset
{
    public string Name = "";
    public virtual decimal Liability => 0;
    // public virtual Asset Clone() => new Asset { Name = Name };
    public abstract decimal NetValue { get; }
}

public class Stock : Asset
{
    public long SharesOwner;
    public decimal CurrentPrice;
    public override decimal NetValue => CurrentPrice * SharesOwner;
}

public class House : Asset
{
    public decimal Mortgage;
    public override decimal Liability => base.Liability + Mortgage; // base keyword implementation

    public override decimal NetValue => throw new NotImplementedException();

    // public override Asset Clone() => new House { Name = Name, Mortgage = Mortgage };
}

public class A { public int Counter = 1; }
public class B : A { public new int Counter = 2; }

public class Baseclass
{
    public int X;
    public Baseclass() { }
    public Baseclass(int x) => X = X;
}

// public class Subclass : Baseclass { } // ILLEGAL: Subclass doesn't have a constructor taking an int

public class Subclass : Baseclass
{
    public Subclass(int x) : base(x) {}
}
