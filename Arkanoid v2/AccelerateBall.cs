using SFML.Graphics;
using SFML.System;
namespace Arkanoid_v2
{
    class AccelerateBall : Bonus
    {
        private int acceleration = 1;//zmienna typu całkowitego

        public AccelerateBall(int pos_X, int pos_Y) : base(pos_X, pos_Y) //konstruktor który przyjmuje dwie wartości tylu całkowitego 
        {
            rect.FillColor = Color.White;
            id = 3; //ustawiamy Id

            //ustawiamy teksture
            sprite = new Sprite(DataLoader.accelerateBallBonus);
            sprite.Scale = new Vector2f(0.6f, 0.8f);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public AccelerateBall()
        { 
        }
        public void Accelerate(ref Ball ball) //metoda typu pustego 
        {
            ball.ChangeSpeed(this.acceleration);
        }
    }
}
