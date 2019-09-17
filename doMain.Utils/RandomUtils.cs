using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    public class RandomUtils
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int minimum, int maximum)
        {
            lock (syncLock)
                return random.Next(minimum, maximum + 1);
            
        }

        public static Color GetRandomColor()
        {
            lock (syncLock)
                return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        //public static Color GetRandomColor()
        //{
        //    lock (syncLock)
        //        return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        //}

        //        private static List<string> GetColors()
        //        {
        //            create a generic list of strings
        //            List<string> colors = new List<string>();
        //            get the color names from the Known color enum
        //            string[] colorNames = Enum.GetNames(typeof(KnownColor));
        //        iterate thru each string in the colorNames array
        //            foreach (string colorName in colorNames)
        //            {
        //                cast the colorName into a KnownColor
        //                KnownColor knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), colorName);
        //        check if the knownColor variable is a System color
        //                if (knownColor > KnownColor.Transparent)
        //                {
        //                    add it to our list
        //                    colors.Add(colorName);
        //    }
        //}
        //            return the color list
        //            return colors;
        //        }

        public static string GetRandomString(string[] names)
        {
            lock (syncLock)
            {
                //Random randomGen = new Random();

                string randomColorName = names[random.Next(names.Length)];
                //Color randomColor = Color.FromKnownColor(randomColorName);
                return randomColorName;
            }
        }

        public static Color GetRandomBasicColor()
        {
            var names = new List<KnownColor>(); //(KnownColor[])Enum.GetValues(typeof(KnownColor));
            //names.Add(KnownColor.Blue);
            names.Add(KnownColor.SlateBlue);
            names.Add(KnownColor.Orange);
            names.Add(KnownColor.OrangeRed);
            names.Add(KnownColor.Green);
            names.Add(KnownColor.SteelBlue);
            names.Add(KnownColor.YellowGreen);
            names.Add(KnownColor.RoyalBlue);

            names.Add(KnownColor.Chocolate);
            names.Add(KnownColor.Gold);
            names.Add(KnownColor.Goldenrod);

            lock (syncLock)
            {
                //Random randomGen = new Random();
                
                KnownColor randomColorName = names[random.Next(names.Count)];
                Color randomColor = Color.FromKnownColor(randomColorName);
                return randomColor;
            }
        }
    }
}
