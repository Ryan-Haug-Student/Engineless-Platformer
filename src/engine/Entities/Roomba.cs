using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Roomba : PhysicalEntity
    {
        Vector2 moveDir = new Vector2 (1, 0);
        float moveSpeed = 200f;

        float maxY = 10000f;

        public Roomba(Vector2 pos)
        {
            position = pos ;
            scale.X = WorldData.tileScale;
            scale.Y = WorldData.tileScale / 2;

            sprite.Width = scale.X;
            sprite.Height = scale.Y;
            sprite.Fill = Brushes.DarkRed;
        }

        public override void update(double deltaTime)
        {
            // move horizontally at constant speed
            velocity.X = moveDir.X * moveSpeed;

            base.update(deltaTime);

            // flip direction if horizontal velocity is zero (hit a wall)
            if (velocity.X == 0)
                moveDir.X *= -1;

            if (position.Y > maxY)
            {
                this.Destroy();
                Debug.WriteLine("destroyed " + this);
            }
        }
    }
}
