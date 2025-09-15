// Object Type
Console.WriteLine("---Why Object Type Useful?---");
Stack stack = new Stack();
stack.Push("sausage");
string s = (string)stack.Pop();
Console.WriteLine(s);

stack.Push(3);
int three = (int)stack.Pop();
Console.WriteLine(three);

stack.Push(5.5);
double gotDouble = (double)stack.Pop();
Console.WriteLine(gotDouble);
Console.WriteLine();

Console.WriteLine("---Boxing and Unboxing---");
// Boxing
int x = 20;
object obj = x;
Console.Write(obj);
Console.WriteLine(" // Boxing implementation");

// Unboxing
int y = (int)obj;
Console.Write(y);
Console.WriteLine(" // Unboxing implementation");
Console.WriteLine();


public class Stack
{
    int position;
    readonly object[] data = new object[10];

    public void Push(object obj) { data[position++] = obj; }
    public object Pop() { return data[--position]; }
}
