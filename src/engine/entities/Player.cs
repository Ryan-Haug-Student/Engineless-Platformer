using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnginelessPhysics.src.engine.entities
{
    public class Player : Entity
    {
        //movement vars
        public float playerSpeed = 250f;

        //input state (key held)
        private bool leftPressed;
        private bool rightPressed;
        private bool upPressed;
        private bool downPressed;

        public Player()
        {
            sprite = new Rectangle();
            sprite.Fill = Brushes.White;

            sprite.Width = 50;
            sprite.Height = 100;

            position = new Vector2(100, 100);
            previousPosition = position;
        }

        public override void update(double deltaTime)
        {
            Move(deltaTime);
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
                case Key.W: upPressed = true; break;
                case Key.A: leftPressed = true; break;
                case Key.S: downPressed = true; break;
                case Key.D: rightPressed = true; break;
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: upPressed = false; break;
                case Key.A: leftPressed = false; break;
                case Key.S: downPressed = false; break;
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
            if (upPressed) moveDir.Y -= 1;
            if (downPressed) moveDir.Y += 1;

            //apply move direction
            if (moveDir.LengthSquared() > 0)
            {
                Vector2 moveDirNormal = Vector2.Normalize(moveDir);
                position += moveDirNormal * playerSpeed * (float)dt;
            }
        }
    }
}
