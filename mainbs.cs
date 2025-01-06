using System;

class Battleship
{
    static void Main(string[] args)
    {
        const int gridSize = 5;
        char[,] grid = new char[gridSize, gridSize];
        InitializeGrid(grid);

        // Randomly place the ship
        Random random = new Random();
        int shipRow = random.Next(gridSize);
        int shipCol = random.Next(gridSize);

        // Debug Mode
        bool debugMode = false;
        if (debugMode)
        {
            Console.WriteLine($"Debug: Ship is at ({shipRow + 1}, {shipCol + 1})");
        }

        int attempts = 0;
        bool isSunk = false;

        Console.WriteLine("Welcome to Battleship!");
        Console.WriteLine("Legend: ~ = water, O = miss, X = hit");
        Console.WriteLine($"Try to sink the ship hidden on a {gridSize}x{gridSize} grid.");
        Console.WriteLine("Enter your guesses in the format 'row,col' (e.g., 1,2).");

        while (!isSunk)
        {
            DisplayGrid(grid);
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            if (!ParseInput(input, gridSize, out int row, out int col))
            {
                Console.WriteLine("Invalid input. Please enter a valid guess in the format 'row,col'.");
                continue;
            }

            if (grid[row, col] == 'O' || grid[row, col] == 'X')
            {
                Console.WriteLine("You've already guessed this spot. Try a new one.");
                continue;
            }

            attempts++;

            if (row == shipRow && col == shipCol)
            {
                grid[row, col] = 'X'; // Mark hit
                isSunk = true;
                Console.WriteLine($"Hit! You sunk the ship in {attempts} attempts!");
            }
            else
            {
                grid[row, col] = 'O'; // Mark miss
                Console.WriteLine("Miss! Try again.");
            }
        }

        DisplayGrid(grid);
    }

    static void InitializeGrid(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = '~'; // Water
            }
        }
    }

    static void DisplayGrid(char[,] grid)
    {
        Console.WriteLine("\nGrid:");
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static bool ParseInput(string input, int gridSize, out int row, out int col)
    {
        row = -1;
        col = -1;

        string[] parts = input.Split(',');
        if (parts.Length != 2) return false;

        if (int.TryParse(parts[0], out row) && int.TryParse(parts[1], out col))
        {
            row--; // Convert to zero-based index
            col--;
            return row >= 0 && row < gridSize && col >= 0 && col < gridSize;
        }

        return false;
    }
}
