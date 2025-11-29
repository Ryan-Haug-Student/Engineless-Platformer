using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Spike : PhysicalEntity
    {
        public Spike(Vector2 pos) 
        {
            position = new Vector2(pos.X + WorldData.tileScale / 2, pos.Y + WorldData.tileScale / 2);
            scale.X = WorldData.tileScale / 2;
            scale.Y = WorldData.tileScale / 2;

            sprite.Width = scale.X;
            sprite.Height = scale.Y;
            sprite.Fill = Brushes.Red;

            gravity = Vector2.Zero;
        }

        public override void update(double deltaTime){}
    }
}
