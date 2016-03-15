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

        public override Texture2D texture
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Coin() : base()
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
