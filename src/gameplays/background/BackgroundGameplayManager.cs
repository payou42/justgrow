using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Justgrow.Engine.Services.Core;
using Justgrow.Engine.Services.Resources;
using Justgrow.Gameplays.Core;

namespace Justgrow.Gameplays.Background
{
    public class BackgroundGameplayManager : Gameplay
    {
        protected Texture2D forest;
        protected Texture2D ground;
        protected Texture2D grass;
        protected Texture2D water;
        protected BackgroundMode backgroundMode;

        protected int groundLevel;

        public BackgroundMode BackgroundMode
        {
            get
            {
                return backgroundMode;
            }
            set
            {
                backgroundMode = value;
            }
        }

        public BackgroundGameplayManager(MainGame g) : base(g)
        {
            BackgroundMode = BackgroundMode.Aerial;
            groundLevel = TargetGroundLevel;        
        }

        public override GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Background;
            }
        }

        public override void Initialize()
        {
            // Load background
            ResourcesService resources = game.services.Get<ResourcesService>(ServicesDefinition.Resources);
            forest = resources.Load<Texture2D>(Constants.textureForest);
            ground = resources.Load<Texture2D>(Constants.textureGround);
            grass = resources.Load<Texture2D>(Constants.textureGrass);
            water  = resources.Load<Texture2D>(Constants.textureWater);
        }

        public int TargetGroundLevel
        {
            get
            {
                switch (BackgroundMode)
                {
                    case BackgroundMode.Overview: return (int)(Constants.windowHeight * Constants.overviewRatio);
                    case BackgroundMode.Aerial: return Constants.windowHeight;
                    case BackgroundMode.Underground: return Constants.groundOffset;
                }
                return 0;
            }
        }

        public int GroundLevel
        {
            get
            {
                return groundLevel;
            }
        }

        public override void Update(GameTime gameTime)
        {
            int target = TargetGroundLevel;
            if (groundLevel != target)
            {
                groundLevel += (int)Math.Ceiling((target - groundLevel) * gameTime.ElapsedGameTime.TotalSeconds * Constants.groundAnimationDuration);
            }
        }

        protected void DrawGround(SpriteBatch spriteBatch)
        {
            int y = groundLevel;
            int x = 0;
            while (y < Constants.windowHeight)
            {
                x = 0;
                while (x < (int)(Constants.windowHeight * Constants.windowRatio))
                {
                    spriteBatch.Draw(ground, new Vector2(x, y), Color.White);
                    x += ground.Width;
                }
                y += ground.Height;
            }
        }

        protected void DrawGrass(SpriteBatch spriteBatch)
        {
            int y = groundLevel - grass.Height;
            int x = 0;
            while (x < (int)(Constants.windowHeight * Constants.windowRatio))
            {
                spriteBatch.Draw(grass, new Vector2(x, y), Color.White);
                x += grass.Width;
            }
        }

        protected void DrawBackground(SpriteBatch spriteBatch)
        {
            // todo make sure all the background is covered
            spriteBatch.Draw(forest, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, (float)Constants.windowHeight / forest.Height, SpriteEffects.None, 0f);
        }

        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the background
            DrawBackground(spriteBatch);

            // Draw the ground
            DrawGround(spriteBatch);

            // Draw the grass
            DrawGrass(spriteBatch);
        }
    }
}