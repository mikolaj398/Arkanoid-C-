using SFML.Graphics;
using SFML.System;
namespace Arkanoid_v2
{
    class BluePaddle : Paddle
    {
        public Ball secondBall;
        public BluePaddle():base()
        {
            secondBall = new Ball();
            secondBall.ChangeColor(Color.Blue);
            rect.FillColor=Color.Blue;
            active = false;
            price = 500;
            id = 3;
            sprite = new Sprite(DataLoader.paddles[3]);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                        sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
    }
}
