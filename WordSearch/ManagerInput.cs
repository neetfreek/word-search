/*==============================================================*
*  Handle mouse, keyboard input                                 *
*===============================================================*
* Called by MainGame.Update()                                   *
* ==============================================================*/

using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WordSearch.Common;

namespace WordSearch
{
    public class ManagerInput
    {
        private Vector2 posMouse;
        public Vector2 PosMouse
        {
            get { return posMouse; }
            set { posMouse = value; }
        }
        private static bool cooldownClick, cooldownRunning;

        public ManagerInput()
        {
            posMouse = new Vector2(0f, 0f);
        }

        public void UpdateInputMouse(MainGame game)
        {
            MouseState mouseState = Mouse.GetState();
            posMouse.X = mouseState.X;
            posMouse.Y = mouseState.Y;            

            if (cooldownClick && !cooldownRunning)
            {
                CooldownClickStart();
            } 
            
            if (!cooldownClick)

            {
                if (MainGame.InGame)
                {
                    HandleMouseButtonsGame(game);
                }
                else
                {
                    HandleMouseButtonsMenu(game);
                }

                HandleClickButton(mouseState, game);
            }

            MainGame.MousedOverButton = ButtonMenu.none;
            MainGame.ClickedButton = ButtonMenu.none;
            MainGame.MousedOverTile = null;
            MainGame.ClickedTile = null;
        }

        private void HandleMouseButtonsMenu(MainGame game)
        {
            foreach (SpriteRectangle button in MainGame.listButtonsMenuStart)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.listButtonsCategories)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.listButtonsSizes)
            {
                button.Update(posMouse);
            }
        }
        private void HandleMouseButtonsGame(MainGame game)
        {
            foreach (ButtonTile tile in MainGame.listLettersGrid)
            {
                tile.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.listButtonsGame)
            {
                button.Update(posMouse);
            }
        }

        private void HandleClickButton(MouseState mouseState, MainGame game)
        {
            if (MainGame.MousedOverButton != ButtonMenu.none && mouseState.LeftButton == ButtonState.Pressed)
            {
                MainGame.ClickedButton = MainGame.MousedOverButton;
                ClickButton(game);
                cooldownClick = true;
            }
            if (MainGame.MousedOverTile != null && mouseState.LeftButton == ButtonState.Pressed)
            {
                MainGame.ClickedTile = MainGame.MousedOverTile;
                ClickTile(game);
                cooldownClick = true;
            }
        }
        private void ClickButton(MainGame game)
        {
            switch (MainGame.ClickedButton)
            {
                case ButtonMenu.start:
                    MainGame.SelectedMenu = SelectedMenu.categories;
                    game.ToggleSizeListButtons(MainGame.listButtonsMenuStart, false);
                    game.ToggleSizeListButtons(MainGame.listButtonsCategories, true);

                    break;
                case ButtonMenu.menu:
                    if (MainGame.InGame)
                    {
                        game.ClearListsGame();
                        MainGame.SelectedMenu = SelectedMenu.start;
                        game.HandleSetupMenu();
                    }
                    else
                    {
                        MainGame.SelectedMenu = SelectedMenu.start;
                        game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsMenuStart, true);
                    }
                    break;
                case ButtonMenu.quit:
                    game.Quit();
                    break;
                case ButtonMenu.instruments:
                    game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, true);
                    MainGame.SelectedCategory = MainGame.ClickedButton;
                    MainGame.SelectedMenu = SelectedMenu.sizes;
                    break;
                case ButtonMenu.mammals:
                    game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, true);
                    MainGame.SelectedCategory = MainGame.ClickedButton;
                    MainGame.SelectedMenu = SelectedMenu.sizes;
                    break;
                case ButtonMenu.occupations:
                    game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, true);
                    MainGame.SelectedCategory = MainGame.ClickedButton;
                    MainGame.SelectedMenu = SelectedMenu.sizes;
                    break;
                case ButtonMenu.small:
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                    MainGame.selectedSize = SettingsSize.small;
                    game.HandleSetupGameScreen(MainGame.SelectedCategory.ToString(), (int)MainGame.selectedSize);
                    game.ClearListsMenu();
                    break;
                case ButtonMenu.medium:
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                    MainGame.selectedSize = SettingsSize.medium;
                    game.HandleSetupGameScreen(MainGame.SelectedCategory.ToString(), (int)MainGame.selectedSize);
                    game.ClearListsMenu();
                    break;
                case ButtonMenu.large:
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                    MainGame.selectedSize = SettingsSize.large;
                    game.HandleSetupGameScreen(MainGame.SelectedCategory.ToString(), (int)MainGame.selectedSize);
                    game.ClearListsMenu();
                    break;
            }
        }
        private void ClickTile(MainGame game)
        {
            Console.WriteLine($"Clicked {MainGame.ClickedTile.NameDraw}");
            if (MainGame.listLinesGrid.Contains((MainGame.ClickedTile)))
            {
                MainGame.listLinesGrid.Remove(MainGame.ClickedTile);
            }
            else
            {
                MainGame.listLinesGrid.Add(MainGame.ClickedTile);
            }
        }

        private void CooldownClickStart()
        {
            cooldownRunning = true;
            Task.Delay(300).ContinueWith(t => CooldownClickEnd());
        }
        private void CooldownClickEnd()
        {
            cooldownClick = false;
            cooldownRunning = false;
        }

        public void UpdateInputKeyboard(MainGame game)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.Quit();
            }
        }
    }    
}