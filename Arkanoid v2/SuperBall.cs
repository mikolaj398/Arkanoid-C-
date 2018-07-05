using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class SuperBall : Bonus
    {
        public int help = 0;

        private int duration = 10;
        private Clock superBallClock= new Clock();
        private Time superBallElapsedTime= new Time();

        public  SuperBall(int pos_X, int pos_Y) : base(pos_X, pos_Y)
        {
            rect.FillColor = Color.Cyan;
            id = 5;
            sprite = new Sprite(DataLoader.superBallBonus);
            sprite.Scale = new Vector2f(0.6f, 0.8f);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public SuperBall()
        {
        }
        public void Restart()
        {
            superBallClock.Restart();
        }
        public int ElapsedTime()
        {
            return (int)(this.superBallElapsedTime.AsSeconds());
        }
        public void GetTime()
        {
            this.superBallElapsedTime = superBallClock.ElapsedTime;
        }
        public int Duration()
        {
            return this.duration;
        }
    }
}
