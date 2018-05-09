using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Justgrow.Engine.Containers.Hexgrid;
using Justgrow.Engine.Utilities.LSystem;
using Justgrow.Engine.Services.Core;
using Justgrow.Engine.Services.Resources;
using Justgrow.Gameplays.Core;
using Justgrow.Gameplays.Background;
using Justgrow.Gameplays.Wind;

namespace Justgrow.Gameplays.Tree
{
    public class UndergroundGrid : SparseGrid<UndergroundCell>
    {
        public delegate void OnCellTraversedDelegate(UndergroundCell cell, Direction direction);

        public event OnCellTraversedDelegate OnCellTraversed;

        public UndergroundGrid() : base()
        {
            // Create some test data
            UndergroundCell root = this[0, 0];
            root.Size = 3;

            UndergroundCell leftMain = GetNeighbour(0, 0, Direction.BottomLeft);
            leftMain.Size = 2;
            GetNeighbour(leftMain, Direction.TopLeft).Size = 1;
            GetNeighbour(leftMain, Direction.BottomLeft).Size = 1;

            UndergroundCell rightMain = GetNeighbour(0, 0, Direction.BottomRight);
            rightMain.Size = 2;
            GetNeighbour(rightMain, Direction.TopRight).Size = 1;
            GetNeighbour(rightMain, Direction.BottomRight).Size = 1;
        }

        public void Traverse()
        {
            // First set all cells to not visited
            foreach (Dictionary<int, UndergroundCell> row in cells.Values)            
            {
                foreach(UndergroundCell cell in row.Values)
                {
                    cell.Visited = false;
                }
            }

            // Now start from (0,0)
            Queue<Tuple<UndergroundCell, Direction>> visited = new Queue<Tuple<UndergroundCell, Direction>>();
            if (IsCellCreated(0, 0))
            {
                this[0, 0].Visited = true;
                visited.Enqueue(new Tuple<UndergroundCell, Direction>(this[0, 0], Direction.Bottom));
            }
            while (visited.Count > 0)
            {
                ProcessStack(visited);
            }
        }

        protected void ProcessStack(Queue<Tuple<UndergroundCell, Direction>> visited)
        {
            // Unstack first item
            Tuple<UndergroundCell, Direction> head = visited.Dequeue();
            if (OnCellTraversed != null)    
            {
                OnCellTraversed(head.Item1, head.Item2);
            }

            // Stack children
            for (int n = 0; n < (int)Direction.Count; ++n)
            {
                Tuple<int, int> coordinates = GetNeighbourCoordinates(head.Item1, (Direction)n);
                if (IsCellCreated(coordinates.Item1, coordinates.Item2))
                {
                    UndergroundCell child = this[coordinates.Item1, coordinates.Item2];
                    if (!child.Visited)
                    {
                        child.Visited = true;
                        visited.Enqueue(new Tuple<UndergroundCell, Direction>(child, (Direction)n));
                    }
                }
                
            }
        }
    }
}