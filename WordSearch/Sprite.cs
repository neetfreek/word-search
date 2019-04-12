/*==========================================================================================*
* Get (set rectangleSource), draw (set rectangleDestination) sprites from texture atlasses  *
*                                                                                           *
* ==========================================================================================*/
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WordSearch
{
    class Sprite
    {
        private Texture2D texture;
        private int columns;
        private int rows;
        private int numberSprites;

        public Texture2D Texture
        {
            get { return texture; }
        }
        public int Columns
        {
            get { return columns; }
        }
        public int Rows
        {
            get { return rows; }
        }

        private DictionaryTextures dictionaryCharacters = new DictionaryTextures();

        public Sprite(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            numberSprites = Rows * Columns;
        }

        // Draw 
        public void Draw(SpriteBatch spriteBatch, char letter, Vector2 location)
        {
            // convert letter to index value through dictionary
            dictionaryCharacters.characters.TryGetValue(letter, out int letterIndex);
            // single texture (letter) dimensions            
            int height = Texture.Height / Rows;
            int width = Texture.Width / Columns;
            int posRow = (letterIndex) / (numberSprites / Rows) * height;
            int posCol = ((letterIndex + 1 * letterIndex) / (numberSprites / Columns) * width) - Texture.Width * (posRow / height);
            System.Console.WriteLine($"letter: {letter}, letterIndex: {letterIndex}");
            System.Console.WriteLine($"Texture.Height: {Texture.Height}, Texture.Width: {Texture.Width}");
            // for last texture in spriteAlphabet, pos = Texture.Width, defaults to 0-with; manually reset to end
            if (posCol < 0)
            {
                posCol = Texture.Width - width;
            }

            // Select rectangle within texture to draw, rectangle where texture is drawn
            Rectangle rectangleSource = new Rectangle(posCol, posRow, height, width);
            Rectangle rectangleDesination = new Rectangle((int)location.Y, (int)location.X, width, height);

            // draw texture on screen
            spriteBatch.Begin(SpriteSortMode.Texture,
            BlendState.AlphaBlend, SamplerState.PointWrap);
            spriteBatch.Draw(Texture, rectangleDesination, rectangleSource, Color.White);
            spriteBatch.End();
        }
    }
}