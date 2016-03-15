using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SuperMarioWorldInXNA.StaticObjects;
using System.ComponentModel;

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
            Coin
        }

        GameObject[,] _level { get; set; }
        private Bitmap _levelBmp;
        private Color _pixelColor;
        private string _pixelColorAsHex;
        Enums enums = new Enums();

        public Level(string path)
        {
            _level = BuildLevel(path);
        }

        /// <summary>
        /// Fills an 2D array of GameObjects using the hexcodes of the pixels of an image file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private GameObject[,] BuildLevel(string path)
        {
            _levelBmp = new Bitmap(path);
            GameObject[,] _level = new GameObject[_levelBmp.Width, _levelBmp.Height];
            for (int y = 0; y < _levelBmp.Height; y++)
            {
                for (int x = 0; x < _levelBmp.Width; x++)
                {
                    _pixelColor = _levelBmp.GetPixel(x, y);
                    _pixelColorAsHex = ColorTranslator.ToHtml(_pixelColor);

                    if (_pixelColorAsHex.Equals(enums.GetEnumDescription(_objects.Nothing)))
                    {
                        _level[x, y] = null;
                    }
                    else if (_pixelColorAsHex.Equals(enums.GetEnumDescription(_objects.Coin)))
                    {
                        Console.WriteLine("{0}, {1} is a Coin", x, y);
                        _level[x, y] = new Coin();
                    }
                    else if (_pixelColorAsHex.Equals(enums.GetEnumDescription(_objects.MysteryBlockEmpty)))
                    {
                        Console.WriteLine("{0}, {1} is a EmptyMysteryBlock", x, y);
                        _level[x, y] = new MysteryBlock();
                    }
                    else if (_pixelColorAsHex.Equals(enums.GetEnumDescription(_objects.Player)))
                    {
                        Console.WriteLine("{0}, {1} is a player", x, y);
                        //_level[x, y] = new Player();
                    }
                    else
                    {
                        _level[x, y] = null; //moet error block worden
                    }

                }
            }
            return _level;
        }
    }
}
