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

namespace Justgrow.Gameplays.Background
{
    public enum BackgroundMode
    {
        Overview = 0,

        Aerial = 1,

        Underground = 2
    }
}