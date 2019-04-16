using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WordSearch.Common;

namespace WordSearch
{
    public class ButtonMenu
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
                rectangle = new Rectangle((int)pos.X, (int)pos.Y,
                    (int)(TextureButtonMenu.Width),
                    (int)(TextureButtonMenu.Height));
            }
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public Texture2D TextureButtonMenu;
        // Field: scale sprite
        private float scaleSprite;

        public ButtonMenu(string name, Texture2D texture)
        {
            this.name = name;
            nameDraw = Helper.RemoveNonLetters(Name);
            TextureButtonMenu = texture;
            Pos = new Vector2(0f, 0f);
        }

        public bool Update(Vector2 posMouse)
        {
            if (posMouse.X >= Rectangle.X && posMouse.X <= Rectangle.X + TextureButtonMenu.Width
                && posMouse.Y >= Rectangle.Y && posMouse.Y <= Rectangle.X + TextureButtonMenu.Height)
            {
                System.Console.WriteLine($"Mouse over {Name}");
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch sb, Vector2 position, float scale)
        {
            // Set base sprite draw scale
            scaleSprite = scale;
            Pos = position;
            // Set rectangle from sprite atlas texture to draw
            Rectangle rectangleSource = new Rectangle(0, 0, TextureButtonMenu.Width, TextureButtonMenu.Height);
            // Set rectangle on screen where texture is drawn
            Rectangle rectangleDesination = new Rectangle((int)Pos.X, (int)Pos.Y, TextureButtonMenu.Width, TextureButtonMenu.Height);

            // Setup spriteBatch
            sb.Begin(SpriteSortMode.Texture,
            BlendState.AlphaBlend, SamplerState.PointWrap, transformMatrix: ManagerDisplay.ScaleMatrix);
            // Draw sprite on screen
            sb.Draw(TextureButtonMenu, rectangleDesination, rectangleSource, Color.White);
            sb.End();
        }
    }
}