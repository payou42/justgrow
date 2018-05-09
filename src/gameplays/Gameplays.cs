using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Justgrow.Gameplays.Core;

namespace Justgrow.Gameplays
{
    public class Gameplays : Gameplay
    {
        // The list of registered services
        protected Dictionary<GameplaysDefinition, Gameplay> gameplays;

        // Graphics devica
        public Gameplays(MainGame g) : base(g)
        {
            gameplays = new Dictionary<GameplaysDefinition, Gameplay>();
        }

        public T Get<T>(GameplaysDefinition definition) where T : Gameplay
        {
            return (T)gameplays[definition];
        }

        public void Register(Gameplay gameplay)
        {
            gameplays[gameplay.Definition] = gameplay;
        }

        public override void Initialize()
        {
            foreach (Gameplay gp in gameplays.Values)
            {
                gp.Initialize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Gameplay gp in gameplays.Values)
            {
                gp.Update(gameTime);
            }
        }

        public override void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Gameplay gp in gameplays.Values)
            {
                gp.DrawSprites(gameTime, spriteBatch);
            }
        }
    }
}