using SFML.Graphics;

namespace Arkanoid_v2
{
    static class DataLoader
    { 
        public static Font font1 { get; private set; } //czcionka I
        public static Font font2 { get; private set; } //czcionka II
        public static Texture[] paddles = new Texture[PaddleChooser.howManyPaddles]; //tablica przechowywująca tekstury paletek
        public static Texture accelerateBallBonus { get; private set; } //tekstura bonusu przyśpieszenia piłki
        public static Texture coinBonus { get; private set; }  //tekstura monety
        public static Texture expandPaddleBonus { get; private set; }  //tekstura bonusu rozszerzenia paletki
        public static Texture reverseControlBonus { get; private set; }  //tekstura bonusu zmiany systemu sterowania
        public static Texture shortenPaddleBonus { get; private set; }  //tekstura bonusu zwęrzenia paletki
        public static Texture slowBallBonus { get; private set; }  //tekstura bonusu spowolnienia piłki
        public static Texture superBallBonus { get; private set; }  //tekstura bonusu super piłki
        public static Texture[] levels = new Texture[LevelMenager.howManyLevels]; //tablica przechowywująca tekstury poziomów

        public static void LoadAllData()
        {
            LoadFonts(); //metoda ładująca czciąki
            LoadPaddles(); //metoda ładowania tekstur paletek
            LoadLevelsSprites(); //metodał ładowania tekstur poziomów
            LoadBonuses(); //metoda ładowania tekstu bonusów
        }
        public static bool LoadFonts()
        {
            font1 = new Font("Content\\Fonts\\Games.ttf");
            font2 = new Font("Content\\Fonts\\Gameplay.ttf");
            return ((font1 != null) && (font2 != null));
        }
        public static bool LoadPaddles()
        {
            //paddles = new Texture[5];
            paddles[0] = new Texture("Content\\Sprites\\yellowPaddle.png");
            paddles[1] = new Texture("Content\\Sprites\\redPaddle.png");
            paddles[2] = new Texture("Content\\Sprites\\greenPaddle.png");
            paddles[3] = new Texture("Content\\Sprites\\bluePaddle.png");
            paddles[4] = new Texture("Content\\Sprites\\whitePaddle.png");
            return (paddles[0] == null && paddles[1] == null && paddles[4] == null
                    && paddles[2] == null && paddles[3] == null);
        }
        public static bool LoadBonuses()
        {

            accelerateBallBonus = new Texture("Content\\Sprites\\accelerateBall.png");
            coinBonus = new Texture("Content\\Sprites\\coin.png");
            expandPaddleBonus = new Texture("Content\\Sprites\\expandPaddle.png");
            reverseControlBonus = new Texture("Content\\Sprites\\reverseControl.png");
            shortenPaddleBonus = new Texture("Content\\Sprites\\shortenPaddle.png");
            slowBallBonus = new Texture("Content\\Sprites\\slowBall.png");
            superBallBonus = new Texture("Content\\Sprites\\superBall.png");
            return (accelerateBallBonus == null && coinBonus == null && expandPaddleBonus == null
                       && reverseControlBonus == null && shortenPaddleBonus == null &&
                       slowBallBonus == null && superBallBonus == null);
        }
        public static bool LoadLevelsSprites()
        {
            //levels = new Texture[10];
            levels[0] = new Texture("Content\\Sprites\\lvl_1.png");
            levels[1] = new Texture("Content\\Sprites\\lvl_2.png");
            levels[2] = new Texture("Content\\Sprites\\lvl_3.png");
            levels[3] = new Texture("Content\\Sprites\\lvl_4.png");
            levels[4] = new Texture("Content\\Sprites\\lvl_5.png");
            levels[5] = new Texture("Content\\Sprites\\lvl_6.png");
            levels[6] = new Texture("Content\\Sprites\\lvl_7.png");
            levels[7] = new Texture("Content\\Sprites\\lvl_8.png");
            levels[8] = new Texture("Content\\Sprites\\lvl_9.png");
            levels[9] = new Texture("Content\\Sprites\\lvl_10.png");
            return (levels[1] == null && levels[2] == null && levels[3] == null && levels[4] == null
                    && levels[5] == null && levels[6] == null && levels[7] == null && levels[8] == null
                    && levels[9] == null && levels[0] == null);
        }
    }
}