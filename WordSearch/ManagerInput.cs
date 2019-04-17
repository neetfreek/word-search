/*==============================================================*
*  Handle mouse, keyboard input                                 *
*===============================================================*
* Called by MainGame.Update()                                   *
* ==============================================================*/

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

            //Console.WriteLine($"Mousing over {MainGame.ButtonMousedOver}, selectedMenu {MainGame.SelectedMenu}");


            MainGame.ButtonMousedOver = ButtonMenu.none;
            MainGame.ButtonClicked = ButtonMenu.none;
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
            foreach (ButtonTile button in MainGame.listLettersGrid)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.listButtonsGame)
            {
                button.Update(posMouse);
            }
        }

        private void HandleClickButton(MouseState mouseState, MainGame game)
        {
            Console.WriteLine($"Mousing over {MainGame.ButtonMousedOver}");

            if (MainGame.ButtonMousedOver != ButtonMenu.none && mouseState.LeftButton == ButtonState.Pressed)
            {
                MainGame.ButtonClicked = MainGame.ButtonMousedOver;
                Console.WriteLine($"Clicked {MainGame.ButtonClicked}");
                switch (MainGame.ButtonClicked)
                {
                    case ButtonMenu.start:
                        if (MainGame.InGame)
                        {
                            //MainGame.InGame = false;
                            MainGame.SelectedMenu = SelectedMenu.start;
                            game.HandleSetupMenu();
                        }
                        else
                        {
                            MainGame.SelectedMenu = SelectedMenu.categories;
                            game.ToggleSizeListButtons(MainGame.listButtonsMenuStart, false);
                            game.ToggleSizeListButtons(MainGame.listButtonsCategories, true);
                        }
                        break;
                    case ButtonMenu.menu:
                        MainGame.SelectedMenu = SelectedMenu.start;
                        game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsMenuStart, true);
                        break;
                    case ButtonMenu.quit:
                        game.Quit();
                        break;
                    case ButtonMenu.instruments:
                        game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, true);
                        MainGame.selectedCategory = MainGame.ButtonClicked;
                        MainGame.SelectedMenu = SelectedMenu.sizes;
                        break;
                    case ButtonMenu.mammals:
                        game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, true);
                        MainGame.selectedCategory = MainGame.ButtonClicked;
                        MainGame.SelectedMenu = SelectedMenu.sizes;
                        break;
                    case ButtonMenu.occupations:
                        game.ToggleSizeListButtons(MainGame.listButtonsCategories, false);
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, true);
                        MainGame.selectedCategory = MainGame.ButtonClicked;
                        MainGame.SelectedMenu = SelectedMenu.sizes;
                        break;
                    case ButtonMenu.small:
                        MainGame.selectedSize = SettingsSize.small;
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                        game.HandleSetupGameScreen(MainGame.selectedCategory.ToString(), (int)MainGame.selectedSize);
                        game.ClearLists();
                        break;
                    case ButtonMenu.medium:
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                        MainGame.selectedSize = SettingsSize.medium;
                        game.HandleSetupGameScreen(MainGame.selectedCategory.ToString(), (int)MainGame.selectedSize);
                        game.ClearLists();
                        break;
                    case ButtonMenu.large:
                        game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                        MainGame.selectedSize = SettingsSize.large;
                        game.HandleSetupGameScreen(MainGame.selectedCategory.ToString(), (int)MainGame.selectedSize);
                        game.ClearLists();
                        break;
                }
                cooldownClick = true;
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
            Console.WriteLine("DONE!");
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
