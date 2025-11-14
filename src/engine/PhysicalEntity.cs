using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
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
        public Vector2 gravity = new Vector2 (0, 900f);

        public float linearFriction = 0.925f;

        public Shape sprite = new Rectangle { };

        //add update function here so that entities that need to run on update can access
        public virtual void update(double deltaTime)
        {
            //apply gravity and friction (vertical friction is havled)
            velocity += gravity * (float)deltaTime;
            velocity.X *= linearFriction;
            velocity.Y *= linearFriction + ((1 - linearFriction)/2);

            //check for collisions with **static** entities
            Vector2 futurePos = position + velocity * (float)deltaTime;
            foreach (var entity in WorldData.staticEntities)
            {
                //hoizontal colisions
                if (
                    futurePos.X < entity.position.X + entity.scale.X &&
                    futurePos.X + scale.X > entity.position.X && 
                    position.Y < entity.position.Y + entity.scale.Y &&
                    position.Y + scale.Y > entity.position.Y
                    ) { velocity.X = 0; }

                //vertical collisions
                if (
                    position.X < entity.position.X + entity.scale.X &&
                    position.X + scale.X > entity.position.X &&
                    futurePos.Y < entity.position.Y + entity.scale.Y &&
                    futurePos.Y + scale.Y > entity.position.Y
                    ) { velocity.Y = 0; }
            }

            //stop object completely if value is too small to prevent small odd values
            if (0 < velocity.X && velocity.X < 1)
                velocity.X = 0;

            Trace.WriteLine(velocity);
            position += velocity * (float)deltaTime;
        }

        // interpolate is a dynamic draw for smooth movement
        public virtual void Interpolate(double alpha)
        {
            Vector2 renderPos = Vector2.Lerp(previousPosition, position, (float)alpha);
            Canvas.SetLeft(sprite, renderPos.X);
            Canvas.SetTop(sprite, renderPos.Y);
        }

        public virtual void Destroy()
        {
            MainWindow.canvas.Children.Remove(sprite);
            WorldData.entities.Remove(this);
        }
    }
}
