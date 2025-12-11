using EnginelessPhysics.src.engine.Components;
using EnginelessPhysics.src.engine.entities;
using EnginelessPhysics.src.game;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Flag : PhysicalEntity
    {
        private Animator animator;
        private static BitmapImage activeAnim = new BitmapImage(new Uri("src/game/sprites/interactables/Portal.png", UriKind.Relative));

        public Flag(Vector2 pos)
        {
            float tileSize = WorldData.tileScale;

            float flagWidth = tileSize;
            float flagHeight = tileSize * 2;

            scale = new Vector2(flagWidth, flagHeight);

            // centered horizontally, bottom-aligned vertically
            position = new Vector2(
                pos.X + (tileSize - flagWidth),
                pos.Y + (tileSize - flagHeight)
            );

            sprite = new Rectangle
            {
                Width = flagWidth,
                Height = flagHeight,
                Fill = Brushes.DarkGoldenrod
            };

            animator = new Animator(this, new Vector2(16, 32));
            animator.Play(activeAnim, 6, 2000, true);
        }

        public override void OnCollisionEnter(PhysicalEntity collider)
        {
            if (collider is Player)
                GameManager.LevelCompleted();
        }
    }
}
