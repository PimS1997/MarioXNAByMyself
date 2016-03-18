using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA.StaticObjects
{
    class Coin : StaticObject
    {
        private bool isPickedUp { get; set; }
        private Vector2 basePosition;

        public Level Level
        {
            get { return level; }
        }
        Level level;
        public Vector2 Position
        {
            get
            {
                return basePosition;
            }
        }

        public Coin(Level level, Vector2 position) : base()
        {
            isPickedUp = false;
            //this.texture = texture;
        }

        public void PickUp()
        {
            isPickedUp = true;
        }
    }
}
