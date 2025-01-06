# Battleship Game
A simple console-based implementation of the classic Battleship game.

## Description
This is a single-player game where the player attempts to locate and sink a hidden ship on a 5x5 grid. The player inputs guesses in the format `row,column`, and the program provides feedback on whether the guess was a hit or a miss.

The game continues until the ship is sunk.

## Features
- **Grid Initialization**: The grid is initialized with water (`~`).
- **Ship Placement**: The ship is randomly placed on the grid.
- **Player Interaction**: The player guesses grid locations using the `row,column` format.
- **Feedback**: The grid updates to show hits (`X`) and misses (`O`).
- **Debug Mode**: Option to reveal the ship's location for debugging purposes (disabled by default).
- **Validation**: Ensures valid input and prevents duplicate guesses.

## How to Play
1. **Run the Program**: Execute the game in a C# runtime environment.
2. **Guess the Ship's Location**: Enter guesses in the format `row,column` (e.g., `1,3`).
3. **Get Feedback**:
   - `X` marks a hit.
   - `O` marks a miss.
4. **Sink the Ship**: Continue guessing until you hit the ship.
5. **End Game**: The program ends when the ship is sunk, displaying the total number of attempts.

## Development

### Prerequisites
- .NET SDK installed
- A C# IDE or text editor (e.g., Visual Studio, Visual Studio Code)
