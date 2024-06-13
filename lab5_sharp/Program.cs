using System;

class TMatrix {
    protected int[][] data;
    protected int rows;
    protected int cols;

    public TMatrix() {
        rows = 0;
        cols = 0;
        data = new int[0][];
    }

    public TMatrix(int r, int c) {
        rows = r;
        cols = c;
        data = new int[r][];
        for (int i = 0; i < r; i++) {
            data[i] = new int[c];
        }
    }

    public TMatrix(TMatrix other) {
        rows = other.rows;
        cols = other.cols;
        data = new int[rows][];
        for (int i = 0; i < rows; i++) {
            data[i] = new int[cols];
            Array.Copy(other.data[i], data[i], cols);
        }
    }

    public void Input() {
        Console.WriteLine("Введіть елементи матриці (рядок за рядком):");
        for (int i = 0; i < rows; ++i) {
            string input = Console.ReadLine();
            if (input != null) {
                string[] inputs = input.Split();
                if (inputs.Length != cols) {
                    throw new ArgumentException("Кількість введених чисел не відповідає кількості стовпців.");
                }
                for (int j = 0; j < cols; ++j) {
                    if (int.TryParse(inputs[j], out int value)) {
                        data[i][j] = value;
                    } else {
                        throw new ArgumentException("Введено некоректне число.");
                    }
                }
            } else {
                throw new ArgumentException("Введення не може бути порожнім.");
            }
        }
    }

    public void Output() {
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                Console.Write(data[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public int FindMaxElement() {
        int maxElement = int.MinValue;
        for (int i = 0; i < rows; ++i) {
            maxElement = Math.Max(maxElement, Max(data[i]));
        }
        return maxElement;
    }

    public int FindMinElement() {
        int minElement = int.MaxValue;
        for (int i = 0; i < rows; ++i) {
            minElement = Math.Min(minElement, Min(data[i]));
        }
        return minElement;
    }

    public int SumElements() {
        int sum = 0;
        for (int i = 0; i < rows; ++i) {
            sum += Sum(data[i]);
        }
        return sum;
    }

    public int GetRows() {
        return rows;
    }

    public int GetCols() {
        return cols;
    }

    private int Max(int[] arr) {
        int max = int.MinValue;
        foreach (int item in arr) {
            max = Math.Max(max, item);
        }
        return max;
    }

    private int Min(int[] arr) {
        int min = int.MaxValue;
        foreach (int item in arr) {
            min = Math.Min(min, item);
        }
        return min;
    }

    private int Sum(int[] arr) {
        int sum = 0;
        foreach (int item in arr) {
            sum += item;
        }
        return sum;
    }
}

class TOpMatrix : TMatrix {
    public TOpMatrix() : base() {}

    public TOpMatrix(int r, int c) : base(r, c) {}

    public TOpMatrix(TMatrix other) : base(other) {}

    public static TOpMatrix operator+(TOpMatrix m1, TOpMatrix m2) {
        if (m1.rows != m2.rows || m1.cols != m2.cols) {
            throw new ArgumentException("Розміри матриці не збігаються для додавання.");
        }
        TOpMatrix result = new TOpMatrix(m1.rows, m1.cols);
        for (int i = 0; i < m1.rows; ++i) {
            for (int j = 0; j < m1.cols; ++j) {
                result.data[i][j] = m1.data[i][j] + m2.data[i][j];
            }
        }
        return result;
    }

    public static TOpMatrix operator-(TOpMatrix m1, TOpMatrix m2) {
        if (m1.rows != m2.rows || m1.cols != m2.cols) {
            throw new ArgumentException("Розміри матриці не збігаються для віднімання.");
        }
        TOpMatrix result = new TOpMatrix(m1.rows, m1.cols);
        for (int i = 0; i < m1.rows; ++i) {
            for (int j = 0; j < m1.cols; ++j) {
                result.data[i][j] = m1.data[i][j] - m2.data[i][j];
            }
        }
        return result;
    }

    public static TOpMatrix operator*(TOpMatrix m1, TOpMatrix m2) {
        if (m1.cols != m2.rows) {
            throw new ArgumentException("Розміри матриці не збігаються для множення.");
        }
        TOpMatrix result = new TOpMatrix(m1.rows, m2.cols);
        for (int i = 0; i < m1.rows; ++i) {
            for (int j = 0; j < m2.cols; ++j) {
                result.data[i][j] = 0;
                for (int k = 0; k < m1.cols; ++k) {
                    result.data[i][j] += m1.data[i][k] * m2.data[k][j];
                }
            }
        }
        return result;
    }
}

class Program {
    static void Main(string[] args) {
        try {
            int rows1, cols1, rows2, cols2;
            Console.Write("Введіть кількість рядків і стовпців для матриці 1: ");
            string[] dimensions1 = Console.ReadLine().Split();
            rows1 = int.Parse(dimensions1[0]);
            cols1 = int.Parse(dimensions1[1]);
            TOpMatrix mat1 = new TOpMatrix(rows1, cols1);
            Console.WriteLine("Введення матриці 1:");
            mat1.Input();
            Console.WriteLine("Матриця:");
            mat1.Output();

            Console.Write("Введіть кількість рядків і стовпців для матриці 2: ");
            string[] dimensions2 = Console.ReadLine().Split();
            rows2 = int.Parse(dimensions2[0]);
            cols2 = int.Parse(dimensions2[1]);
            TOpMatrix mat2 = new TOpMatrix(rows2, cols2);
            Console.WriteLine("Введення матриці 2:");
            mat2.Input();
            Console.WriteLine("Матриця:");
            mat2.Output();

            try {
                TOpMatrix matSum = mat1 + mat2;
                Console.WriteLine("Сума матриць:");
                matSum.Output();
            } catch (ArgumentException e) {
                Console.WriteLine("Помилка додавання: " + e.Message);
            }

            try {
                TOpMatrix matDiff = mat1 - mat2;
                Console.WriteLine("Різниця матриць:");
                matDiff.Output();
            } catch (ArgumentException e) {
                Console.WriteLine("Помилка віднімання: " + e.Message);
            }

            try {
                TOpMatrix matProd = mat1 * mat2;
                Console.WriteLine("Добуток матриць:");
                matProd.Output();
            } catch (ArgumentException e) {
                Console.WriteLine("Помилка множення: " + e.Message);
            }

            Console.WriteLine("Максимальний елемент у матриці 1: " + mat1.FindMaxElement());
            Console.WriteLine("Мінімальний елемент у матриці 1: " + mat1.FindMinElement());
            Console.WriteLine("Сума елементів у матриці 1: " + mat1.SumElements());
        } catch (Exception e) {
            Console.WriteLine("Сталася помилка: " + e.Message);
        }
    }
}
