using System.Numerics;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Roomba : PhysicalEntity
    {
        Vector2 moveDir = new Vector2(1, 0);
        float moveSpeed = 200f;

        float maxY = 10000f;

        public Roomba(Vector2 pos)
        {
            position = pos;
            scale = new Vector2(WorldData.tileScale, WorldData.tileScale / 2);

            sprite = new Rectangle
            {
                Width = scale.X,
                Height = scale.Y,
                Fill = Brushes.DarkRed
            };
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
            { //stop object falling to prevent extreme numbers
                gravity = Vector2.Zero;
                velocity = Vector2.Zero;
            }
        }

        public override void OnCollisionEnter(PhysicalEntity collider)
        {
            if (collider is Roomba)
                moveDir *= -1;
        }
    }
}
