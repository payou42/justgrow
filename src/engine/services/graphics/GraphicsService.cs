using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Justgrow.Engine.Services.Core;

namespace Justgrow.Engine.Services.Graphics
{
    public class GraphicsService : Service
    {
        // Graphics devica
        protected GraphicsDeviceManager graphics;

        // Sprites management
        protected SpriteBatch spriteBatch;

        public bool IsFullScreen { get; set; }

        public int Height { get; set; }

        public int Width
        {
            get
            {
                return (int)(Height * Ratio);
            }
        }

        public override ServicesDefinition Definition
        {
            get
            {
                return ServicesDefinition.Graphics;
            }
        }

        public float Ratio { get; set; }

        public GraphicsService(MainGame g) : base(g)
        {
            IsFullScreen = false;
            Height = Constants.windowHeight;
            Ratio = Constants.windowRatio;
        }

        public override void Create()
        {
            // Create the device
            graphics = new GraphicsDeviceManager(game);
            graphics.IsFullScreen = IsFullScreen;
            graphics.PreferredBackBufferHeight = Height;
            graphics.PreferredBackBufferWidth = Width;
        }

        public override void Initialize()
        {
            // Initialize sprite batch
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return spriteBatch;
            }
        }

        public void BeginDraw()
        {
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
        }

        public void EndDraw()
        {
            spriteBatch.End();
        }        
    }
}