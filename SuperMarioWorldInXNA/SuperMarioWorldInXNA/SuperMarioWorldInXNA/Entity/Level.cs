using Microsoft.Xna.Framework.Content;
using System;
using System.IO;

namespace SuperMarioWorldInXNA
{
    class Level : IDisposable
    {
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Player Player
        {
            get { return player; }
        }
        Player player;


        private void LoadTiles(Stream FileStream)
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}