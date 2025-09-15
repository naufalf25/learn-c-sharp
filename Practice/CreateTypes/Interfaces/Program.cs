// Basic Interface
Console.WriteLine("---Basic Interfaces---");
IEnumerator e = new Countdown();
Console.WriteLine("The results of the countdown is:");
while (e.MoveNext())
    Console.Write(e.Current + " ");
Console.WriteLine();


// Interface Inherit
Console.WriteLine("\n---Interfaces Inherit---");
TextEditor textEditor = new TextEditor();
textEditor.AddNewText("This is a new text added here!");
textEditor.DisplayCurrentText();
textEditor.EditText("This is edited text");
textEditor.DisplayCurrentText();
textEditor.Undo();
textEditor.DisplayCurrentText();
textEditor.Redo();
textEditor.DisplayCurrentText();
Console.WriteLine();

// Explicit Interface
Console.WriteLine("---Explicit Interface---");
Widget widget = new Widget();
widget.Foo();
((I1)widget).Foo();
((I2)widget).Foo();
Console.WriteLine();

// Interface Member Virtually and Reimplementing an Interface in a Subclass
Console.WriteLine("---Interface Member Virtually and Reimplementing an Interface in a Subclass---");
RichTextBox richTextBox = new RichTextBox();
richTextBox.Undo();
((IUndoable)richTextBox).Undo();
((TextEditor)richTextBox).Undo();
Console.WriteLine();

// Default Interface Member
Console.WriteLine("---Default Interface Member---");
((ILogger)new Logger()).Log("message");
Console.WriteLine();

// Static Interface Member
Console.WriteLine("---Static Interface Member---");
ILogger.Prefix = "File log: ";
((ILogger)new Logger()).Log("New Message of static interface member");

public interface IEnumerator
{
    bool MoveNext();
    object Current { get; }
    void Reset();
}

class Countdown : IEnumerator
{
    int count = 11;

    public bool MoveNext() => count-- > 0;
    public object Current => count;
    public void Reset() { throw new NotSupportedException(); }
}

public interface IUndoable { void Undo(); }
public interface IRedoable : IUndoable { void Redo(); }

class TextEditor : IRedoable
{
    private string _currentText = "";
    private string _previousText = "";
    private string _tempText = "";

    public void AddNewText(string text)
    {
        Console.WriteLine("Text Editor: Creating New Text...");
        _currentText = text;
        _previousText = _currentText;
        _tempText = _currentText;
    }

    public void EditText(string text)
    {
        Console.WriteLine("Text Editor: Editing the text...");
        _currentText = text;
        _tempText = text;
    }

    public virtual void Undo()
    {
        Console.WriteLine("Text Editor: Undo to previous text...");
        // _currentText = _previousText;
    }

    public void Redo()
    {
        Console.WriteLine("Text Editor: Redo to the text before...");
        _currentText = _tempText;
    }

    public void DisplayCurrentText()
    {
        Console.WriteLine("Text Editor: The current text is:");
        Console.WriteLine(_currentText);
    }
}

class RichTextBox : TextEditor, IUndoable
{
    public override void Undo() => Console.WriteLine("RichTextBox.Undo");
}

interface I1 { void Foo(); }
interface I2 { int Foo(); }

public class Widget : I1, I2
{
    public void Foo() => Console.WriteLine("Widget's implementation of I1.Foo");

    int I2.Foo()
    {
        Console.WriteLine("Widget's implementation of I2.Foo");
        return 42;
    }
}

interface ILogger
{
    void Log(string text) => Console.WriteLine(text);

    void NewLog(string text) => Console.WriteLine(Prefix + text);
    static string Prefix = "";
}

class Logger : ILogger { }
