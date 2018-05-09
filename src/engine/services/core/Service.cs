using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Justgrow.Engine.Services.Core
{
    public class Service
    {
        // The main game instance
        protected MainGame game;

        // Graphics devica
        public Service(MainGame g)
        {
            game = g;
        }

        public virtual ServicesDefinition Definition
        {
            get
            {
                return ServicesDefinition.Undefined;
            }
        }

        public virtual void Create()
        {
        }

        public virtual void Initialize()
        {
        }
    }
}