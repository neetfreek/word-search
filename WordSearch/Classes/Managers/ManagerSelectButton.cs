/*==========================================================*
*  Handle actions for selecting menu buttons with mouse     *
*   in menu, game                                           *
*===========================================================*/

namespace WordSearch
{
    public static class ManagerSelectButton
    {
        public static void SelectButton(MainGame game)
        {
            switch (MainGame.ClickedButton)
            {
                case ButtonMenu.start:
                    MainGame.SelectedMenu = SelectedMenu.categories;
                    game.ToggleSizeListButtons(MainGame.listButtonsMenuStart, false);
                    game.ToggleSizeListButtons(MainGame.listButtonsCategories, true);
                    break;
                case ButtonMenu.quit:
                    game.Quit();
                    break;
                case ButtonMenu.menu:
                    if (MainGame.InGame)
                    {
                        game.ClearGame();
                        MainGame.SelectedMenu = SelectedMenu.start;
                        game.HandleSetupMenu();
                    }
                    else
                    {
                        game.ClearListsMenu();

                        MainGame.SelectedMenu = SelectedMenu.start;
                        game.HandleSetupMenu();
                    }
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
                    MainGame.SelectedSize = SettingsSize.small;
                    game.HandleSetupGameScreen(MainGame.SelectedCategory.ToString(), (int)MainGame.SelectedSize);
                    game.ClearListsMenu();
                    break;
                case ButtonMenu.medium:
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                    MainGame.SelectedSize = SettingsSize.medium;
                    game.HandleSetupGameScreen(MainGame.SelectedCategory.ToString(), (int)MainGame.SelectedSize);
                    game.ClearListsMenu();
                    break;
                case ButtonMenu.large:
                    game.ToggleSizeListButtons(MainGame.listButtonsSizes, false);
                    MainGame.SelectedSize = SettingsSize.large;
                    game.HandleSetupGameScreen(MainGame.SelectedCategory.ToString(), (int)MainGame.SelectedSize);
                    game.ClearListsMenu();
                    break;
            }
        }
    }
}