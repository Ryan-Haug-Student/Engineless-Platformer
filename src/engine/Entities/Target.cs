using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Target : Entity {
        public Target(Vector2 pos, float scale)
        {
            position = pos + new Vector2(scale, scale) /2;
            sprite = new Ellipse();
            sprite.Fill = Brushes.CadetBlue;
            sprite.Stroke = Brushes.Blue;
            sprite.StrokeThickness = 4;

            sprite.Width = scale;
            sprite.Height = scale;
        }
        public override void update(double deltaTime) {}
    }

}
