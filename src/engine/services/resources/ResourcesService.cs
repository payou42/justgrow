using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Justgrow.Engine.Services.Core;

namespace Justgrow.Engine.Services.Resources
{
    public class ResourcesService: Service
    {
        // The already loaded resources
        protected Dictionary<string, object> contentCache;

        public ResourcesService(MainGame g) : base(g)
        {
            game.Content.RootDirectory = "assets";
            contentCache = new Dictionary<string, object>();
        }

        public override ServicesDefinition Definition
        {
            get
            {
                return ServicesDefinition.Resources;
            }
        }

        public T Load<T>(string name)
        {
            // Cache hit
            if (contentCache.ContainsKey(name))
            {
                return (T)(contentCache[name]);
            }

            // Cache miss
            T resource = game.Content.Load<T>(name);
            contentCache[name] = resource;
            return resource;
        }
    }
}