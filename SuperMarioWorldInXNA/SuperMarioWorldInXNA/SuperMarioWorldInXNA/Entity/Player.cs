using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarioWorldInXNA
{
    class Player
    {
        //Animations
        private Animation runAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;

        private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer sprite;

        //Current user movement input for support with game controllers
        private float movement;

        private const float MaxMoveSpeed = 500f;
        private const float MoveAcceleration = 13000f;

        private int spriteSheetWidth = 32; //Tiles
        private int spriteSheetHeight = 16; //Tiles

        private float frameTime = 0.1f;

        private float scale = 2.0f;

        private bool IsOnGround = true;

        private SoundEffect jumpSound;

        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;

        float VelocityX;
        float VelocityY = 0.0f;
        float gravity = 0.45f;
        float JumpHeight = 250.0f;
        Vector2 startPos;

        private const float GroundDragFactor = 0.48f;

        private float previousBottom;

        ContentManager content;

        public Level Level
        {
            get { return level; }
        }
        Level level;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;
        public bool IsAlive
        {
            get { return isAlive; }
        }
        bool isAlive;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        private Rectangle localBounds;
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - sprite.Origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - sprite.Origin.Y) + localBounds.Y;

                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }

        public Player(Vector2 position, IServiceProvider services, Level level)
        {
            this.level = level;

            LoadContent(services);

            Reset(position);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");

            runAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), frameTime, true, spriteSheetWidth, spriteSheetHeight, 2, 0, scale);
            idleAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), frameTime, true, spriteSheetWidth, spriteSheetHeight, 1, 0, scale);
            jumpAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), frameTime, true, spriteSheetWidth, spriteSheetHeight, 1, 1, scale);

            int width = (int)(idleAnimation.FrameWidth * 0.4);
            int left = (idleAnimation.FrameWidth - width) / 2;
            int height = (int)(idleAnimation.FrameWidth * 0.8);
            int top = idleAnimation.FrameHeight - height;
            localBounds = new Rectangle(left, top, width, height);

            //jumpSound = Level.Content.Load<SoundEffect>("Sounds/PlayerJump");
        }
        public void Reset(Vector2 position)
        {
            startPos = position;
            Position = position;
            Velocity = Vector2.Zero;
            isAlive = true;
            sprite.PlayAnimation(idleAnimation);
        }
        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            GetInput(keyboardState, gameTime);
            ApplyPhysics(gameTime);
            PlayAnimation();
        }
        private void PlayAnimation()
        {
            if (Math.Abs(Velocity.X) != 0)
            {
                sprite.PlayAnimation(runAnimation);
            }
            else
            {
                sprite.PlayAnimation(idleAnimation);
            }

            if (Math.Abs(Velocity.Y) > 0.0f)
            {
                sprite.PlayAnimation(jumpAnimation);
            }
        }
        public void GetInput(KeyboardState keyboardState, GameTime gameTime)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Move(false, gameTime);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                Move(true, gameTime);
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                Jump(gameTime);
            }

        }

        private void Move(bool movingRight, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (movingRight)
            {
                velocity.X = 350.0f;
            }
            else
            {
                velocity.X = -350.0f;
            }
            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));
        }

        private float Jump(GameTime gameTime)
        {
            if (IsOnGround)
            {
                velocity.Y = -12.0f;
                IsOnGround = false;
            }
            return velocity.Y;
        }
        private void CheckCollision()
        {
            Rectangle bounds = BoundingRectangle;
            int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
            int rightTile = (int)Math.Ceiling((float)bounds.Right / Tile.Width) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
            int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / Tile.Height) - 1;

            IsOnGround = false;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x < rightTile; ++x)
                {
                    //if the is collidable
                    TileCollision collision = level.GetCollision(x, y);
                    if(collision != TileCollision.Passable)
                    {
                        Rectangle tileBounds = level.GetBounds(x, y);
                        Vector2 depth = RectangleExtensions.GetIntersectionDepth(bounds, tileBounds);
                        if(depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);

                            if(absDepthY < absDepthX || collision == TileCollision.Platform)
                            {
                                if(previousBottom <= tileBounds.Top)
                                {
                                    IsOnGround = true;
                                }

                                if(collision == TileCollision.Impassable || IsOnGround)
                                {
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);

                                    bounds = BoundingRectangle;
                                }
                            }
                            else if(collision == TileCollision.Impassable)
                            {
                                Position = new Vector2(Position.X + depth.X, Position.Y);

                                bounds = BoundingRectangle;
                            }
                        }
                    }
                }
            }

            previousBottom = bounds.Bottom;
        }


        public void ApplyPhysics(GameTime gameTime)
        {
            Vector2 previousPosition = Position;

            if (position.Y > startPos.Y)
            {
                position.Y = startPos.Y;
                velocity.Y = 0.0f;
                IsOnGround = true;
            }
            if (!IsOnGround)
            {
                velocity.Y += gravity;
                position.Y += velocity.Y;
            }
            else
            {
                position.Y = startPos.Y;
            }

            CheckCollision();

            // If the collision stopped us from moving, reset the velocity to zero.
            if (Position.X == previousPosition.X)
                velocity.X = 0;

            if (Position.Y == previousPosition.Y)
                velocity.Y = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0)
                flip = SpriteEffects.None;
            else if (Velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;

            sprite.Draw(gameTime, spriteBatch, Position, flip);
            velocity.X = 0f;
        }
    }
}
