using System.Numerics;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine
{
    public abstract class Entity
    {
        public Vector2 position = Vector2.Zero;
        public Vector2 scale = Vector2.One;

        public Shape? sprite;

        //add update function here so that entities that need to run on update can access
        public abstract void update(double deltaTime);

        public virtual void Draw()
        {
            Canvas.SetLeft(sprite, position.X);
            Canvas.SetTop(sprite, position.Y);
        }

        public virtual void Destroy()
        {
            MainWindow.canvas.Dispatcher.Invoke(() => {
                MainWindow.canvas.Children.Remove(sprite);
                WorldData.staticEntities.Remove(this);
            });
        }
    }
}
