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
    public struct WindowGenerator
    {
        public static GameObject[] GenerateWindows(Texture2D texture, Rectangle rect, GraphicsDevice graphicsDevice)
        {
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            //graphicsDevice.Clear(Color.White);
            
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);

            DrawFrames(spriteBatch, rect, texture);
            int nUnitWidth = rect.Width / HouseGenerator.HouseWidth;
            int nUnitHeight = (rect.Height / HouseGenerator.HouseHeight)-1;

            //first window on the bottom floor
            Rectangle frameRect = new Rectangle((rect.Width / nUnitWidth) / 2 - ((rect.Width / nUnitWidth) / 6),
                (rect.Height - HouseGenerator.HouseHeight) / 2 - ((rect.Height - HouseGenerator.HouseHeight) / 6),
                (rect.Width / nUnitWidth) / 3,
                (rect.Height - HouseGenerator.HouseHeight) / 3);

            switch (RandomManager.GetInt(0, 3))
            {
                case 0:
                    {
                        Rectangle windowRect = new Rectangle(frameRect.Width / 10, frameRect.Width / 10, frameRect.Width - frameRect.Width / 5, frameRect.Height - frameRect.Width / 5);
                        //draw the glass(find a glasscolour)
                        spriteBatch.Draw(texture, windowRect, Color.Blue);
                        break;
                    }
                case 1:
                    {
                        Rectangle temp = new Rectangle(frameRect.X, frameRect.Y, frameRect.Width / 2, frameRect.Height);
                        Rectangle[] windowRect = new Rectangle[2];
                        windowRect[0] = new Rectangle(temp.Width / 10, temp.Width / 10, temp.Width - temp.Width / 5, temp.Height - temp.Width / 5);
                        windowRect[1] = new Rectangle(temp.Width + temp.Width / 10, temp.Width / 10, temp.Width - temp.Width / 5, temp.Height- temp.Width / 5);
                        spriteBatch.Draw(texture, windowRect[0], Color.Blue);
                        spriteBatch.Draw(texture, windowRect[1], Color.Blue);
                        break;
                    }
                case 2:
                    {
                        //windows arranged 0 1
                        //                 2 3
                        Rectangle temp = new Rectangle(frameRect.X, frameRect.Y, frameRect.Width / 2, frameRect.Height / 2);
                        Rectangle[] windowRect = new Rectangle[4];
                        windowRect[0] = new Rectangle(temp.Width / 10, temp.Width / 10, temp.Width - temp.Width / 5, temp.Height - temp.Width / 5);
                        windowRect[1] = new Rectangle(temp.Width + temp.Width / 10, temp.Width / 10, temp.Width - temp.Width / 5, temp.Height - temp.Width / 5);
                        windowRect[2] = new Rectangle(temp.Width / 10, temp.Height + temp.Width / 10, temp.Width - temp.Width / 5, temp.Height - temp.Width / 5);
                        windowRect[3] = new Rectangle(temp.Width + temp.Width / 10, temp.Height + temp.Width / 10, temp.Width - temp.Width / 5, temp.Height - temp.Width / 5);
                        break;
                    }
            }
            spriteBatch.End();
            return returnWindows(HouseGenerator.RenderGraphicsDevice(graphicsDevice, rect), rect);
        }
        public static void DrawFrames(SpriteBatch spriteBatch, Rectangle rect, Texture2D texture)
        {
            if (RandomManager.GetInt(0, 2) == 0)
            {
                //white window frames
                spriteBatch.Draw(texture, rect, Color.White);
            }
            else
            {
                //wooden window frames(brown)
                spriteBatch.Draw(texture, rect, Color.Brown);
            }
        }
        public static GameObject[] returnWindows(Texture2D window, Rectangle rect)
        {
            int nUnitWidth = rect.Width / HouseGenerator.HouseWidth;
            int nUnitHeight = (rect.Height / HouseGenerator.HouseHeight)-1;
            int count = 0;
            Rectangle[] frameRect = new Rectangle[nUnitHeight*nUnitWidth];
            for (int i = 1; i <= nUnitWidth; i++)
            {
                for (int j = 1; j <= nUnitHeight; j++)
                {
                    frameRect[count] = new Rectangle((rect.Width / i) - ((rect.Width / nUnitWidth) / 2) - ((rect.Width / nUnitWidth) / 6),
                      (rect.Height - (HouseGenerator.HouseHeight * j)) / 2 - ((rect.Height - HouseGenerator.HouseHeight) / 6),
                      (rect.Width / nUnitWidth) / 3,
                      (rect.Height - HouseGenerator.HouseHeight) / 3);
                    count++;
                }
            }
            GameObject[] windows = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                windows[i] = new GameObject(window, frameRect[i], 0.2f);
            }
            return windows;

        }
    }
}
