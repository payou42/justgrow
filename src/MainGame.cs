using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Justgrow.Engine.Services;
using Justgrow.Engine.Services.Core;
using Justgrow.Engine.Services.Graphics;
using Justgrow.Engine.Services.Resources;
using Justgrow.Gameplays;
using Justgrow.Gameplays.Core;
using Justgrow.Gameplays.Background;
using Justgrow.Gameplays.Controls;
using Justgrow.Gameplays.Menu;
using Justgrow.Gameplays.Music;
using Justgrow.Gameplays.Tree;
using Justgrow.Gameplays.Wind;

namespace Justgrow
{
    public class MainGame : Game
    {
        public Justgrow.Engine.Services.Services services;

        public Justgrow.Gameplays.Gameplays gameplays;
       
        public MainGame()
        {
            // Create and register services
            CreateServices();

            // Create and register gameplay components
            CreateGameplays();
        }

        protected void CreateServices()
        {
            // Instanciate the services management
            services = new Justgrow.Engine.Services.Services(this);

            // Register services
            services.Register(new GraphicsService(this));
            services.Register(new ResourcesService(this));

            // Create the services
            services.Create();
        }

        protected void CreateGameplays()
        {
            // Instanciate the gameplay components
            gameplays = new Justgrow.Gameplays.Gameplays(this);        

            // Register our managers
            gameplays.Register(new BackgroundGameplayManager(this));
            gameplays.Register(new ControlsGameplayManager(this));
            gameplays.Register(new MusicGameplayManager(this));
            gameplays.Register(new WindGameplayManager(this));
            gameplays.Register(new TreeGameplayManager(this));
            gameplays.Register(new MenuGameplayManager(this));
        }        

        protected override void Initialize()
        {
            // Set the window title
            Window.Title = "Just Grow";
            
            // SHow mouse
            IsMouseVisible = true;

            // Initilaize the services
            services.Initialize();

            // Monogame initialization
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Initialize Gameplays
            gameplays.Initialize();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Update Gameplays
            gameplays.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsService graphics = services.Get<GraphicsService>(ServicesDefinition.Graphics);
            graphics.BeginDraw();
            gameplays.DrawSprites(gameTime,  graphics.SpriteBatch);
            graphics.EndDraw();
            base.Draw(gameTime);
        }
    }
}