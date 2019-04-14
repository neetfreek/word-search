using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;
using WordSearch.Common;

namespace WordSearch
{
    public class MainGame : Game
    {
        // Display
        private GraphicsDeviceManager gdManager;
        private SpriteBatch sb;
        public static ManagerDisplay managerDisplay;
        // Textures
        private Sprite spriteLetters;
        private Sprite spriteLines;
        // Grid
        private Grid grid;

        public MainGame()
        {
            Content.RootDirectory = "Content";
            Setup();
            StartGame();
        }

        private void Setup()
        {
            // Setup display
            gdManager = new GraphicsDeviceManager(this);
            gdManager.ApplyChanges();
            managerDisplay = new ManagerDisplay(gdManager);
            // setup grid
            grid = new Grid();
        }

        protected override void Initialize()
        {
            base.Initialize();
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

        private void StartGame()
        {
            grid.SetupGridGame("Mammals", 16);
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

            DrawGrid(sb, spriteLetters, grid.GridGame, 0.6f);

            base.Draw(gameTime);
        }

        private void DrawGrid(SpriteBatch sb, Sprite spriteLetters, char[,] grid, float scale)
        {
            int numCols = grid.GetLength(0);
            int numRows = grid.GetLength(1);
            char[] gridElements = Helper.MatrixToVector(grid);
            int counterNewLine = 0;
            int counterRow = 0;
            int counterCol = 0;

            float posModifierW = (gdManager.GraphicsDevice.Viewport.Width * 0.5f) - (numCols * spriteLetters.WidthSprite / 2);
            float posModifierH = (gdManager.GraphicsDevice.Viewport.Height * 0.5f) - (numRows * spriteLetters.HeightSprite / 2);

            for (int counter = 0; counter < gridElements.Length; counter++)
            {
                //Fix counterRow for moving to next rown once enumerated enough columns
                if (counter == 0)
                {
                    counterRow = 1;
                }

                spriteLetters.Draw(sb, gridElements[counter],
                    new Vector2(posModifierW + counterCol * spriteLetters.WidthSprite, posModifierH + (counterRow - 1) * spriteLetters.HeightSprite),
                    scale);

                counterCol++;
                counterNewLine++;

                // Move to next row once enumerated enough columns
                if (counterNewLine % numCols == 0)
                {
                    counterCol = 0;
                    counterRow++;
                }
            }
        }

        // Test draw sprites
        private void TestDrawLetters(SpriteBatch sb, Sprite spriteLetters, float scale)
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

                spriteLetters.Draw(sb, DictionaryTextures.ValueLetters(counter),
                    new Vector2(counterCol * spriteLetters.WidthSprite, (counterRow-1)  * spriteLetters.HeightSprite),
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
        // Test draw word
        private void TestDrawWord(string word, SpriteBatch sb, Sprite spriteLetters, float scale)
        {
            string toDraw = word;

            // Counter variables
            int counterNewLine = 0;
            int counterRow = 0;
            int counterCol = 0;
            int sizeRow = 10;

            for (int counter = 0; counter < toDraw.Length; counter++)
            {
                //Fix counterRow for moving to next rown once enumerated enough columns
                if (counter == 0)
                {
                    counterRow = 1;
                }

                System.Console.WriteLine($"Test draw: {toDraw}");

                spriteLetters.Draw(sb, toDraw[counter],
                    new Vector2(counterCol * spriteLetters.WidthSprite, (counterRow - 1) * spriteLetters.HeightSprite),
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