using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class ShortenPaddle : Bonus
    {
        private int decrease = -10;

        public ShortenPaddle(int pos_X, int pos_Y) : base(pos_X, pos_Y)
        {
            rect.FillColor = Color.Red;
            id = 2;
            sprite = new Sprite(DataLoader.shortenPaddleBonus);
            sprite.Scale = new Vector2f(0.6f, 0.8f);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public ShortenPaddle()
        { 
        }
        public void Shorten(ref Paddle paddle)
        {
            paddle.ChangeSize(this.decrease);
        }
    }
}
