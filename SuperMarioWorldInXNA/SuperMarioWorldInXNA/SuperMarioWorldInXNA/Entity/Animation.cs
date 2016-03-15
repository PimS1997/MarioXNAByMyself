

using Microsoft.Xna.Framework.Graphics;

namespace SuperMarioWorldInXNA
{
    public class Animation
    {
        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;
        public int Rows
        {
            get { return rows; }
        }
        int rows;

        public int Columns
        {
            get { return columns; }
        }
        int columns;
        public float Scale
        {
            get { return scale; }
        }
        float scale;

        public int StartPos
        {
            get { return startPos; }
        }
        int startPos;
        /// <summary>
        /// Duration of time to show each frame.
        /// </summary>
        public float FrameTime
        {
            get { return frameTime; }
        }
        float frameTime;
        /// <summary>
        /// When the end of the animation is reached, should it
        /// continue playing from the beginning?
        /// </summary>
        public bool IsLooping
        {
            get { return isLooping; }
        }
        bool isLooping;

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public int FrameCount
        {
            get { return frameCount; }
        }
        int frameCount;
        /// <summary>
        /// Gets the width of a frame in the animation.
        /// </summary>
        public int FrameWidth
        {
            // Assume square frames.
            get { return Texture.Width / columns; }
        }

        /// <summary>
        /// Gets the height of a frame in the animation.
        /// </summary>
        public int FrameHeight
        {
            get { return Texture.Height / rows; }
        }

        /// <summary>
        /// Constructors a new animation.
        /// </summary>        
        public Animation(Texture2D texture, float frameTime, bool isLooping, int columns, int rows, int frameCount, int startPos, float scale)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
            this.rows = rows;
            this.columns = columns;
            this.frameCount = frameCount;
            this.startPos = startPos;
            this.scale = scale;
        }
    }
}
