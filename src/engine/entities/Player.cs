using EnginelessPhysics.src.engine.Entities;
using EnginelessPhysics.src.game;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.entities
{
    public class Player : PhysicalEntity
    {
        //stats
        public float playerSpeed = 180f;
        public float jumpForce = 65f;

        public float lives = 4f;

        public float grappleDistance = 170f;
        private bool targeted = false;
        private Vector2 targetedPoint = Vector2.Zero;
        public static Target? target;

        public static Vector2 spawnPos;

        //input state (key held)
        private bool leftPressed;
        private bool rightPressed;

        private bool jumpPressed;
        private bool grapplePressed;

        public Player(Vector2 Pos, Vector2 Scale)
        {
            sprite = new Rectangle();
            sprite.Fill = Brushes.White;

            scale = Scale;
            sprite.Width = scale.X;
            sprite.Height = scale.Y;

            position = Pos;
            spawnPos = Pos;
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

        public override void OnCollisionEnter(PhysicalEntity collider)
        {
            if (collider is Spike || collider is Roomba)
                if (lives > 1)
                {   //undo inputs to stop player from immediately running off platform
                    leftPressed = false;
                    rightPressed = false;
                    jumpPressed = false;
                    grapplePressed = false;

                    velocity = Vector2.Zero;
                    position = spawnPos;
                    lives--;

                    MainWindow.canvas.Dispatcher.BeginInvoke(() =>
                    {
                        GameManager.UpdateUI();
                    });
                }
                else 
                { MainWindow.LoadMainMenu(); }
                    
        }

        //player input, using bools to keep smooth and continous movement even if opposite key is pressed
        public void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: jumpPressed = true; break;
                case Key.E: grapplePressed = true; break;

                case Key.A: leftPressed = true; break;
                case Key.D: rightPressed = true; break;

                case Key.R: Reset(); break;
                case Key.P: MainWindow.physicsRunning = MainWindow.physicsRunning ? false : true; break;
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: jumpPressed = false; break;
                case Key.E: grapplePressed = false; break;

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
                velocity.Y = -jumpForce * 1000 * (float)dt;

            if (grapplePressed && targeted)
                Grapple();
        }

        private void CheckForGrapple()
        {
            if (!targeted)
                foreach (Vector2 p in WorldData.grapplePoints)
                {
                    if (Vector2.Distance(p, position) < grappleDistance)
                    {   //push the creation to UI thread
                        MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            target = new Target(p, WorldData.tileScale / 2);
                            MainWindow.canvas.Children.Add(target.sprite);
                            target.Draw();
                        }));

                        targetedPoint = p;
                        targeted = true;
                        break;
                    }
                }
            else if (Vector2.Distance(targetedPoint, position) > grappleDistance && targetedPoint != Vector2.Zero)
            {   //push the deletion to UI thread
                MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                {
                    target.Destroy();
                }));
                targetedPoint = Vector2.Zero;
                targeted = false;
            }
        }

        private void Grapple()
        {
            if (Vector2.Distance(targetedPoint, position) > 30f) //based on distance either make the grapple weaker or stronger
                velocity += Vector2.Distance(targetedPoint, position) < 140f
                    ? (targetedPoint - position) : (targetedPoint - position) * 1.2f;
        }

        private void Reset()
        {
            velocity = Vector2.Zero;
            position = new Vector2(50, 100);
        }
    }
}
