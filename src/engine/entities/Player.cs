using System;
using System.Collections.Generic;
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
        public float playerSpeed = 70f;
        private Vector2 moveDir = new Vector2(1, 0);

        public Player()
        {
            sprite = new Rectangle();
            sprite.Fill = Brushes.White;

            sprite.Width = 50;
            sprite.Height = 100;

            position = new Vector2(100, 100);
        }

        public override void update(double deltaTime)
        {
            Move(deltaTime);
        }

        public override void Draw()
        {
            Canvas.SetLeft(sprite, position.X);
            Canvas.SetTop(sprite, position.Y);
        }

        public void OnKeyDown(KeyEventArgs e)
        {

        }

        public void OnKeyUp(KeyEventArgs e)
        {

        }

        // ======================================

        private void Move(double dt)
        {
            Vector2 moveDirection = Vector2.Normalize(moveDir);

            position += moveDirection * playerSpeed * (float)dt;
        }
    }
}
