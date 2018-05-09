using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Justgrow.Engine.Utilities.LSystem
{
    public class Generator
    {
        public delegate void OnExecuteDelegate(bool isBranch, State current, Vector3 from, Vector3 to);
	
        public event OnExecuteDelegate onExecute;

        protected string input = "";

        protected string output = "";

        protected Dictionary<string, Rule> rules;

        protected Random random;

        protected State initialState;

        public string Input
        {
            get
            {
                return input;
            }

            set
            {
                input = value;
            }
        }

        public string Output
        {
            get
            {
                return output;
            }
        }

        public State InitialState
        {
            get
            {
                return initialState;
            }
        }

        public Generator()
        {
            input = "";
            random = new Random();
            rules = new Dictionary<string, Rule>();
            initialState = new State();
        }

        public void AddRule(string key, float probability, string rewrite)
        {
            if (!rules.ContainsKey(key))
            {
                rules[key] = new Rule();
            }
            rules[key].Probabilities.Add(new Tuple<float, string>(probability, rewrite));
        }
        public void Generate(int depth)
        {
            output = input;
            for (int i = 0; i < depth; ++i)
            {
                output = Replace(output);
            }
        }

        protected string SelectRule(Rule rule, string d)
        {
            // Simple case optimisation
            if (rule.Probabilities.Count == 1)
            {
                return rule.Probabilities[0].Item2;
            }

            // Probability check
            float r = (float)random.NextDouble();
            foreach (Tuple<float, string> probability in rule.Probabilities)
            {
                if (probability.Item1 >= r)
                {
                    return probability.Item2;
                }
                r -= probability.Item1;
            }

            // Default
            return d;
        }

        protected string Replace(string s)
        {
            StringBuilder sb = new StringBuilder();

            string[] parsed = s.Split(' ');
            foreach (string item in parsed)
            {
                if (rules.ContainsKey(item))
                {
                    sb.Append(SelectRule(rules[item], item));
                }
                else
                {
                    sb.Append(item);
                }
                sb.Append(' ');
            }

            return sb.ToString().Trim();
        }

        protected State ApplyYaw(State state, float angle)
        {
            State next = state.Clone();
            next.heading = Vector3.Multiply(state.heading, (float)Math.Cos(angle)) - Vector3.Multiply(state.left, (float)Math.Sin(angle));
            next.left = Vector3.Multiply(state.heading, (float)Math.Sin(angle)) + Vector3.Multiply(state.left, (float)Math.Cos(angle));
            return next;
        }

        protected State ApplyPitch(State state, float angle)
        {
            State next = state.Clone();
            next.heading = Vector3.Multiply(state.heading, (float)Math.Cos(angle)) - Vector3.Multiply(state.up, (float)Math.Sin(angle));
            next.up = Vector3.Multiply(state.heading, -(float)Math.Sin(angle)) + Vector3.Multiply(state.up, (float)Math.Cos(angle));
            return next;
        }

        protected State ApplyRoll(State state, float angle)
        {
            State next = state.Clone();
            next.left = Vector3.Multiply(state.left, (float)Math.Cos(angle)) + Vector3.Multiply(state.up, (float)Math.Sin(angle));
            next.up = Vector3.Multiply(state.left, -(float)Math.Sin(angle)) + Vector3.Multiply(state.up, (float)Math.Cos(angle));
            return next;
        }

        protected State ApplyWind(State state, float angle)
        {
            State next = state.Clone();
            Matrix rotation = Matrix.CreateRotationZ(angle);
            next.heading = Vector3.TransformNormal(state.heading, rotation);
            next.left = Vector3.TransformNormal(state.left, rotation);
            next.up = Vector3.TransformNormal(state.up, rotation);
            return next;
        }

        public void Execute()
        {
            State state = initialState.Clone();
            Stack<State> states = new Stack<State>();

            string[] parsed = output.Split(' ');
            foreach (string s in parsed)
            {
                // Draw commands
                if (s.StartsWith("F"))
                {
                    Vector3 newPosition = state.position + Vector3.Multiply(state.heading, state.size / (1f + 0.5f * state.diameter));
                    onExecute(true, state, state.position, newPosition);
                    state.position = newPosition;
                    continue;
                }

                if (s.StartsWith("f"))
                {
                    Vector3 newPosition = state.position + Vector3.Multiply(state.heading, state.size / (1f + 0.5f * state.diameter));
                    onExecute(false, state, state.position, newPosition);
                    // state.position = newPosition;
                    continue;
                }

                // Standard commands
                switch (s)
                {
                    case @"+":
                    {
                        state = ApplyYaw(state, state.angle);
                        state = ApplyWind(state, state.windIntensity);
                        break;
                    }
                    case @"-":
                    {
                        state = ApplyYaw(state, -state.angle);
                        state = ApplyWind(state, state.windIntensity);
                        break;
                    }
                    case @"&":
                    {
                        state = ApplyPitch(state, state.angle);
                        state = ApplyWind(state, state.windIntensity);
                        break;
                    }
                    case @"^":
                    {
                        state = ApplyPitch(state, -state.angle);
                        state = ApplyWind(state, state.windIntensity);
                        break;
                    }
                    case @"\":
                    {
                        state = ApplyRoll(state, state.angle);
                        state = ApplyWind(state, state.windIntensity);
                        break;
                    }
                    case @"/":
                    {
                        state = ApplyRoll(state, -state.angle);
                        state = ApplyWind(state, state.windIntensity);
                        break;
                    }
                    case @">":
                    {
                        state.size *= (1 - state.sizeGrowth);
                        break;
                    }
                    case @"<":
                    {
                        state.size *= (1 + state.sizeGrowth);
                        break;
                    }
                    case @")":
                    {
                        state.angle *= (1 + state.angleGrowth);
                        break;
                    }
                    case @"(":
                    {
                        state.angle *= (1 - state.angleGrowth);
                        break;
                    }
                    case @"[":
                    {
                        states.Push(state.Clone());
                        break;
                    }
                    case @"]":
                    {
                        state = states.Pop();
                        break;
                    }
                    case @"!":
                    {
                        state.diameter += 1;
                        break;
                    }
                    case @"'":
                    {
                        state.index += 1;
                        break;
                    }
                }
            }
        }
    }
}