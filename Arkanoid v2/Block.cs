using SFML.Graphics;
using SFML.System;
namespace Arkanoid_v2
{
    class Block: Object
    {
        private bool destroyed;

        public Block(int pos_X, int pos_Y, float Width, float Heigth)
        {
            rect.Position=new Vector2f(pos_X, pos_Y);
            rect.Size = new Vector2f(Width, Heigth);
            rect.Origin= new Vector2f(Width / 2.0f, Heigth / 2.0f);
            rect.FillColor = Color.Yellow;
            destroyed = false;
        }
        public bool IsDestroyed()
        {
            return this.destroyed;
        }
        public void Destroy()
        {
            this.destroyed = true;
        }
        Vector2f GetSize()
        {
            return rect.Size;
        }
    }
}
