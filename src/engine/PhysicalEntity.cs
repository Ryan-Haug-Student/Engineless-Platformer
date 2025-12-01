using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        public Vector2 gravity = new Vector2(0, 2500f);

        public float maxVelocityX = 300f;
        public float maxVelocityY = 860f;

        //for friction the lower the value the more its slowed
        public float linearFriction = 0.9f;
        public bool grounded = false;

        //higher value more bounce
        public float restitution = 0f;

        public Shape sprite = new Rectangle { };

        //add update function here so that entities that need to run on update can access
        public virtual void update(double deltaTime)
        {
            //apply gravity and friction (vertical friction is havled)
            velocity += gravity * (float)deltaTime;

            CheckCollisions(deltaTime);

            //stop object completely if value is too small to prevent small odd values
            if (0 < velocity.X && velocity.X < 10 || velocity.X < 0 && velocity.X > -10)
                velocity.X = 0;

            //limit velocitys
            velocity.X = MathF.Abs(velocity.X) > maxVelocityX ?
                (velocity.X * maxVelocityX) / MathF.Abs(velocity.X) :
                velocity.X;

            velocity.Y = MathF.Abs(velocity.Y) > maxVelocityY ?
                (velocity.Y * maxVelocityY) / MathF.Abs(velocity.Y) :
                velocity.Y;

            grounded = velocity.Y == 0 ?
                true : false;

            float friction = grounded ? linearFriction : 0.99f;

            velocity.X *= friction;
            velocity.Y *= friction + ((1 - friction) / 2);

            //Trace.WriteLine(velocity);
            position += velocity * (float)deltaTime;
        }

        private void CheckCollisions(double deltaTime)
        {
            //check for collisions with **static** entities
            Vector2 futurePos = position + velocity * (float)deltaTime;
            foreach (var entity in WorldData.staticEntities)
            {
                //hoizontal colisions
                if (
                    futurePos.X < entity.position.X + entity.scale.X &&
                    futurePos.X + scale.X > entity.position.X &&
                    position.Y < entity.position.Y + entity.scale.Y &&
                    position.Y + scale.Y > entity.position.Y) 
                { velocity.X = 0; }

                //vertical collisions
                if (
                    position.X < entity.position.X + entity.scale.X &&
                    position.X + scale.X > entity.position.X &&
                    futurePos.Y < entity.position.Y + entity.scale.Y &&
                    futurePos.Y + scale.Y > entity.position.Y)
                {
                    //check for bounce
                    if (velocity.Y > 0) // falling
                    {
                        position.Y = entity.position.Y - scale.Y;
                        if (MathF.Abs(velocity.Y) < 50f)
                            velocity.Y = 0;

                        velocity.Y = -velocity.Y * restitution;
                    }
                    else // rising
                    {
                        position.Y = entity.position.Y + entity.scale.Y;
                        velocity.Y = -velocity.Y * restitution + .1f;
                    }
                    break;
                } // break the loop because there wont be multiple vertical colisions on the same frame
            }

            //Physics entities reset future pos based on the physical object colisions
            futurePos = position + velocity * (float)deltaTime;
            foreach (var e in WorldData.entities)
            {
                if (
                    e != this &&

                    futurePos.X < e.position.X + e.scale.X &&
                    futurePos.X + scale.X > e.position.X &&
                    futurePos.Y < e.position.Y + e.scale.Y &&
                    futurePos.Y + scale.Y > e.position.Y
                    ) // run on collision enter for both objects
                { OnCollisionEnter(e); e.OnCollisionEnter(this); }
            }
        }

        public virtual void OnCollisionEnter(PhysicalEntity collider)
        {
            
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
