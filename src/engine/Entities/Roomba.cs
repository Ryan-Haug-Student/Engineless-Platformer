using EnginelessPhysics.src.engine.Components;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Roomba : PhysicalEntity
    {
        Vector2 moveDir = new Vector2(1, 0);
        float moveSpeed = 200f;

        private Vector2 origPos;

        float maxY = 10000f;

        Animator animator;
        private static BitmapImage activeAnim = new BitmapImage(new Uri("src/game/sprites/enemies/Roomba.png", UriKind.Relative));

        public Roomba(Vector2 pos)
        {
            position = pos;
            scale = new Vector2(WorldData.tileScale, WorldData.tileScale / 2);

            sprite = new Rectangle
            {
                Width = scale.X,
                Height = scale.X,
            };

            animator = new Animator(this, new Vector2(16, 16));
            animator.Play(activeAnim, 2, 250, true);
        }

        public override void update(double deltaTime)
        {
            // move horizontally at constant speed
            velocity.X = moveDir.X * moveSpeed;

            base.update(deltaTime);

            // flip direction if horizontal velocity is zero (hit a wall)
            if (velocity.X == 0)
                moveDir.X *= -1;

            animator.flipped = moveDir.X > 0 ? true : false;

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

        //override so it draws at the correct position due to halved size
        public override void Interpolate(double alpha)
        {
            Vector2 renderPos = Vector2.Lerp(previousPosition, position, (float)alpha);
            Canvas.SetLeft(sprite, renderPos.X);
            Canvas.SetTop(sprite, renderPos.Y - scale.Y);
        }
    }
}
