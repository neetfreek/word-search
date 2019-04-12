/*==========================================================================*
* Handle sprite atlas textures, drawing sprites from atlases                *
*===========================================================================*
* 1. Draw sprite from sprite atlas texture:                                 *
*   - Set sprite scale                                                      *
*   - Get sprite from sprite atlas texture, set rectangleSource             *
*   - Set rectangleDestination sprite from texture atlasses, draw sprite    *
* 2. Apply scaling to sprite                                                *
* ==========================================================================*/
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace WordSearch
{
    class Sprite
    {
        // Fields: sprite atlas setup
        private int columns;
        private int rows;
        private int numberSprites;
        private int heightSprite;
        private int widthSprite;
        // Field: scale sprite
        private float scaleSprite;

        // Properties: scaled height, width for Draw() callers
        public int HeightSprite
        {
            get { return Scale(heightSprite, scaleSprite); }
        }
        public int WidthSprite
        {
            get { return Scale(widthSprite, scaleSprite); }
        }

        // Private class
        private Texture2D texture;

        // Constructor
        public Sprite(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            heightSprite = texture.Height / rows;
            widthSprite = texture.Width / columns;
            numberSprites = rows * columns;
        }

        public void Draw(SpriteBatch spriteBatch, char letter, Vector2 location, float scale)
        {
            // set sprite draw scale
            scaleSprite = scale;
            int heightScaled = Scale(heightSprite, scale);
            int widthScaled = Scale(widthSprite, scale);

            // select, convert letter to index value through, dictionary
            int indexSprite;
            if (char.IsLetter(letter))
            {
                indexSprite = DictionaryTextures.KeyLetters(letter);
            }
            else
            {
                indexSprite = DictionaryTextures.KeyLines(letter);
            }

            // Set position of sprite to get from sprite atlas texture
            int posRow = (indexSprite) / (numberSprites / rows) * heightSprite;
            int posCol = indexSprite * widthSprite;

            // Set rectangle from sprite atlas texture to draw, rectangle where texture is drawn
            Rectangle rectangleSource = new Rectangle(posCol, posRow, heightSprite, widthSprite);
            Rectangle rectangleDesination = new Rectangle((int)location.Y, (int)location.X, HeightSprite, WidthSprite);

            // Draw sprite
            spriteBatch.Begin(SpriteSortMode.Texture,
            BlendState.AlphaBlend, SamplerState.PointWrap);
            spriteBatch.Draw(texture, rectangleDesination, rectangleSource, Color.White);
            spriteBatch.End();
        }

        // Apply scale to sprite
        private int Scale(int dimension, float scaler)
        {               
            return (int)(dimension * scaler);
        }
    }
}