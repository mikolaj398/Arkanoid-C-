using SFML.Graphics;
using SFML.System;

namespace Arkanoid_v2
{
    class Object : Drawable
    {
        public Sprite sprite = new Sprite();
        public RectangleShape rect = new RectangleShape();
        public static int windowWidth = 1280;
        public static int windowHeigth = 800;

        public float Left()
        {
            return rect.Position.X - rect.Size.X / 2.0f;
        }
        public float Right()
        {
            return rect.Position.X + rect.Size.X / 2.0f;
        }
        public float Top()
        {
            return rect.Position.Y - rect.Size.Y / 2.0f;
        }
        public float Bottom()
        {
            return rect.Position.Y + rect.Size.Y / 2.0f;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(rect, states);
        }
        public Vector2f GetPosition()
        {
            return rect.Position;
        }
        public void SetPosition(float pos_X, float pos_Y)
        {
            rect.Position = new Vector2f(pos_X, pos_Y);
        }
    }
}
