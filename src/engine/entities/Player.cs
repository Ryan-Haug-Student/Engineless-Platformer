using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace EnginelessPhysics.src.engine.entities
{
    public class Player : PhysicalEntity
    {
        //movement vars
        public float playerSpeed = 160f;
        public float jumpForce = 55f;

        public float grappleDistance = 150f;
        private bool targeted = false;
        private Vector2 targetedPoint = Vector2.Zero;

        //input state (key held)
        private bool leftPressed;
        private bool rightPressed;
        private bool jumpPressed;

        public Player(Vector2 Pos, Vector2 Scale)
        {
            sprite = new Rectangle();
            sprite.Fill = Brushes.White;

            scale = Scale;
            sprite.Width = scale.X;
            sprite.Height = scale.Y;

            position = Pos;
            previousPosition = position;
        }

        public override void update(double deltaTime)
        {
            base.update(deltaTime);

            Move(deltaTime);
            CheckForGrapple();
        }

        // Interpolated render between previousPosition and position
        public override void Interpolate(double alpha)
        {
            Vector2 renderPos = Vector2.Lerp(previousPosition, position, (float)alpha);
            Canvas.SetLeft(sprite, renderPos.X);
            Canvas.SetTop(sprite, renderPos.Y);

            MainWindow.MoveCamera(renderPos);
        }

        //player input, using bools to keep smooth and continous movement even if opposite key is pressed
        public void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: jumpPressed = true; break;

                case Key.A: leftPressed = true; break;
                case Key.D: rightPressed = true; break;

                case Key.R: Reset(); break;
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: jumpPressed = false; break;

                case Key.A: leftPressed = false; break;
                case Key.D: rightPressed = false; break;
            }
        }

        // ======================================

        private void Move(double dt)
        {
            //create a movement vector
            Vector2 moveDir = Vector2.Zero;
            if (leftPressed) moveDir.X -= 1;
            if (rightPressed) moveDir.X += 1;

            //apply move direction
            if (moveDir.X != 0)
            {
                Vector2 moveDirNormal = Vector2.Normalize(moveDir);
                velocity += moveDirNormal * playerSpeed * 10 * (float)dt;
            }

            if (jumpPressed && grounded)
            {
                velocity.Y = -jumpForce * 1000 * (float)dt;
            }
        }

        private void CheckForGrapple()
        {
            if (!targeted)
                foreach (Vector2 p in WorldData.grapplePoints)
                {
                    if (Vector2.Distance(p, position) < grappleDistance)
                    {
                        Trace.WriteLine($"targeted at {p}");
                        targetedPoint = p;
                        targeted = true;
                        break;
                    }
                }
            else if (Vector2.Distance(targetedPoint, position) > grappleDistance)
            {
                Trace.WriteLine("detargeted");
                targeted = false;
            }
        }

        private void Reset()
        {
            velocity = Vector2.Zero;
            position = new Vector2(50, 100);
        }
    }
}
