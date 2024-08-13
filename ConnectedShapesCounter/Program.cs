using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string fileName = @"TextFiles\data_small.txt"; //data_large.txt

        // read the grid and create a jagged array where # of rows = number of lines in the file
        char[][] grid = CreateGridFromFile(fileName);
        int result = CountConnectedShapes(grid);
        Console.WriteLine($"Number of connected shapes in {fileName}: {result}");
    }

    static char[][] CreateGridFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        char[][] grid = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            grid[i] = lines[i].ToCharArray();
        }

        return grid;
    }

    static int CountConnectedShapes(char[][] grid)
    {
        // number of rows in the grid
        int rowCount = grid.Length;

        // number of columns in the first row of the grid
        int columnCount = grid[0].Length;

        // track visited cells -> init same size as grid
        bool[][] visited = new bool[rowCount][];

        for (int i = 0; i < rowCount; i++)
        {
            visited[i] = new bool[columnCount];
        }

        int shapeCount = 0;

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                if (grid[row][column] == '1' && !visited[row][column])
                {
                    // perform a Depth-First-Search for a new shape
                    DepthFirstSearch(grid, visited, row, column);
                    shapeCount++;
                }
            }
        }
        return shapeCount;
    }

    static void DepthFirstSearch(char[][] grid, bool[][] visited, int row, int column)
    {
        // possible directions -> (-1,0) = up, (1,0) = down, etc
        int[] directionRow = { -1, 1, 0, 0 };
        int[] directionColumn = { 0, 0, -1, 1 };

        // mark current cell as visited
        visited[row][column] = true;

        // check adjacent cells
        for (int i = 0; i < 4; i++)
        {
            int newRow = row + directionRow[i];
            int newColumn = column + directionColumn[i];
            if (IsAdjacent(grid, visited, newRow, newColumn))
            {
                // call recursively to check for more adjacent 1s
                DepthFirstSearch(grid, visited, newRow, newColumn);
            }
        }
    }

    static bool IsAdjacent(char[][] grid, bool[][] visited, int row, int column)
    {
        return row >= 0 && row < grid.Length &&
            column >= 0 && column < grid[0].Length &&
            grid[row][column] == '1' &&
            !visited[row][column];
    }
}