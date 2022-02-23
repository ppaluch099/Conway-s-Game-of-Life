namespace Conway.Models
{
    public class Population
    {
        public int[,] grid { get; set; }
        public Population(int size)
        {
            grid = new int[size, size];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = 0;
                }
            }
        }

        bool IndexExists(int x, int y)
        {
            return x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1);
        }

        public bool CellLife(int x, int y) // false = dead, true = alive
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (IndexExists(x + i, y + j) && (!(i == 0 && j == 0)))
                    {
                        if (grid[x + i, y + j] == 1)
                        {
                            count++;
                        }
                    }
                }
            }

            if ((count >= 2 && count < 4) && grid[x, y] == 1)
            {
                return true;
            }
            else if (count == 3 && grid[x, y] == 0)
            {
                return true;
            }
            return false;
        }
    }
}
