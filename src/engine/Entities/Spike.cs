using System.Numerics;
using System.Printing;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Spike : PhysicalEntity
    {
        private static BitmapImage sheet = new BitmapImage(new Uri("src/game/sprites/enemies/spikes.png", UriKind.Relative));

        CroppedBitmap[] sprites =
        {
            new CroppedBitmap(sheet, new Int32Rect(0,  0, 16, 16)),
            new CroppedBitmap(sheet, new Int32Rect(16, 0, 16, 16)),
            new CroppedBitmap(sheet, new Int32Rect(32, 0, 16, 16))
        };

        Vector2 origPos;

        public Spike(Vector2 pos)
        {
            origPos = pos;

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
                Width = tileSize,
                Height = tileSize,
                Fill = new ImageBrush(sprites[new Random().Next(0, 3)]) 
                {
                    Stretch = Stretch.Fill,
                    AlignmentX = AlignmentX.Center,
                    AlignmentY = AlignmentY.Bottom,
                }
            };
        }

        public override void Interpolate(double alpha)
        {
            Canvas.SetTop(sprite, origPos.Y);
            Canvas.SetLeft(sprite, origPos.X);
        }

        public override void update(double deltaTime) { }
    }
}
