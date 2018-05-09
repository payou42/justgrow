using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Justgrow.Engine.Utilities.LSystem
{
    [Serializable]
    public class Rule
    {
        public List<Tuple<float, string>> Probabilities = new List<Tuple<float, string>>();
    }
}