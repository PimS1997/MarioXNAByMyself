using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SuperMarioWorldInXNA.StaticObjects;
using System.ComponentModel;
using Microsoft.Xna.Framework.Content;

namespace SuperMarioWorldInXNA
{
    public class Level
    {
        private enum _objects
        {
            [Description("#FFFFFF")]
            Nothing,
            [Description("#FF0000")]
            Player,
            [Description("#FF6A00")]
            MysteryBlockEmpty,
            [Description("#FFD800")]
            Coin,
            [Description("#00C200")]
            Ground
        }
        public int Width
        {
            get { return _level.GetLength(0); }
        }
        public int Height
        {
            get { return _level.GetLength(1); }
        }
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        private Tile[,] _level { get; set; }
        private Bitmap _levelBmp;
        private Color _pixelColor;
        private string _pixelColorAsHex;
        private Enums _enums = new Enums();

        private List<Coin> _coins = new List<Coin>();

        public Level(IServiceProvider services, string path)
        {
            content = new ContentManager(services, "Content");

            BuildLevel(path);
        }

        /// <summary>
        /// Fills an 2D array of GameObjects using the hexcodes of the pixels of an image file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void BuildLevel(string path)
        {
            _levelBmp = new Bitmap(path);
            Tile[,] _level = new Tile[_levelBmp.Width, _levelBmp.Height];
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

        private Tile LoadTile(Color pixelColor, int x, int y)
        {
            _pixelColor = _levelBmp.GetPixel(x, y);
            _pixelColorAsHex = ColorTranslator.ToHtml(_pixelColor);

            if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.Nothing)))
            {
                return new Tile(null, TileCollision.Passable);
            }
            else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.Coin)))
            {
                Console.WriteLine("{0}, {1} is a Coin", x, y);
                return LoadCoinTile(x, y);
            }
            else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.MysteryBlockEmpty)))
            {
                Console.WriteLine("{0}, {1} is a EmptyMysteryBlock", x, y);
                return LoadMysteryTile();
            }
            else if (_pixelColorAsHex.Equals(_enums.GetEnumDescription(_objects.Player)))
            {
                Console.WriteLine("{0}, {1} is a player tile", x, y);
                return LoadPlayerTile();
            }
            else
            {
                throw new NotSupportedException("This is not a valid block"); //moet error block worden
            }
        }

        private Tile LoadMysteryTile()
        {
            throw new NotImplementedException();
        }

        private Tile LoadPlayerTile()
        {
            throw new NotImplementedException();
        }

        private Tile LoadTile(TileCollision tileCollision)
        {
            return new Tile();
        }
        private Tile LoadCoinTile(int x, int y)
        {
            return new Tile();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
             
        }

    }
}
