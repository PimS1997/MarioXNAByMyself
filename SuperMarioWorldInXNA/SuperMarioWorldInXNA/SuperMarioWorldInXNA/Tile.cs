using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA
{
    enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
        Platform = 2,
    }

    struct Tile
    {
        public Texture2D Texture;
        public int blockID;
        public TileCollision Collision;

        public const int Width = 16;
        public const int Height = 16;

        public Tile(Texture2D Texture, int blockID, TileCollision Collision)
        {
            this.Texture = Texture;
            this.Collision = Collision;
            this.blockID = blockID;
            //Changes
        }
    }
    
}
