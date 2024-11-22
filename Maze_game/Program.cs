using System;
using System.Diagnostics;

class MazeGame
{
    static void Main()
    {
        const int rows = 21; 
        const int cols = 21;
        const int timeLimitSeconds = 60; 

        char[,] maze = GenerateMaze(rows, cols);
        bool[,] revealed = new bool[rows, cols];
        (int playerRow, int playerCol) = (1, 1); 

       
        maze[rows - 2, cols - 2] = 'E';

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        
        while (true)
        {
            Console.Clear();
            DisplayMaze(maze, revealed, playerRow, playerCol);

            
            int timeLeft = timeLimitSeconds - (int)stopwatch.Elapsed.TotalSeconds;
            if (timeLeft <= 0)
            {
                Console.Clear();
                Console.WriteLine("Время вышло! Вы не успели найти выход.");
                break;
            }

            Console.WriteLine($"\nОставшееся время: {timeLeft} секунд.");
            Console.WriteLine("Управление: W (вверх), S (вниз), A (влево), D (вправо), Escape (выход).");

            ConsoleKey key = Console.ReadKey(true).Key;

            
            int newRow = playerRow, newCol = playerCol;
            switch (key)
            {
                case ConsoleKey.W: 
                    newRow--;
                    break;
                case ConsoleKey.S: 
                    newRow++;
                    break;
                case ConsoleKey.A: 
                    newCol--;
                    break;
                case ConsoleKey.D: 
                    newCol++;
                    break;
                case ConsoleKey.Escape: 
                    Console.WriteLine("Игра завершена.");
                    return;
            }

            
            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && maze[newRow, newCol] != '#')
            {
                playerRow = newRow;
                playerCol = newCol;
            }

           
            revealed[playerRow, playerCol] = true;

            
            if (maze[playerRow, playerCol] == 'E')
            {
                Console.Clear();
                DisplayMaze(maze, revealed, playerRow, playerCol);
                Console.WriteLine("\nПоздравляем! Вы нашли выход из лабиринта!");
                Console.WriteLine($"Вы справились за {stopwatch.Elapsed.TotalSeconds:F1} секунд.");
                break;
            }
        }
    }

    static char[,] GenerateMaze(int rows, int cols)
    {
        char[,] maze = new char[rows, cols];
        Random random = new Random();

        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                maze[i, j] = '#';
            }
        }

        
        maze[1, 1] = ' ';
        var walls = new System.Collections.Generic.List<(int, int)>
        {
            (1, 2), (2, 1)
        };

        
        while (walls.Count > 0)
        {
            int index = random.Next(walls.Count);
            (int wallRow, int wallCol) = walls[index];
            walls.RemoveAt(index);

            if (IsValidPath(maze, wallRow, wallCol))
            {
                maze[wallRow, wallCol] = ' ';
                foreach (var (r, c) in GetNeighbors(wallRow, wallCol, rows, cols))
                {
                    if (maze[r, c] == '#') walls.Add((r, c));
                }
            }
        }

        return maze;
    }

    static bool IsValidPath(char[,] maze, int row, int col)
    {
        int paths = 0;

        if (maze[row - 1, col] == ' ') paths++;
        if (maze[row + 1, col] == ' ') paths++;
        if (maze[row, col - 1] == ' ') paths++;
        if (maze[row, col + 1] == ' ') paths++;

        return paths == 1; 
    }

    static System.Collections.Generic.List<(int, int)> GetNeighbors(int row, int col, int rows, int cols)
    {
        var neighbors = new System.Collections.Generic.List<(int, int)>
        {
            (row - 1, col),
            (row + 1, col),
            (row, col - 1),
            (row, col + 1)
        };

        neighbors.RemoveAll(n => n.Item1 <= 0 || n.Item1 >= rows - 1 || n.Item2 <= 0 || n.Item2 >= cols - 1);
        return neighbors;
    }

    static void DisplayMaze(char[,] maze, bool[,] revealed, int playerRow, int playerCol)
    {
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == playerRow && j == playerCol)
                {
                    Console.Write('P'); 
                }
                else if (revealed[i, j])
                {
                    Console.Write(maze[i, j]);
                }
                else
                {
                    Console.Write('░'); 
                }
            }
            Console.WriteLine();
        }
    }
}
