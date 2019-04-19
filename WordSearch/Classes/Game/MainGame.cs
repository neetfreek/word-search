/*==============================================================*
*  ISSUES:                                                      *
*===============================================================*
* Must apply scaling to add buttons, calculations relating  *   *
*   to their placement                                          *
* Change game sizes; grid sometimes too large for screen        *
/*=============================================================*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        // Texture Managers
        public static SpriteRectangle spriteButton;
        public static SpriteTile spriteCursor;
        public static SpriteTile spriteLetters;
        public static SpriteTile spriteLines;
        // Input Manager 
        public static ManagerInput managerInput;
        // Textures
        private Texture2D textureCursor;
        private Texture2D textureLetters;
        private Texture2D textureLines;
        private Texture2D textureButtonMenu;
        public static SpriteFont fontWords;
        private SpriteFont fontHeadings;
        // Positions
        private Vector2 posBackgroundMenu, posButtonGame, posButtonMenu;
        private Vector2 posMidScreen;
        private Vector2 posMouse;
        private Vector2 posHeadingNameCategoryList, posHeadingWordsList;
        // Dimensions
        private Vector2 scaleBackgroundMenu, scaleDefault, sizeScreen, sizeMenu;
        private int widthGrid, heightGrid;
        // Contain all grid tiles
        public static List<ButtonTile> listLettersGrid = new List<ButtonTile>();
        public static List<string> listWordsToFind = new List<string>();
        // Contain currently selected tiles to draw lines over
        public static List<ButtonTile> listTilesTemporary = new List<ButtonTile>();
        public static string wordTilesTemporary;
        // Contain tiles of found word to always draw lines over
        public static List<ButtonTile> listTilesPermanent = new List<ButtonTile>();
        // Contain tile clicked to highlight
        public static List<ButtonTile> listTileHighlight = new List<ButtonTile>();
        public static List<ButtonTile> listLineTilesHorizontal = new List<ButtonTile>();
        public static List<ButtonTile> listLineTilesVertical = new List<ButtonTile>();
        public static List<ButtonTile> listLineTilesDownRight = new List<ButtonTile>();
        public static List<ButtonTile> listLineTilesUpRight = new List<ButtonTile>();
        // Contain menu buttons
        public static List<SpriteRectangle> listButtonsGame = new List<SpriteRectangle>();
        public static List<SpriteRectangle> listButtonsMenuStart = new List<SpriteRectangle>();
        public static List<SpriteRectangle> listButtonsSizes = new List<SpriteRectangle>();
        public static List<SpriteRectangle> listButtonsCategories = new List<SpriteRectangle>();
        // Grid
        private Grid grid;
        // Menu background
        private SpriteRectangle backgroundMenu;
        // State
        public static bool InGame;
        public static ButtonMenu MousedOverButton, ClickedButton, SelectedCategory;
        public static ButtonTile MousedOverTile, ClickedTile;
        public static SelectedMenu SelectedMenu;
        public static SettingsSize SelectedSize;

        /*==============* 
        *  Manage Game  *
        *===============*/
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
            // Setup input
            managerInput = new ManagerInput();
            posMouse = new Vector2(gdManager.GraphicsDevice.Viewport.Width * 0.5f,
                gdManager.GraphicsDevice.Viewport.Height * 0.5f);
            // Setup grid
            grid = new Grid();
        }
        protected override void Initialize()
        {
            scaleDefault = new Vector2(1f, 1f);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // SpriteBatch for drawing multiple sprites at once
            sb = new SpriteBatch(GraphicsDevice);

            // SetupScale sprite atlas textures for drawing sprites
            textureButtonMenu = Content.Load<Texture2D>("button");
            textureCursor = Content.Load<Texture2D>("cursor");
            spriteCursor = new SpriteTile(textureCursor, 1, 1);
            textureLetters = Content.Load<Texture2D>("alphabet");
            spriteLetters = new SpriteTile(textureLetters, 2, 13);
            textureLines = Content.Load<Texture2D>("lines");
            spriteLines = new SpriteTile(textureLines, 1, 4);
            fontWords = Content.Load<SpriteFont>("fontWords");
            fontHeadings = Content.Load<SpriteFont>("fontHeadings");

            HandleSetupMenu();
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        public void Quit()
        {
            Exit();
        }
                                      
        /*==============*
        *  Setup Menu   *
        *===============*/
        public void HandleSetupMenu()
        {

            System.Console.WriteLine($"selectedMenu: {SelectedMenu}, selectedSize: {SelectedSize}");
            InGame = false;
            ManagerInput.ClickCooldown = Utility.CLICK_COOLDOWN_MENU;
            scaleBackgroundMenu = new Vector2(2f, 10f);
            backgroundMenu = new SpriteRectangle(Utility.nameBackground, textureButtonMenu, scaleBackgroundMenu, "");
            SetupListButtonsMenuStart();
            SetupListCategories();
            SetupListSizes();
        }
        private void SetupListButtonsMenuStart()
        {
            listButtonsMenuStart.Add(new SpriteRectangle(Utility.nameButtonStart, textureButtonMenu, scaleDefault, Utility.textButtonStart));
            listButtonsMenuStart.Add(new SpriteRectangle(Utility.nameButtonQuit, textureButtonMenu, scaleDefault, Utility.textButtonQuit));
        }
        private void SetupListCategories()
        {
            // Get names, number of buttons from XML            
            List<string> lists = ManagerData.AllLists();
            foreach (string list in lists)
            {
                listButtonsCategories.Add(new SpriteRectangle(list.ToLower(), textureButtonMenu, scaleDefault, Helper.UppercaseFirst(list)));
            }
            listButtonsCategories.Add(new SpriteRectangle(Utility.nameButtonMenu, textureButtonMenu, scaleDefault, Utility.textbuttonMenu));
        }
        private void SetupListSizes()
        {
            listButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonSmall, textureButtonMenu, scaleDefault, Utility.textButtonSmall));
            listButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonMedium, textureButtonMenu, scaleDefault, Utility.textButtonMedium));
            listButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonLarge, textureButtonMenu, scaleDefault, Utility.textButtonLarge));
            listButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonMenu, textureButtonMenu, scaleDefault, Utility.textbuttonMenu));
        }
        public void ClearListsMenu()
        {
            listButtonsMenuStart.Clear();
            listButtonsCategories.Clear();
            listButtonsSizes.Clear();
        }

        // true == maximise, false == minimise buttons
        public void ToggleSizeListButtons(List<SpriteRectangle> list, bool maximise)
        {
            Vector2 scale;

            if (maximise)
            {
                scale = new Vector2(1f, 1f);
            }
            else
            {
                scale = new Vector2(0f, 0f);

            }

            foreach (SpriteRectangle button in list)
            {
                button.Scale = scale;
            }
        }

        /*======================*
        *  Setup Game Screen    *
        *=======================*/
        public void HandleSetupGameScreen(string listChosen, int sizeChosen)
        {
            grid.SetupGridGame(listChosen, sizeChosen);
            Utility.nameHeadingNameList = listChosen;

            // Create buttons, place in listLettersGrid list
            SetupListWordsToFind();
            SetupListLettersGrid();
            SetupListButtonsGame();
            InGame = true;
        }
        private void SetupListWordsToFind()
        {
            foreach (string word in grid.WordsGame)
            {
                listWordsToFind.Add(word);
            }
        }
        private void SetupListLettersGrid()
        {
            int counter = 0;

            foreach (char letter in grid.GridGame)
            {
                listLettersGrid.Add(new ButtonTile(letter.ToString() + counter.ToString(),
                    new Vector2(0f, 0f)));
                counter++;
            }
        }
        private void SetupListButtonsGame()
        {
            // Set up game menu buttons
            listButtonsGame.Add(new SpriteRectangle(Utility.nameButtonMenu, textureButtonMenu, scaleDefault, Utility.textbuttonMenu));
            listButtonsGame.Add(new SpriteRectangle(Utility.nameButtonQuit, textureButtonMenu, scaleDefault, Utility.textButtonQuit));
        }
        public void ClearGame()
        {
            listLettersGrid.Clear();
            listButtonsGame.Clear();
            listTilesTemporary.Clear();
            listTilesPermanent.Clear();
            listTileHighlight.Clear();
            listLineTilesHorizontal.Clear();
            listLineTilesVertical.Clear();
            listLineTilesDownRight.Clear();
            listLineTilesUpRight.Clear();
            listWordsToFind.Clear();
            wordTilesTemporary = "";
            SelectedMenu = SelectedMenu.start;
            SelectedSize = 0;
            ManagerSelectTile.ResetValues();
        }

        /*==========*
        *  Update   *
        *===========*/
        protected override void Update(GameTime gameTime)
        {
            UpdatePositions();
            managerInput.UpdateInputMouse(this);
            managerInput.UpdateInputKeyboard(this);

            //MousedOverButton = ButtonMenu.none;
            base.Update(gameTime);
        }
        private void UpdatePositions()
        {            
            sizeScreen.X = gdManager.GraphicsDevice.Viewport.Width;
            sizeScreen.Y = gdManager.GraphicsDevice.Viewport.Height;
            posMidScreen.X = (sizeScreen.X * 0.5f);
            posMidScreen.Y = (sizeScreen.Y * 0.5f);

            if (InGame)
            {
                // Number columns * width of letter sprites
                widthGrid = grid.GridGame.GetLength(0) * spriteLetters.WidthSprite;
                // Number rows * width of letter sprites
                heightGrid = grid.GridGame.GetLength(1) * spriteLetters.HeightSprite;

                // Screen width - (quarter difference between screen width and grid width) - half texture width
                posButtonGame.X = (sizeScreen.X - ((sizeScreen.X - widthGrid) * 0.25f)) - (textureButtonMenu.Width * 0.5f) * managerDisplay.ScaleWidth;
            }
            else
            {
                sizeMenu.X = backgroundMenu.TextureButtonMenu.Width * backgroundMenu.Scale.X;
                sizeMenu.Y = backgroundMenu.TextureButtonMenu.Height * backgroundMenu.Scale.Y;

                // Mid screen - half scaled width of texture
                posBackgroundMenu.X = posMidScreen.X - (sizeMenu.X * 0.5f);
                // Mid screen - half scaled height of texture
                posBackgroundMenu.Y = posMidScreen.Y - (sizeMenu.Y * 0.5f);

                posButtonMenu.X = posBackgroundMenu.X + (int)(sizeMenu.X * 0.5f) - (textureButtonMenu.Width * 0.5f);
                posButtonMenu.Y = posBackgroundMenu.Y + (sizeMenu.Y * 0.1f);
            }
        }

        /*======*
        *  Draw *
        *=======*/
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Teal);

            if (InGame)
            {
                DrawGrid(spriteLetters);
                DrawButtonsGame();
                HighlightTile();
                DrawLinesCurrentSelection(listTilesTemporary);
                DrawLinesHorizontal(listLineTilesHorizontal);
                DrawLinesVertical(listLineTilesVertical);
                DrawLinesUpRight(listLineTilesUpRight);
                DrawLinesDownRight(listLineTilesDownRight);
                DrawHeadings();
                DrawWordsList();
            }
            else
            {
                switch (SelectedMenu)
                {
                    case SelectedMenu.start:
                        DrawMenu(listButtonsMenuStart);
                        break;
                    case SelectedMenu.categories:
                        DrawMenu(listButtonsCategories);
                        break;
                    case SelectedMenu.sizes:
                        DrawMenu(listButtonsSizes);
                        break;

                }
                //DrawMenu(listButtonsSizes);
            }

            DrawMouse(spriteCursor, Utility.SCALE_CURSOR);

            base.Draw(gameTime);
        }

        public void DrawMenu(List<SpriteRectangle> list)
        {
            // Background
            backgroundMenu.Draw(sb, new Vector2(posBackgroundMenu.X, posBackgroundMenu.Y));

            // Space between buttons and buttons, menu edges
            float gapButtons = 0f;
            // Adjusted position to include gaps between each button
            Vector2 newPos = posButtonMenu;

            // Draw buttons
            for (int counter = 0; counter < list.Count; counter++)
            {
                list[counter].Draw(sb, newPos);

                // Update newPos for next button
                gapButtons = sizeMenu.Y * 0.15f;
                newPos.Y += gapButtons;
            }
        }

        public void DrawHeadings()
        {
            string wordLongest = Helper.LongestWord(grid.WordsGame);
            // screen width - grid width + half length of longest word
            posHeadingWordsList.X = (int)((posMidScreen.X - widthGrid) * 0.5) + fontHeadings.MeasureString(wordLongest).X / 2;
            // 10% screen height
            posHeadingWordsList.Y = sizeScreen.Y * 0.175f;
            // half screen width - half heading text width
            posHeadingNameCategoryList.X = posMidScreen.X - (fontHeadings.MeasureString(Utility.nameHeadingNameList).X * 0.5f);
            // half screen height - half height of grid - half heading text height
            posHeadingNameCategoryList.Y = posMidScreen.Y - (heightGrid * 0.5f) - fontHeadings.MeasureString(Utility.nameHeadingNameList).Y * 1.1f;

            // Draw
            sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend,
                SamplerState.PointWrap, transformMatrix: ManagerDisplay.ScaleMatrix);
            sb.DrawString(fontHeadings, Helper.UppercaseFirst(Utility.nameHeadingNameList), posHeadingNameCategoryList, Color.Cornsilk);
            sb.DrawString(fontHeadings, Utility.nameHeadingWordsList, posHeadingWordsList, Color.Cornsilk);

            sb.End();
        }
        public void DrawWordsList()
        {
            // For aligning all words in list
            string wordLongest = $"  .{Helper.LongestWord(grid.WordsGame)}";
            // Set up position
            Vector2 position = new Vector2(0f, 0f);
            float posWidth = (int)((posMidScreen.X - widthGrid) * 0.5) + fontHeadings.MeasureString(wordLongest).X / 2;
            float posHeight = (sizeScreen.Y * 0.25f);

            // Draw
            sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend,
                SamplerState.PointWrap, transformMatrix: ManagerDisplay.ScaleMatrix);
            int counter = 0;
            foreach (string word in listWordsToFind)
            {
                // Modify position for each word in list
                position.X = posWidth;
                position.Y = posHeight + (fontWords.LineSpacing * counter);

                sb.DrawString(fontWords, $"{(counter + 1).ToString()}. {word}" , position, Color.Cornsilk);

                counter++;
            }
            sb.End();
        }

        public void DrawButtonsGame()
        {
            // Set up position
            float posHeightStart = (sizeScreen.Y * 0.725f);
            float posHeightQuit = (sizeScreen.Y * 0.825f);

            // Draw
            // screenWidth - ((screenWidth - widthGrid) / 2) - buttonWidth
            listButtonsGame[0].Draw(sb, new Vector2(posButtonGame.X, posHeightStart));
            listButtonsGame[1].Draw(sb, new Vector2(posButtonGame.X, posHeightQuit));
        }

        public void DrawGrid(SpriteTile spriteLetters)
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
                spriteLetters.Draw(sb, toDraw, position, Utility.SCALE_TILES, Color.White);
                // Update letter posSprite
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
        public void HighlightTile()
        {
            foreach (ButtonTile button in listTileHighlight)
            {
                spriteLetters.Draw(sb, char.Parse(button.NameDraw), button.Pos, Utility.SCALE_TILES, Color.Green);
            }
        }

        public void DrawLinesCurrentSelection(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                spriteLines.Draw(sb, ManagerSelectTile.bearingChar, tile.Pos, Utility.SCALE_TILES, Color.LightGreen);
            }

        }
        public void DrawLinesHorizontal(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                spriteLines.Draw(sb, '-', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }
        public void DrawLinesVertical(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                spriteLines.Draw(sb, '|', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }
        public void DrawLinesUpRight(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                spriteLines.Draw(sb, '/', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }
        public void DrawLinesDownRight(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                spriteLines.Draw(sb, '\\', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }

        public void DrawMouse(SpriteTile textureCursor, float scale)
        {
            // Draw
            textureCursor.Draw(sb, '0', managerInput.PosMouse, scale, Color.White);
        }       
    }
}