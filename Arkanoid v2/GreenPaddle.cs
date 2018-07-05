using SFML.Graphics;
using SFML.System;
namespace Arkanoid_v2
{
    class GreenPaddle : Paddle
    {
        private int lives = 9;

        public GreenPaddle():base()
        {
            rect.FillColor = Color.Green;
            active = false;
            price = 400;
            id = 2;
            sprite = new Sprite(DataLoader.paddles[2]);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public void RemoveLife()
        {
            this.lives--;
        }
        public int HowManyLivesLeft()
        {
            return lives;
        }
    }
}
