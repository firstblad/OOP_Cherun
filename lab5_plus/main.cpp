#include <iostream>
#include <vector>
#include <algorithm>
#include <limits>
#include <numeric>
#include <stdexcept>

class TMatrix {
protected:
    std::vector<std::vector<int> > data;
    int rows;
    int cols;

public:
    // Конструктор без параметрів
    TMatrix() : rows(0), cols(0) {}

    // Конструктор з параметрами
    TMatrix(int r, int c) : rows(r), cols(c) {
        data.resize(r, std::vector<int>(c, 0));
    }

    // Конструктор копіювання
    TMatrix(const TMatrix &other) : rows(other.rows), cols(other.cols), data(other.data) {}

    // Метод для введення даних
    void input() {
        std::cout << "Введіть елементи матриці:" << std::endl;
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                std::cin >> data[i][j];
            }
        }
    }

    // Метод для виведення даних
    void output() const {
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                std::cout << data[i][j] << " ";
            }
            std::cout << std::endl;
        }
    }

    // Метод для знаходження найбільшого елемента
    int findMaxElement() const {
        int maxElement = std::numeric_limits<int>::min();
        for (size_t i = 0; i < data.size(); ++i) {
            maxElement = std::max(maxElement, *std::max_element(data[i].begin(), data[i].end()));
        }
        return maxElement;
    }

    // Метод для знаходження найменшого елемента
    int findMinElement() const {
        int minElement = std::numeric_limits<int>::max();
        for (size_t i = 0; i < data.size(); ++i) {
            minElement = std::min(minElement, *std::min_element(data[i].begin(), data[i].end()));
        }
        return minElement;
    }

    // Метод для знаходження суми елементів
    int sumElements() const {
        int sum = 0;
        for (size_t i = 0; i < data.size(); ++i) {
            sum += std::accumulate(data[i].begin(), data[i].end(), 0);
        }
        return sum;
    }

    int getRows() const {
        return rows;
    }

    int getCols() const {
        return cols;
    }
};

class TOpMatrix : public TMatrix {
public:
    // Конструктор без параметрів
    TOpMatrix() : TMatrix() {}

    // Конструктор з параметрами
    TOpMatrix(int r, int c) : TMatrix(r, c) {}

    // Конструктор копіювання
    TOpMatrix(const TMatrix &other) : TMatrix(other) {}

    // Перевантаження оператора +
    TOpMatrix operator+(const TOpMatrix &other) const {
        if (rows != other.rows || cols != other.cols) {
            throw std::invalid_argument("Розміри матриці не збігаються для додавання.");
        }
        TOpMatrix result(rows, cols);
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                result.data[i][j] = this->data[i][j] + other.data[i][j];
            }
        }
        return result;
    }

    // Перевантаження оператора -
    TOpMatrix operator-(const TOpMatrix &other) const {
        if (rows != other.rows || cols != other.cols) {
            throw std::invalid_argument("Розміри матриці не збігаються для віднімання.");
        }
        TOpMatrix result(rows, cols);
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < cols; ++j) {
                result.data[i][j] = this->data[i][j] - other.data[i][j];
            }
        }
        return result;
    }

    // Перевантаження оператора *
    TOpMatrix operator*(const TOpMatrix &other) const {
        if (cols != other.rows) {
            throw std::invalid_argument("Розміри матриці не збігаються для множення.");
        }
        TOpMatrix result(rows, other.cols);
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < other.cols; ++j) {
                result.data[i][j] = 0;
                for (int k = 0; k < cols; ++k) {
                    result.data[i][j] += this->data[i][k] * other.data[k][j];
                }
            }
        }
        return result;
    }
};

int main() {
    int rows1, cols1, rows2, cols2;

    // Введення розмірів першої матриці
    std::cout << "Введіть кількість рядків і стовпців для матриці 1: ";
    std::cin >> rows1 >> cols1;
    TOpMatrix mat1(rows1, cols1);
    std::cout << "Введення матриці 1:" << std::endl;
    mat1.input();
    std::cout << "Матриця:" << std::endl;
    mat1.output();

    // Введення розмірів другої матриці
    std::cout << "Введіть кількість рядків і стовпців для матриці 2: ";
    std::cin >> rows2 >> cols2;
    TOpMatrix mat2(rows2, cols2);
    std::cout << "Введення матриці 2:" << std::endl;
    mat2.input();
    std::cout << "Матриця:" << std::endl;
    mat2.output();

    // Додавання матриць
    try {
        TOpMatrix matSum = mat1 + mat2;
        std::cout << "Сума матриць:" << std::endl;
        matSum.output();
    } catch (const std::invalid_argument &e) {
        std::cerr << "Помилка додавання: " << e.what() << std::endl;
    }

    // Віднімання матриць
    try {
        TOpMatrix matDiff = mat1 - mat2;
        std::cout << "Різниця матриць:" << std::endl;
        matDiff.output();
    } catch (const std::invalid_argument &e) {
        std::cerr << "Помилка додавання: " << e.what() << std::endl;
    }

    // Множення матриць
    try {
        TOpMatrix matProd = mat1 * mat2;
        std::cout << "Добуток матриць:" << std::endl;
        matProd.output();
    } catch (const std::invalid_argument &e) {
        std::cerr << "Помилка множення: " << e.what() << std::endl;
    }

    // Знаходження найбільшого та найменшого елементів
    std::cout << "Максимальний елемент у матриці1: " << mat1.findMaxElement() << std::endl;
    std::cout << "Мінімальний елемент у матриці 1: " << mat1.findMinElement() << std::endl;

    // Знаходження суми елементів
    std::cout << "Сума елементів у матриці 1: " << mat1.sumElements() << std::endl;

    return 0;
}
