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

namespace HouseGeneratorAssignment
{
    public struct ChimneyGenerator
    {
        public static Texture2D GenerateChimney(Texture2D brick, Rectangle rect, GraphicsDevice graphicsDevice)
        {
            //Instantiate all the variables for drawing
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            Rectangle brickRect = new Rectangle(rect.X, rect.Y, (int)Math.Round(HouseGenerator.HouseWidth / 10f), (int)Math.Round(HouseGenerator.HouseHeight / 10f));
            Color brickFilter = Color.White;

            //graphicsDevice.Clear(Color.White);
            
            //Calculate how many bricks are needed
            int height = rect.Height / brickRect.Height;
            int width = rect.Width / brickRect.Width;

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
            for (int i = 0; i < height / 2; i++)
            {
                Rectangle sourceRect1 = new Rectangle(0, 0, brick.Width / 2, brick.Height);
                Rectangle sourceRect2 = new Rectangle(brick.Width / 2, 0, brick.Width / 2, brick.Height);
                for (int j = 0; j < width * 2; j++)
                {
                    Rectangle drawRect1 = new Rectangle(rect.X + (j * (brickRect.Width / 2)), rect.Y + (i * brickRect.Height), brickRect.Width, brickRect.Height);
                    Rectangle drawRect2 = new Rectangle(rect.X + (j * (brickRect.Width / 2)), rect.Y + ((i + 1) * brickRect.Height), brickRect.Width, brickRect.Height);

                    spriteBatch.Draw(brick, drawRect1, sourceRect1, Color.White);
                    spriteBatch.Draw(brick, drawRect2, sourceRect2, Color.White);

                    sourceRect1.X += brick.Width / 2;
                    if (sourceRect1.X >= brick.Width)
                    {
                        sourceRect1.X = 0;
                    }
                    sourceRect2.X += brick.Width / 2;
                    if (sourceRect2.X >= brick.Width)
                    {
                        sourceRect2.X = 0;
                    }
                }
            }
            spriteBatch.End();

            return HouseGenerator.RenderGraphicsDevice(graphicsDevice, rect);
        }
    }
}
