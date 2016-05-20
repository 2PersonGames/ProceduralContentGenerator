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
    public class GameObject
    {
        Rectangle rect;
        public Rectangle Rect
        {
            get { return rect; }
        }
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        Texture2D sprite;
        public Texture2D Sprite
        {
            get { return sprite; }
        }
        float depth;
        public float Depth
        {
            get { return depth; }
        }

        public GameObject(Texture2D nSprite, Rectangle nRect, float nDepth)
        {
            sprite = nSprite;
            rect = nRect;
            position = new Vector2(rect.X, rect.Y);
            depth = nDepth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0f, Vector2.Zero, 0f, SpriteEffects.None, depth);
        }

        public void Dispose()
        {
            sprite.Dispose();
        }
    }
}
