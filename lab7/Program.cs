using System;

public interface IVector
{
    double VectorLength();
    double[] VectorSum(double[] vector);
    double[] VectorScalarMultiply(double scalar);
    double ScalarProduct(double[] vector);
    double[] GetStartPoint();
    double[] GetEndPoint();
}

public class Vector : IVector
{
    private double[] startPoint;
    private double[] endPoint;

    public Vector()
    {
        startPoint = new double[2];
        endPoint = new double[2];
    }

    public Vector(double[] start, double[] end)
    {
        startPoint = start;
        endPoint = end;
    }

    public double VectorLength()
    {
        double deltaX = endPoint[0] - startPoint[0];
        double deltaY = endPoint[1] - startPoint[1];
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public double[] VectorSum(double[] vector)
    {
        double[] result = new double[2];
        result[0] = endPoint[0] + vector[0] - startPoint[0];
        result[1] = endPoint[1] + vector[1] - startPoint[1];
        return result;
    }

    public double[] VectorScalarMultiply(double scalar)
    {
        double[] result = new double[2];
        result[0] = scalar * (endPoint[0] - startPoint[0]);
        result[1] = scalar * (endPoint[1] - startPoint[1]);
        return result;
    }

    public double ScalarProduct(double[] vector)
    {
        double deltaX1 = endPoint[0] - startPoint[0];
        double deltaY1 = endPoint[1] - startPoint[1];
        double deltaX2 = vector[0];
        double deltaY2 = vector[1];
        return deltaX1 * deltaX2 + deltaY1 * deltaY2;
    }

    public double[] GetStartPoint()
    {
        return startPoint;
    }

    public double[] GetEndPoint()
    {
        return endPoint;
    }
}

public class Program
{
    public static void PrintMenu()
    {
        Console.WriteLine("\nМеню:");
        Console.WriteLine("1. Створити вектор зі стандартними координатами");
        Console.WriteLine("2. Створити вектор з користувацькими координатами");
        Console.WriteLine("3. Показати вектор");
        Console.WriteLine("4. Знайти довжину вектора");
        Console.WriteLine("5. Знайти суму двох векторів");
        Console.WriteLine("6. Помножити вектор на скаляр");
        Console.WriteLine("7. Знайти скалярний добуток двох векторів");
        Console.WriteLine("8. Вийти");
    }

    public static void Main(string[] args)
    {
        Vector vec = new Vector();
        bool exit = false;

        while (!exit)
        {
            PrintMenu();
            Console.Write("Виберіть варіант: ");
            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    {
                        double[] defaultStart = { 0, 0 };
                        double[] defaultEnd = { 3, 4 };
                        vec = new Vector(defaultStart, defaultEnd);
                        Console.WriteLine("Вектор створено з координатами за замовчуванням.");
                        break;
                    }
                case 2:
                    {
                        vec = new Vector();
                        Console.WriteLine("Введіть координати початкової точки:");
                        vec.GetStartPoint()[0] = Convert.ToDouble(Console.ReadLine());
                        vec.GetStartPoint()[1] = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Введіть координати кінцевої точки:");
                        vec.GetEndPoint()[0] = Convert.ToDouble(Console.ReadLine());
                        vec.GetEndPoint()[1] = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Вектор створений із власними координатами.");
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine($"Початкова точка: ({vec.GetStartPoint()[0]}, {vec.GetStartPoint()[1]})");
                        Console.WriteLine($"Кінцева точка: ({vec.GetEndPoint()[0]}, {vec.GetEndPoint()[1]})");
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Довжина вектора: " + vec.VectorLength());
                        break;
                    }
                case 5:
                    {
                        double[] secondVector = new double[2];
                        Console.WriteLine("Введіть координати для другого вектора:");
                        secondVector[0] = Convert.ToDouble(Console.ReadLine());
                        secondVector[1] = Convert.ToDouble(Console.ReadLine());
                        double[] sum = vec.VectorSum(secondVector);
                        Console.WriteLine($"Результат: ({sum[0]}, {sum[1]})");
                        break;
                    }
                case 6:
                    {
                        Console.Write("Введіть скалярне значення: ");
                        double scalar = Convert.ToDouble(Console.ReadLine());
                        double[] product = vec.VectorScalarMultiply(scalar);
                        Console.WriteLine($"Результат: ({product[0]}, {product[1]})");
                        break;
                    }
                case 7:
                    {
                        double[] secondVector = new double[2];
                        Console.WriteLine("Введіть координати для другого вектора:");
                        secondVector[0] = Convert.ToDouble(Console.ReadLine());
                        secondVector[1] = Convert.ToDouble(Console.ReadLine());
                        double dotProduct = vec.ScalarProduct(secondVector);
                        Console.WriteLine("Скалярний добуток: " + dotProduct);
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("Вихід з програми...");
                        exit = true;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Недійсний варіант. Виберіть правильний варіант.");
                        break;
                    }
            }
        }
    }
}


