using Microsoft.Xna.Framework;
using WordSearch.Common;

namespace WordSearch
{
    public class ButtonTile
    {
        private readonly string name;
        private string nameDraw;
        private Vector2 pos;
        private Rectangle rectangle;
        public string Name
        {
            get { return name; }
        }
        // Name without letters, used for Draw calls
        public string NameDraw
        {
            get { return nameDraw; }
            set { nameDraw = value; }
        }
        public Vector2 Pos
        {
            get { return pos; }
            set
            {
                pos = value;
                // rectangle (collider) reduced to 50% size of tile
                rectangle = new Rectangle((int)InnerRectangleOrigin().X, (int)InnerRectangleOrigin().Y,
                (int)(MainGame.spriteLetters.WidthSprite * 0.5),
                    (int)(MainGame.spriteLetters.HeightSprite * 0.5));
            }
        }
        public Rectangle Rectangle
        {
            get { return rectangle;}
        }

        public ButtonTile(string name, Vector2 pos)
        {
            this.name = name;
            nameDraw = Helper.RemoveNonLetters(Name);
            this.pos.X = pos.X;
            this.pos.Y = pos.Y;
        }

        public void Update(Vector2 posMouse)
        {
            if (rectangle.Contains(posMouse.X, posMouse.Y))
            {
                MainGame.MousedOverTile = this;
            }
        }

        // rectangle (collider) pos move 25% right, 25% down
        private Vector2 InnerRectangleOrigin()
        {
            Vector2 midPoint;
            midPoint.X = Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.25);
            midPoint.Y = Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.25);

            return midPoint;
        }
    }
}