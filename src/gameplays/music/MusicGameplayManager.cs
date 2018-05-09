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

namespace Justgrow.Gameplays.Music
{
    public class MusicGameplayManager : Gameplay
    {
        // The main game instance
        public MusicGameplayManager(MainGame g) : base(g)
        {
        }

        public override GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Music;
            }
        }

        public override void Initialize()
        {
            // Load music
            ResourcesService resources = game.services.Get<ResourcesService>(ServicesDefinition.Resources);
            Song background = resources.Load<Song>(Constants.musicForest);

            // Play music in loop
            MediaPlayer.Volume = 0.4f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(background);            
        }

        public float Volume
        {
            get
            {
                return MediaPlayer.Volume;
            }

            set
            {
                MediaPlayer.Volume = value;
            }
        }
    }
}