using SFML.System;
namespace Arkanoid_v2
{
    class Bonus : Object
    {
        protected int id;

        private bool destroyed;
        private float bonusWidth = 60.0f;
        private float bonusHeigth = 30.0f;
        private float bonusVelocity = 6.0f;

        public Bonus()
        {
        }
        public Bonus(int pos_X, int pos_Y)
        {
            rect.Position = new Vector2f(pos_X, pos_Y);
            rect.Size = new Vector2f(bonusWidth, bonusHeigth);
            rect.Origin = new Vector2f(this.bonusWidth / 2.0f, this.bonusHeigth / 2.0f);
            this.destroyed = false;
        }
        public void MoveDown()
        { 
            rect.Position = new Vector2f(rect.Position.X, rect.Position.Y+bonusVelocity);
            sprite.Position = rect.Position;
        }
        public void Destroy()
        {
            this.destroyed = true;
        }
        public bool IsDestroyed()
        {
            return this.destroyed;
        }
        public int GetId()
        {
            return this.id;
        }
    }
}
