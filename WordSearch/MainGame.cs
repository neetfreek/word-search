using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WordSearch.Common;
using System.Collections.Generic;

namespace WordSearch
{
    public class MainGame : Game
    {
        // Display
        private GraphicsDeviceManager gdManager;
        private SpriteBatch sb;
        public static ManagerDisplay managerDisplay;
        public const float SCALE_TILES = 0.6f; 
        public const float SCALE_CURSOR = 0.3f; 
        // Mouse
        Vector2 posMouse;
        // Textures
        public static Sprite spriteCursor;
        public static Sprite spriteLetters;
        public static Sprite spriteLines;
        // Text
        private SpriteFont fontWords;
        private SpriteFont fontHeadings;
        private Vector2 headingNameListPos;
        private Vector2 headingWordsPos; 
        private string headingNameList = "";
        private readonly string headingWords = "Your List of Words:";
        // Buttons
        public static List<ButtonLetter> listLettersGrid = new List<ButtonLetter>();
        // Grid
        private Grid grid;
        // States

        public MainGame()
        {
            SetupClassReferences();

            Content.RootDirectory = "Content";
        }

        private void SetupClassReferences()
        {
            // Setup display
            gdManager = new GraphicsDeviceManager(this);
            gdManager.ApplyChanges();
            managerDisplay = new ManagerDisplay(gdManager);
            // Setup mouse
            posMouse = new Vector2(gdManager.GraphicsDevice.Viewport.Width * 0.5f,
                gdManager.GraphicsDevice.Viewport.Height * 0.5f);
            // Setup grid
            grid = new Grid();
        }

        protected override void Initialize()
        {
            SetupGame();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // SpriteBatch for drawing multiple sprites at once
            sb = new SpriteBatch(GraphicsDevice);

            // SetupScale sprite atlas textures for drawing sprites
            Texture2D textureCursor = Content.Load<Texture2D>("cursor");
            spriteCursor = new Sprite(textureCursor, 1, 1);
            Texture2D textureLetters = Content.Load<Texture2D>("alphabet");
            spriteLetters = new Sprite(textureLetters, 2, 13);
            Texture2D textureLines = Content.Load<Texture2D>("lines");
            spriteLines = new Sprite(textureLines, 1, 4);
            fontWords = Content.Load<SpriteFont>("fontWords");
            fontHeadings = Content.Load<SpriteFont>("fontHeadings");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        // Setup game from user input
        private void SetupGame()
        {
            grid.SetupGridGame("Mammals", 16);
            headingNameList = "Mammals";

            // Create buttons, place in listLettersGrid list
            SetupListLettersGrid();
        }
        private void SetupListLettersGrid()
        {
            int counter = 0;

            foreach (char letter in grid.GridGame)
            {
                ButtonLetter button = new ButtonLetter(letter.ToString() + counter.ToString(),
                    new Vector2(0f, 0f));
                listLettersGrid.Add(button);
                counter++;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            UpdateInput();

            base.Update(gameTime);
        }
        private void UpdateInput()
        {
            // Mouse input
            MouseState mouseState = Mouse.GetState();
            posMouse.X = mouseState.X;
            posMouse.Y = mouseState.Y;

            foreach (ButtonLetter button in listLettersGrid)
            {
                button.MouseOver(posMouse);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                //
            }

            // Keyboard input
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Teal);
            //GraphicsDevice.Clear(Color.LightSkyBlue);

            DrawGrid(sb, spriteLetters, SCALE_TILES);
            DrawHeadings(sb);
            DrawWordsList(sb);
            DrawMouse(sb, spriteCursor, SCALE_CURSOR);

            base.Draw(gameTime);
        }

        private void DrawHeadings(SpriteBatch sb)
        {
            sb.Begin();

            string wordLongest = Helper.LongestWord(grid.WordsGame);

            headingNameListPos = new Vector2((gdManager.GraphicsDevice.Viewport.Width * 0.5f) - (fontHeadings.MeasureString(headingNameList).X / 2),
            (gdManager.GraphicsDevice.Viewport.Height / 2) - (grid.GridGame.GetLength(1) * spriteLetters.HeightSprite / 2) - fontHeadings.MeasureString(headingNameList).Y);
            sb.DrawString(fontHeadings, headingNameList, headingNameListPos, Color.Cornsilk);

            headingWordsPos = new Vector2((gdManager.GraphicsDevice.Viewport.Width * 0.5f) - (grid.GridGame.GetLength(0) * spriteLetters.WidthSprite) + fontHeadings.MeasureString(wordLongest).X/2,
                gdManager.GraphicsDevice.Viewport.Height * 0.1f);
            sb.DrawString(fontHeadings, headingWords, headingWordsPos, Color.Cornsilk);

            sb.End();
        }
        private void DrawWordsList(SpriteBatch sb)
        {
            // Position details
            Vector2 position = new Vector2(0f, 0f);
            string wordLongest = $"  .{Helper.LongestWord(grid.WordsGame)}";

            float posWidth = ((gdManager.GraphicsDevice.Viewport.Width * 0.5f) - (grid.GridGame.GetLength(0) * spriteLetters.WidthSprite)) + fontWords.MeasureString(wordLongest).X / 2;
            float posHeight = (gdManager.GraphicsDevice.Viewport.Height * 0.175f);
            // longest word (to center)

            int counter = 0;

            sb.Begin();
            foreach (string word in grid.WordsGame)
            {
                position = new Vector2(posWidth,
                    posHeight + (fontWords.LineSpacing * counter));
                sb.DrawString(fontWords, $"{(counter + 1).ToString()}. {word}" , position, Color.Cornsilk);

                counter++;
            }
            sb.End();
        }
        private void DrawGrid(SpriteBatch sb, Sprite spriteLetters, float scale)
        {
            int numCols = grid.GridGame.GetLength(0);
            int numRows = grid.GridGame.GetLength(1);

            int counterNewLine = 0;
            int counterRow = 0;
            int counterCol = 0;

            // Position details
            Vector2 position = new Vector2(0f, 0f);
            float posModifierW = (gdManager.GraphicsDevice.Viewport.Width * 0.5f)
                - (numCols * spriteLetters.WidthSprite / 2);
            float posModifierH = (gdManager.GraphicsDevice.Viewport.Height * 0.5f)
                - (numRows * spriteLetters.HeightSprite / 2);

            for (int counter = 0; counter < listLettersGrid.Count; counter++)
            {
                //Fix counterRow for moving to next rown once enumerated enough columns
                if (counter == 0)
                {
                    counterRow = 1;
                }

                // Convert letter NameDraw to char
                char.TryParse(listLettersGrid[counter].NameDraw, out char toDraw);
                // Get position to place letter
                position = new Vector2(posModifierW + counterCol * spriteLetters.WidthSprite,
                    posModifierH + (counterRow - 1) * spriteLetters.HeightSprite);
                // Draw sprite
                spriteLetters.Draw(sb, toDraw, position, scale);
                // Update letter pos
                listLettersGrid[counter].Pos = position;

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
        private void DrawMouse(SpriteBatch sb, Sprite textureCursor, float scale)
        {
            textureCursor.Draw(sb, '0',
                new Vector2(posMouse.X, posMouse.Y),
                scale);
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
                    new Vector2(counterCol * spriteLetters.WidthSprite,
                    (counterRow-1)  * spriteLetters.HeightSprite),
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
                    new Vector2(counterCol * spriteLines.WidthSprite,
                    (counterRow - 1) * spriteLines.HeightSprite),
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
                    new Vector2(counterCol * spriteLetters.WidthSprite,
                    (counterRow - 1) * spriteLetters.HeightSprite),
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