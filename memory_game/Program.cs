using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        const int gridSize = 4; 
        char[,] board = GenerateBoard(gridSize);
        char[,] visibleBoard = InitializeBoard(gridSize);
        bool[,] revealed = new bool[gridSize, gridSize];

        Console.CursorVisible = false;
        int moves = 0;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        while (true)
        {
            Console.Clear();
            DisplayBoard(visibleBoard);

            
            Console.WriteLine("\nВыберите первую карточку.");
            (int row1, int col1) = GetUserInput(gridSize, revealed);
            visibleBoard[row1, col1] = board[row1, col1];
            Console.Clear();
            DisplayBoard(visibleBoard);

            
            Console.WriteLine("\nВыберите вторую карточку.");
            (int row2, int col2) = GetUserInput(gridSize, revealed);
            visibleBoard[row2, col2] = board[row2, col2];
            Console.Clear();
            DisplayBoard(visibleBoard);

            moves++;
            if (board[row1, col1] == board[row2, col2])
            {
                Console.WriteLine("Совпадение!");
                revealed[row1, col1] = true;
                revealed[row2, col2] = true;
            }
            else
            {
                Console.WriteLine("Нет совпадения. Карточки переворачиваются.");
                System.Threading.Thread.Sleep(1000);
                visibleBoard[row1, col1] = '#';
                visibleBoard[row2, col2] = '#';
            }

            if (AllCardsRevealed(revealed))
            {
                stopwatch.Stop();
                Console.Clear();
                DisplayBoard(visibleBoard);
                Console.WriteLine($"\nПоздравляем! Вы нашли все пары за {moves} ходов.");
                Console.WriteLine($"Затраченное время: {stopwatch.Elapsed.TotalSeconds:F2} секунд.");
                break;
            }
        }
    }

    static char[,] GenerateBoard(int gridSize)
    {
        List<char> symbols = new List<char>();
        for (char c = 'A'; c < 'A' + gridSize * gridSize / 2; c++)
        {
            symbols.Add(c);
            symbols.Add(c);
        }

        Random rand = new Random();
        symbols = symbols.OrderBy(x => rand.Next()).ToList();

        char[,] board = new char[gridSize, gridSize];
        int index = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                board[i, j] = symbols[index++];
            }
        }

        return board;
    }

    static char[,] InitializeBoard(int gridSize)
    {
        char[,] board = new char[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                board[i, j] = '#';
            }
        }

        return board;
    }

    static void DisplayBoard(char[,] board)
    {
        int gridSize = board.GetLength(0);
        Console.WriteLine("   " + string.Join(" ", Enumerable.Range(0, gridSize).Select(x => x.ToString().PadLeft(2))));
        for (int i = 0; i < gridSize; i++)
        {
            Console.Write(i.ToString().PadLeft(2) + " ");
            for (int j = 0; j < gridSize; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static (int, int) GetUserInput(int gridSize, bool[,] revealed)
    {
        int row, col;
        while (true)
        {
            Console.Write("Введите координаты (строка столбец): ");
            string[] input = Console.ReadLine()?.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            
            if (input == null || input.Length != 2)
            {
                Console.WriteLine("Введите ровно два числа, разделенных пробелом.");
                continue;
            }

            
            if (!int.TryParse(input[0], out row) || !int.TryParse(input[1], out col))
            {
                Console.WriteLine("Координаты должны быть числами.");
                continue;
            }

            
            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
            {
                Console.WriteLine("Координаты вне диапазона. Введите значения от 0 до {0}.", gridSize - 1);
                continue;
            }

            
            if (revealed[row, col])
            {
                Console.WriteLine("Эта карточка уже открыта. Выберите другую.");
                continue;
            }

            
            return (row, col);
        }
    }

    static bool AllCardsRevealed(bool[,] revealed)
    {
        foreach (bool card in revealed)
        {
            if (!card) return false;
        }
        return true;
    }
}
