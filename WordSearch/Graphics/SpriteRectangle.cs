/*==========================================================*
*  Clickable rectangle sprites for drawing buttons, menus   *
*===========================================================*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace WordSearch
{
    public class SpriteRectangle
    {
        private readonly string name;
        private Vector2 posSprite;
        private Vector2 posText;
        private Rectangle rectangle;
        public string Name
        {
            get { return name; }
        }
        private Color colorSprite = Color.White;
        public string Text;
        public Vector2 PosSprite
        {
            get { return posSprite; }
            set
            {
                posSprite = value;
                rectangle = new Rectangle((int)posSprite.X, (int)posSprite.Y,
                    (int)(TextureButtonMenu.Width * Scale.X), (int)(TextureButtonMenu.Height * Scale.Y));
            }
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
        }
        public Texture2D TextureButtonMenu;
        // Field: scale sprite
        public Vector2 Scale;

        public SpriteRectangle(string name, Texture2D texture , Vector2 scale, string text)
        {
            this.name = name;
            Text = text;
            TextureButtonMenu = texture;
            PosSprite = new Vector2(0f, 0f);
            this.Scale = scale;

            if (Text == "")
            {
                colorSprite = Color.CadetBlue;
            }
            else
            {
                colorSprite = Color.BlanchedAlmond;
            }
        }

        public void Update(Vector2 posMouse)
        {
            if (posMouse.X >= Rectangle.X && posMouse.X <= Rectangle.X + TextureButtonMenu.Width * Scale.X
                && posMouse.Y >= Rectangle.Y && posMouse.Y <= Rectangle.Y + TextureButtonMenu.Height * Scale.Y)
            {
                MainGame.mouseOver = (ButtonMenu)Enum.Parse(typeof(ButtonMenu), Name);
            }
        }

        public void Draw(SpriteBatch sb, Vector2 position)
        {
            // Set base sprite draw Scale
            PosSprite = position;
            // Set rectangle from sprite atlas texture to draw
            Rectangle rectangleSource = new Rectangle(0, 0, TextureButtonMenu.Width, TextureButtonMenu.Height);
            // Set rectangle on screen where texture is drawn
            Rectangle rectangleDesination = new Rectangle((int)PosSprite.X, (int)PosSprite.Y, TextureButtonMenu.Width * (int)Scale.X, TextureButtonMenu.Height * (int)Scale.Y);
            // Set text position
            // Sprite position + (half button width) - half width of text 
            posText.X = PosSprite.X + ((TextureButtonMenu.Width * (int)Scale.X) * 0.5f) - (MainGame.fontWords.MeasureString(Text).X * 0.5f);
            posText.Y = PosSprite.Y + ((TextureButtonMenu.Height * (int)Scale.Y) * 0.5f) - (MainGame.fontWords.MeasureString(Text).Y * 0.5f);
            // Setup spriteBatch
            sb.Begin(SpriteSortMode.Texture,
            BlendState.AlphaBlend, SamplerState.PointWrap, transformMatrix: ManagerDisplay.ScaleMatrix);
            // Draw sprite on screen
            sb.Draw(TextureButtonMenu, rectangleDesination, rectangleSource, colorSprite);
            // Draw text over sprite
            sb.DrawString(MainGame.fontWords, Text, posText, Color.DarkSlateGray);
            sb.End();
        }
    }
}