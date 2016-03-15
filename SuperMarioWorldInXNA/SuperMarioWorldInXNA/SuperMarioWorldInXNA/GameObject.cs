using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA
{
    abstract class GameObject
    {
        public abstract Texture2D texture { get; set; }
    }
}
