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
        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;
        public TileCollision Collision;

        public const int Width = 16;
        public const int Height = 16;

        public Tile(Texture2D Texture, TileCollision Collision)
        {
            this.texture = Texture;
            this.Collision = Collision;
            //Changes
        }
        public Tile(Texture2D Texture, Rectangle subImg, TileCollision Collision)
        {
            this.texture = Texture;
            this.Collision = Collision;
            //Changes
        }
    }
    
}
