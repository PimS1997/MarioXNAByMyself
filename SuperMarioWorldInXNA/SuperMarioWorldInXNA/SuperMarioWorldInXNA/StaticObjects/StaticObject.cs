using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA.StaticObjects
{
    abstract class StaticObject : GameObject
    {
        private bool isBreakable { get; set; }
        private bool isSolid { get; set; }
        
        public StaticObject() : base()
        {

        }
    }
}
