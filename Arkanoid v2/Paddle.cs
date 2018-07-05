using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
namespace Arkanoid_v2
{
    class Paddle : Object
    {
        protected const float paddleWidth = 160.0f;
        protected const float paddleHeigth = 20.0f;
        protected const float paddleVelocity = 11.0f;
        protected Vector2f paddleVelocityVector = new Vector2f(0, 0);
        protected int control = 1;
        protected bool active = true;
        protected int price = 0;
        protected int id;

        public Paddle()
        {
            rect.Size = new Vector2f(paddleWidth, paddleHeigth);
            rect.Origin = new Vector2f(paddleWidth / 2.0f, paddleHeigth / 2.0f);
            rect.FillColor = Color.Red;
            sprite = new Sprite(DataLoader.paddles[4]);
            sprite.Origin = new Vector2f(rect.Origin.X - 18, rect.Origin.Y);
            sprite.Scale = new Vector2f(1.3f, 1.0f);
        }
        public void Reset()
        {
            rect.Size = new Vector2f(paddleWidth, paddleHeigth);
            rect.Origin = new Vector2f(paddleWidth / 2.0f, paddleHeigth / 2.0f);
            sprite.Scale = new Vector2f(1.3f, 1.0f);
        }
        public void Update()
        {
            rect.Position += paddleVelocityVector;
            sprite.Position = new Vector2f(rect.Position.X, rect.Position.Y);
            if (control == 1) NormalControl();
            else ReverseControl();
        }
        public void SetActive()
        {
            active = true;
        }
        public void Deactive()
        {
            active = false;
        }
        public bool IsActive()
        {
            return active;
        }
        public int GetId()
        {
            return this.id;
        }
        public int GetPrice()
        {
            return this.price;
        }
        public void ChangeControl()
        {
            this.control *= (-1);
        }
        void NormalControl()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Left)) && (Left() > 0))
                paddleVelocityVector.X = -paddleVelocity;
            else if ((Keyboard.IsKeyPressed(Keyboard.Key.Right)) && (Right() < Object.windowWidth))
                paddleVelocityVector.X = paddleVelocity;
            else paddleVelocityVector.X = 0.0f;
        }
        void ReverseControl()
        {
            if ((Keyboard.IsKeyPressed(Keyboard.Key.Right)) && (Left() > 0))
                paddleVelocityVector.X = -paddleVelocity;
            else if ((Keyboard.IsKeyPressed(Keyboard.Key.Left)) && (Right() < Object.windowWidth))
                paddleVelocityVector.X = paddleVelocity;
            else paddleVelocityVector.X = 0.0f;
        }
        public void ChangeSize(int increase)
        {
            float newWidth = rect.Size.X + increase;
            rect.Size = new Vector2f(newWidth, paddleHeigth);
            rect.Origin = new Vector2f(newWidth / 2.0f, paddleHeigth / 2.0f);
            sprite.Scale = new Vector2f(rect.Size.X / 115.0f, 1.0f);
        }
    }
}
