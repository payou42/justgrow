using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Justgrow.Engine.Services.Core;

namespace Justgrow.Engine.Services
{
    public class Services : Service
    {
        // The list of registered services
        protected Dictionary<ServicesDefinition, Service> services;

        // Graphics devica
        public Services(MainGame g) : base(g)
        {
            services = new Dictionary<ServicesDefinition, Service>();
        }

        public T Get<T>(ServicesDefinition definition) where T : Service
        {
            return (T)services[definition];
        }

        public void Register(Service service)
        {
            services[service.Definition] = service;
        }
        
        public override void Create()
        {
            foreach (Service s in services.Values)
            {
                s.Create();
            }
        }

        public override void Initialize()
        {
            foreach (Service s in services.Values)
            {
                s.Initialize();
            }
        }
    }
}