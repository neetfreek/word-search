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
        public const string nameButtonStart = "start", nameButtonQuit = "quit", nameButtonMenu = "menu", nameButtonHowTo = "howTo",
            nameButtonCategory = "categories", nameButtonSize = "sizes", nameButtonSmall = "small", nameButtonMedium = "medium",
            nameButtonLarge = "large";
        // Text 
        public const string nameBackground = "backgroundMenu", textButtonStart = "Start Game", textButtonQuit = "Quit Game",
            textbuttonMenu = "Menu", textButtonHowTo = "How to Play", textButtonCategory = "Select a category", textButtonSize = "Select a Size:",
            textButtonSmall = "4 Words", textButtonMedium = "8 Words", textButtonLarge = "12 Words", textMessageWin = "You found all the words!", textMessageHowTo = "Left-click to select adjacent tiles to find the words on the list. Right click to deselect tiles.";
    }
}

public enum ButtonMenu
{
    none,
    start,
    menu,
    quit,
    howTo,
    instruments,
    mammals,
    occupations,
    colours,
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