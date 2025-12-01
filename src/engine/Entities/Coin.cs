using EnginelessPhysics.src.engine.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.Entities
{
    public class Coin : PhysicalEntity
    {
        private float trueScale = WorldData.tileScale * .75f;

        public Coin(Vector2 tilePos)
        {
            float tileScale = WorldData.tileScale;
            float coinScale = tileScale * 0.75f;

            // center coin inside the tile
            position = new Vector2(
                tilePos.X + (tileScale - coinScale) * 0.5f,
                tilePos.Y + (tileScale - coinScale) * 0.5f
            );

            scale = new Vector2(coinScale, coinScale);

            sprite = new Ellipse
            {
                Width = coinScale,
                Height = coinScale,
                Fill = Brushes.Yellow
            };
        }

        public override void update(double deltaTime) { }

        public override void OnCollisionEnter(PhysicalEntity collider)
        {
            if (collider is Player)
                MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() => { 
                    Destroy();
                }));
        }
    }
}
