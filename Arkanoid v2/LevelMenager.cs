using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using System.IO;
namespace Arkanoid_v2
{
    class LevelMenager : Object
    {
        public static int howManyLevels = 10;
        public RectangleShape[] levelsPictures;
        public Sprite[] levelsSprites;
        public Text[] levelNumber;
        public RectangleShape[] blackRecktangles;
        public GameLevels lvl;
        public bool[] availableLevels;
        public bool changeForGameState = false;

        private const int howManyBlocks = 8;
        private float levelsPicturesSize = 200;
        private float levelsPicturesWidth = 230;
        private float levelsPicturesHeigth = 100;
        private float blackRectangleWidth;
        private float blackRectangleHeigth;
        private bool buttonReleased = false;
        private FileStream levelMenagerFile;
        private StreamReader sr;

        public LevelMenager()
        {
            SetUpLevelMenagerTables();
            SetUpLevelPictures();
            SetUpLevelTexts();
            levelMenagerFile = new FileStream("Content\\Files\\levelMenager.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sr = new StreamReader(levelMenagerFile);
            blackRectangleWidth = levelsPicturesWidth - 10;
            blackRectangleHeigth = levelsPicturesHeigth - 10;
            LoadAvailableLevels();
        }
        void SetUpLevelMenagerTables()
        {
            levelsPictures = new RectangleShape[howManyLevels];
            levelNumber = new Text[howManyLevels];
            blackRecktangles = new RectangleShape[howManyLevels];
            availableLevels = new bool[howManyLevels];
            levelsSprites = new Sprite[howManyLevels];
        }
        void SetSprites()
        {
            for (int i = 0; i < howManyLevels; i++)
            {
                levelsSprites[i] = new Sprite(DataLoader.levels[i]);
                levelsSprites[i].Origin = new Vector2f(445, 175);
                levelsSprites[i].Position = new Vector2f(levelsPictures[i].Position.X, levelsPictures[i].Position.Y);
                levelsSprites[i].Scale = new Vector2f(0.2f, 0.4f);
            }
            levelsSprites[0].Origin = new Vector2f(335, 175);
            levelsSprites[2].Origin = new Vector2f(335, 175);
            levelsSprites[6].Origin = new Vector2f(390, 175);
        }
        void SetUpLevelPictures()
        {
            for (int i = 0; i < howManyLevels; i++)
            {
                levelsPictures[i] = new RectangleShape(new Vector2f(levelsPicturesSize, levelsPicturesSize));
                if (i < 5) levelsPictures[i].Position = new Vector2f(140 + (i * 250), 150);
                else levelsPictures[i].Position = new Vector2f(140 + ((i - 5) * 250), windowHeigth - 250);

                levelsPictures[i].FillColor = Color.Cyan;
                levelsPictures[i].Origin = new Vector2f(levelsPicturesSize / 2.0f, levelsPicturesSize / 2.0f);

                blackRecktangles[i] = new RectangleShape(new Vector2f(this.blackRectangleWidth, this.blackRectangleHeigth));
                blackRecktangles[i].Origin = new Vector2f(this.blackRectangleWidth / 2.0f, this.blackRectangleHeigth / 2.0f);
                blackRecktangles[i].Position = new Vector2f(levelsPictures[i].Position.X, levelsPictures[i].Position.Y);
                blackRecktangles[i].FillColor = Color.Yellow;
            }
            SetSprites();
        }
        void SetUpLevelTexts()
        {
            string[] levelNumbers = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7", "Level 8", "Level 9", "Level 10" };
            for (int i = 0; i < howManyLevels; i++)
            {
                levelNumber[i] = new Text(levelNumbers[i], DataLoader.font2);
                if (i < 5) levelNumber[i].Position = new Vector2f(45 + (i * 250), 265);
                else levelNumber[i].Position = new Vector2f(45 + ((i - 5) * 250), windowHeigth - 130);

                levelNumber[i].CharacterSize = 40;
                levelNumber[i].Color = Color.Yellow;
            }
        }
        void LoadAvailableLevels()
        {
            string pom;
            int i = 0;
            while (sr.EndOfStream != true)
            {
                pom = sr.ReadLine();
                availableLevels[i] = StringToBool(pom);
                i++;
            }
            availableLevels[0] = true;
        }
        public void SaveLevels()
        {
            sr.Close();
            File.Delete("Content\\Files\\levelMenager.txt");
            StreamWriter sw = new StreamWriter("Content\\Files\\levelMenager.txt", true);
            for (int i = 0; i < howManyLevels; ++i) sw.WriteLine(availableLevels[i]);
            sw.Close();
        }
        bool StringToBool(string a)
        {
            if (a == "False") return false;
            return true;
        }
        public void Update(Vector2f mouse)
        {
            for (int i = 0; i < 10; i++)

                if ((levelsPictures[i].GetGlobalBounds().Contains(mouse.X, mouse.Y)) && (availableLevels[i] == true))
                {
                    levelsPictures[i].FillColor = Color.Magenta;
                    if (buttonReleased == true)
                    {
                        lvl = (GameLevels)(i);
                        changeForGameState = true;
                    }
                }
                else levelsPictures[i].FillColor = Color.Cyan;
        }    
        public void IsButtonReleased()
        {
            this.buttonReleased = true;
        }        
        public void DisableButton()
        {
            this.buttonReleased = false;
        }
        public void LoadLevel(Ball ball, ref Paddle paddle, ref List<Block> blocks)
        {
            ball.SetPosition(Object.windowWidth / 2, Object.windowHeigth / 2);
            paddle.SetPosition(Object.windowWidth / 2, Object.windowHeigth - 60);

            switch (lvl)
            {
                case GameLevels.LEVEL_1: LoadLevel_1(ref blocks); break;
                case GameLevels.LEVEL_2: LoadLevel_2(ref blocks); break;
                case GameLevels.LEVEL_3: LoadLevel_3(ref blocks); break;
                case GameLevels.LEVEL_4: LoadLevel_4(ref blocks); break;
                case GameLevels.LEVEL_5: LoadLevel_5(ref blocks); break;
                case GameLevels.LEVEL_6: LoadLevel_6(ref blocks); break;
                case GameLevels.LEVEL_7: LoadLevel_7(ref blocks); break;
                case GameLevels.LEVEL_8: LoadLevel_8(ref blocks); break;
                case GameLevels.LEVEL_9: LoadLevel_9(ref blocks); break;
                case GameLevels.LEVEL_10: LoadLevel_10(ref blocks); break;
                default: break;
            }
        }
        void LoadLevel_1(ref List<Block> blocks)
        {
            int[,] levelSetUp =  { { 1,1,1,1,1,1,0,0},
                                   { 1,1,0,0,1,1,0,0},
                                   { 1,1,0,0,1,1,0,0},
                                   { 1,1,1,1,1,1,0,0},
                                   { 1,1,0,0,0,0,0,0},
                                   { 1,1,0,0,0,0,0,0},
                                   { 1,1,0,0,0,0,0,0},
                                   { 1,1,0,0,0,0,0,0} };
            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_2(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,1,0,0,0,0,1,1 },
                                  { 1,1,0,0,0,0,1,1 },
                                  { 1,1,0,0,0,0,1,1 },
                                  { 1,1,0,1,1,0,1,1 },
                                  { 1,1,1,0,0,1,1,1 },
                                  { 1,1,1,0,0,1,1,1 },
                                  { 1,1,0,0,0,0,1,1 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_3(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,1,1,1,1,1,0,0 },
                                  { 1,1,0,0,1,1,0,0 },
                                  { 1,1,0,0,1,1,0,0 },
                                  { 1,1,1,1,1,1,0,0 },
                                  { 1,1,1,0,0,0,0,0 },
                                  { 1,1,0,1,0,0,0,0 },
                                  { 1,1,0,0,1,0,0,0 },
                                  { 1,1,0,0,0,1,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_4(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 0,0,0,1,1,0,0,0 },
                                  { 0,0,1,1,1,1,0,0 },
                                  { 0,1,0,1,1,0,1,0 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 0,1,0,1,1,0,1,0 },
                                  { 0,0,1,1,1,1,0,0 },
                                  { 0,0,0,1,1,0,0,0 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_5(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,1,1,1,1,1,1,1 },
                                  { 1,0,0,0,0,0,1,1 },
                                  { 1,0,0,0,0,1,0,1 },
                                  { 1,0,0,1,1,0,0,1 },
                                  { 1,0,1,0,0,0,0,1 },
                                  { 1,1,0,0,0,0,0,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_6(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,0,1,0,1,0,1,0 },
                                  { 0,1,0,1,0,1,0,1 },
                                  { 1,0,1,0,1,0,1,0 },
                                  { 0,1,0,1,0,1,0,1 },
                                  { 1,0,1,0,1,0,1,0 },
                                  { 0,1,0,1,0,1,0,1 },
                                  { 1,0,1,0,1,0,1,0 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_7(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,0,0,1,0,0,1,0 },
                                  { 0,1,0,1,0,1,0,0 },
                                  { 0,0,1,1,1,0,0,0 },
                                  { 1,1,1,1,1,1,1,0 },
                                  { 0,0,1,1,1,0,0,0 },
                                  { 0,1,0,1,0,1,0,0 },
                                  { 1,0,0,1,0,0,1,0 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_8(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,1,1,0,0,1,1,1 },
                                  { 1,1,1,0,0,1,1,1 },
                                  { 1,1,1,0,0,1,1,1 },
                                  { 0,0,0,1,1,0,0,0 },
                                  { 0,0,0,1,1,0,0,0 },
                                  { 1,0,0,0,0,0,0,1 },
                                  { 0,1,1,1,1,1,1,0 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_9(ref List<Block> blocks)
        {
            int[,] levelSetUp = { {1,1,1,1,1,1,1,1 },
                                  { 1,1,0,0,0,0,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,0,0,0,0,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,0,0,0,0,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void LoadLevel_10(ref List<Block> blocks)
        {
            int[,] levelSetUp = { { 1,1,1,1,1,1,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 1,1,1,1,1,1,1,1 },
                                  { 0,0,0,0,0,0,0,0 } };

            SetBlocks(levelSetUp, ref blocks);
        }
        void SetBlocks(int[,] level, ref List<Block> blocks)
        {
            for (int i = 0; i < howManyBlocks; i++)
                for (int j = 0; j < howManyBlocks; j++)
                    if (level[i, j] == 1)
                    {
                        Block block = new Block(150 + ((j + 1) * 110), 20 + ((i + 1) * 50), 100, 30);
                        blocks.Add(block);
                    }
        }
        public void ChangeToNextLevel()
        {
            lvl += 1;
        }
    }
}
