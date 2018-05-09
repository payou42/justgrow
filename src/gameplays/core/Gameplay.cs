using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Justgrow.Gameplays.Core
{
    public class Gameplay
    {
        // The main game instance
        protected MainGame game;
        
        public Gameplay(MainGame g)
        {
            game = g;
        }

        public virtual GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Undefined;
            }
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void DrawSprites(GameTime gameTime, SpriteBatch spriteBatch)
        {            
        }
    }
}