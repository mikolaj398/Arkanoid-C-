using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class ExpandPaddle : Bonus
    {
        private int increase = 10;
        public ExpandPaddle(int pos_X, int pos_Y) : base(pos_X, pos_Y)
        {
            rect.FillColor = Color.Green;
            id = 1;
            sprite = new Sprite(DataLoader.expandPaddleBonus);
            sprite.Scale = new Vector2f(0.6f, 0.8f);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public ExpandPaddle()
        {
        }
        public void Expand(ref Paddle paddle)
        {
            paddle.ChangeSize(this.increase);
        }
    }
}
