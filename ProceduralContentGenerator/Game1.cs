using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using HouseGeneratorAssignment;

namespace Demo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle viewportRect;
        Rectangle mapRect;
        Vector3 viewPosition;
        const int MapWidth = 2000;
        Texture2D backGround;
        Matrix camera;
        const bool Debug = true;
        GameObject[] houses;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            spriteBatch = new SpriteBatch(GraphicsDevice);
            SetResolution(1280, 720);
            viewPosition = new Vector3(viewportRect.Width / 2f, viewportRect.Height / 2f, 0);
            camera = Matrix.CreateTranslation(-viewPosition);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            backGround = Content.Load<Texture2D>("Backgrounds\\BG");
            HouseGenerator.Load(Content);
            RandomManager.Initialise();
            houses = HouseGenerator.GenerateHouse(mapRect, 1f, GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            UpdateView();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, camera);

            spriteBatch.Draw(backGround, mapRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            //spriteBatch.Draw(houses[0].Sprite, mapRect, Color.White);

            foreach (GameObject house in houses)
            {
                house.Draw(spriteBatch);
                //spriteBatch.Draw(house.Sprite, house.Rect, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void UpdateView()
        {
            Vector2 velocity = Vector2.Zero;

            //Update X velocity
            velocity.X = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -5;
            }

            //Update Y velocity
            velocity.Y = -GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                velocity.Y = 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                velocity.Y = -5;
            }

            if (!Debug)
            {
                velocity = BoundaryCheck(velocity);
            }

            //Update camera
            viewPosition.X += velocity.X;
            viewPosition.Y += velocity.Y;

            Vector3 cameraPosition = new Vector3(-((viewPosition.X) - (viewportRect.Width / 2)),
                                         -((viewPosition.Y) - (viewportRect.Height / 2)), 0);
            if (!Debug)
            {
                float mapWidthBorderLeft = 0;
                float mapWidthBorderRight = -(mapRect.Width - (viewportRect.Width));
                cameraPosition.X = MathHelper.Clamp(cameraPosition.X, mapWidthBorderRight, mapWidthBorderLeft);

                float mapHeightBorderTop = 0;
                float mapHeightBorderBottom = -(mapRect.Height - (viewportRect.Height));
                cameraPosition.Y = MathHelper.Clamp(cameraPosition.Y, mapHeightBorderBottom, mapHeightBorderTop);
            }
            camera = Matrix.CreateTranslation(cameraPosition);
        }

        protected Vector2 BoundaryCheck(Vector2 velocity)
        {
            //Check for boundary collision
            if (viewPosition.X + velocity.X >= mapRect.Width + mapRect.X)
            {
                viewPosition.X = mapRect.Width + mapRect.X;
                velocity.X = 0;
            }
            else if (viewPosition.X + velocity.X <= mapRect.X)
            {
                viewPosition.X = mapRect.X;
                velocity.X = 0;
            }
            if (viewPosition.Y + velocity.Y >= mapRect.Height + mapRect.Y)
            {
                viewPosition.Y = mapRect.Height + mapRect.Y;
                velocity.Y = 0;
            }
            else if (viewPosition.Y + velocity.Y <= mapRect.Y)
            {
                viewPosition.Y = mapRect.Y;
                velocity.Y = 0;
            }

            return velocity;
        }

        protected void SetResolution(int newWidth, int newHeight)
        {
            if (graphics.GraphicsDevice.GraphicsDeviceCapabilities.MaxTextureWidth >= newWidth
                            && graphics.GraphicsDevice.GraphicsDeviceCapabilities.MaxTextureHeight >= newHeight)
            {
                graphics.PreferredBackBufferHeight = newHeight;
                graphics.PreferredBackBufferWidth = newWidth;
                graphics.ApplyChanges();

                viewportRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
                mapRect = new Rectangle(0, 0, viewportRect.Width * 3, viewportRect.Height);
            }
        }
    }
}
