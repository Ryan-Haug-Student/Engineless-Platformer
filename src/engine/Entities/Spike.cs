using System.Numerics;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Spike : PhysicalEntity
    {
        public Spike(Vector2 pos)
        {
            float tileSize = WorldData.tileScale;

            float spikeWidth = tileSize * 0.5f;
            float spikeHeight = tileSize * 0.5f;

            scale = new Vector2(spikeWidth, spikeHeight);

            // centered horizontally, bottom-aligned vertically
            position = new Vector2(
                pos.X + (tileSize - spikeWidth) * 0.5f,
                pos.Y + (tileSize - spikeHeight)
            );

            sprite = new Rectangle
            {
                Width = spikeWidth,
                Height = spikeHeight,
                Fill = Brushes.Red
            };
        }

        public override void update(double deltaTime) { }
    }
}
