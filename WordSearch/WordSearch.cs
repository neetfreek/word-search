using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WordSearch
{
    public class WordSearch : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch sb;

        private Sprite spriteAlphabet;
        private Sprite spriteLines;

        public WordSearch()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // SpriteBatch for drawing multiple sprites at once
            sb = new SpriteBatch(GraphicsDevice);

            // Setup sprite atlas textures for drawing sprites
            Texture2D textureAlphabet = Content.Load<Texture2D>("alphabet");
            spriteAlphabet = new Sprite(textureAlphabet, 2, 13);
            Texture2D textureLines = Content.Load<Texture2D>("Lines");
            spriteLines = new Sprite(textureLines, 1, 4);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Test draw all sprites in both sprite atlasses
            TestDrawLetters(sb, spriteAlphabet, 0.5f);
            TestDrawLines(sb, spriteLines, 0.5f);

            base.Draw(gameTime);
        }

        // Test methods for drawing all letters, lines; to become base drawing methods
        private void TestDrawLetters(SpriteBatch sb, Sprite spriteAlphabet, float scale)
        {
            int numberColumns = 10;

            int counterNewLine = 0;
            int counterRow = 0;
            int counterCol = 0;
            for (int counter = 0; counter < DictionaryTextures.Letters.Count; counter++)
            {
                //Fix counterRow for moving to next rown once enumerated enough columns
                if (counter == 0)
                {
                    counterRow = 1;
                }

                spriteAlphabet.Draw(sb, DictionaryTextures.ValueLetters(counter),
                    new Vector2((counterRow - 1) * spriteAlphabet.HeightSprite, counterCol * spriteAlphabet.WidthSprite),
                    scale);

                counterCol++;
                counterNewLine++;

                // Move to next row once enumerated enough columns
                if (counterNewLine % numberColumns == 0)
                {
                    counterCol = 0;
                    counterRow++;
                }
            }
        }
        private void TestDrawLines(SpriteBatch sb, Sprite spriteLines, float scale)
        {
            int sizeRow = 10;

            int counterNewLine = 0;
            int counterRow = 0;
            int counterCol = 0;
            for (int counter = 0; counter < DictionaryTextures.Lines.Count; counter++)
            {
                //Fix counterRow for moving to next rown once enumerated enough columns
                if (counter == 0)
                {
                    counterRow = 1;
                }

                spriteLines.Draw(sb, DictionaryTextures.ValueLines(counter),
                    new Vector2((counterRow - 1) * spriteLines.HeightSprite, counterCol * spriteLines.WidthSprite),
                    scale);

                counterCol++;
                counterNewLine++;

                // Move to next row once enumerated enough columns
                if (counterNewLine % sizeRow == 0)
                {
                    counterCol = 0;
                    counterRow++;
                }
            }
        }
    }
}