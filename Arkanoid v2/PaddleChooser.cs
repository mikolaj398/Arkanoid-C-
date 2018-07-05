using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class PaddleChooser : Object
    {
        public static int howManyPaddles = 5;
        public RectangleShape[] paddlesPictures;
        public Text[] paddlesNamesText;
        public Sprite[] paddlesSprites;
        public RectangleShape[] blackRecktangles;

        private bool buttonReleased = false;
        private float pladdlesPicturesWidth = 230;
        private float pladdlesPicturesHeigth = 100;
        private float blackRectangleWidth;
        private float blackRectangleHeigth;

        public  PaddleChooser()
        {
            blackRectangleWidth = pladdlesPicturesWidth - 10;
            blackRectangleHeigth = pladdlesPicturesHeigth - 10;
            SetPaddleChooserTables();
            SetPaddlesPictures();
            SetPaddlesTexts();
        }
        void SetPaddleChooserTables()
        {
            paddlesPictures = new RectangleShape[howManyPaddles];
            paddlesNamesText = new Text[howManyPaddles];
            paddlesSprites = new Sprite[howManyPaddles];
            blackRecktangles = new RectangleShape[howManyPaddles];
        }
        void SetSprites()
        {
            for (int i = 0; i < howManyPaddles; i++)
            {
                paddlesSprites[i] = new Sprite(DataLoader.paddles[i]);
                paddlesSprites[i].Position = new Vector2f(paddlesPictures[i].Position.X - 108, paddlesPictures[i].Position.Y - 22);
                paddlesSprites[i].Scale = new Vector2f(1.7f, 1.7f);
            }
        }
        void SetPaddlesPictures()
        {
            for (int i = 0; i < howManyPaddles; i++)
            {
                paddlesPictures[i] = new RectangleShape(new Vector2f(230, 100));
                if (i < 2) paddlesPictures[i].Position = new Vector2f(200 + (i * 875), 150);
                else paddlesPictures[i].Position =new Vector2f(200 + ((i - 2) * 875), Object.windowHeigth - 250); 

                paddlesPictures[i].FillColor = Color.Cyan;
                paddlesPictures[i].Origin = new Vector2f(230 / 2.0f, 100 / 2.0f);

                blackRecktangles[i] = new RectangleShape(new Vector2f(blackRectangleWidth, blackRectangleHeigth));
                blackRecktangles[i].Origin = new Vector2f(this.blackRectangleWidth / 2.0f, this.blackRectangleHeigth / 2.0f);
                blackRecktangles[i].Position = new Vector2f(paddlesPictures[i].Position.X, paddlesPictures[i].Position.Y);
                blackRecktangles[i].FillColor = Color.Black;
            }
            paddlesPictures[4].Position = new Vector2f(Object.windowWidth / 2, Object.windowHeigth / 2);
            blackRecktangles[4].Position = new Vector2f(paddlesPictures[4].Position.X, paddlesPictures[4].Position.Y);
            SetSprites();
        }
        void SetPaddlesTexts()
        {
            string[] itemNames = { "Yellow", "Red", "Green", "Blue", "White" };
            for (int i = 0; i < howManyPaddles; i++)
            {
                paddlesNamesText[i] = new Text(itemNames[i], DataLoader.font1);
                if (i < 2) paddlesNamesText[i].Position = new Vector2f(200 + (i * 875), 275);
                else paddlesNamesText[i].Position = new Vector2f(200 + ((i - 2) * 875), Object.windowHeigth - 125);
                paddlesNamesText[i].CharacterSize = 30;
                paddlesNamesText[i].Origin = new Vector2f(paddlesNamesText[i].GetLocalBounds().Left + paddlesNamesText[i].GetLocalBounds().Width / 2.0f,
                                                paddlesNamesText[i].GetLocalBounds().Top + paddlesNamesText[i].GetLocalBounds().Height / 2.0f);
            }
            paddlesNamesText[4].Position = new Vector2f(Object.windowWidth / 2, (Object.windowHeigth / 2) + 125);
            paddlesNamesText[0].Color = Color.Yellow;
            paddlesNamesText[1].Color = Color.Red;
            paddlesNamesText[2].Color = Color.Green;
            paddlesNamesText[3].Color = Color.Blue;
        }
        public int Update(Vector2f mouse)
        {
            for (int i = 0; i < howManyPaddles; i++)
            {
                if (paddlesPictures[i].GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    paddlesPictures[i].FillColor = Color.Magenta;
                    if (buttonReleased == true) return i;
                }
                else paddlesPictures[i].FillColor=Color.Cyan;
            }
            return 5;
        }
        public void IsButtonReleased()
        {
            this.buttonReleased = true;
        }    
        public  void DisableButton()
        {
            this.buttonReleased = false;
        }
        public void SetNewPlayer(Paddle oldPlayer,Paddle newPlayer)
        {
            oldPlayer.rect.FillColor = newPlayer.rect.FillColor;
            oldPlayer.sprite = new Sprite(newPlayer.sprite);
        }
    }
}
