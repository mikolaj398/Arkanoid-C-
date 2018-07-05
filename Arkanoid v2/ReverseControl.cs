using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class ReverseControl : Bonus
    {
        public ReverseControl(int pos_X, int pos_Y) : base(pos_X, pos_Y)
        {
            rect.FillColor = new Color(123, 123, 123);
            id = 6;

            sprite = new Sprite(DataLoader.reverseControlBonus);
            sprite.Scale = new Vector2f(0.6f, 0.8f);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public ReverseControl()
        {
        }
    }
}
