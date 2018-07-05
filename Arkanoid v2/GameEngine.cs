using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.IO;
using Arkanoid_v2;

namespace Arkanoid_v2
{
    class GameEngine : Object
    {
        public RenderWindow window;

        GameState state;

        private FileStream coinsFile;
        private StreamReader srCoinsFile;

        private LevelMenager level;
        private PaddleChooser paddleChooser;
        private Shop shop;
        private Coins coins;

        private Ball ball;
        private SuperBall superBall = new SuperBall();

        private int amountOfCoins;
        private int howMauchBlocks;

        private Paddle player= new Paddle();
        private RedPaddle redPaddle= new RedPaddle();
        private BluePaddle bluePaddle= new BluePaddle();
        private YellowPaddle yellowPaddle=new YellowPaddle();
        private GreenPaddle greenPaddle= new GreenPaddle();
        private int eventsHelp;
        ExpandPaddle expandBonus;
        ShortenPaddle shortenBonus;
        AccelerateBall accelerateBonus;
        SlowBall slowBonus;

        public GameEngine()
        {
            DataLoader.LoadAllData();
            coins = new Coins();
            shop = new Shop();
            paddleChooser = new PaddleChooser();
            level = new LevelMenager();
            expandBonus = new ExpandPaddle();
            shortenBonus = new ShortenPaddle();
            accelerateBonus = new AccelerateBall();
            slowBonus = new SlowBall();
            ball = new Ball();
           
            window = new RenderWindow(new VideoMode((uint)Object.windowWidth, (uint)Object.windowHeigth), "Arkanoid", Styles.Default);
            window.SetFramerateLimit(60);
            state = GameState.MENU;
            window.Closed += (sender, arg) => Save();
            window.MouseButtonReleased += MouseReleased;  // ustawiamy obsługę zdarzeń 
            window.KeyPressed += KeyPressed;
            GetCoins();

        }
        private void MouseReleased(object sender, MouseButtonEventArgs e)
        {
            switch (state) //w zależności od stanu gry rozpatrujemy różne przypadki zwolnienia przycisku myszy
            {
                case GameState.MENU: CheckMenuEvents(e); break;
                case GameState.LEVEL_MENAGER: CheckLevelMenagerEvents(e); break;
                case GameState.SHOP: CheckShopEvents(e); break;
                case GameState.PADDLE_CHOOSER: CheckPaddleChoserEvents(e); break;
            }
        }
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            switch (state) //w zależności od stanu gry rozpatrujemy różne przypadki przyciśnięcia klawisza na klawiaturze
            {
                case GameState.GAME: CheckGameEvents(e); break;
                case GameState.LEVEL_MENAGER: CheckLevelMenagerEvents(e); break;
                case GameState.GAME_OVER: CheckGameOverEvents(e); break;
                case GameState.LEVEL_COMPLETED: CheckLevelCompletedEvents(e); break;
                case GameState.SHOP: CheckShopEvents(e); break;
                case GameState.PADDLE_CHOOSER: CheckPaddleChoserEvents(e); break;

            }
        }

        void GetCoins()
        {
            coinsFile = new FileStream("Content\\Files\\amountOfCoins.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            srCoinsFile = new StreamReader(coinsFile);

            while (srCoinsFile.EndOfStream != true) amountOfCoins = Int32.Parse(srCoinsFile.ReadLine());

            srCoinsFile.Close();
        }

        public void RunGame()
        {
            while (state != GameState.END)
            {
                switch (state)
                {
                    case GameState.MENU: Menu(); break;
                    case GameState.GAME: Game(); break;
                    case GameState.GAME_OVER: GameOver(); break;
                    case GameState.LEVEL_MENAGER:
                        level.changeForGameState = false;
                        LevelMenagerFun();
                        break;
                    case GameState.LEVEL_COMPLETED: LevelCompleted(); break;
                    case GameState.SHOP: ShopFun(); break;
                    case GameState.PADDLE_CHOOSER: PaddleChooserFun(); break;
                }
            }
        }
        void Save()
        {
            File.Delete("Content\\Files\\amountOfCoins.txt");
            StreamWriter sw = new StreamWriter("Content\\Files\\amountOfCoins.txt", true);
            sw.WriteLine(amountOfCoins.ToString());
            sw.Close();
            level.SaveLevels();
            shop.SaveItems();
            state = GameState.END;
            window.Close();
        }
        //Menu i zwiazane z nim funkcje
        void Menu()
        {
            Text title = new Text("Arkanoid!", DataLoader.font1, 150);
            title.Color = Color.Cyan;
            title.Position = new Vector2f((Object.windowWidth - title.GetGlobalBounds().Width) / 2, (Object.windowHeigth / 20));

            const int howManyTextsInMenu = 3;
            Text[] texts = new Text[howManyTextsInMenu];
            SetMenuTexts(howManyTextsInMenu, texts);

            while (state == GameState.MENU)
            {
                window.Clear();

                Vector2f mouse = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);
                eventsHelp = ColorMenuTexts(howManyTextsInMenu, texts, mouse);

                for (int i = 0; i < howManyTextsInMenu; i++) window.Draw(texts[i]);
                window.DispatchEvents();
                window.Draw(title);
                window.Display();

            }
        }
        void SetMenuTexts(int howManyTextsInMenu, Text[] texts)
        {
            string[] str = { "Play", "Shop", "Exit" };

            for (int i = 0; i < howManyTextsInMenu; i++)
            {
                texts[i] = new Text();
                texts[i].Font = DataLoader.font1;
                texts[i].CharacterSize = 85;

                texts[i].DisplayedString = str[i];
                texts[i].Position = new Vector2f((Object.windowWidth - texts[i].GetGlobalBounds().Width) / 2, Object.windowHeigth / 3 + (i * 120));
            }
        }
        int ColorMenuTexts(int howManyTextsInMenu, Text[] texts, Vector2f mouse)
        {
            for (int i = 0; i < howManyTextsInMenu; i++)
            {
                if (texts[i].GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    texts[i].Color = Color.Magenta;
                    return i;
                }
                else texts[i].Color = Color.Cyan;
            }
            return howManyTextsInMenu;
        }
        void CheckMenuEvents(MouseButtonEventArgs e)
        { 
            if (e.Button == Mouse.Button.Left)
            {
                if (eventsHelp == 0) state = GameState.PADDLE_CHOOSER;
                else if (eventsHelp == 1) state = GameState.SHOP;
                else if (eventsHelp == 2) Save();
            }
            //eventsHelp - zmienna typu całkowitego która przyjmuje inną wartość dla każdego tekstu klikniętego tekstu
        }

        //Gra i zwiazane z nia funkcje
        void CheckGameEvents(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) state = GameState.MENU;
        }
        void Game()
        {
            List<Block> blocks = new List<Block>();
            List<Bonus> bonuses = new List<Bonus>();

            level.LoadLevel(ball, ref player, ref blocks);
            howMauchBlocks = blocks.Count;
            ball.MoveStraightDown();
            player.Reset();
            ball.Reset();
            while (state == GameState.GAME)
            {
                window.Clear();
                superBall.GetTime();

                ball.Update();
                player.Update();

                if (Collisions(ref player,ref ball)) player.SetPosition(player.GetPosition().X, player.GetPosition().Y - 1);

                if (redPaddle.IsActive()) RedPaddleSpecial();
                else if (bluePaddle.IsActive()) BluePaddleSpecial();

                UpdateAndRemoveBlocks(ref blocks,ref bonuses);
                UpdateAndRemoveBonuses(ref bonuses);

                if (howMauchBlocks <= 0)
                {
                    level.ChangeToNextLevel();
                    state = GameState.LEVEL_COMPLETED;
                    break;
                }
                if (CheckGameOver(ref ball))
                {
                    if (greenPaddle.IsActive()) GreenPaddleSpecial();
                    else state = GameState.GAME_OVER;
                }
                window.DispatchEvents();
                window.Draw(coins.ShowCoinsInGame(amountOfCoins));
                window.Draw(ball);
                window.Draw(player.sprite);
                window.Display();
            }
        }
        void RedPaddleSpecial()
        {
            window.Draw(redPaddle.secondPaddle.rect);
            redPaddle.UpdateSecondPaddle();
            Collisions(ref redPaddle.secondPaddle, ref ball);
        }
        void GreenPaddleSpecial()
        {
            greenPaddle.RemoveLife();
            if (greenPaddle.HowManyLivesLeft() < 0) state = GameState.GAME_OVER;
        }
        void BluePaddleSpecial()
        {
            window.Draw(bluePaddle.secondBall);
            bluePaddle.secondBall.Update();
        }
        void UpdateAndRemoveBlocks(ref List<Block> blocks, ref List<Bonus> bonuses)
        {
            Random rand = new Random();
            int random = rand.Next(1,15);
            foreach (Block block in blocks)
            {
                if (!block.IsDestroyed()) window.Draw(block);
                if ((Collisions(block, ref ball)) && (random < 8) && (random > 0))
                     bonuses.Add(SpawnBonus(block.GetPosition().X, block.GetPosition().Y, random));
                if (bluePaddle.IsActive()) Collisions(block, ref bluePaddle.secondBall);
            }
        }
        void UpdateAndRemoveBonuses(ref List<Bonus> bonuses)
        {
            foreach (Bonus bonus in bonuses)
            {
                if (!bonus.IsDestroyed() && Collisions(bonus, ref player)) UseBonus(bonus.GetId());
                if (bonus.GetPosition().Y >= Object.windowHeigth) bonus.Destroy();
                bonus.MoveDown();
                if (!bonus.IsDestroyed()) window.Draw(bonus.sprite);
            }
        }
        bool CheckGameOver(ref Ball ball)
        {
            if (ball.GetPosition().Y >= Object.windowHeigth - ball.GetRadius()) return true;
            else return false;
        }
        Bonus SpawnBonus(float pos_X, float pos_Y, int rand)
        {
            int pos_x = (int)pos_X;
            int pos_y = (int)pos_Y;
            Bonus bonus = new Bonus(pos_x,pos_y);
            if (rand == 1) bonus = new ExpandPaddle(pos_x, pos_y);
            else if (rand == 2) bonus = new ShortenPaddle(pos_x, pos_y);
            else if (rand == 3) bonus = new AccelerateBall(pos_x, pos_y);
            else if (rand == 4) bonus = new SlowBall(pos_x, pos_y);
            else if (rand == 5) bonus = new SuperBall(pos_x, pos_y);
            else if (rand == 6) bonus = new ReverseControl(pos_x, pos_y);
            else if (rand == 7) bonus = new Coins(pos_x, pos_y);
            return bonus;
        }
        void UseBonus(int id)
        {
            switch (id)
            {
                case 1: expandBonus.Expand(ref player); break;
                case 2: shortenBonus.Shorten(ref player); break;
                case 3: accelerateBonus.Accelerate(ref ball); break;
                case 4: slowBonus.Slow(ref ball); break;
                case 5: superBall.Restart(); superBall.help = 1; break;
                case 6: player.ChangeControl(); break;
                case 7:
                    if (yellowPaddle.IsActive()) amountOfCoins += 10;
                    else amountOfCoins += 5;
                    break;
                default: System.Console.WriteLine("Bonus Id error"); break;
            }
        }

        //Funkcja pokazujaca wybor poziomow
        void CheckLevelMenagerEvents(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) state = GameState.MENU;
        }
        void CheckLevelMenagerEvents(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left) level.IsButtonReleased();
        }

        void LevelMenagerFun()
        { 
            while (state == GameState.LEVEL_MENAGER)
            {
                Vector2f mouse = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);
                window.Clear();

                level.DisableButton();

                for (int i = 0; i < LevelMenager.howManyLevels; i++) window.Draw(level.levelsPictures[i]);
                for (int i = 0; i < LevelMenager.howManyLevels; i++) window.Draw(level.levelsSprites[i]);
                for (int i = 0; i < LevelMenager.howManyLevels; i++) window.Draw(level.levelNumber[i]);

                if (level.changeForGameState == true) state = GameState.GAME;
                window.DispatchEvents();
                level.Update(mouse);
                window.Display();
            }
        }

        //Komunikat o porazce i zwiazane z tym funkcje
        void CheckGameOverEvents(KeyEventArgs e)
        {
            if ((e.Code == Keyboard.Key.Escape) || (e.Code == Keyboard.Key.Space)) state = GameState.MENU;
        }
        void GameOver()
        {
            const int howManyTexts = 3;
            Text[] texts = new Text[howManyTexts];
            SetUpGameOverTexts(howManyTexts, texts);

            while (state == GameState.GAME_OVER)
            {
                window.Clear();
                for (int i = 0; i < howManyTexts; i++) window.Draw(texts[i]);
                window.DispatchEvents();
                window.Display();
            }
        }
        void SetUpGameOverTexts(int howManyTexts, Text[] texts)
        {
            string[] str = { "You Lost!", "Press Space", "to continue" };
            for (int i = 0; i < howManyTexts; i++)
            {
                texts[i] = new Text(str[i], DataLoader.font1);
                texts[i].CharacterSize = 60;
                texts[i].Color = Color.Cyan;
                texts[i].Position = new Vector2f((Object.windowWidth - texts[i].GetGlobalBounds().Width) / 2, (Object.windowHeigth / 2) + (i * 70));
            }
            texts[0].CharacterSize = 150;
            texts[0].Position = new Vector2f((Object.windowWidth - texts[0].GetGlobalBounds().Width) / 2, Object.windowHeigth / 7);
        }

        //Komunikat o wygranej i zwiazane funkcje
        void CheckLevelCompletedEvents(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) state = GameState.MENU;
            else if (e.Code == Keyboard.Key.Space) state = GameState.GAME;
        }
        void LevelCompleted()
        {
            Text levelText = new Text();
            Text continueText = new Text();
            Text menuText = new Text();

            SetUpLevelCompletedTexts(ref levelText, ref continueText, ref menuText);
            level.availableLevels[(int)(level.lvl)] = true;

            while (state == GameState.LEVEL_COMPLETED)
            {
                window.Clear();
                window.DispatchEvents();
                window.Draw(levelText);
                window.Draw(continueText);
                window.Draw(menuText);
                window.Display();
            }
        }
        void SetUpLevelCompletedTexts(ref Text levelText, ref Text continueText, ref Text menuText)
        {
            levelText.DisplayedString = "    Level \ncompleted!";
            levelText.Font = DataLoader.font1;
            levelText.CharacterSize = 150;
            levelText.Position = new Vector2f(200, 75);
            levelText.Color = Color.Cyan;

            continueText.DisplayedString = "Press Space to continue";
            continueText.Font = DataLoader.font1;
            continueText.CharacterSize = 60;
            continueText.Position = new Vector2f(250, 500);
            continueText.Color = Color.Cyan;

            menuText.DisplayedString = "Press Escape to continue";
            menuText.Font = DataLoader.font1;
            menuText.CharacterSize = 60;
            menuText.Position = new Vector2f(235, 575);
            menuText.Color = Color.Cyan;
        }

        //Sklep i zwiazane z nim funkcje
        void CheckShopEvents(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) state = GameState.MENU;
        }
        void CheckShopEvents(MouseButtonEventArgs e)
        {
            Vector2f mouse = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);
            shop.IsButtonReleased();
            if (e.Button == Mouse.Button.Left) CheckBuyingItems(mouse);
        }
        void ShopFun()
        {
            while (state == GameState.SHOP)
            {
                Vector2f mouse = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);

                window.Clear();
                shop.DisableButton();
                CheckBuyingItems(mouse);

                for (int i = 0; i < Shop.howManyItems; i++) window.Draw(shop.shopItems[i]);
                for (int i = 0; i < Shop.howManyItems; i++) window.Draw(shop.blackRecktangles[i]);
                for (int i = 0; i < Shop.howManyItems; i++) window.Draw(shop.ItemsSprites[i]);
                for (int i = 0; i < Shop.howManyItems; i++) window.Draw(shop.shopItemNames[i]);
                window.Draw(coins.ShowCoinsInShop(amountOfCoins));
                window.DispatchEvents();
                window.Display();
            }
        }
        void CheckBuyingItems(Vector2f mouse)
        {
            Paddle newPaddle;
            switch (shop.Update(mouse, ref window))
            {
                case 0:
                    newPaddle = new YellowPaddle();
                    shop.BuyPaddle(ref newPaddle, ref amountOfCoins);
                    break;
                case 1:
                    newPaddle = new RedPaddle();
                    shop.BuyPaddle(ref newPaddle, ref amountOfCoins);
                    break;
                case 2:
                    newPaddle = new GreenPaddle();
                    shop.BuyPaddle(ref newPaddle, ref amountOfCoins);
                    break;
                case 3:
                    newPaddle = new BluePaddle();
                    shop.BuyPaddle(ref newPaddle, ref amountOfCoins);
                    break;
                default: break;
            }
        }

        //Wybor paletek i zwiazane z tym funkcje
        void CheckPaddleChoserEvents(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) state = GameState.MENU;
        }
        void CheckPaddleChoserEvents(MouseButtonEventArgs e)
        {
            Vector2f mouse = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);
            paddleChooser.IsButtonReleased();
            int paddleNumber = paddleChooser.Update(mouse);
            if (e.Button == Mouse.Button.Left) ChoosePaddle(paddleNumber);
        }
        void PaddleChooserFun()
        {
            redPaddle.Deactive();
            bluePaddle.Deactive();
            greenPaddle.Deactive();
            yellowPaddle.Deactive();

            while (state == GameState.PADDLE_CHOOSER)
            {
                Vector2f mouse = new Vector2f(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y);


                window.Clear();

                paddleChooser.DisableButton();
                paddleChooser.Update(mouse);

                for (int i = 0; i < PaddleChooser.howManyPaddles; i++) window.Draw(paddleChooser.paddlesPictures[i]);
                for (int i = 0; i < PaddleChooser.howManyPaddles; i++) window.Draw(paddleChooser.blackRecktangles[i]);
                for (int i = 0; i < PaddleChooser.howManyPaddles; i++) window.Draw(paddleChooser.paddlesSprites[i]);
                for (int i = 0; i < PaddleChooser.howManyPaddles; i++) window.Draw(paddleChooser.paddlesNamesText[i]);
                window.DispatchEvents();
                window.Display();
            }
        }
        void ChoosePaddle(int paddleNumber)
        {
            Paddle newPaddle = new Paddle();
            if ((paddleNumber == 0) && (shop.availableItems[paddleNumber] == true))
            {
                newPaddle = new YellowPaddle();
                yellowPaddle.SetActive();
                state = GameState.LEVEL_MENAGER;
            }
            else if ((paddleNumber == 1) && (shop.availableItems[paddleNumber] == true))
            {
                redPaddle.SetActive();
                newPaddle = new RedPaddle();
                state = GameState.LEVEL_MENAGER;
            }
            else if ((paddleNumber == 2) && (shop.availableItems[paddleNumber] == true))
            {
                greenPaddle.SetActive();
                newPaddle = new GreenPaddle();
                state = GameState.LEVEL_MENAGER;
            }
            else if ((paddleNumber == 3) && (shop.availableItems[paddleNumber] == true))
            {
                bluePaddle.SetActive();
                newPaddle = new BluePaddle();
                state = GameState.LEVEL_MENAGER;
            }
            else if ((paddleNumber == 4))
            {
                state = GameState.LEVEL_MENAGER;
            }
            paddleChooser.SetNewPlayer( player,  newPaddle);
        }

        //Funkcje zwiazane z kolizjami obiektow
        bool Collisions(ref Paddle paddle, ref Ball ball)
        {
            if ((paddle.Right() >= ball.Left()) && (paddle.Left() <= ball.Right())
                && (paddle.Bottom() >= ball.Top()) && (paddle.Top() <= ball.Bottom()))
            {
                ball.MoveUp();
                if (ball.GetPosition().X < paddle.GetPosition().X) ball.MoveLeft();
                else if (ball.GetPosition().X > paddle.GetPosition().X) ball.MoveRight();
                return true;
            }
            return false;
        }

        bool Collisions( Block block, ref Ball ball)
        {
            if ((block.Right() >= ball.Left()) && (block.Left() <= ball.Right())
               && (block.Bottom() >= ball.Top()) && (block.Top() <= ball.Bottom())&&!block.IsDestroyed())
            {
                howMauchBlocks--;
                block.Destroy();
                if ((superBall.ElapsedTime() > superBall.Duration()) || (superBall.help == 0))
                {
                    float overLapLeft = ball.Right() - block.Left();
                    float overLapRight = block.Right() - ball.Left();
                    float overLapTop = ball.Bottom() - block.Top();
                    float overLapBottom = block.Bottom() - ball.Top();

                    bool ballFromLeft = (Math.Abs(overLapLeft) < Math.Abs(overLapRight));
                    bool ballFromTop = (Math.Abs(overLapTop) < Math.Abs(overLapBottom));


                    float minOverLapX;
                    if (ballFromLeft) minOverLapX = overLapLeft;
                    else minOverLapX = overLapRight;

                    float minOverLapY;
                    if (ballFromTop) minOverLapY = overLapTop;
                    else minOverLapY = overLapBottom;
                 

                    if (Math.Abs(minOverLapX) < Math.Abs(minOverLapY))
                    {
                        if (ballFromLeft) ball.MoveLeft();
                        else ball.MoveRight();
                    }
                    else
                    {
                        if (ballFromTop) ball.MoveUp();
                        else ball.MoveDown();
                    }
                }
                return true;
            }
            return false;

        }
        bool Collisions( Bonus bonus,ref  Paddle paddle)
        {
            if ((bonus.Right() >= paddle.Left()) && (bonus.Left() <= paddle.Right())
                && (bonus.Bottom() >= paddle.Top()) && (bonus.Top() <= paddle.Bottom()))
            {
                bonus.Destroy();
                return true;
            }
            return false;
        }
    }
}
