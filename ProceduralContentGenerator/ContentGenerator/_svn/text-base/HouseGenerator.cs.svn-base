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
    public static class HouseGenerator
    {
        //This is how many different brick textures are saved
        const int NumberOfBricks = 1;
        //The width and height of each unit of a house
        public const int HouseWidth = 100;
        public const int HouseHeight = 100;

        static Texture2D[] bricks;
        static Texture2D blank;

        public static void Load(ContentManager Content)
        {
            blank = Content.Load<Texture2D>("HouseAssets\\blank");
            bricks = new Texture2D[NumberOfBricks];
            for (int i = 0; i < NumberOfBricks; i++)
            {
                bricks[i] = Content.Load<Texture2D>("HouseAssets\\brick" + i.ToString());
            }
        }

        public static GameObject[] GenerateHouse(Rectangle map, float density, GraphicsDevice graphicsDevice, ContentManager Content)
        {
            //Make sure the class has been loaded
            if (blank == null)
            {
                Load(Content);
            }

            //Calculate number of houses
            int total = CalculateDensity(density, map.Width);

            //Instantiate array
            GameObject[] houses = new GameObject[total];

            //Instantiate a SpriteBatch for rendering textures
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);

            for (int i = 0; i < total; i++)
            {
                //Calculate the rectangle the building should fit in
                //Using the density float
                Rectangle rect = CalculateHouse(i, total, map);

                //Select a brick texture to use
                Texture2D brick = bricks[RandomManager.GetInt(0, bricks.Length)];

                //Instantiate the house's elements
                GameObject building = GenerateBuilding(rect, brick, graphicsDevice);
                GameObject roof = GenerateRoof(rect, brick, graphicsDevice);
                GameObject chimney = GenerateChimney(rect, brick, graphicsDevice);
                GameObject[] windows = GenerateWindows(blank, rect, graphicsDevice);
                GameObject door = GenerateDoor(blank, rect, graphicsDevice);

                //Draw all the elements to the graphics card
                graphicsDevice.Clear(Color.White);
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
                chimney.Draw(spriteBatch);
                building.Draw(spriteBatch);
                roof.Draw(spriteBatch);
                foreach (GameObject window in windows)
                {
                    window.Draw(spriteBatch);
                }
                door.Draw(spriteBatch);
                spriteBatch.End();

                //Render the house to a texture
                //Don't know if this will work
                Texture2D houseSprite = RenderGraphicsDevice(graphicsDevice, rect);

                //Push house on to array
                houses[i] = new GameObject(houseSprite, rect, 0.2f);

                //Dispose of the GameObjects
                building.Dispose();
                roof.Dispose();
                chimney.Dispose();
                foreach (GameObject window in windows)
                {
                    window.Dispose();
                }
                door.Dispose();
            }

            return houses;
        }

        static GameObject GenerateBuilding(Rectangle rect, Texture2D brick, GraphicsDevice graphicsDevice)
        {
            //Modify the rectangle so it's only the building, not the roof
            Rectangle buildingRect = new Rectangle(rect.X, rect.Y + HouseHeight, rect.Width, rect.Height - HouseHeight);

            //Create the GameObject with the correct sprite
            GameObject building = new GameObject(BuildingGenerator.GenerateBuilding(brick, buildingRect, graphicsDevice), rect, 0.2f);
            //GameObject building = new GameObject(blank, rect, 0.2f);

            return building;
        }

        static GameObject GenerateRoof(Rectangle rect, Texture2D brick, GraphicsDevice graphicsDevice)
        {
            //Modify the rectangle so it's only the roof
            Rectangle roofRect = new Rectangle(rect.X, rect.Y, rect.Width, HouseHeight);

            //Create the GameObject with the correct sprite
            GameObject roof = new GameObject(RoofGenerator.GenerateRoof(brick, roofRect, graphicsDevice), roofRect, 0.2f);
            //GameObject roof = new GameObject(blank, roofRect, 0.2f);

            return roof;
        }

        static GameObject GenerateChimney(Rectangle rect, Texture2D brick, GraphicsDevice graphicsDevice)
        {
            //Modify the rectangle so it's only the chimney
            Rectangle chimneyRect = new Rectangle(rect.X + (int)Math.Round(rect.Width / 3f), rect.Y, (int)Math.Round(rect.Width / 6f), HouseHeight);

            //Create the GameObject with the correct sprite
            GameObject chimney = new GameObject(ChimneyGenerator.GenerateChimney(brick, chimneyRect, graphicsDevice), chimneyRect, 0.2f);
            //GameObject chimney = new GameObject(blank, chimneyRect, 0.2f);

            return chimney;
        }

        static GameObject[] GenerateWindows(Texture2D blank, Rectangle rect, GraphicsDevice graphicsDevice)
        {
            GameObject[] windows = WindowGenerator.GenerateWindows(blank, rect, graphicsDevice);

            return windows;
        }

        static GameObject GenerateDoor(Texture2D blank,Rectangle rect, GraphicsDevice graphicsDevice)
        {
            Rectangle doorRect = new Rectangle(rect.Width / 2 - rect.Width / 16, rect.Height - rect.Height / 3, rect.Width / 8, rect.Height / 3);

            GameObject door = new GameObject(DoorGenerator.generateDoor(blank, doorRect, graphicsDevice), doorRect, 0.2f);
            //GameObject door = new GameObject(blank, doorRect, 0.2f);

            return door;
        }

        public static Texture2D RenderGraphicsDevice(GraphicsDevice graphicsDevice, Rectangle rect)
        {
            PresentationParameters pp = graphicsDevice.PresentationParameters;
            ResolveTexture2D resolveTexture = new ResolveTexture2D(graphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight, 1, graphicsDevice.DisplayMode.Format);

            graphicsDevice.ResolveBackBuffer(resolveTexture);
            resolveTexture.Save("test.jpg", ImageFileFormat.Jpg);
            Texture2D returnTexture = (Texture2D)resolveTexture;

            return returnTexture;
        }

        static int CalculateDensity(float density, int length)
        {
            double result = density * length;
            //return (int)Math.Round(Math.Pow(result, 2));
            return 1;
        }

        static Rectangle CalculateHouse(int number, int total, Rectangle map)
        {
            Rectangle rect = new Rectangle();

            rect.X = (int)Math.Round((map.Width / (double)total) * number);
            rect.Y = map.Height + map.Y;

            //Currently house width is always two units
            rect.Width = HouseWidth * 2;

            //Calculates the maximum number of floors a house can have before leaving the map
            int maxNumberOfFloors = (int)Math.Floor(map.Height / (double)HouseHeight);
            int numberOfFloors = 0;

            //Calculates the house's height
            int temp = rect.X - map.Width;
            if (temp < 0)
            {
                temp = -temp;
            }
            numberOfFloors = (int)Math.Round(temp / (double)rect.Width);

            //Insures the house is atleast two floors high
            numberOfFloors = (int)MathHelper.Clamp(numberOfFloors, 2, maxNumberOfFloors);
            rect.Height = HouseWidth * numberOfFloors;

            //Moves the house up into the map
            rect.Y -= rect.Height;

            return rect;
        }
    }
}
