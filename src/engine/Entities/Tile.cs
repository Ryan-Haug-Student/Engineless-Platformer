using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Tile : Entity
    {
        public Tile(Vector2 position, float scale)
        {
            this.position = position;
            this.scale.X = scale;
            this.scale.Y = scale;
        }

        public override void update(double deltaTime) {}
    }
}
