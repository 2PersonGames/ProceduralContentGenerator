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
    public static class RandomManager
    {
        public static Random rand;

        //this needs to be called in game 1, or wherever the program starts
        public static void Initialise()
        {
            rand = new Random();
        }

        public static int GetInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static Color GetRandomColour()
        {
            byte r = (byte)rand.Next(0, 256);
            byte g = (byte)rand.Next(0, 256);
            byte b = (byte)rand.Next(0, 256);
            return new Color(r, g, b);
        }
        public static double RandomDouble
        {
            get { return rand.NextDouble(); }
        }
    }
}