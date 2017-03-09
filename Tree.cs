using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lab1WPF.Logic
{
    public class Tree
    {
      
            private const int VertexCount = 1;
            public Color Color { get; private set; }
            public Point[] Points { get; private set; }

            public Tree()
            {
                var random = new Random((int)DateTime.Now.Ticks);

                Points = new Point[VertexCount];
                for (int i = 0; i < VertexCount; ++i)
                    Points[i] = TreeHelper.GetRandomPoint(random);

                Color = TreeHelper.GetRandomColor(random);
            }
        }

    internal static class TreeHelper
    {
        public static Point TextPosition(Tree tree, double width, double height)
        {
            double minY = double.MaxValue;
            double minX = double.MaxValue;

            foreach (var point in tree.Points)
            {
                if (point.Y < minY)
                    minY = point.Y;
                if (point.X < minX)
                    minX = point.X;
            }

            Point textPosition = new Point();

            if (minY < 0)
                textPosition.Y = (1 + minY) * 100 * height / 2 / 100;
            else
                textPosition.Y = minY * 100 * height / 2 / 100 + height / 2.0;

            if (minX < 0)
                textPosition.X = (1 + minX) * 100 * width / 2 / 100;
            else
                textPosition.X = minX * 100 * width / 2 / 100 + width / 2.0;

            return textPosition;
        }

        public static Point GetRandomPoint(Random random)
        {   
            return new Point()
            {
                X = GetRandomNumber(random),
                Y = GetRandomNumber(random)
            };
        }

        private static double GetRandomNumber(Random random)
        {
            return random.NextDouble() * (random.NextDouble() > 0.5 ? 1 : -1);
        }

        public static Color GetRandomColor(Random random)
        {
            return new Color
            {
                R = (byte)random.Next(255),
                G = (byte)random.Next(255),
                B = (byte)random.Next(255)
            };
        }
    }
}

