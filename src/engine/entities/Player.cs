using EnginelessPhysics.src.engine.Components;
using EnginelessPhysics.src.engine.Entities;
using EnginelessPhysics.src.game;
using System.ComponentModel;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        //animations
        private Animator animator;
        private BitmapImage playerIdle = new BitmapImage(new Uri("src/game/sprites/player/Wisp.png", UriKind.Relative));  //6 frames
        private BitmapImage playerWalk = new BitmapImage(new Uri("src/game/sprites/player/WispWalk.png", UriKind.Relative)); //4 frames
        private BitmapImage playerFall = new BitmapImage(new Uri("src/game/sprites/player/WispFall.png", UriKind.Relative)); //3 frames

        private int currentAnim = -1;

        public Player(Vector2 Pos, Vector2 Scale)
        {
            sprite = new Rectangle();
            sprite.Fill = Brushes.White;

            scale = Scale;
            sprite.Width = scale.X * 1.2f;
            sprite.Height = scale.Y * 1.2f;

            position = Pos;
            spawnPos = Pos;
            previousPosition = position;

            animator = new Animator(this, new Vector2(32, 48));
            
        }

        public override void update(double deltaTime)
        {
            linearFriction = .86f;
            base.update(deltaTime);

            Move(deltaTime);
            CheckForGrapple();
        }

        // Interpolated render between previousPosition and position
        public override void Interpolate(double alpha)
        {
            Vector2 renderPos = Vector2.Lerp(previousPosition, position, (float)alpha);
            Canvas.SetLeft(sprite, renderPos.X - (scale.X - (scale.X * .8f)));
            Canvas.SetTop(sprite, renderPos.Y - (scale.Y - (scale.Y * .8f)));

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
                { GameManager.currencyCount = 0;  MainWindow.LoadMainMenu(); }
                    
        }

        //player input, using bools to keep smooth and continous movement even if opposite key is pressed
        public void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: case Key.W: jumpPressed = true; break;
                case Key.LeftShift: grapplePressed = true; break;

                case Key.A: leftPressed = true; break;
                case Key.D: rightPressed = true; break;

                case Key.P: MainWindow.physicsRunning = MainWindow.physicsRunning ? false : true; break;
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: case Key.W: jumpPressed = false; break;
                case Key.LeftShift: grapplePressed = false; break;

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
            {
                // find closest point inside distance
                Vector2 bestTarget = Vector2.Zero;
                float bestDist = float.MaxValue;

                foreach (Vector2 p in WorldData.grapplePoints)
                {
                    float d = Vector2.Distance(p, position);
                    if (d < grappleDistance && d < bestDist)
                    {
                        bestDist = d;
                        bestTarget = p;
                    }
                }

                if (bestTarget != Vector2.Zero)
                {
                    MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        target = new Target(bestTarget, WorldData.tileScale / 2);
                        MainWindow.canvas.Children.Add(target.sprite);
                        target.Draw();
                    }));

                    targetedPoint = bestTarget;
                    targeted = true;
                }
            }

            else
            {
                // if we already have a target, check if a closer one exists
                Vector2 best = Vector2.Zero;
                float bestDist = float.MaxValue;

                foreach (Vector2 p in WorldData.grapplePoints)
                {
                    float d = Vector2.Distance(p, position);
                    if (d < grappleDistance && d < bestDist)
                    {
                        bestDist = d;
                        best = p;
                    }
                }

                // no points in range then remove old
                if (best == Vector2.Zero)
                {
                    MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                    {
#pragma warning disable CS8602
                        target.Destroy();
                    }));
                    targetedPoint = Vector2.Zero;
                    targeted = false;
                    return;
                }

                // a different point is closer then switch target
                if (best != targetedPoint)
                {
                    MainWindow.canvas.Dispatcher.BeginInvoke(new Action(() =>
                    {
#pragma warning disable CS8602
                        target.Destroy();

                        target = new Target(best, WorldData.tileScale / 2);
                        MainWindow.canvas.Children.Add(target.sprite);
                        target.Draw();
                    }));

                    targetedPoint = best;
                }
            }
        }


        private void Grapple()
        {
            if (Vector2.Distance(targetedPoint, position) > 30f) //based on distance either make the grapple weaker or stronger
                velocity += Vector2.Distance(targetedPoint, position) < 120f
                    ? (targetedPoint - position) : (targetedPoint - position) * 1.2f;
        }

        public void AnimController()
        {
            int newAnim;

            if (velocity.Y > 0) newAnim = 1;        // falling
            else if (velocity.X != 0) newAnim = 2;  // walking
            else newAnim = 0;                       // idle

            if (newAnim != currentAnim)
            {
                currentAnim = newAnim;

                switch (currentAnim)
                {
                    case 0: animator.Play(playerIdle, 6, 1000, true); break;
                    case 1: animator.Play(playerFall, 3, 500, true); break;
                    case 2: animator.Play(playerWalk, 4, 500, true); break;
                }
            }

            animator.flipped = velocity.X < 0;
        }
    }
}
