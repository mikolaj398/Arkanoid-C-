using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class Ball : Drawable
    {
        private CircleShape circle;
        private const float ballRadius = 10.0f;
        private const float ballVelocity = 10.0f;
        private float ballSpeed;
        private Vector2f ballVelocityVector = new Vector2f(5.0f, 5.0f);

        public Ball() //konstruktor domyślny
        {
            circle = new CircleShape(ballRadius);
            circle.Origin = new Vector2f(ballRadius / 2f, ballRadius / 2f);
            circle.FillColor = Color.White;
            ballSpeed = ballVelocity;
        }
        public void Draw(RenderTarget target, RenderStates states) // funkcja imolementowana z interfejsu Drawable umożliwia wyświetlnie kształtu na ekranie
        {
            target.Draw(this.circle, states);
        }
        public void Update()
        {
            circle.Position += ballVelocityVector;

            if (Left() < 0) MoveRight();
            else if (Left() > Object.windowWidth) MoveLeft();

            if (Top() < 0) MoveDown();
            else if (Bottom() > Object.windowHeigth) MoveUp();
        }
        public float Left()
        {
            return this.circle.Position.X - this.circle.Radius;
        }
        public float Right()
        {
            return this.circle.Position.X + this.circle.Radius;
        }
        public float Top()
        {
            return this.circle.Position.Y - this.circle.Radius;
        }
        public float Bottom()
        {
            return this.circle.Position.Y + this.circle.Radius;
        }
        public void MoveLeft()
        {
            ballVelocityVector.X = -ballSpeed;
        }
        public void MoveRight()
        {
            ballVelocityVector.X = ballSpeed;
        }
        public void MoveUp()
        {
            ballVelocityVector.Y = -ballSpeed;
        }
        public void MoveDown()
        {
            ballVelocityVector.Y = ballSpeed;
        }
        public void MoveStraightDown()
        {
            ballVelocityVector.Y = ballSpeed;
            ballVelocityVector.X = 0;
        }
        public Vector2f GetPosition()
        {
            return circle.Position;
        }
        public void SetPosition(int pos_X, int pos_Y)
        {
            circle.Position = new Vector2f(pos_X, pos_Y);
        }
        public float GetRadius()
        {
            return ballRadius;
        }
        public void ChangeSpeed(int change)
        {
            if ((change < 0) && (this.ballSpeed <= 2)) return;
            this.ballSpeed += change;
        }
        public void Reset()
        {
            this.ballSpeed = ballVelocity;
        }
        public void ChangeColor(Color color)
        {
            circle.FillColor = color;
        }
    }
}
