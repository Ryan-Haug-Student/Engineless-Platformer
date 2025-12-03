using EnginelessPhysics.src.engine.entities;
using EnginelessPhysics.src.game;
using System.Numerics;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Flag : PhysicalEntity
    {
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
        }

        public override void OnCollisionEnter(PhysicalEntity collider)
        {
            if (collider is Player)
                GameManager.LevelCompleted();
        }
    }
}
