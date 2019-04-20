/*==============================================================*
*  ISSUES:                                                      *
*===============================================================*
* Must apply scaling to add buttons, calculations relating  *   *
*   to their placement                                          *
* Change game sizes; grid sometimes too large for screen        *
/*=============================================================*/

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WordSearch.Common;

namespace WordSearch
{
    public class MainGame : Game
    {
        // Display
        private GraphicsDeviceManager gdManager;
        private SpriteBatch sb;
        public static ManagerDisplay ManagerDisplay;
        // Texture Managers
        public static SpriteRectangle SpriteButton;
        public static SpriteTile SpriteCursor;
        public static SpriteTile SpriteLetters;
        public static SpriteTile SpriteLines;
        // Input Manager 
        public static ManagerInput ManagerInput;
        // Textures
        private Texture2D textureCursor;
        private Texture2D textureLetters;
        private Texture2D textureLines;
        private Texture2D textureButtonMenu;
        public static SpriteFont FontWords;
        private SpriteFont fontHeadings;
        // Positions
        private Vector2 posBackgroundMenu, posBackgroundMessageWin, posButtonGame, posButtonMenu;
        private Vector2 posMidScreen;
        private Vector2 posMouse;
        private Vector2 posHeadingNameCategoryList, posHeadingWordsList;
        // Dimensions
        private Vector2 scaleDefault, sizeScreen, sizeMenu, sizeMessageWin;
        private int widthGrid, heightGrid;
        // Contain all grid tiles
        public static List<ButtonTile> ListLettersGrid = new List<ButtonTile>();
        public static List<string> ListWordsToFind = new List<string>();
        // Contain currently selected tiles to draw lines over
        public static List<ButtonTile> ListTilesTemporary = new List<ButtonTile>();
        public static string WordTilesTemporary;
        // Contain tiles of found word to always draw lines over
        public static List<ButtonTile> ListTilesPermanent = new List<ButtonTile>();
        // Contain tile clicked to highlight
        public static List<ButtonTile> ListTileHighlight = new List<ButtonTile>();
        public static List<ButtonTile> ListLineTilesHorizontal = new List<ButtonTile>();
        public static List<ButtonTile> ListLineTilesVertical = new List<ButtonTile>();
        public static List<ButtonTile> ListLineTilesDownRight = new List<ButtonTile>();
        public static List<ButtonTile> ListLineTilesUpRight = new List<ButtonTile>();
        // Contain menu buttons
        public static List<SpriteRectangle> ListButtonsGame = new List<SpriteRectangle>();
        public static List<SpriteRectangle> ListButtonsMenuStart = new List<SpriteRectangle>();
        public static List<SpriteRectangle> ListButtonsSizes = new List<SpriteRectangle>();
        public static List<SpriteRectangle> ListButtonsCategories = new List<SpriteRectangle>();
        // Grid
        private Grid grid;
        // Menu background
        private SpriteRectangle backgroundMenu, backgroundMessageHowTo, backgroundMessageWin;
        // State
        public static bool InGame, HowTo, WonGame;
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
            ManagerDisplay = new ManagerDisplay(gdManager);
            // Setup input
            ManagerInput = new ManagerInput();
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
            SpriteCursor = new SpriteTile(textureCursor, 1, 1);
            textureLetters = Content.Load<Texture2D>("alphabet");
            SpriteLetters = new SpriteTile(textureLetters, 2, 13);
            textureLines = Content.Load<Texture2D>("lines");
            SpriteLines = new SpriteTile(textureLines, 1, 4);
            FontWords = Content.Load<SpriteFont>("fontWords");
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
            InGame = false;
            ManagerInput.ClickCooldown = Utility.CLICK_COOLDOWN_MENU;
            Vector2 scaleBackgroundMenu = new Vector2(2f, 10f);
            backgroundMenu = new SpriteRectangle(Utility.nameBackground, textureButtonMenu, scaleBackgroundMenu, "");
            SetupListButtonsMenuStart();
            SetupListCategories();
            SetupListSizes();
        }
        private void SetupListButtonsMenuStart()
        {
            ListButtonsMenuStart.Add(new SpriteRectangle(Utility.nameButtonStart, textureButtonMenu, scaleDefault, Utility.textButtonStart));
            ListButtonsMenuStart.Add(new SpriteRectangle(Utility.nameButtonQuit, textureButtonMenu, scaleDefault, Utility.textButtonQuit));
        }
        private void SetupListCategories()
        {
            // Get names, number of buttons from XML            
            List<string> lists = ManagerData.AllLists();
            foreach (string list in lists)
            {
                ListButtonsCategories.Add(new SpriteRectangle(list.ToLower(), textureButtonMenu, scaleDefault, Helper.UppercaseFirst(list)));
            }
            ListButtonsCategories.Add(new SpriteRectangle(Utility.nameButtonMenu, textureButtonMenu, scaleDefault, Utility.textbuttonMenu));
        }
        private void SetupListSizes()
        {
            ListButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonSmall, textureButtonMenu, scaleDefault, Utility.textButtonSmall));
            ListButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonMedium, textureButtonMenu, scaleDefault, Utility.textButtonMedium));
            ListButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonLarge, textureButtonMenu, scaleDefault, Utility.textButtonLarge));
            ListButtonsSizes.Add(new SpriteRectangle(Utility.nameButtonMenu, textureButtonMenu, scaleDefault, Utility.textbuttonMenu));
        }
        public void ClearListsMenu()
        {
            ListButtonsMenuStart.Clear();
            ListButtonsCategories.Clear();
            ListButtonsSizes.Clear();
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
            Vector2 scaleBackgroundMessage = new Vector2(4f, 3f);
            backgroundMessageWin = new SpriteRectangle(Utility.nameBackground, textureButtonMenu, scaleBackgroundMessage, Utility.textMessageWin);
            backgroundMessageHowTo = new SpriteRectangle(Utility.nameBackground, textureButtonMenu, scaleBackgroundMessage, Utility.textMessageHowTo);
            // Create buttons, place in ListLettersGrid list
            SetupListWordsToFind();
            SetupListLettersGrid();
            SetupListButtonsGame();
            InGame = true;
        }
        private void SetupListWordsToFind()
        {
            foreach (string word in grid.WordsGame)
            {
                ListWordsToFind.Add(word);
            }
        }
        private void SetupListLettersGrid()
        {
            int counter = 0;

            foreach (char letter in grid.GridGame)
            {
                ListLettersGrid.Add(new ButtonTile(letter.ToString() + counter.ToString(),
                    new Vector2(0f, 0f)));
                counter++;
            }
        }
        private void SetupListButtonsGame()
        {
            // Set up game menu buttons
            ListButtonsGame.Add(new SpriteRectangle(Utility.nameButtonHowTo, textureButtonMenu, scaleDefault, Utility.textButtonHowTo));
            ListButtonsGame.Add(new SpriteRectangle(Utility.nameButtonMenu, textureButtonMenu, scaleDefault, Utility.textbuttonMenu));
            ListButtonsGame.Add(new SpriteRectangle(Utility.nameButtonQuit, textureButtonMenu, scaleDefault, Utility.textButtonQuit));

        }
        public void ClearGame()
        {
            ListLettersGrid.Clear();
            ListButtonsGame.Clear();
            ListTilesTemporary.Clear();
            ListTilesPermanent.Clear();
            ListTileHighlight.Clear();
            ListLineTilesHorizontal.Clear();
            ListLineTilesVertical.Clear();
            ListLineTilesDownRight.Clear();
            ListLineTilesUpRight.Clear();
            ListWordsToFind.Clear();
            WonGame = false;
            HowTo = false;
            WordTilesTemporary = "";
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
            ManagerInput.UpdateInputMouse(this);
            CheckWonGame();

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
                widthGrid = grid.GridGame.GetLength(0) * SpriteLetters.WidthSprite;
                // Number rows * width of letter sprites
                heightGrid = grid.GridGame.GetLength(1) * SpriteLetters.HeightSprite;

                // Screen width - (quarter difference between screen width and grid width) - half texture width
                posButtonGame.X = (sizeScreen.X - ((sizeScreen.X - widthGrid) * 0.25f)) - (textureButtonMenu.Width * 0.5f) * ManagerDisplay.ScaleWidth;

                if (WonGame || HowTo)
                {
                    sizeMessageWin.X = backgroundMessageWin.TextureButtonMenu.Width * backgroundMessageWin.Scale.X;
                    sizeMessageWin.Y = backgroundMessageWin.TextureButtonMenu.Height * backgroundMessageWin.Scale.Y;

                    // Mid screen - half scaled width of texture
                    posBackgroundMessageWin.X = posMidScreen.X - (sizeMessageWin.X * 0.5f);
                    // Mid screen - half scaled height of texture
                    posBackgroundMessageWin.Y = posMidScreen.Y - (sizeMessageWin.Y * 0.5f);
                }
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
        private void CheckWonGame()
        {
            if (InGame == true && ListWordsToFind.Count == 0)
            {
                WonGame = true;
                
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
                DrawGrid(SpriteLetters);
                DrawButtonsGame();
                HighlightTile();
                DrawLinesCurrentSelection(ListTilesTemporary);
                DrawLinesHorizontal(ListLineTilesHorizontal);
                DrawLinesVertical(ListLineTilesVertical);
                DrawLinesUpRight(ListLineTilesUpRight);
                DrawLinesDownRight(ListLineTilesDownRight);
                DrawHeadings();
                DrawWordsList();

                if (WonGame)
                {
                    DrawMessageWin();
                }
                if (HowTo)
                {
                    DrawHowTo();
                }
            }
            else
            {
                switch (SelectedMenu)
                {
                    case SelectedMenu.start:
                        DrawMenu(ListButtonsMenuStart);
                        break;
                    case SelectedMenu.categories:
                        DrawMenu(ListButtonsCategories);
                        break;
                    case SelectedMenu.sizes:
                        DrawMenu(ListButtonsSizes);
                        break;

                }
            }

            DrawMouse(SpriteCursor, Utility.SCALE_CURSOR);

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
        public void DrawMessageWin()
        {
            backgroundMessageWin.Draw(sb, new Vector2(posBackgroundMessageWin.X, posBackgroundMessageWin.Y));
        }
        public void DrawHowTo()
        {
            backgroundMessageHowTo.Draw(sb, new Vector2(posBackgroundMessageWin.X, posBackgroundMessageWin.Y));
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
            foreach (string word in ListWordsToFind)
            {
                // Modify position for each word in list
                position.X = posWidth;
                position.Y = posHeight + (FontWords.LineSpacing * counter);

                sb.DrawString(FontWords, $"{(counter + 1).ToString()}. {word}" , position, Color.Cornsilk);

                counter++;
            }
            sb.End();
        }

        public void DrawButtonsGame()
        {
            // Set up position
            float posHeightHowTo = (sizeScreen.Y * 0.175f);
            float posHeightStart = (sizeScreen.Y * 0.725f);
            float posHeightQuit = (sizeScreen.Y * 0.825f);

            // Draw
            // screenWidth - ((screenWidth - widthGrid) / 2) - buttonWidth
            ListButtonsGame[0].Draw(sb, new Vector2(posButtonGame.X, posHeightHowTo));
            ListButtonsGame[1].Draw(sb, new Vector2(posButtonGame.X, posHeightStart));
            ListButtonsGame[2].Draw(sb, new Vector2(posButtonGame.X, posHeightQuit));
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

            for (int counter = 0; counter < ListLettersGrid.Count; counter++)
            {
                //Fix counterRow for moving to next rown once enumerated enough columns
                if (counter == 0)
                {
                    counterRow = 1;
                }

                // Convert letter NameDraw to char
                char.TryParse(ListLettersGrid[counter].NameDraw, out char toDraw);
                // Get position to place letter
                position = new Vector2(gridStartX + counterCol * spriteLetters.WidthSprite,
                    gridStartY + (counterRow - 1) * spriteLetters.HeightSprite);
                // Draw sprite
                spriteLetters.Draw(sb, toDraw, position, Utility.SCALE_TILES, Color.White);
                // Update letter posSprite
                ListLettersGrid[counter].Pos = position;

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
            foreach (ButtonTile button in ListTileHighlight)
            {
                SpriteLetters.Draw(sb, char.Parse(button.NameDraw), button.Pos, Utility.SCALE_TILES, Color.Green);
            }
        }

        public void DrawLinesCurrentSelection(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                SpriteLines.Draw(sb, ManagerSelectTile.bearingChar, tile.Pos, Utility.SCALE_TILES, Color.LightGreen);
            }

        }
        public void DrawLinesHorizontal(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                SpriteLines.Draw(sb, '-', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }
        public void DrawLinesVertical(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                SpriteLines.Draw(sb, '|', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }
        public void DrawLinesUpRight(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                SpriteLines.Draw(sb, '/', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }
        public void DrawLinesDownRight(List<ButtonTile> list)
        {
            foreach (ButtonTile tile in list)
            {
                SpriteLines.Draw(sb, '\\', tile.Pos, Utility.SCALE_TILES, Color.White);
            }

        }

        public void DrawMouse(SpriteTile textureCursor, float scale)
        {
            // Draw
            textureCursor.Draw(sb, '0', ManagerInput.PosMouse, scale, Color.White);
        }       
    }
}