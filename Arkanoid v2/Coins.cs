using SFML.Graphics;
using SFML.System;
namespace Arkanoid_v2
{
    class Coins : Bonus
    {
        private Text gameCoinsText;
        private Text shopConisText;

        public Coins()
        {
            gameCoinsText = new Text();
            shopConisText = new Text();
            SetGameCoinsText();
            SetShopCoinsText();
        }
        public Coins(int pos_X, int pos_Y) : base(pos_X, pos_Y)
        {
            rect.FillColor = Color.White;
            id = 7;
            sprite = new Sprite(DataLoader.coinBonus);
            sprite.Scale = new Vector2f(1.2f, 1.4f);
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Left + sprite.GetLocalBounds().Width / 2.0f,
                                         sprite.GetLocalBounds().Top + sprite.GetLocalBounds().Height / 2.0f);
        }
        public Text ShowCoinsInGame(int amount)
        {
            gameCoinsText.DisplayedString = ("Coins: " + amount.ToString());
            return gameCoinsText;
        }
        public Text ShowCoinsInShop(int amount)
        {
            shopConisText.DisplayedString = ("Coins: " + amount.ToString());
            return shopConisText;
        }
        void SetGameCoinsText()
        {
            gameCoinsText.Color = Color.Cyan;
            gameCoinsText.CharacterSize = 20;
            gameCoinsText.Font = DataLoader.font1;
            gameCoinsText.Position = new Vector2f(windowWidth - 170, 20);
        }
        void SetShopCoinsText()
        {
            shopConisText.Color = Color.Yellow;
            shopConisText.CharacterSize = 60;
            shopConisText.Font = DataLoader.font1;
            shopConisText.Position = new Vector2f((windowWidth / 3.0f) + 25, windowHeigth - 100);
        }
    }
}
