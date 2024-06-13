// lab 6 #  

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Завдання 1.1
    static List<double> ConcatenatePositive(List<double> x, List<double> y)
    {
        List<double> z = new List<double>();
        foreach (double val in x)
        {
            if (val > 0)
                z.Add(val);
        }
        foreach (double val in y)
        {
            if (val > 0)
                z.Add(val);
        }
        return z;
    }

    // Завдання 1.2
    static List<double> CalculateExpression(List<double> a, List<double> b, List<double> c)
    {
        List<double> result = new List<double>();
        for (int i = 0; i < a.Count; i++)
        {
            result.Add(2 * (a[i] + c[i]) - b[i]);
        }
        return result;
    }

    // Завдання 1.3
    static void TransformArray(List<double> arr, double a, double b)
{
    var inInterval = arr.Where(val => (int)val >= a && (int)val <= b).ToList();
    var outOfInterval = arr.Where(val => (int)val < a || (int)val > b).ToList();

    arr.Clear();
    arr.AddRange(inInterval);
    arr.AddRange(outOfInterval);
}


    // Завдання 2.1
    static void CyclicShiftColumns(List<List<int>> matrix, int k)
    {
        int numRows = matrix.Count;
        int numCols = matrix[0].Count;

        k = k % numCols;

        List<int>[] tempCols = new List<int>[numCols];
        for (int col = 0; col < numCols; col++)
        {
            tempCols[col] = new List<int>();
            for (int row = 0; row < numRows; row++)
            {
                tempCols[col].Add(matrix[row][col]);
            }
        }

        for (int col = 1; col < numCols; col += 2)
        {
            int newCol = (col + k) % numCols;
            for (int row = 0; row < numRows; row++)
            {
                matrix[row][newCol] = tempCols[col][row];
            }
        }
    }



    // Завдання 2.2
    static void CompressMatrix(List<List<int>> matrix)
{
    List<int> rowsToRemove = new List<int>();

    for (int i = 0; i < matrix.Count; i++)
    {
        bool allZeros = true; 
        for (int j = 0; j < matrix[i].Count; j++)
        {
            if (matrix[i][j] != 0)
            {
                allZeros = false;
                break;
            }
        }
        if (allZeros)
        {
            rowsToRemove.Add(i); 
        }
    }

    foreach (int rowIndex in rowsToRemove.OrderByDescending(x => x))
    {
        matrix.RemoveAt(rowIndex);
    }

    HashSet<int> colsToRemove = new HashSet<int>();

    for (int j = 0; j < matrix[0].Count; j++)
    {
        bool allZeros = true; 
        for (int i = 0; i < matrix.Count; i++)
        {
            if (matrix[i][j] != 0)
            {
                allZeros = false;
                break;
            }
        }
        if (allZeros)
        {
            colsToRemove.Add(j); 
        }
    }

    foreach (int colIndex in colsToRemove.OrderByDescending(x => x))
    {
        foreach (var row in matrix)
        {
            row.RemoveAt(colIndex);
        }
    }
}


    // Завдання 2.3
    static int FindFirstPositiveRow(List<List<int>> matrix)
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            if (matrix[i].Any(val => val > 0))
            {
                return i + 1;
            }
        }
        return -1;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Виберіть завдання:");
        Console.WriteLine("1. Одновимірні масиви");
        Console.WriteLine("2. Двовимірні масиви");

        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine("Виберіть підзавдання для одновимірних масивів:");
            Console.WriteLine("1. Побудова вектора з додатніми координатами");
            Console.WriteLine("2. Обчислення виразу");
            Console.WriteLine("3. Трансформація масиву");

            int subChoice = int.Parse(Console.ReadLine());

            if (subChoice == 1)
            {
                Console.WriteLine("Введіть розмірність векторів:");
                int n = int.Parse(Console.ReadLine());
                List<double> x = new List<double>(n);
                List<double> y = new List<double>(n);

                Console.WriteLine("Введіть координати вектора x:");
                for (int i = 0; i < n; i++)
                {
                    x.Add(double.Parse(Console.ReadLine()));
                }

                Console.WriteLine("Введіть координати вектора y:");
                for (int i = 0; i < n; i++)
                {
                    y.Add(double.Parse(Console.ReadLine()));
                }

                List<double> z = ConcatenatePositive(x, y);
                Console.WriteLine("Результат:");
                Console.WriteLine(string.Join(" ", z));
            }
            else if (subChoice == 2)
            {
                Console.WriteLine("Введіть розмірність векторів:");
                int n = int.Parse(Console.ReadLine());
                List<double> a = new List<double>(n);
                List<double> b = new List<double>(n);
                List<double> c = new List<double>(n);

                Console.WriteLine("Введіть координати вектора a:");
                for (int i = 0; i < n; i++)
                {
                    a.Add(double.Parse(Console.ReadLine()));
                }

                Console.WriteLine("Введіть координати вектора b:");
                for (int i = 0; i < n; i++)
                {
                    b.Add(double.Parse(Console.ReadLine()));
                }

                Console.WriteLine("Введіть координати вектора c:");
                for (int i = 0; i < n; i++)
                {
                    c.Add(double.Parse(Console.ReadLine()));
                }

                List<double> result = CalculateExpression(a, b, c);
                Console.WriteLine("Результат:");
                Console.WriteLine(string.Join(" ", result));
            }
            else if (subChoice == 3)
            {
                Console.WriteLine("Введіть розмірність масиву:");
                int n = int.Parse(Console.ReadLine());
                List<double> arr = new List<double>(n);

                Console.WriteLine("Введіть елементи масиву:");
                for (int i = 0; i < n; i++)
                {
                    arr.Add(double.Parse(Console.ReadLine()));
                }

                Console.WriteLine("Введіть інтервал [a, b]:");
                double a = double.Parse(Console.ReadLine());
                double b = double.Parse(Console.ReadLine());

                TransformArray(arr, a, b);
                Console.WriteLine("Результат:");
                Console.WriteLine(string.Join(" ", arr));
            }
            else
            {
                Console.WriteLine("Неправильний вибір підзавдання!");
            }
        }
        else if (choice == 2)
        {
            Console.WriteLine("Виберіть підзавдання для двовимірних масивів:");
            Console.WriteLine("1. Циклічно зсунути стовпці матриці зліва направо на k позицій");
            Console.WriteLine("2. Ущільнення матриці");
            Console.WriteLine("3. Пошук рядка з позитивними елементами");
                    int subChoice = int.Parse(Console.ReadLine());

        if (subChoice == 1)
        {
            Console.WriteLine("Введіть розмірність квадратної матриці:");
            int n = int.Parse(Console.ReadLine());
            List<List<int>> matrix = new List<List<int>>();

            Console.WriteLine("Введіть елементи матриці:");
            for (int i = 0; i < n; i++)
            {
                matrix.Add(Console.ReadLine().Split().Select(int.Parse).ToList());
            }

            Console.WriteLine("Введіть кількість позицій для зсуву стовпців:");
            int k = int.Parse(Console.ReadLine());

            CyclicShiftColumns(matrix, k);
            Console.WriteLine("Результат:");
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
        else if (subChoice == 2)
        {
            Console.WriteLine("Введіть розміри матриці (m x n):");
            int m = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            List<List<int>> matrix = new List<List<int>>();

            Console.WriteLine("Введіть елементи матриці:");
            for (int i = 0; i < m; i++)
            {
                matrix.Add(Console.ReadLine().Split().Select(int.Parse).ToList());
            }

            CompressMatrix(matrix);
            Console.WriteLine("Результат:");
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }
        else if (subChoice == 3)
        {
            Console.WriteLine("Введіть розміри матриці (m x n):");
            int m = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            List<List<int>> matrix = new List<List<int>>();

            Console.WriteLine("Введіть елементи матриці:");
            for (int i = 0; i < m; i++)
            {
                matrix.Add(Console.ReadLine().Split().Select(int.Parse).ToList());
            }

            int rowIndex = FindFirstPositiveRow(matrix);
            if (rowIndex != -1)
                Console.WriteLine("Номер першого рядка з позитивними елементами: " + rowIndex);
            else
                Console.WriteLine("Жоден рядок не містить позитивних елементів.");
        }
        else
        {
            Console.WriteLine("Неправильний вибір підзавдання!");
        }
    }
    else
    {
        Console.WriteLine("Неправильний вибір завдання!");
    }
}
}


