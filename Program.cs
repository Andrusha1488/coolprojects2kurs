using System;
using System.Collections.Generic;

class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
    }
}

class Program
{
    static List<Note> notes = new List<Note>
    {
        new Note("Note 1", "Description 1", new DateTime(2022, 12, 3)),
        new Note("Note 2", "Description 2", new DateTime(2022, 12, 4)),
        new Note("Note 3", "Description 3", new DateTime(2022, 12, 5)),
        new Note("Note 4", "Description 4", new DateTime(2022, 12, 6)),
        new Note("Note 5", "Description 5", new DateTime(2022, 12, 7))
    };

    static int currentNoteIndex = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow)
            {
                currentNoteIndex = Math.Max(0, currentNoteIndex - 1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                currentNoteIndex = Math.Min(notes.Count - 1, currentNoteIndex + 1);
            }
            else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.LeftArrow)
            {
                NavigateNotes(key.Key == ConsoleKey.RightArrow);
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                ShowNoteDetails();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine($"Selected date: {notes[currentNoteIndex].Date.ToString("d")}");
        for (int i = 0; i < notes.Count; i++)
        {
            if (i == currentNoteIndex)
            {
                Console.WriteLine($"-> {i + 1}. {notes[i].Title}");
            }
            else
            {
                Console.WriteLine($"   {i + 1}. {notes[i].Title}");
            }
        }
    }

    static void NavigateNotes(bool right)
    {
        if (right)
        {
            currentNoteIndex = (currentNoteIndex + 1) % notes.Count;
        }
        else
        {
            currentNoteIndex = (currentNoteIndex - 1 + notes.Count) % notes.Count;
        }
    }

    static void ShowNoteDetails()
    {
        Console.Clear();
        Console.WriteLine($"{notes[currentNoteIndex].Title}");
        Console.WriteLine(new string('-', 25));
        Console.WriteLine($"Description: {notes[currentNoteIndex].Description}");
        Console.WriteLine($"Date: {notes[currentNoteIndex].Date.ToString("d")}");
        Console.WriteLine("Press Enter to return.");
        while (Console.ReadKey().Key != ConsoleKey.Enter) ;
    }
}
