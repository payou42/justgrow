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

namespace Justgrow.Gameplays.Wind
{
    public class WindGameplayManager : Gameplay
    {
        protected SoundEffectInstance windSound;

        protected float intensity;
        protected float target;
        protected Random random;

        // The main game instance
        public WindGameplayManager(MainGame g) : base(g)
        {
            random = new Random();
            intensity = 0.0f;
            target = 0.0f;
        }

        public override GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Wind;
            }
        }

        public override void Initialize()
        {
            // Load sound effect
            ResourcesService resources = game.services.Get<ResourcesService>(ServicesDefinition.Resources);
            windSound = resources.Load<SoundEffect>(Constants.soundWind).CreateInstance();
            windSound.IsLooped = true;
            windSound.Volume = intensity;
            windSound.Play();        
        }

        public float Intensity
        {
            get
            {
                return intensity;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // If target is reached, generate a new one
            if (Math.Abs(intensity - target) < 0.001f)
            {
                // Randomly change the wind intensity
                target = (float)random.NextDouble();
            }
            else
            {
                // Move slowly the intensity toward the target
                float direction = (target > intensity) ? 1.0f : -1.0f;
                float amount = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / Constants.windLatency);
                amount = Math.Min(amount, Math.Abs(target - intensity));
                intensity += direction * amount;
                windSound.Volume = 0.5f * intensity;
            }
        }        
    }
}