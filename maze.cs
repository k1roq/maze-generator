char[,] maze = new char[21, 21];
int[] rowdirections = new int[4];
int[] coldirections = new int[4];
coldirections[0] = -2;
coldirections[1] = +2;
rowdirections[2] = -2;
rowdirections[3] = +2;
bool[,] visited = new bool[21, 21];
bool[,] pathVisited = new bool[21, 21];
for (int row = 0; row < maze.GetLength(0); row++)
{
    for (int col = 0; col < maze.GetLength(1); col++)
    {
        maze[row, col] = '#';
    }
}
Random random = new Random();
GenerateMaze(maze, visited, 1, 1, rowdirections, coldirections, random);
static void Shuffle(int[] rowdirections, int[] coldirections, Random random)
{
    int[] direction1 = rowdirections;
    int[] direction2 = coldirections;
    for(int i = direction1.Length - 1; i > 0; i--)
    {
        int j = random.Next(i + 1);
        int temp = direction1[i];
        int temp1 = direction2[i];
        direction1[i] = direction1[j];
        direction2[i] = direction2[j];
        direction1[j] = temp;
        direction2[j] = temp1;
    }
}
maze[1, 1] = 'S';
maze[19, 19] = 'E';
static void GenerateMaze(char[,] maze, bool[,] visited, int row, int col, int[] rowdirections, int[] coldirections, Random random)
{

    visited[row, col] = true;
    Shuffle(rowdirections, coldirections, random);
    for(int i = 0; i < 4; i++)
    {
        int wallRow = row + rowdirections[i] / 2;
        int wallCol = col + coldirections[i] / 2;
        int newRow = row + rowdirections[i];
        int newCol = col + coldirections[i];
        if (newRow >= maze.GetLength(0) || newRow < 0 || newCol >= maze.GetLength(1) || newCol < 0)
        {
            continue;
        }
        if (wallRow >= maze.GetLength(0) || wallRow < 0 || wallCol >= maze.GetLength(1) || wallCol < 0)
        {
            continue;
        }
        if(visited[newRow, newCol] == false)
        {
            maze[newRow, newCol] = ' ';
            maze[wallRow, wallCol] = ' ';
            GenerateMaze(maze, visited, newRow, newCol, rowdirections, coldirections, random);

        }
    }
}
static bool SolveMaze(char[,] maze, bool[,] pathVisited, int row, int col, int[] rowdirections, int[] coldirections)
{
    if (maze[row, col] == 'E')
    {
        return true;
    }
    if (pathVisited[row, col] == true || maze[row, col] == '#')
    {
        return false;
    }
    pathVisited[row, col] = true;
    for (int i = 0; i < 4; i++)
    {
        int wallRow = row + rowdirections[i] / 2;
        int wallCol = col + coldirections[i] / 2;
        int newRow = row + rowdirections[i];
        int newCol = col + coldirections[i];
        if (newRow >= maze.GetLength(0) || newRow < 0 || newCol >= maze.GetLength(1) || newCol < 0)
        {
            continue;
        }
        if (wallRow >= maze.GetLength(0) || wallRow < 0 || wallCol >= maze.GetLength(1) || wallCol < 0)
        {
            continue;
        }
        if (maze[wallRow, wallCol] == '#')
        {
        continue;
        }
        if (SolveMaze(maze, pathVisited, newRow, newCol, rowdirections, coldirections))
        {
            return true;
        }

    }
    pathVisited[row, col] = false;
    return false;
}
bool solved = SolveMaze(maze, pathVisited, 1, 1, rowdirections, coldirections);
if (solved == false)
    {
        Console.WriteLine("No solution found.");
        return;
    }
else
{
    for (int row = 0; row < maze.GetLength(0); row++)
    {
        for (int col = 0; col < maze.GetLength(1); col++)
        {
        if (maze[row, col] == 'S' || maze[row, col] == 'E')
        {
            Console.Write(maze[row, col]);
        }   
        else if (pathVisited[row, col])
        {
            Console.Write("-");
        }
        else
        {
            Console.Write(maze[row, col]);
        }
        }
        Console.WriteLine();
    }
}
