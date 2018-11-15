using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication2
{
    public class Star
    {
        int x, y, z;
        int sx, sy;
        int w, h;
        Ellipse circle;

        public Star(Random r, int width, int height)
        {
            x = r.Next(0, width);
            y = r.Next(0, height);
            z = r.Next(width);
            w = width;
            h = height;
        }

        public void update(Canvas Canvas2D)
        {
            z = z - 2;
            if (z < 1)
                z = w;
            sx = (int)((float)((float)x / (float)z) * w);
            sy = (int)((float)((float)y / (float)z) * h);
            Canvas.SetLeft(circle, sx);
            Canvas.SetTop(circle, sy);
        }

        public void initialize(Canvas Canvas2D)
        {
            circle = new Ellipse();
            circle.Width = 1;
            circle.Height = 1;
            circle.StrokeThickness = 2;
            circle.Stroke = Brushes.LightYellow;
            SolidColorBrush b = new SolidColorBrush();
            b.Color = Colors.LightYellow;
            circle.Fill = b;
            Canvas2D.Children.Add(circle);
        }
    }
}
