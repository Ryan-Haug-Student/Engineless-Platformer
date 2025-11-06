using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

using EnginelessPhysics;

namespace EnginelessPhysics.src.engine
{
    public class Entity
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 scale = Vector2.One;

        public Shape sprite = new Rectangle { };

        public virtual void Draw()
        {
            Canvas.SetLeft(sprite, position.X);
            Canvas.SetTop(sprite, position.Y);
        }

        public virtual void Destroy()
        {
            MainWindow.canvas.Children.Remove(this.sprite);
        }
    }
}
