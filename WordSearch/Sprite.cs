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
        // Fields
        private Texture2D texture;
        private int columns;
        private int rows;
        private int numberSprites;
        private int heightSprite;
        private int widthSprite;
        private float scaleSprite;

        // Properties: scaled height, width
        public int HeightSprite
        {
            get { return Scale(heightSprite, scaleSprite); }
        }
        public int WidthSprite
        {
            get { return Scale(widthSprite, scaleSprite); }
        }

        private DictionaryTextures dictionaryCharacters = new DictionaryTextures();

        public Sprite(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            heightSprite = texture.Height / rows;
            widthSprite = texture.Width / columns;
            numberSprites = rows * columns;
        }

        // Draw 
        public void Draw(SpriteBatch spriteBatch, char letter, Vector2 location, float scale)
        {
            // set sprite draw scale
            scaleSprite = scale;

            // convert letter to index value through dictionary
            dictionaryCharacters.characters.TryGetValue(letter, out int letterIndex);
            // single texture (letter) dimensions
            int posRow = (letterIndex) / (numberSprites / rows) * heightSprite;
            int posCol = ((letterIndex + 1 * letterIndex) / (numberSprites / columns) * widthSprite) - texture.Width * (posRow / heightSprite);
            // for last texture in spriteAlphabet cols; pos becomes Texture.Width, defaults to 0-with; manually reset to end pos
            if (posCol < 0)
            {
                posCol = texture.Width - widthSprite;
            }

            // Select rectangle within texture to draw, rectangle where texture is drawn
            Rectangle rectangleSource = new Rectangle(posCol, posRow, heightSprite, widthSprite);
            Rectangle rectangleDesination = new Rectangle((int)location.Y, (int)location.X, Scale(widthSprite, scaleSprite), Scale(heightSprite, scaleSprite));

            // draw texture on screen
            spriteBatch.Begin(SpriteSortMode.Texture,
            BlendState.AlphaBlend, SamplerState.PointWrap);
            spriteBatch.Draw(texture, rectangleDesination, rectangleSource, Color.White);
            spriteBatch.End();
        }

        private int Scale(int dimension, float scaler)
        {               
            return (int)((float)dimension * scaler);
        }
    }
}