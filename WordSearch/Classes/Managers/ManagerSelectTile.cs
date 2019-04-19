/*==========================================================*
*  Handle actions for selecting tiles with mouse in-game    *
*===========================================================*/
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace WordSearch
{
    public static class ManagerSelectTile
    {
        private static float distance;
        private static bool bearingSet;
        private static Vector2 tilePosMidSelected;
        private static Vector2 tilePosMidCompare;
        private static Vector2 bearing;
        public static char bearingChar;

        public static void SelectTile(MainGame game)
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;

            // If > 0 tiles in ListTilesTemporary
            if (!MainGame.ListTilesTemporary.Contains(MainGame.ClickedTile) &&
                MainGame.ListTilesTemporary.Count > 0 && TileAdjacentToAdded())
            {
                SetBearing();
                // Third+ tiles
                if (MainGame.ListTilesTemporary.Count > 1)
                {
                    if (SameBearing())
                    {
                        HandleNewSelectedTile();
                    }
                }
                // Second tile
                else if (MainGame.ListTilesTemporary.Count == 1)
                {
                    HandleNewSelectedTile();
                }
            }
            // First tile
            else if (MainGame.ListTilesTemporary.Count == 0)
            {
                HandleNewSelectedTile();
            }
        }
        public static void UnselectTile()
        {
            // if right-clicked tile one of previously selected
            if (MainGame.ListTilesTemporary.Contains(MainGame.MousedOverTile))
            {
                MainGame.ListTileHighlight.Clear();
                MainGame.ClickedTile = MainGame.MousedOverTile;
                RemoveTileListTilesTemporary();
            }
        }

        public static void ResetValues()
        {
            distance = 0;
            bearingSet = false;
            tilePosMidSelected = new Vector2(0f, 0f);
            tilePosMidCompare = new Vector2(0f, 0f);
            bearing = new Vector2(0f, 0f);
        }

        private static void HandleNewSelectedTile()
        {
            MainGame.ListTileHighlight.Clear();
            MainGame.ListTileHighlight.Add(MainGame.ClickedTile);
            
            if (!MainGame.ListTilesTemporary.Contains(MainGame.ClickedTile))
            {
                MainGame.ListTilesTemporary.Add(MainGame.ClickedTile);
                MainGame.WordTilesTemporary += MainGame.ClickedTile.NameDraw;
                if (SelectionIsWord())
                {
                    AddSelectionToPermanent();
                    RemoveWordFromListWordsToFind();
                    ClearTemporary();
                }
            }
        }
        private static void RemoveTileListTilesTemporary()
        {
            if (MainGame.ListTilesTemporary.Contains((MainGame.ClickedTile)))
            {
                MainGame.ListTilesTemporary.Remove(MainGame.ClickedTile);
                MainGame.WordTilesTemporary = MainGame.WordTilesTemporary.Substring(0, MainGame.WordTilesTemporary.Length-1);

                if (MainGame.ListTilesTemporary.Count == 0)
                {
                    ResetValues();
                }
            }
        }
        private static void AddSelectionToPermanent()
        {
            for (int counter = 0; counter < MainGame.ListTilesTemporary.Count; counter++)
            {
                AddSelectionToDrawLines().Add(MainGame.ListTilesTemporary[counter]);
                MainGame.ListTileHighlight.Add(MainGame.ListTilesTemporary[counter]);
                MainGame.ListTilesPermanent.Add(MainGame.ListTilesTemporary[counter]);
            }
        }
        private static List<ButtonTile> AddSelectionToDrawLines()
        {
            switch (bearingChar)
            {
                case '-':
                    return MainGame.ListLineTilesHorizontal;
                case '|':
                    return MainGame.ListLineTilesVertical;
                case '\\':
                    return MainGame.ListLineTilesDownRight;
                case '/':
                    return MainGame.ListLineTilesUpRight;
            }
            return null;
        }
        private static void RemoveWordFromListWordsToFind()
        {
            if (MainGame.ListWordsToFind.Contains(MainGame.WordTilesTemporary))
            {
                MainGame.ListWordsToFind.Remove(MainGame.WordTilesTemporary);
            }
        }
        private static void ClearTemporary()
        {
            ResetValues();
            MainGame.ListTilesTemporary.Clear();
            MainGame.WordTilesTemporary = "";
        }


        // Check selection is adjacenet to prior
        private static bool TileAdjacentToAdded()
        {
            ButtonTile tileCompare = MainGame.ListTilesTemporary[ MainGame.ListTilesTemporary.Count - 1];

            float sizeTile = (MainGame.SpriteLetters.WidthSprite + MainGame.SpriteLetters.HeightSprite) * 0.5f;


            tilePosMidSelected.X = MainGame.ClickedTile.Pos.X + (float)(MainGame.SpriteLetters.WidthSprite * 0.5);
            tilePosMidSelected.Y = MainGame.ClickedTile.Pos.Y + (float)(MainGame.SpriteLetters.HeightSprite * 0.5);

            // get center pos of clicked tile
            tilePosMidCompare.X = tileCompare.Pos.X + (float)(MainGame.SpriteLetters.WidthSprite * 0.5);
            tilePosMidCompare.Y = tileCompare.Pos.Y + (float)(MainGame.SpriteLetters.HeightSprite * 0.5);

            // get distance between tiles
            Vector2 distanceVector;
            distanceVector = tilePosMidSelected - tilePosMidCompare;
            distance = Vector2.Distance(tilePosMidSelected, tilePosMidCompare);
            // if tiles close enough return
            if (distance < sizeTile * 2)
            {
                return true;
            }
            return false;
        }
        // Check if next tile chosen is same bearing (horizontal, diagonal direction) as prior
        private static void SetBearing()
        {
            // get bearing 
            if (!bearingSet && tilePosMidCompare != new Vector2(0f, 0f))
            {
                bearing = tilePosMidSelected - tilePosMidCompare;
                bearingSet = true;
                bearingChar = BearingLine();
            }
        }
        private static bool SameBearing()
        {
            Console.WriteLine($"bearingNew: {tilePosMidSelected - tilePosMidCompare}, bearingSet: {bearing}");

            if (bearing == tilePosMidSelected - tilePosMidCompare)
            //if (distance == distanceOld)
            {
                return true;
            }

            return false;
        }
        private static char BearingLine()
        {
            char bearingChar = ' ';
            if (bearing.X < 0 && bearing.Y < 0 ||
                bearing.X > 0 && bearing.Y > 0)
            {
                bearingChar = '\\';
            }
            if (bearing.X > 0 && bearing.Y < 0 ||
                bearing.X < 0 && bearing.Y > 0)
            {
                bearingChar = '/';
            }
            if (bearing.X == 0 && bearing.Y < 0 ||
                bearing.X == 0 && bearing.Y > 0)
            {
                bearingChar = '|';
            }
            if (bearing.X < 0 && bearing.Y == 0 ||
                bearing.X > 0 && bearing.Y == 0)
            {
                bearingChar = '-';
            }
            return bearingChar;
        }
        // Check if current selected work matches word to find
        private static bool SelectionIsWord()
        {
            foreach (string word in MainGame.ListWordsToFind)
            {
                if (word == MainGame.WordTilesTemporary)
                {                    
                    return true;
                }
            }


            return false;
        }
    }
}