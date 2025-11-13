using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine
{
    public abstract class PhysicalEntity
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 previousPosition = Vector2.Zero;
        public Vector2 scale = Vector2.One;

        //physics vars
        public Vector2 velocity = Vector2.Zero;
        public Vector2 gravity = new Vector2 (0, 9.81f);
        public float gravityScale = 1.0f;

        public Shape sprite = new Rectangle { };

        //add update function here so that entities that need to run on update can access
        public abstract void update(double deltaTime);

        // interpolate is a dynamic draw for smooth movement
        public virtual void Interpolate(double alpha)
        {
            Vector2 renderPos = Vector2.Lerp(previousPosition, position, (float)alpha);
            Canvas.SetLeft(sprite, renderPos.X);
            Canvas.SetTop(sprite, renderPos.Y);
        }

        //static 
        public virtual void Draw()
        {
            Canvas.SetLeft(sprite, position.X);
            Canvas.SetTop(sprite, position.Y);
        }

        public virtual void Destroy()
        {
            MainWindow.canvas.Children.Remove(sprite);
            MainWindow.entities.Remove(this);
        }
    }
}
