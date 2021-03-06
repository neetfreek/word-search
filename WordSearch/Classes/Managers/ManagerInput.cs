﻿/*==============================================================*
*  Handle mouse, keyboard input                                 *
*===============================================================*
* Called by MainGame.Update()                                   *
* ==============================================================*/
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
        public static float ClickCooldown;

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
                    UpdateClickablesGame(game);
                }
                else
                {
                    UpdateClickablesMenu(game);
                }

                HandleMouseClicks(mouseState, game);
            }

            MainGame.MousedOverButton = ButtonMenu.none;
            MainGame.ClickedButton = ButtonMenu.none;
            MainGame.MousedOverTile = null;
            MainGame.ClickedTile = null;
        }

        // Call update on anything clickable in menu, game
        private void UpdateClickablesMenu(MainGame game)
        {
            foreach (SpriteRectangle button in MainGame.ListButtonsMenuStart)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.ListButtonsCategories)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.ListButtonsSizes)
            {
                button.Update(posMouse);
            }
        }
        private void UpdateClickablesGame(MainGame game)
        {
            foreach (ButtonTile tile in MainGame.ListLettersGrid)
            {
                tile.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.ListButtonsGame)
            {
                button.Update(posMouse);
            }
        }

        // Routes to ManagerSelectButton.cs, ManagerSelectTile based on clicked object
        private void HandleMouseClicks(MouseState mouseState, MainGame game)
        {
            // Left-click menu buttons
            if (MainGame.MousedOverButton != ButtonMenu.none && mouseState.LeftButton == ButtonState.Pressed)
            {
                MainGame.ClickedButton = MainGame.MousedOverButton;
                ManagerSelectButton.SelectButton(game);
                cooldownClick = true;
            }
            // Left-click grid letter tiles
            if (MainGame.MousedOverTile != null && mouseState.LeftButton == ButtonState.Pressed)
            {
                ManagerSelectTile.SelectTile(game);
            }
            // Right-click grid letter tiles
            if (MainGame.MousedOverTile != null && mouseState.RightButton == ButtonState.Pressed)
            {
                ManagerSelectTile.UnselectTile();
            }
        }

        // Handle waiting period between clicks in menus to avoid multiple clicks on same button
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
    }    
}