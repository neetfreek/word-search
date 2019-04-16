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
        // Global constants
        public const float SCALE_CURSOR = 0.3f, SCALE_TILES = 0.6f; 
        // Mouse
        Vector2 posMouse;
        // Textures
        private Texture2D textureCursor;
        private Texture2D textureLetters;
        private Texture2D textureLines;
        private Texture2D textureButtonMenu;
        private SpriteFont fontWords;
        private SpriteFont fontHeadings;
        // Texture Managers
        public static ButtonMenu spriteButton;
        public static Tile spriteCursor;
        public static Tile spriteLetters;
        public static Tile spriteLines;
        // Positions
        private Vector2 buttonStartPos;
        private Vector2 gridStartPos;
        private Vector2 midScreen;
        private Vector2 sizeScreen;
        private Vector2 posHeadingNameList;
        private Vector2 posHeadingWordsList;
        // Dimensions
        private int widthGrid, heightGrid;
        // menu, headings
        private string nameHeadingNameList = "";
        private const string nameHeadingWordsList = "List of Words:",
            promptCategory = "Select a category", promptSize = "Select a Size:";
        private const string nameButtonStart = "Start Game", nameButtonQuit = "Quit Game";
        // Button Lists
        public static List<ButtonTile> listLettersGrid = new List<ButtonTile>();
        public static List<ButtonMenu> listButtonsMenu = new List<ButtonMenu>();
        // Grid
        private Grid grid;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // SpriteBatch for drawing multiple sprites at once
            sb = new SpriteBatch(GraphicsDevice);

            // SetupScale sprite atlas textures for drawing sprites
            textureButtonMenu = Content.Load<Texture2D>("button");
            textureCursor = Content.Load<Texture2D>("cursor");
            spriteCursor = new Tile(textureCursor, 1, 1);
            textureLetters = Content.Load<Texture2D>("alphabet");
            spriteLetters = new Tile(textureLetters, 2, 13);
            textureLines = Content.Load<Texture2D>("lines");
            spriteLines = new Tile(textureLines, 1, 4);
            fontWords = Content.Load<SpriteFont>("fontWords");
            fontHeadings = Content.Load<SpriteFont>("fontHeadings");

            SetupGame();

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        // Setup game from user input
        private void SetupGame()
        {
            grid.SetupGridGame("Mammals", 16);
            nameHeadingNameList = "Mammals";

            // Create buttons, place in listLettersGrid list
            SetupListLettersGrid();
            SetupListButtonsMenu();
        }
        private void SetupListLettersGrid()
        {
            int counter = 0;

            foreach (char letter in grid.GridGame)
            {
                ButtonTile button = new ButtonTile(letter.ToString() + counter.ToString(),
                    new Vector2(0f, 0f));
                listLettersGrid.Add(button);
                counter++;
            }
        }
        private void SetupListButtonsMenu()
        {
            // Set up game menu buttons
            ButtonMenu buttonStart = new ButtonMenu(nameButtonStart, textureButtonMenu);
            listButtonsMenu.Add(buttonStart);
            ButtonMenu buttonQuit = new ButtonMenu(nameButtonQuit, textureButtonMenu);
            listButtonsMenu.Add(buttonQuit);
            // Set up game customisation menu buttons

        }

        protected override void Update(GameTime gameTime)
        {
            UpdatePositions();
            UpdateInput();

            base.Update(gameTime);
        }
        private void UpdatePositions()
        {
            sizeScreen.X = gdManager.GraphicsDevice.Viewport.Width;
            sizeScreen.Y = gdManager.GraphicsDevice.Viewport.Height;
            midScreen.X = (sizeScreen.X * 0.5f);
            midScreen.Y = (sizeScreen.Y * 0.5f);
            widthGrid = grid.GridGame.GetLength(0) * spriteLetters.WidthSprite;
            heightGrid = grid.GridGame.GetLength(1) * spriteLetters.HeightSprite;

            // screen width - (quarter difference between screen width and grid width) - half width of texture
            buttonStartPos.X = (sizeScreen.X - ((sizeScreen.X - widthGrid) * 0.25f)) - (textureButtonMenu.Width * 0.5f) * managerDisplay.ScaleWidth;

        }
        private void UpdateInput()
        {
            // Mouse input
            MouseState mouseState = Mouse.GetState();
            posMouse.X = mouseState.X;
            posMouse.Y = mouseState.Y;

            foreach (ButtonTile button in listLettersGrid)
            {
                button.Update(posMouse);
            }
            foreach (ButtonMenu button in listButtonsMenu)
            {
                button.Update(posMouse);
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
            DrawGrid(sb, spriteLetters, SCALE_TILES);
            DrawButtonsMenu(sb);
            DrawHeadings(sb);
            DrawWordsList(sb);
            DrawMouse(sb, spriteCursor, SCALE_CURSOR);

            base.Draw(gameTime);
        }

        private void DrawHeadings(SpriteBatch sb)
        {
            string wordLongest = Helper.LongestWord(grid.WordsGame);
            // screen width - grid width + half length of longest word
            posHeadingWordsList.X = (midScreen.X - widthGrid) + fontHeadings.MeasureString(wordLongest).X / 2;
            // 10% screen height
            posHeadingWordsList.Y = sizeScreen.Y * 0.1f;
            // half screen width - half heading text width
            posHeadingNameList.X = midScreen.X - (fontHeadings.MeasureString(nameHeadingNameList).X * 0.5f);
            // half screen height - half height of grid - half heading text height
            posHeadingNameList.Y = midScreen.Y - (heightGrid * 0.5f) - fontHeadings.MeasureString(nameHeadingNameList).Y;

            // Draw
            sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend,
                SamplerState.PointWrap, transformMatrix: ManagerDisplay.ScaleMatrix);
            sb.DrawString(fontHeadings, nameHeadingNameList, posHeadingNameList, Color.Cornsilk);
            sb.DrawString(fontHeadings, nameHeadingWordsList, posHeadingWordsList, Color.Cornsilk);

            sb.End();
        }
        private void DrawWordsList(SpriteBatch sb)
        {
            // For aligning all words in list
            string wordLongest = $"  .{Helper.LongestWord(grid.WordsGame)}";
            // Set up position
            Vector2 position = new Vector2(0f, 0f);
            float posWidth = (midScreen.X - (widthGrid)) + fontWords.MeasureString(wordLongest).X / 2;
            float posHeight = (sizeScreen.Y * 0.175f);

            // Draw
            sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend,
                SamplerState.PointWrap, transformMatrix: ManagerDisplay.ScaleMatrix);
            int counter = 0;
            foreach (string word in grid.WordsGame)
            {
                // Modify position for each word in list
                position.X = posWidth;
                position.Y = posHeight + (fontWords.LineSpacing * counter);

                sb.DrawString(fontWords, $"{(counter + 1).ToString()}. {word}" , position, Color.Cornsilk);

                counter++;
            }
            sb.End();
        }
        private void DrawButtonsMenu(SpriteBatch sb)
        {
            // Set up position
            float posHeightStart = (sizeScreen.Y * 0.1f);
            float posHeightQuit = (sizeScreen.Y * 0.825f);

            // Draw

            // To center buttons between grid, screen end:
            // screenWidth - ((screenWidth - widthGrid) / 2) - buttonWidth
            listButtonsMenu[0].Draw(sb, new Vector2(buttonStartPos.X, posHeightStart), 1f);
            listButtonsMenu[1].Draw(sb, new Vector2(buttonStartPos.X, posHeightQuit), 1f);
        }
        private void DrawGrid(SpriteBatch sb, Tile spriteLetters, float scale)
        {
            int numCols = grid.GridGame.GetLength(0);
            int numRows = grid.GridGame.GetLength(1);

            int counterNewLine = 0;
            int counterRow = 0;
            int counterCol = 0;

            // Position details
            Vector2 position = new Vector2(0f, 0f);
            float gridStartX = (sizeScreen.X * 0.5f)- (numCols * spriteLetters.WidthSprite * 0.5f);
            float gridStartY = (sizeScreen.Y * 0.5f) - (numRows * spriteLetters.HeightSprite * 0.5f);

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
                position = new Vector2(gridStartX + counterCol * spriteLetters.WidthSprite,
                    gridStartY + (counterRow - 1) * spriteLetters.HeightSprite);
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
        private void DrawMouse(SpriteBatch sb, Tile textureCursor, float scale)
        {
            // Draw
            textureCursor.Draw(sb, '0', new Vector2(posMouse.X, posMouse.Y), scale);
        }       
    }
}