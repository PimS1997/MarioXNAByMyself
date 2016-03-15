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

        private SpriteEffects flip = SpriteEffects.None;
        private AnimationPlayer sprite;

        //Current user movement input for support with game controllers
        private float movement;

        private const float MaxMoveSpeed = 500f;
        private const float MoveAcceleration = 13000f;

        private int spriteSheetWidth = 32; //Tiles
        private int spriteSheetHeight = 16; //Tiles

        private int frameCountRun = 2;
        private int frameCountIdle = 1;
        private float frameTime = 0.1f;
        private int startPosRun = 0;

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

        public Player(Vector2 position, IServiceProvider services)
        {
            //this.level = level;

            LoadContent(services);

            Reset(position);
        }

        public void LoadContent(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");

            runAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), frameTime, true, spriteSheetWidth, spriteSheetHeight, frameCountRun, startPosRun, scale);
            idleAnimation = new Animation(content.Load<Texture2D>("Sprites/Mario/mario"), frameTime, true, spriteSheetWidth, spriteSheetHeight, frameCountIdle, startPosRun, scale);

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

            GetInput(keyboardState, gameTime);

            //ApplyPhysics(gameTime);

            if (Math.Abs(Velocity.X) != 0)
            {
                sprite.PlayAnimation(runAnimation);
            }
            else
            {
                sprite.PlayAnimation(idleAnimation);
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
            if(keyboardState.IsKeyDown(Keys.W))
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

        public void ApplyPhysics(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;
            velocity.X += movement * MoveAcceleration * elapsed;


            //Prevents the player from walking faster than his max speed
            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            velocity.X *= GroundDragFactor;

            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));


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
