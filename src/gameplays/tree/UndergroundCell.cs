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
    public class UndergroundCell : Cell
    {
        public int Size { get; set; }

        public bool Visited { get; set; }

        public UndergroundCell() : base()
        {
            Size = 0;
            Visited = false;
        }
    }
}