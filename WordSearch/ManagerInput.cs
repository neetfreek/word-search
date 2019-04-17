/*==============================================================*
*  Handle mouse, keyboard input                                 *
*===============================================================*
* Called by MainGame.Update()                                   *
* ==============================================================*/

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

        public ManagerInput()
        {
            posMouse = new Vector2(0f, 0f);
        }

        public void UpdateInputMouse(MainGame game)
        {
            MouseState mouseState = Mouse.GetState();
            posMouse.X = mouseState.X;
            posMouse.Y = mouseState.Y;

            if (MainGame.inGame)
            {
                HandleMouseButtonsGame();
            }
            else
            {
                HandleMouseButtonsMenu();
            }

            HandleClickButton(mouseState);

            MainGame.mouseOver = ButtonMenu.none;
        }

        private void HandleMouseButtonsMenu()
        {
            foreach (SpriteRectangle button in MainGame.listButtonsMenuStart)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.listButtonsSizes)
            {
                button.Update(posMouse);
            }
            foreach (SpriteRectangle button in MainGame.listButtonsMenuStart)
            {
                button.Update(posMouse);
            }
        }
        private void HandleMouseButtonsGame()
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

        private void HandleClickButton(MouseState mouseState)
        {
            if (MainGame.mouseOver != ButtonMenu.none && mouseState.LeftButton == ButtonState.Pressed)
            {
                System.Console.WriteLine($"Clicked {MainGame.mouseOver}");
            }
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
