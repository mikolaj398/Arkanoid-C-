using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class YellowPaddle : Paddle
    {
        public YellowPaddle():base()
        {
            rect.FillColor=Color.Yellow;
            active = false;
            price = 200;
            id = 0;
            sprite = new Sprite(DataLoader.paddles[0]);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                        sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
    }
}
