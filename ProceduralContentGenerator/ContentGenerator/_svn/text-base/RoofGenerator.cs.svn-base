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
    public struct RoofGenerator
    {
        public static Texture2D GenerateRoof(Texture2D brick, Rectangle rect, GraphicsDevice graphicsDevice)
        {
            //Instantiate the drawing variables
            Color tileFilter = Color.DarkRed;
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            //graphicsDevice.Clear(Color.White);
            Rectangle tileRect = new Rectangle(rect.X, rect.Y, (int)Math.Round(rect.Width / 10f), (int)Math.Round(rect.Height / 10f));

            //Calculate how many tiles will be required
            int height = (int)Math.Round(rect.Height / 10f);
            int width = (int)Math.Round(rect.Width / 10f);

            int widthLimit = rect.Width;
            int i = 0;
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
            for (Vector2 drawPosition = new Vector2(rect.X, rect.Y + rect.Height - tileRect.Height); drawPosition.Y < rect.Y; drawPosition.Y += tileRect.Height)
            {
                drawPosition.X = rect.X + (i * (int)Math.Round(tileRect.Width / 2f));
                for ( ; drawPosition.X < widthLimit; drawPosition.X += tileRect.Height)
                {
                    Rectangle drawRect = new Rectangle((int)drawPosition.X, (int)drawPosition.Y, tileRect.Width, tileRect.Height);
                    spriteBatch.Draw(brick, drawRect, tileFilter);
                }
                widthLimit -= (int)Math.Round(tileRect.Width / 2f);
                i++;
            }
            spriteBatch.End();

            return HouseGenerator.RenderGraphicsDevice(graphicsDevice, rect);
        }
    }
}
