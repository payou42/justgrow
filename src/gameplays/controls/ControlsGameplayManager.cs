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

namespace Justgrow.Gameplays.Controls
{
    public class ControlsGameplayManager : Gameplay
    {
        public delegate void OnMouseMoveDelegate(int X, int Y);
        public delegate void OnMouseClickDelegate(int X, int Y, MouseButtons buttons);

        public event OnMouseMoveDelegate OnMouseMove;
        
        public event OnMouseClickDelegate OnMouseClick;

        protected GamePadState padState;

        protected MouseState mouseState;

        protected KeyboardState keyboardState;



        public ControlsGameplayManager(MainGame g) : base(g)
        {
        }

        public override GameplaysDefinition Definition
        {
            get
            {
                return GameplaysDefinition.Controls;
            }
        }

        public override void Initialize()
        {
            padState = GamePad.GetState(PlayerIndex.One);
            keyboardState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            TreeGameplayManager gpTree = game.gameplays.Get<TreeGameplayManager>(GameplaysDefinition.Tree);
            GamePadState pad = GamePad.GetState(PlayerIndex.One);            
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            HandleMouse(mouse, mouseState);

            if (keyboard.IsKeyDown(Keys.Z) && keyboardState.IsKeyUp(Keys.Z)) {
                gpTree.Age++;
            }
            if (keyboard.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A)) {
                gpTree.Age--;
            }

            if (pad.Buttons.Start == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }

            keyboardState = keyboard;
            padState = pad;
            mouseState = mouse;
        }

        protected void HandleMouse(MouseState newState, MouseState oldState)
        {
            // Check mouse move
            if ((newState.X != oldState.X) || (newState.Y != oldState.Y))
            {
                OnMouseMove(newState.X, newState.Y);
            }

            // Check each buttons
            MouseButtons buttons = MouseButtons.None;
            if ((newState.LeftButton == ButtonState.Pressed) && (oldState.LeftButton == ButtonState.Released))
            {
                buttons |= MouseButtons.Left;
            }
            if ((newState.RightButton == ButtonState.Pressed) && (oldState.RightButton == ButtonState.Released))
            {
                buttons |= MouseButtons.Right;
            }
            if ((newState.MiddleButton == ButtonState.Pressed) && (oldState.MiddleButton == ButtonState.Released))
            {
                buttons |= MouseButtons.Middle;
            }
            if (buttons != MouseButtons.None)
            {
                OnMouseClick(newState.X, newState.Y, buttons);
            }
        }
    }
}