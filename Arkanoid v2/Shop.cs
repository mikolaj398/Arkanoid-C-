using SFML.Graphics;
using SFML.System;
using System.IO;

namespace Arkanoid_v2
{
    class Shop : Object
    {
        public static int howManyItems = 4;
        public RectangleShape[] shopItems;
        public Text[] shopItemNames= new Text[4];
        public Sprite[] ItemsSprites= new Sprite[4];
        public RectangleShape[] blackRecktangles= new RectangleShape[4];
        public bool[] availableItems;

        private float shopItemWidth = 230;
        private float shopItemHeigth = 100;
        private float blackRectangleWidth;
        private float blackRectangleHeigth;
        private bool buttonReleased = true;
        private Text[] descriptions;
        private FileStream shopFile;
        private FileStream descriptionFile;
        private StreamReader sr;

        public Shop()
        {
            blackRectangleHeigth = shopItemHeigth - 10;
            blackRectangleWidth = shopItemWidth - 10;
            SetUpTabels();
            SetUpItemsPictures();
            SetUpItemsTexts();
            OpenDescriptions();
            shopFile = new FileStream("Content\\Files\\items.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sr = new StreamReader(shopFile);
            LoadAvailableItems();
        }

        void SetUpTabels()
        {
            shopItems = new RectangleShape[howManyItems];
            shopItemNames = new Text[howManyItems];
            ItemsSprites = new Sprite[howManyItems];
            blackRecktangles = new RectangleShape[howManyItems];
            descriptions = new Text[howManyItems];
            availableItems = new bool[howManyItems];
        }
        void SetSprites()
        {
            for (int i = 0; i < howManyItems; i++)
            {
                ItemsSprites[i] = new Sprite(DataLoader.paddles[i]);
                ItemsSprites[i].Position = new Vector2f(shopItems[i].Position.X - 108, shopItems[i].Position.Y - 22);
                ItemsSprites[i].Scale = new Vector2f(1.7f, 1.7f);
            }
        }
        void SetUpItemsPictures()
        {
            for (int i = 0; i < howManyItems; i++)
            {
                shopItems[i] = new RectangleShape(new Vector2f(this.shopItemWidth, this.shopItemHeigth));
                if (i < 2) shopItems[i].Position= new Vector2f(200 + (i * 875), 150);
                else shopItems[i].Position = new Vector2f(200 + ((i - 2) * 875), Object.windowHeigth - 250);

                shopItems[i].FillColor = Color.Cyan;
                shopItems[i].Origin = new Vector2f(this.shopItemWidth / 2.0f, this.shopItemHeigth / 2.0f);
                
                blackRecktangles[i] = new RectangleShape(new Vector2f(this.blackRectangleWidth, this.blackRectangleHeigth));
                blackRecktangles[i].Origin = new Vector2f(this.blackRectangleWidth / 2.0f, this.blackRectangleHeigth / 2.0f);
                blackRecktangles[i].Position = new Vector2f(shopItems[i].Position.X, shopItems[i].Position.Y);
                blackRecktangles[i].FillColor = Color.Black;
            }
            SetSprites();
        }
        void SetUpItemsTexts()
        {
            string[] itemNames = { "Yellow\n    200", "Red\n300", "Green\n  400", "Blue\n 500" };

            for (int i = 0; i < howManyItems; i++)
            {
                shopItemNames[i] = new Text(itemNames[i], DataLoader.font2);
                if (i < 2) shopItemNames[i].Position = new Vector2f(175 + (i * 890), 300);
                else shopItemNames[i].Position = new Vector2f(180 + ((i - 2) * 885), windowHeigth - 100);

                shopItemNames[i].Origin = new Vector2f(shopItemNames[i].GetLocalBounds().Left + shopItemNames[i].GetLocalBounds().Width / 2.0f,
                                            shopItemNames[i].GetLocalBounds().Top + shopItemNames[i].GetLocalBounds().Height / 2.0f);
                shopItemNames[i].CharacterSize = 40;
            }
            shopItemNames[0].Color = Color.Yellow;
            shopItemNames[1].Color = Color.Red;
            shopItemNames[2].Color = Color.Green;
            shopItemNames[3].Color = Color.Blue;
        }
        void LoadAvailableItems()
        {
            string pom2;
            int i = 0;
            while (sr.EndOfStream != true)
            {
                pom2 = sr.ReadLine();
                availableItems[i] = StringToBool(pom2);
                i++;
            }
        }
        public void SaveItems()
        {
            sr.Close();
            File.Delete("Content\\Files\\items.txt");
            StreamWriter sw = new StreamWriter("Content\\Files\\items.txt", true);

            for (int i = 0; i < howManyItems; ++i) sw.WriteLine(availableItems[i]);
            sw.Close();
        }
        bool StringToBool(string a)
        {
            if (a == "False") return false;
            return true;
        }
        public int Update(Vector2f mouse, ref RenderWindow window)
        {
            for (int i = 0; i < howManyItems; i++)
            {
                if (shopItems[i].GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    window.Draw(descriptions[i]);
                    shopItems[i].FillColor = Color.Magenta;
                    if (buttonReleased == true) return i;
                }
                else shopItems[i].FillColor = Color.Cyan;
            }
            return 4;
        }
        public void IsButtonReleased()
        {
           this.buttonReleased = true;
        }
        public void DisableButton()
        {
            this.buttonReleased = false;
        }
        public void BuyPaddle(ref Paddle paddle, ref int amountOfCoins)
        {
            if (paddle.GetPrice() <= amountOfCoins)
            {
                availableItems[paddle.GetId()] = true;
                amountOfCoins -= paddle.GetPrice();
            }

        }
        void OpenDescriptions()
        {
            OpenDescription("Content\\Files\\yellowPaddle.txt", 0);
            descriptions[0].Color = Color.Yellow;
            OpenDescription("Content\\Files\\redPaddle.txt", 1);
            descriptions[1].Color = Color.Red;
            OpenDescription("Content\\Files\\greenPaddle.txt", 2);
            descriptions[2].Color = Color.Green;
            OpenDescription("Content\\Files\\bluePaddle.txt", 3);
            descriptions[3].Color = Color.Blue;
        }
        void OpenDescription(string path, int id)
        {
            descriptionFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sr = new StreamReader(descriptionFile);
            string text = "";

            while (sr.EndOfStream != true) text += ("\n"+sr.ReadLine());
            descriptionFile.Close();
            sr.Close();

            descriptions[id] = new Text(text, DataLoader.font2);
            descriptions[id].Position=new Vector2f(400, 125);
            descriptions[id].CharacterSize=40;

        }
    }
}
