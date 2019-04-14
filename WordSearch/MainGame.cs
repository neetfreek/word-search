using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WordSearch
{
    public class MainGame : Game
    {
        public static ManagerDisplay managerDisplay; // handle resolution, scaling

        //private GraphicsDevice gd;
        private GraphicsDeviceManager gdManager;
        private SpriteBatch sb;

        private Sprite spriteLetters;
        private Sprite spriteLines;

        public MainGame()
        {
            Content.RootDirectory = "Content";
            Display();
        }

        private void Display()
        {
            gdManager = new GraphicsDeviceManager(this);
            gdManager.ApplyChanges();
            DisplaySetup();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        private void DisplaySetup()
        {
            //gdManager.PreferredBackBufferHeight = 1080;
            //gdManager.PreferredBackBufferWidth = 1920;



            managerDisplay = new ManagerDisplay(gdManager);
        }


        protected override void LoadContent()
        {
            // SpriteBatch for drawing multiple sprites at once
            sb = new SpriteBatch(GraphicsDevice);

            // SetupScale sprite atlas textures for drawing sprites
            Texture2D textureLetters = Content.Load<Texture2D>("alphabet");
            spriteLetters = new Sprite(textureLetters, 2, 13);
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
            TestDrawLetters(sb, spriteLetters, 1f);
            TestDrawLines(sb, spriteLines, 1f);
            TestDrawCornersMid(sb, spriteLetters, 1f);

            base.Draw(gameTime);
        }

        // Test methods for drawing all letters, lines; to become base drawing methods
        private void TestDrawLetters(SpriteBatch sb, Sprite spriteAlphabet, float scale)
        {
            int numberColumns = 11;

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
                    new Vector2(counterCol * spriteAlphabet.WidthSprite, (counterRow-1)  * spriteAlphabet.HeightSprite),
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
                    new Vector2(counterCol * spriteLines.WidthSprite, (counterRow - 1) * spriteLines.HeightSprite),
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
        private void TestDrawCornersMid(SpriteBatch sb, Sprite spriteLetters, float scale)
        {
            spriteLetters.Draw(sb, 'a', new Vector2(0, managerDisplay.HeightTargetScaled - spriteLetters.HeightSprite), 1f);
            spriteLetters.Draw(sb, 'x', new Vector2((gdManager.GraphicsDevice.Viewport.Width / 2) - spriteLetters.WidthSprite / 2, (gdManager.GraphicsDevice.Viewport.Height / 2) - spriteLetters.HeightSprite / 2), 1f);
            spriteLetters.Draw(sb, 'z', new Vector2(1920 - spriteLetters.WidthSprite, 1080 - spriteLetters.HeightSprite), 1f);
        }
    }
}