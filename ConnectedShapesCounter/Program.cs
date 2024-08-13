class Program
{
    static void Main(string[] args)
    {
        string fileName = "data_small.txt"; //data_large.txt
        char[][] grid = ReadGridFromFile(fileName);
    }

    static char[][] ReadGridFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        char[][] grid = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            grid[i] = lines[i].ToCharArray();
        }

        return grid;
    }
}