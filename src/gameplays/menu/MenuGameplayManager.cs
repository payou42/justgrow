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
using Justgrow.Gameplays.Controls;
using Justgrow.Gameplays.Background;

namespace Justgrow.Gameplays.Menu
{
    public class MenuGameplayManager : Gameplay
    {
        protected SpriteFont title;

        protected Texture2D overview;

        protected List<InteractiveObject> interactives;

        public MenuGameplayManager(MainGame g) : base(g)
        {
            interactives = new List<InteractiveObject>();
        }

        public override GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Menu;
            }
        }

        public override void Initialize()
        {
            // Load background
            ResourcesService resources = game.services.Get<ResourcesService>(ServicesDefinition.Resources);
            title = resources.Load<SpriteFont>(Constants.fontTitle);
            overview = resources.Load<Texture2D>(Constants.textureOverview);
            
            // Listen to mouse events
            ControlsGameplayManager gpControls = game.gameplays.Get<ControlsGameplayManager>(GameplaysDefinition.Controls);
            gpControls.OnMouseMove += new ControlsGameplayManager.OnMouseMoveDelegate(OnMouseMove);
            gpControls.OnMouseClick += new ControlsGameplayManager.OnMouseClickDelegate(OnMouseClick);

            // Register the overview actions
            BackgroundGameplayManager gpBackground = game.gameplays.Get<BackgroundGameplayManager>(GameplaysDefinition.Background);
            InteractiveObject viewTop = new InteractiveObject();
            viewTop.ActivationZone = new Rectangle(10, (int)(0.5f * (Constants.windowHeight - 0.4f * overview.Height)), (int)(overview.Width * 0.4f), (int)(overview.Height * 0.133f));
            viewTop.OnAction += () => { gpBackground.BackgroundMode = BackgroundMode.Aerial; };
            interactives.Add(viewTop);

            InteractiveObject viewOverview = new InteractiveObject();
            viewOverview.ActivationZone = new Rectangle(viewTop.ActivationZone.X, viewTop.ActivationZone.Y + viewTop.ActivationZone.Height, viewTop.ActivationZone.Width, viewTop.ActivationZone.Height);
            viewOverview.OnAction += () => { gpBackground.BackgroundMode = BackgroundMode.Overview; };
            interactives.Add(viewOverview);

            InteractiveObject viewBottom = new InteractiveObject();
            viewBottom.ActivationZone = new Rectangle(viewTop.ActivationZone.X, viewOverview.ActivationZone.Y + viewTop.ActivationZone.Height, viewTop.ActivationZone.Width, viewTop.ActivationZone.Height);
            viewBottom.OnAction += () => { gpBackground.BackgroundMode = BackgroundMode.Underground; };
            interactives.Add(viewBottom);
        }

        protected void OnMouseMove(int X, int Y)
        {
            foreach (InteractiveObject io in interactives)
            {
                io.Focused = io.ActivationZone.Contains(X, Y);
            }
        }

        protected void OnMouseClick(int X, int Y, MouseButtons buttons)
        {
            foreach (InteractiveObject io in interactives)
            {
                if (io.ActivationZone.Contains(X, Y))
                {
                    io.Trigger();
                }
            }
        }

        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the title
            float textWidth = 550.0f;
            Vector2 positionMain = new Vector2(0.5f * (Constants.windowHeight * Constants.windowRatio - textWidth), Constants.windowHeight * 0.05f);
            Vector2 positionShadow = new Vector2(positionMain.X + 2.0f, positionMain.Y + 2.0f);
            float opacity = (gameTime.TotalGameTime.TotalMilliseconds >= Constants.menuFadeIn) ? 0.0f : (float)((Constants.menuFadeIn - gameTime.TotalGameTime.TotalMilliseconds) / Constants.menuFadeIn);
            spriteBatch.DrawString(title, "Just grow", positionShadow, Color.Black * (1f - opacity));
            spriteBatch.DrawString(title, "Just grow", positionMain, Color.White * (1f - opacity));

            // Draw the overview menu
            Vector2 overviewPosition = new Vector2(10f, 0.5f * (Constants.windowHeight - 0.4f * overview.Height));
            spriteBatch.Draw(overview, overviewPosition, null, Color.White, 0f, Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
        }
    }
}