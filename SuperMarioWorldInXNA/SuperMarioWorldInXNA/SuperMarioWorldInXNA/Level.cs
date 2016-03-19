using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using SuperMarioWorldInXNA.StaticObjects;
using System.ComponentModel;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Drawing;
using System.IO;

namespace SuperMarioWorldInXNA
{
    public class Level
    {
        private enum _objects
        {
            [Description("#FFFFFF")]
            Nothing,
            [Description("#000000")]
            PlayerStart = 2,
            [Description("#FF6A00")]
            MysteryBlockEmpty,
            [Description("#FFD800")]
            Coin,
            [Description("#00C200")]
            GrassTop = 2,
        }

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        private Tile[,] _level;
        private Bitmap _levelBmp;
        private System.Drawing.Color _pixelColor;
        private string _pixelColorAsHex;
        private Enums _enums = new Enums();

        private int scale = 2;

        private List<Coin> _coins = new List<Coin>();

        private SpriteCutter spriteCut;
        public int Width
        {
            get { return _level.GetLength(0); }
        }
        public int Height
        {
            get { return _level.GetLength(1); }
        }

        public Level(IServiceProvider services, Stream fileStream)
        {
            content = new ContentManager(services, "Content");
            spriteCut = new SpriteCutter();
            _levelBmp = new Bitmap(fileStream);
            _level = new Tile[_levelBmp.Width, _levelBmp.Height];

            BuildLevel();
        }

        /// <summary>
        /// Fills an 2D array of GameObjects using the hexcodes of the pixels of an image file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void BuildLevel()
        {
            for (int y = 0; y < _levelBmp.Height; y++)
            {
                for (int x = 0; x < _levelBmp.Width; x++)
                {
                    _pixelColor = _levelBmp.GetPixel(x, y);
                    _level[x, y] = LoadTile(_pixelColor, x, y);
                }
            }
        }
        private void LoadTiles()
        {

        }

        private Tile LoadTile(System.Drawing.Color pixelColor, int x, int y)
        {
            _pixelColor = _levelBmp.GetPixel(x, y);
            _pixelColorAsHex = ColorTranslator.ToHtml(_pixelColor);

            if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.Nothing)))
            {
                return new Tile(null, 0, TileCollision.Passable);
            }
            else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.Coin)))
            {
                Console.WriteLine("{0}, {1} is a Coin", x, y);
                return LoadCoinTile(x, y);
            }
            /*else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.MysteryBlockEmpty)))
            {
                Console.WriteLine("{0}, {1} is a EmptyMysteryBlock", x, y);
                return LoadMysteryTile();
            }
            else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.PlayerStart)))
            {
                Console.WriteLine("{0}, {1} is a player tile", x, y);
                return LoadPlayerTile();
            }*/
            else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.GrassTop)))
            {
                return LoadTile(TileCollision.Impassable);
            }
            else
            {
                return new Tile(null, 0, TileCollision.Passable);
            }
        }

        /*private Tile LoadMysteryTile()
        {
            
        }

        private Tile LoadPlayerTile()
        {
            
        }*/

        private Tile LoadTile(TileCollision tileCollision)
        {
            return new Tile(Content.Load<Texture2D>("Sprites/Tiles/TileSheet"), (int)_objects.GrassTop, TileCollision.Impassable);
        }
        private Tile LoadCoinTile(int x, int y)
        {
            Microsoft.Xna.Framework.Point position = GetBounds(x, y).Center; //Namespace staat er voor omdat er comflictende usings zijn System.Drawing en de XNA
            _coins.Add(new Coin(this, new Vector2(position.X, position.Y)));
            return new Tile();
        }
        public Microsoft.Xna.Framework.Rectangle GetBounds(int x, int y)//Namespace staat er voor omdat er comflictende usings zijn System.Drawing en de XNA
        {
            return new Microsoft.Xna.Framework.Rectangle(x * Tile.Width, y * Tile.Height, Tile.Width, Tile.Height);//Namespace staat er voor omdat er comflictende usings zijn System.Drawing en de XNA
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < _levelBmp.Width; x++)
            {
                for (int y = 0; y < _levelBmp.Height; y++)
                {
                    if(_level[x, y].Texture != null)
                    {
                        Texture2D texture = _level[x, y].Texture;
                        if (texture != null)
                        {
                            Vector2 position = new Vector2(x, y) * (new Vector2(Tile.Width * scale, Tile.Height * scale));
                            spriteBatch.Draw(texture, position, spriteCut.getSubTile(_level[x, y].blockID, texture), Microsoft.Xna.Framework.Color.White, 0.0f, Vector2.Zero, (float)scale, SpriteEffects.None, 0.0f);
                        }
                    }
                }
            }
        }

    }
}
