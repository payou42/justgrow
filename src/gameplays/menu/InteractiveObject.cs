using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Justgrow.Gameplays.Core;
using Justgrow.Gameplays.Tree;

namespace Justgrow.Gameplays.Menu
{
    public class InteractiveObject
    {
        public delegate void OnFocusChangedDelegate(bool focused);
        public delegate void OnActionDelegate();

        public event OnFocusChangedDelegate OnFocusChanged;
        public event OnActionDelegate OnAction;

        public Rectangle ActivationZone { get; set; }

        public bool focused;

        public bool Focused
        {
            get
            {
                return focused;
            }
            set
            {
                if (value != focused)
                {
                    focused = value;
                    if (OnFocusChanged != null)
                    {
                        OnFocusChanged(focused);
                    }
                    Console.WriteLine("Emitting OnFocusChanged");
                }
            }
        }

        public void Trigger()
        {
            if (OnAction != null)
            {
                OnAction();
            }
            Console.WriteLine("Emitting OnAction");
        }

        public InteractiveObject()
        {
            focused = false;
            ActivationZone = new Rectangle();
        }
    }
}