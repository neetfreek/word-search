using Microsoft.Xna.Framework;
using WordSearch.Common;

namespace WordSearch
{
    public class ButtonLetter
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
            set { pos = value;
                rectangle = new Rectangle((int)pos.X, (int)pos.Y,
                    (int)(MainGame.spriteLetters.HeightSprite),
                    (int)(MainGame.spriteLetters.WidthSprite));
            }
        }
        public Rectangle Rectangle
        {
            get { return rectangle;}
        }

        public ButtonLetter(string name, Vector2 pos)
        {
            this.name = name;
            nameDraw = Helper.RemoveNonLetters(Name);
            this.pos.X = pos.X;
            this.pos.Y = pos.Y;
        }

        public bool MouseOver(Vector2 posMouse)
        {
            if (posMouse.X >= Rectangle.X && posMouse.X <= Rectangle.X + MainGame.spriteLetters.WidthSprite
                && posMouse.Y >= Rectangle.Y && posMouse.Y <= Rectangle.X + MainGame.spriteLetters.HeightSprite)
            {
                System.Console.WriteLine($"Mouse over {Name}");
                return true;
            }
            return false;
        }
    }
}