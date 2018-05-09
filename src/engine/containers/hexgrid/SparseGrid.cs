using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Justgrow.Engine.Containers.Hexgrid
{
    public class SparseGrid<T> where T : Cell, new()
    {
        public const float CellHeight = 0.866025404f;
        protected Dictionary<int, Dictionary<int, T>> cells;

        public SparseGrid()
        {
	        cells = new Dictionary<int, Dictionary<int, T>>();
        }

        public T this[int i, int j]
        {
            get
            {
                if ((i+j) % 2 == 1)
                {
                    return null;
                }
                if (!cells.ContainsKey(i))
                {
                    cells[i] = new Dictionary<int, T>();
                }
                
                Dictionary<int, T> row = cells[i];
                if (!row.ContainsKey(j))
                {
                    row[j] = CreateCell(i, j);
                }
                return row[j];
            }
        }

        public bool IsCellCreated(int i, int j)
        {
            if (!cells.ContainsKey(i))
            {
                return false;
            }
                
            Dictionary<int, T> row = cells[i];
            if (!row.ContainsKey(j))
            {
                return false;
            }
            return true;
        }

        public Tuple<int, int> GetNeighbourCoordinates(int i, int j, Direction direction)
        {
            switch (direction)
            {
                case Direction.Me: return new Tuple<int, int>(i, j);
                case Direction.Bottom: return new Tuple<int, int>(i, j - 2);
                case Direction.BottomLeft: return new Tuple<int, int>(i - 1, j - 1);
                case Direction.BottomRight: return new Tuple<int, int>(i + 1, j - 1);
                case Direction.Top: return new Tuple<int, int>(i, j + 1);
                case Direction.TopLeft: return new Tuple<int, int>(i - 1, j + 1);
                case Direction.TopRight: return new Tuple<int, int>(i + 1, j + 1);
            }
            return null;
        }

        public Tuple<int, int> GetNeighbourCoordinates(Cell cell, Direction direction)
        {
            return this.GetNeighbourCoordinates(cell.X, cell.Y, direction);
        }

        public T GetNeighbour(int i, int j, Direction direction)
        {
            Tuple<int, int> coordinates = GetNeighbourCoordinates(i, j, direction);
            return this[coordinates.Item1, coordinates.Item2];
        }

        public T GetNeighbour(Cell cell, Direction direction)
        {
            return this.GetNeighbour(cell.X, cell.Y, direction);
        }

        public Vector2 GetPosition(int i, int j, Vector2 offset, float size = 1f)
        {
            float x = 0.75f * i;
            float y = 0.5f * CellHeight * j;
            return new Vector2(x * size + offset.X, y * size + offset.Y);
        }

        protected T CreateCell (int i, int j)
        {
		    T cell = new T();
            cell.X = i;
            cell.Y = j;
            return cell;
	    }
	}
}