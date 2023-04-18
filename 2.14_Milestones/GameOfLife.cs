using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeV3
{
    public class GameOfLife
    {
        private Cell[,] _cells;
        private int _width;
        private int _height;

        public GameOfLife(int width, int height)
        {
            _width = width;
            _height = height;
            _cells = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _cells[x, y] = new Cell(x, y, false);
                }
            }
        }

        public Cell GetCell(int x, int y)
        {
            return _cells[x, y];
        }

        public void ToggleCell(int x, int y)
        {
            _cells[x, y].IsAlive = !_cells[x, y].IsAlive;
        }

        public void Clear()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _cells[x, y].IsAlive = false;
                }
            }
        }

        public void NextGeneration()
        {
            var newCells = new Cell[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    int livingNeighbors = GetLivingNeighbors(x, y);
                    bool isAlive = _cells[x, y].IsAlive;

                    if (isAlive && (livingNeighbors < 2 || livingNeighbors > 3))
                    {
                        isAlive = false;
                    }
                    else if (!isAlive && livingNeighbors == 3)
                    {
                        isAlive = true;
                    }

                    newCells[x, y] = new Cell(x, y, isAlive);
                }
            }

            _cells = newCells;
        }

        private int GetLivingNeighbors(int x, int y)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int newX = x + i;
                    int newY = y + j;

                    if (newX >= 0 && newX < _width && newY >= 0 && newY < _height)
                    {
                        if (_cells[newX, newY].IsAlive)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}
