using System;

class Battleship
{
    static void Main(string[] args)
    {
        const int gridSize = 10; // Grid size (can adjust if needed)
        char[,] grid = new char[gridSize, gridSize];
        InitializeGrid(grid);

        // Ship lengths
        int[] shipLengths = { 4, 3, 2, 1 };

        // Place all ships
        foreach (var shipLength in shipLengths)
        {
            PlaceShip(grid, gridSize, shipLength);
        }

        // Debug Mode
        bool debugMode = false;
        if (debugMode)
        {
            Console.WriteLine("Debug: Ships placed on the grid.");
            DisplayGrid(grid);
        }

        int attempts = 0;
        int remainingShips = shipLengths.Length;

        Console.WriteLine("Welcome to Battleship!");
        Console.WriteLine("Legend: ~ = water, O = miss, X = hit");
        Console.WriteLine($"Try to sink all {remainingShips} ships hidden on a {gridSize}x{gridSize} grid.");
        Console.WriteLine("Enter your guesses in the format 'row,col' (e.g., 1,2).");

        while (remainingShips > 0)
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

            if (grid[row, col] == 'S') // Ship hit
            {
                grid[row, col] = 'X'; // Mark hit
                Console.WriteLine("Hit!");

                // Check if the ship is fully sunk
                if (IsShipSunk(grid, gridSize, row, col))
                {
                    remainingShips--;
                    Console.WriteLine($"You sunk a ship! {remainingShips} ship(s) remaining.");
                }
            }
            else
            {
                grid[row, col] = 'O'; // Mark miss
                Console.WriteLine("Miss! Try again.");
            }
        }

        Console.WriteLine($"Congratulations! You sunk all the ships in {attempts} attempts!");
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
            // Only display water or unknown spaces ('~') during gameplay
            if (grid[i, j] == 'S') // Hide the ships
                Console.Write("~ ");
            else
                Console.Write(grid[i, j] + " "); // Show misses ('O') and hits ('X')
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

    static void PlaceShip(char[,] grid, int gridSize, int shipLength)
    {
        Random random = new Random();
        bool placed = false;

        while (!placed)
        {
            int shipRow = random.Next(gridSize);
            int shipCol = random.Next(gridSize);
            bool horizontal = random.Next(2) == 0; // Randomize orientation

            if (CanPlaceShip(grid, gridSize, shipRow, shipCol, shipLength, horizontal))
            {
                // Place the ship
                for (int i = 0; i < shipLength; i++)
                {
                    if (horizontal)
                    {
                        grid[shipRow, shipCol + i] = 'S';
                    }
                    else
                    {
                        grid[shipRow + i, shipCol] = 'S';
                    }
                }
                placed = true;
            }
        }
    }

    static bool CanPlaceShip(char[,] grid, int gridSize, int shipRow, int shipCol, int shipLength, bool horizontal)
    {
        for (int i = 0; i < shipLength; i++)
        {
            int checkRow = horizontal ? shipRow : shipRow + i;
            int checkCol = horizontal ? shipCol + i : shipCol;

            if (checkRow >= gridSize || checkCol >= gridSize || grid[checkRow, checkCol] != '~')
            {
                return false; // Out of bounds or overlapping
            }
        }
        return true;
    }

    static bool IsShipSunk(char[,] grid, int gridSize, int row, int col)
    {
        // Check if any part of the ship remains
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (grid[i, j] == 'S') return false;
            }
        }
        return true; // No 'S' found, ship is sunk
    }
}
