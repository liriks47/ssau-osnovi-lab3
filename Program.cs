using System;
using System.Diagnostics;

namespace lab3;

public class Program()
{
    const int ARR_MIN = -1000;
    const int ARR_MAX = 1000;
    const int MAX_DISPLAY_LEN = 10;
    const int ITERATE_TIMES = 10;
    static void Main()
    {
        bool isRunning = true;
        int menuItem = 0;
        string[] menuItems = ["Отгадай ответ", "Об авторе", "Сортировка массива", "Выход"];
        Action[] menuItemsMethods = [Game, About, ArraySort];

        int exitId = menuItemsMethods.Length;

        Console.Clear();

        while (isRunning)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("=== Main Menu ===");
            Console.ResetColor();

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (menuItem == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" {i + 1}. {menuItems[i]}");
                    Console.ResetColor();
                }
                else Console.WriteLine($"{i + 1}. {menuItems[i]}");

            }
            Console.ResetColor();

            ConsoleKeyInfo keyPressed = Console.ReadKey();

            switch (keyPressed.Key.ToString())
            {
                case "UpArrow":
                    {
                        menuItem--;
                        if (menuItem < 0) menuItem = exitId;

                        break;
                    }
                case "DownArrow":
                    {
                        menuItem++;
                        if (menuItem > exitId) menuItem = 0;

                        break;
                    }
                case "Enter":
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"=== {menuItems[menuItem]} ===");
                        Console.ResetColor();

                        if (menuItem == exitId) isRunning = Exit();
                        else menuItemsMethods[menuItem]();

                        break;
                    }

                default:
                    {
                        // Console.WriteLine(keyPressed.KeyChar);

                        break;
                    }
            }

            Console.Clear();
        }
    }

    static void Game()
    {
        Console.WriteLine("Функция: f = (cos^7(pi) + sqrt(ln(b^4))) / (sin(pi/2 + a))^2");
        Console.Write('\n');

        double a = ReadInputNumber("Введите число a (градусы): ", false) * (Math.PI / 180);
        double b = ReadInputNumber("Введите число b: ", false);

        double result = Func1Lab(a, b);

        Guess(result, 3);

        Console.Write('\n');
        Console.Write("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void About()
    {
        Console.WriteLine("Пахомов Максим Евгеньевич, 6101-090301D");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(@"
           *     ,MMM8&&&.            *
                MMMM88&&&&&    .MMMM88&&&&&&&
   *           MMM88&&&&&&&&
               MMM88&&&&&&&&
               'MMM88&&&&&&'
                 'MMM8&&&'      *
        |\___/|
        )     (             .              '
       =\     /=
         )===(       *
        /     \
        |     |
       /       \
       \       /
_/\_/\_/\__  _/_/\_/\_/\_/\_/\_/\_/\_/\_/\_
|  |  |  |( (  |  |  |  |  |  |  |  |  |  |
|  |  |  | ) ) |  |  |  |  |  |  |  |  |  |
|  |  |  |(_(  |  |  |  |  |  |  |  |  |  |
|  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|  |  |  |  |  |  |  |  |  |  |  |  |  |  |");
        Console.ResetColor();

        Console.Write('\n');
        Console.Write("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static void ArraySort()
    {
        // 3 вариант, пузырьковая и выбором

        int arrayLength = (int)ReadInputNumber("Введите длину массива: ", true);

        int[] arrayUnsorted = RandomArray(arrayLength);
        int[] arrayBubble = BubbleSort(ArrayClone(arrayUnsorted));
        int[] arraySelection = SelectionSort(ArrayClone(arrayUnsorted));

        long bubbleTime = GetAvgTime(BubbleSort, ArrayClone(arrayUnsorted));
        long selectionTime = GetAvgTime(SelectionSort, ArrayClone(arrayUnsorted));

        Console.Write("\n");
        WriteArray("Изначальный массив: ", arrayUnsorted);
        Console.Write("\n");

        WriteArray("Пузырьковая сортировка: ", arrayBubble);
        Console.WriteLine($"Затраченное время: {bubbleTime}ms");
        Console.Write("\n");

        WriteArray("Сортировка выбором: ", arraySelection);
        Console.WriteLine($"Затраченное время: {selectionTime}ms");
        Console.Write("\n");

        Console.Write('\n');
        Console.Write("Нажмите любую клавишу...");
        Console.ReadKey();
    }

    static bool Exit()
    {

        Console.Write("Завершить работу? (д/н): ");
        while (true)
        {
            ConsoleKeyInfo symbol = Console.ReadKey();
            if (symbol.KeyChar == 'д')
            {
                return false;
            }
            else if (symbol.KeyChar == 'н')
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nОшибка! Введено не д/н. Повторите попытку: ");
                Console.ResetColor();
            }
        }
        return true;
    }

    // служебные методы
    static double ReadInputNumber(string s, bool isNat)
    {
        double input;
        Console.Write(s);
        if (isNat)
            while (!double.TryParse(Console.ReadLine(), out input) && input > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ошибка ввода! Введите число: ");
                Console.ResetColor();
            }
        else
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ошибка ввода! Введите число: ");
                Console.ResetColor();
            }
        return input;
    }

    public static double Func1Lab(double a, double b)
    {
        try
        {
            if (Math.Abs(b) < 1)
            {
                Console.WriteLine("b^4 должно быть больше 1");
                return double.NaN;
            }
            const double PI = Math.PI;
            double f, s, t; // Промежуточные значения

            f = Math.Pow(Math.Cos(PI), 7);
            s = Math.Sqrt(Math.Log(Math.Pow(b, 4)));
            t = Math.Sin(Math.Pow((PI / 2) + a, 2));

            return Math.Round((f + s) / t, 2);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Деление на 0, т.к. (sin(pi/2 + a))^2 = 0");
            return double.NaN;
        }
    }

    public static void Guess(double result, int tryCount)
    {
        double userGuess;
        while (tryCount >= 1)
        {
            Console.Write('\n');
            Console.WriteLine($"Осталось попыток: {tryCount}");
            userGuess = ReadInputNumber("Введите предполагаемый результат функции (два знака после запятой): ", false);
            if (userGuess != result)
            {
                tryCount--;
                if (tryCount < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('\n');
                    Console.WriteLine($"Неверно, правильный ответ: {result}");
                    Console.ResetColor();

                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверно! Попробуйте снова.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы угадали!");
                Console.ResetColor();
                break;
            }
        }
    }

    static int[] RandomArray(int n)
    {
        Random rnd = new();

        int[] a = new int[n];

        for (int i = 0; i < n; i++)
            a[i] = rnd.Next(ARR_MIN, ARR_MAX + 1);
        return a;
    }

    static void WriteArray(string text, int[] a)
    {
        if (a.Length > MAX_DISPLAY_LEN)
        {
            Console.Write(text);

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Массив слишком длинный! (>{MAX_DISPLAY_LEN})");
            Console.ResetColor();

            return;
        }

        Console.Write(text);

        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.Write("[ ");
        if (a.Length > 0)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i]);
                if (i != a.Length - 1) Console.Write(", ");
            }
        }
        Console.Write(" ]\n");

        Console.ResetColor();
    }

    static long GetAvgTime(Func<int[], int[]> func, int[] a)
    {
        for (int i = 0; i < ITERATE_TIMES; i++)
        {
            func(ArrayClone(a));
        }

        long avgTime = 0;
        for (int i = 0; i < ITERATE_TIMES; i++)
        {
            Stopwatch timer = Stopwatch.StartNew();
            func(ArrayClone(a));
            timer.Stop();
            avgTime += timer.ElapsedMilliseconds;
        }

        return avgTime / ITERATE_TIMES;
    }

    static int[] BubbleSort(int[] a)
    {
        int len = a.Length;
        bool swapped;

        for (int i = 0; i < len; i++)
        {
            swapped = false;

            for (int j = 0; j < len - i - 1; j++)
            {
                if (a[j] > a[j + 1])
                {
                    int temp = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = temp;

                    swapped = true;
                }
            }

            if (!swapped) break;
        }
        return a;
    }

    static int[] SelectionSort(int[] a)
    {

        int len = a.Length;
        int smallest;
        for (int i = 0; i < len; i++)
        {
            smallest = i;
            for (int j = i + 1; j < len; j++)
            {
                if (a[j] < a[smallest])
                {
                    smallest = j;
                }
            }

            int temp = a[smallest];
            a[smallest] = a[i];
            a[i] = temp;
        }

        return a;
    }

    public static int[] ArrayClone(int[] a)
    {
        int[] c = new int[a.Length];
        for (int i = 0; i < a.Length; i++)
            c[i] = a[i];
        return c;
    }
}
