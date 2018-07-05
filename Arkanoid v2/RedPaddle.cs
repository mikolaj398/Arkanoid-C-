using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class RedPaddle:Paddle
    {
        public Paddle secondPaddle;
        public bool direction = true;

	    private const float secondPaddleWidth = 160.0f;
        private const float secondpaddleHeigth = 20.0f;
        private const float secondpaddleVelocity = 6.0f;
        private Vector2f secondVelocityVector;

        public RedPaddle(): base()
        {
            secondPaddle = new Paddle();
            secondPaddle.rect.Size=new Vector2f(secondPaddleWidth,secondpaddleHeigth );
            secondPaddle.rect.Origin=new Vector2f(secondPaddleWidth / 2.0f,secondpaddleHeigth / 2.0f);
            secondPaddle.rect.FillColor=Color.Red;
            secondPaddle.rect.Position= new Vector2f(Object.windowWidth / 2.0f, Object.windowHeigth - 10);

            this.rect.FillColor=Color.Red;
            active = false;
            price = 300;
            id = 1;
            sprite = new Sprite(DataLoader.paddles[1]);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                        sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public void UpdateSecondPaddle()
        {
            secondPaddle.rect.Position=new Vector2f(0f,secondVelocityVector.X);
            WhatDirection();
            if (direction == true) secondVelocityVector.X = -secondpaddleVelocity;
            else secondVelocityVector.X= secondpaddleVelocity;
        }
        void WhatDirection()
        {
            if (secondPaddle.rect.Position.X <= 0) direction = false;
            else if (secondPaddle.rect.Position.X >= windowWidth) direction = true;
        }
    }
}
