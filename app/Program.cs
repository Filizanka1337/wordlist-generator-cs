using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("Witaj w generatorze wordlist!");
        Console.WriteLine("Podaj ścieżkę do folderu, w którym zostanie zapisana wordlista:");
        string folderPath = Console.ReadLine();

        Console.WriteLine("Czy chcesz zawierać znaki specjalne (np. !@#$%^&*())? (T/N)");
        bool includeSpecialChars = Console.ReadLine().Equals("T", StringComparison.OrdinalIgnoreCase);

        Console.WriteLine("Czy chcesz zawierać cyfry? (T/N)");
        bool includeDigits = Console.ReadLine().Equals("T", StringComparison.OrdinalIgnoreCase);

        Console.WriteLine("Od ilu znaków ma zaczynać się hasło?");
        int minLength = int.Parse(Console.ReadLine());

        Console.WriteLine("Do ilu znaków ma sięgać hasło?");
        int maxLength = int.Parse(Console.ReadLine());

        GenerateWordlist(folderPath, includeSpecialChars, includeDigits, minLength, maxLength);

        Console.WriteLine("Wordlista została wygenerowana i zapisana w pliku wordlist.txt.");
        Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
        Console.ReadKey();
    }

    static void GenerateWordlist(string folderPath, bool includeSpecialChars, bool includeDigits, int minLength, int maxLength)
    {
        StringBuilder wordlist = new StringBuilder();

        string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string specialChars = "!@#$%^&*()";
        string digits = "0123456789";

        for (int length = minLength; length <= maxLength; length++)
        {
            GenerateWordlistRecursively(wordlist, "", length, includeSpecialChars, includeDigits, lowercaseLetters, uppercaseLetters, specialChars, digits);
        }

        string filePath = Path.Combine(folderPath, "wordlist.txt");
        File.WriteAllText(filePath, wordlist.ToString());
    }

    static void GenerateWordlistRecursively(StringBuilder wordlist, string current, int length, bool includeSpecialChars, bool includeDigits,
                                            string lowercaseLetters, string uppercaseLetters, string specialChars, string digits)
    {
        if (length == 0)
        {
            wordlist.AppendLine(current);
            return;
        }

        foreach (char c in lowercaseLetters)
        {
            GenerateWordlistRecursively(wordlist, current + c, length - 1, includeSpecialChars, includeDigits, lowercaseLetters, uppercaseLetters, specialChars, digits);
        }

        foreach (char c in uppercaseLetters)
        {
            GenerateWordlistRecursively(wordlist, current + c, length - 1, includeSpecialChars, includeDigits, lowercaseLetters, uppercaseLetters, specialChars, digits);
        }

        if (includeSpecialChars)
        {
            foreach (char c in specialChars)
            {
                GenerateWordlistRecursively(wordlist, current + c, length - 1, includeSpecialChars, includeDigits, lowercaseLetters, uppercaseLetters, specialChars, digits);
            }
        }

        if (includeDigits)
        {
            foreach (char c in digits)
            {
                GenerateWordlistRecursively(wordlist, current + c, length - 1, includeSpecialChars, includeDigits, lowercaseLetters, uppercaseLetters, specialChars, digits);
            }
        }
    }
}
