using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Justgrow.Engine.Utilities.LSystem
{
    [Serializable]
    public class State
    {
        public float size;
        public float angle;
        public float diameter;
        public int index;
        public Vector3 position;
        public Vector3 heading;
        public Vector3 left;
        public Vector3 up;
        public float sizeGrowth;
        public float angleGrowth;
        public float windIntensity;

        public State Clone()
        { 
            return (State) this.MemberwiseClone(); 
        }
    }
}