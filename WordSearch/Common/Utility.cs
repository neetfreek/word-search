using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace WordSearch.Common
{
    static class Utility
    {
        // Global constants
        public const float SCALE_CURSOR = 0.3f, SCALE_TILES = 0.6f;
        public const string nameHeadingWordsList = "List of Words:";
        public const string nameBackground = "backgroundMenu", nameButtonStart = "Start Game", nameButtonBack = "Back",
            nameButtonCategory = "Select a category", nameButtonSize = "Select a Size:", nameButtonQuit = "Quit Game",
            nameButtonSmall = "6 Words", nameButtonMedium = "10 Words", nameButtonLarge = "16 Words";
        
        // Text values
        public static string nameHeadingNameList = "";
    }
}

