using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Justgrow.Engine.Containers.Hexgrid
{
    public class SparseGrid<T> where T : Cell, new()
    {
        public const float CellHeight = 0.866025404f;
        Dictionary<int, Dictionary<int, T>> cells;

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

        public T GetNeighbour(int i, int j, Direction direction)
        {
            switch (direction)
            {
                case Direction.Me: return this[i, j];
                case Direction.Bottom: return this[i, j - 2];
                case Direction.BottomLeft: return this[i - 1, j - 1];
                case Direction.BottomRight: return this[i + 1, j - 1];
                case Direction.Top: return this[i, j + 1];
                case Direction.TopLeft: return this[i - 1, j + 1];
                case Direction.TopRight: return this[i + 1, j + 1];
                case Direction.FarLeft: return this[i - 2, j];
                case Direction.FarRight: return this[i + 2, j];
            }
            return null;
        }

        public Vector2 GetPosition(int i, int j, Vector2 offset, float size = 1f)
        {
            float x = 0.75f * i;
            float y = 0.5f * CellHeight * j;
            return new Vector2(x * size + offset.X, y * size + offset.Y);
        }

        protected T CreateCell (int i, int j)
        {
		    Vector2 position = new Vector2(i, j);
		    T cell = new T();
            cell.Position = position;
            return cell;
	    }
	}
}