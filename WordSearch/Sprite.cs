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

namespace WordSearch
{
    class Sprite
    {
        private ManagerDisplay managerDisplay; // handle resolution, scaling

        // Fields: sprite atlas setup
        private int columns;
        private int rows;
        private int numberSprites;
        private int heightSprite;
        private int widthSprite;
        // Field: scale sprite
        private float scaleSprite;

        // Properties: Scale()-scaled height, width for Draw() callers
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

            SetupScale();
        }

        private void SetupScale()
        {
            managerDisplay = MainGame.managerDisplay;
        }

        public void Draw(SpriteBatch spriteBatch, char letter, Vector2 location, float scale)
        {
            // Set base sprite draw scale
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

            // Set position in sprite atlas texture to get sprite to get from
            int posRow = (indexSprite) / (numberSprites / rows) * heightSprite;
            int posCol = indexSprite * widthSprite;

            // Set rectangle from sprite atlas texture to draw
            Rectangle rectangleSource = new Rectangle(posCol, posRow, heightSprite, widthSprite);
            // Set rectangle on screen where texture is drawn
            Rectangle rectangleDesination = new Rectangle((int)location.Y, (int)location.X, HeightSprite, WidthSprite);

            // Setup spriteBatch
            spriteBatch.Begin(SpriteSortMode.Texture,
            BlendState.AlphaBlend, SamplerState.PointWrap, transformMatrix: managerDisplay.ScaleMatrix);
            // Draw sprite on screen
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