using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Justgrow.Engine.Utilities.LSystem;
using Justgrow.Engine.Services.Core;
using Justgrow.Engine.Services.Resources;
using Justgrow.Gameplays.Core;
using Justgrow.Gameplays.Background;
using Justgrow.Gameplays.Wind;

namespace Justgrow.Gameplays.Tree
{
    public class TreeGameplayManager : Gameplay
    {
        protected Texture2D leaf;
        protected Texture2D branch;
        protected Texture2D root;
        protected Texture2D cell;

        protected Generator treeGenerator;
        protected Generator rootsGenerator;
        protected UndergroundGrid undergroundGrid;

        Vector2 center;

        float rotation;
        SpriteBatch spriteBatch;

        protected int age = 0;

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                if ((value >= 0) && (value <= 5) && (age != value)) {
                    age = value;
                    Generate();
                }
            }
        }

        public override GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Tree;
            }
        }

        public TreeGameplayManager(MainGame g) : base(g)
        {
            // Create the cells
            undergroundGrid = new UndergroundGrid();

            // Create the tree generator
            treeGenerator = new Generator();

            // Create the roots generator
            rootsGenerator = new Generator();
        }

        protected void Generate()
        {
            // Set parameters
            treeGenerator.InitialState.angleGrowth = 0.0f;
            treeGenerator.InitialState.sizeGrowth = 0.0f;
            treeGenerator.InitialState.windIntensity = 0.0f;
            treeGenerator.InitialState.angle = (float)Math.PI * 22.5f / 180f;
            treeGenerator.InitialState.diameter = 0;
            treeGenerator.InitialState.index = 0;
            treeGenerator.InitialState.heading = new Vector3(0f, -1f, 0f);
            treeGenerator.InitialState.left = new Vector3(1f, 0f, 0f);
            treeGenerator.InitialState.up = new Vector3(0f, 0f, 1f);            
            treeGenerator.InitialState.size = 20f;
            treeGenerator.Input = @"F";
            treeGenerator.AddRule("F", 1f, "F F - [ - F + F + F ] + [ + F - F - F ]");
            treeGenerator.Generate(age);

            rootsGenerator.InitialState.angleGrowth = 0.0f;
            rootsGenerator.InitialState.sizeGrowth = 0.0f;
            rootsGenerator.InitialState.windIntensity = 0.0f;
            rootsGenerator.InitialState.angle = (float)Math.PI * 37f / 180f;
            rootsGenerator.InitialState.diameter = 0;
            rootsGenerator.InitialState.index = 0;
            rootsGenerator.InitialState.heading = new Vector3(0f, 1f, 0f);
            rootsGenerator.InitialState.left = new Vector3(-1f, 0f, 0f);
            rootsGenerator.InitialState.up = new Vector3(0f, 0f, 1f);
            rootsGenerator.InitialState.size = 10f;
            rootsGenerator.Input = @"F";
            rootsGenerator.AddRule("F", 1f, "F F - [ - F + F + F ] + [ + F - F - F ]");
            rootsGenerator.Generate(Math.Min(3, age));

            /*generator.Input = @"A";
            generator.AddRule("A", 1f, @"[ & F L ! A ] / / / / / ' [ & F L ! A ] / / / / / / / ' [ & F L ! A ]");
            generator.AddRule("F", 1f, @"S / / / / / F");
            generator.AddRule("S", 1f, @"F L");
            generator.AddRule("L", 1f, @"[ ' ' ' ^ ^ { - f + f + f - | - f + f + f } ]"); 
            generator.Generate(4); */
        }

        public override void Initialize()
        {
            // Bind the rendering callbacks
            treeGenerator.onExecute += new Generator.OnExecuteDelegate(this.OnDrawBranch);
            rootsGenerator.onExecute += new Generator.OnExecuteDelegate(this.OnDrawRoot);

            // First generation
            Generate();

            // Load texture
            ResourcesService resources = game.services.Get<ResourcesService>(ServicesDefinition.Resources);
            leaf = resources.Load<Texture2D>(Constants.textureLeaf);
            branch = resources.Load<Texture2D>(Constants.textureBranch);
            root = resources.Load<Texture2D>(Constants.textureRoot);
            cell = resources.Load<Texture2D>(Constants.textureCell);

            // Store  center
            center = new Vector2(branch.Width / 2f, branch.Height);
        }

        public override void Update(GameTime gameTime)
        {
        }

        protected void DrawGrid()
        {
            // Get the background manager in order to know the ground level
            BackgroundGameplayManager gpBackground = game.gameplays.Get<BackgroundGameplayManager>(GameplaysDefinition.Background);

            // Fill as many cells as possible
            Vector2 center = new Vector2(0.5f * (Constants.windowHeight * Constants.windowRatio) - 0.5f * cell.Width, (float)gpBackground.GroundLevel);
            int i = 0;
            int j = 0;
            bool yFinished = false;

            while (!yFinished)
            {
                while (true)
                {
                    Vector2 position = undergroundGrid.GetPosition(-i, j, center, cell.Width);
                    yFinished = (position.Y > Constants.windowHeight);
                    if (position.X <= 0f)
                    {
                        break;
                    }
                    spriteBatch.Draw(cell, position, Color.White);
                    if (i != 0)
                    {
                        Vector2 opposite = undergroundGrid.GetPosition(i, j, center, cell.Width);
                        spriteBatch.Draw(cell, opposite, Color.White);
                    }

                    i += 2;
                }
                j += 1;
                i = (j % 2);
            }
        }
        
        protected void DrawTree()
        {
            // Get the background manager in order to know the ground level
            BackgroundGameplayManager gpBackground = game.gameplays.Get<BackgroundGameplayManager>(GameplaysDefinition.Background);
            treeGenerator.InitialState.position = new Vector3(0.5f * (Constants.windowHeight * Constants.windowRatio), (float)gpBackground.GroundLevel, 0f);
            rootsGenerator.InitialState.position = new Vector3(0.5f * (Constants.windowHeight * Constants.windowRatio), (float)gpBackground.GroundLevel, 0f);

            // Render top of the tree
            WindGameplayManager gpWind = game.gameplays.Get<WindGameplayManager>(GameplaysDefinition.Wind);
            treeGenerator.InitialState.windIntensity = 0.003f * gpWind.Intensity;
            treeGenerator.Execute();

            // Render the bottom of the tree
            rootsGenerator.Execute();
        }

        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Store the sprite batch for the render callback
            this.spriteBatch = spriteBatch;

            // Draw the tree
            DrawTree();

            // Draw the root cells
            DrawGrid();
        }

        protected void OnDrawBranch(bool isBranch, State current, Vector3 from, Vector3 to)
        {
            Texture2D tex = isBranch ? branch : leaf;

            // Draw a branch or a leaf
            // Projection of the 3D-coordinates on 2D, using orthogonal projection (no perspective)
            Vector2 center = new Vector2(tex.Width / 2f, tex.Height);                
            float length = (float)Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y));
            if (length > 0f)
            {                
                Vector2 origin = new Vector2(from.X, from.Y);
                float direction = (float)Math.Acos((to.X - from.X) / length);
                float scale = length / tex.Height * (isBranch ? 1f : 2f);
                float rotation = (float)(Math.PI / 2) - direction;
                spriteBatch.Draw(tex, origin, null, Color.White, rotation, center, scale, SpriteEffects.None, 0f);
            }
        }

        protected void OnDrawRoot(bool isBranch, State current, Vector3 from, Vector3 to)
        {
            // Draw a root
            // Projection of the 3D-coordinates on 2D, using orthogonal projection (no perspective)
            Vector2 center = new Vector2(root.Width / 2f, root.Height);                
            float length = (float)Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y));
            if (length > 0f)
            {                
                Vector2 origin = new Vector2(from.X, from.Y);
                float direction = (float)Math.Acos((to.X - from.X) / length);
                float scale = length / root.Height;
                float rotation = (float)(Math.PI / 2) + direction;
                spriteBatch.Draw(root, origin, null, Color.White, rotation, center, scale, SpriteEffects.None, 0f);
            }
        }        
    }
}