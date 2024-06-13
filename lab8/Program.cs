using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static readonly List<char> PunctuationSymbols = new List<char>()
    {
        '.', ',', ';', ':', '!', '?', '-', '—', '(', ')', '[', ']', '{', '}', '\"', '\'',
    };

    static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        while (true)
        {
            Console.WriteLine("Оберіть опцію:");
            Console.WriteLine("1. Записати дані у файл");
            Console.WriteLine("2. Прочитати дані та порахувати знаки пунктуації");
            Console.WriteLine("3. Вийти");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Недійсний варіант. Будь ласка, введіть число від 1 до 3.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    WriteToFile();
                    break;
                case 2:
                    ReadAndCountPunctuation();
                    break;
                case 3:
                    Console.WriteLine("Вихід з програми...");
                    return;
            }
        }
    }

    static void WriteToFile()
    {
        try
        {
            Console.WriteLine("Введіть текст для запису у файл:");
            string text = Console.ReadLine();
            File.WriteAllText("file.txt", text);
            Console.WriteLine("Текст успішно записано у файл.");

            string fileContent = File.ReadAllText("file.txt");
            Console.WriteLine("Вміст файлу:");
            Console.WriteLine(fileContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Під час запису у файл сталася помилка: {ex.Message}");
        }
    }

    static void ReadAndCountPunctuation()
    {
        try
        {
            string filePath = "file.txt";
            string fileContent = File.ReadAllText(filePath);
            char[] symbols = fileContent.ToCharArray();

            // array method
            int punctuationCountArray = CountPunctuationArray(symbols);

            // List method
            int punctuationCountList = CountPunctuationList(fileContent);

            Console.WriteLine($"Кількість знаків пунктуації (array method): {punctuationCountArray}");
            Console.WriteLine($"Кількість знаків пунктуації (List method): {punctuationCountList}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка під час читання або підрахунку розділових знаків: {ex.Message}");
        }
    }

    static int CountPunctuationArray(char[] symbols)
    {
        int punctuationCount = 0;
        foreach (char c in symbols)
        {
            if (IsPunctuationCustom(c))
            {
                punctuationCount++;
            }
        }
        return punctuationCount;
    }

    static int CountPunctuationList(string text)
    {
        List<char> characters = text.ToList();
        int punctuationCount = characters.Count(IsPunctuationCustom);
        return punctuationCount;
    }

    static bool IsPunctuationCustom(char c)
    {
        return PunctuationSymbols.Contains(c);
    }
}
