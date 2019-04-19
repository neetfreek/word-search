/*==================================*
*  Contain global variables, enums  *
*===================================*/

namespace WordSearch.Common
{
    static class Utility
    {
        public const float SCALE_CURSOR = 0.3f, SCALE_TILES = 0.50f, CLICK_COOLDOWN_MENU = 300;
        // Names
        public const string nameHeadingWordsList = "List of Words:";
        public static string nameHeadingNameList = "";
        public const string nameBackground = "backgroundMenu", nameButtonStart = "start", nameButtonMenu = "menu",
            nameButtonCategory = "categories", nameButtonSize = "sizes", nameButtonQuit = "quit",
            nameButtonSmall = "small", nameButtonMedium = "medium", nameButtonLarge = "large";
        // Text 
        public const string textButtonStart = "Start Game", textbuttonMenu = "Menu",
            textButtonCategory = "Select a category", textButtonSize = "Select a Size:", textButtonQuit = "Quit Game",
            textButtonSmall = "4 Words", textButtonMedium = "8 Words", textButtonLarge = "12 Words", textMessageWin = "You found all the words!";
    }
}

public enum ButtonMenu
{
    none,
    start,
    menu,
    quit,
    instruments,
    mammals,
    occupations,
    small,
    medium,
    large,
}

public enum SelectedMenu
{
    start,
    categories,
    sizes,
}

public enum SettingsSize
{
    small = 4,
    medium = 8,
    large = 12,
}