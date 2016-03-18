using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SuperMarioWorldInXNA
{
    public class SpriteCutter
    {
        private int columns = 32;
        private int rows = 32;
        public Rectangle getSubTile(int blockID, Texture2D spriteSheet)
        {
            int x;
            int y;

            blockID -= 1;

            int xSize = spriteSheet.Width / columns;
            int ySize = spriteSheet.Height / rows;

            if (blockID >= columns)
            {
                y = blockID / columns;
            }
            else
            {
                y = 0;
            }

            x = blockID % columns;

            return new Rectangle(x * 16, y * 16, xSize, ySize);
        }
    }
}
