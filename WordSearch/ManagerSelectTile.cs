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
        private static float distanceOld;
        private static bool distanceOldSet;
        private static bool bearingOldSet;
        private static Vector2 tilePosMidSelected;
        private static Vector2 tilePosMidCompare;
        private static Vector2 bearingOld;

        public static void SelectTile(MainGame game)
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;

            // If > 0 tiles in listTilesTemporary
            if (!MainGame.listTilesTemporary.Contains(MainGame.ClickedTile) &&
                MainGame.listTilesTemporary.Count > 0 && TileAdjacentToAdded())
            {
                // Third+ tiles
                if (MainGame.listTilesTemporary.Count > 1)
                {
                    if (SameBearing())
                    {
                        HandleNewSelectedTile();
                    }
                }
                // Second tile
                else if (MainGame.listTilesTemporary.Count == 1)
                {
                    HandleNewSelectedTile();
                }
            }
            // First tile
            else if (MainGame.listTilesTemporary.Count == 0)
            {
                HandleNewSelectedTile();
            }
        }
        public static void UnselectTile()
        {
            MainGame.listTileHighlight.Clear();
            MainGame.ClickedTile = MainGame.MousedOverTile;
            RemoveTileListTilesTemporary();
        }

        public static void ResetValues()
        {
            distance = 0;
            distanceOld = 0;
            distanceOldSet = false;
            bearingOldSet = false;
            tilePosMidSelected = new Vector2(0f, 0f);
        }

        private static void HandleNewSelectedTile()
        {
            MainGame.listTileHighlight.Clear();
            MainGame.listTileHighlight.Add(MainGame.ClickedTile);
            
            if (!MainGame.listTilesTemporary.Contains(MainGame.ClickedTile))
            {
                MainGame.listTilesTemporary.Add(MainGame.ClickedTile);
                MainGame.wordTilesTemporary += MainGame.ClickedTile.NameDraw;
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
            if (MainGame.listTilesTemporary.Contains((MainGame.ClickedTile)))
            {
                MainGame.listTilesTemporary.Remove(MainGame.ClickedTile);
                MainGame.wordTilesTemporary = MainGame.wordTilesTemporary.Substring(0, MainGame.wordTilesTemporary.Length-1);

                if (MainGame.listTilesTemporary.Count == 0)
                {
                    ResetValues();
                }
            }
        }
        private static void AddSelectionToPermanent()
        {
            for (int counter = 0; counter < MainGame.listTilesTemporary.Count; counter++)
            {
                MainGame.listTileHighlight.Add(MainGame.listTilesTemporary[counter]);
                MainGame.listTilesPermanent.Add(MainGame.listTilesTemporary[counter]);
            }
        }
        private static void RemoveWordFromListWordsToFind()
        {
            if (MainGame.listWordsToFind.Contains(MainGame.wordTilesTemporary))
            {
                MainGame.listWordsToFind.Remove(MainGame.wordTilesTemporary);
            }
        }
        private static void ClearTemporary()
        {
            ResetValues();
            MainGame.listTilesTemporary.Clear();
            MainGame.wordTilesTemporary = "";
        }


        // Check selection is adjacenet to prior
        private static bool TileAdjacentToAdded()
        {
            ButtonTile tileCompare = MainGame.listTilesTemporary[ MainGame.listTilesTemporary.Count - 1];

            float sizeTile = (MainGame.spriteLetters.WidthSprite + MainGame.spriteLetters.HeightSprite) * 0.5f;

            // get bearingOld 
            if (!bearingOldSet && tilePosMidSelected != new Vector2(0f, 0f))
            {
                bearingOld = tilePosMidSelected - tilePosMidCompare;
                bearingOldSet = true;
            }

            tilePosMidSelected.X = MainGame.ClickedTile.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidSelected.Y = MainGame.ClickedTile.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);

            // get center pos of clicked tile
            tilePosMidCompare.X = tileCompare.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidCompare.Y = tileCompare.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);

            // get distance between tiles
            Vector2 distanceVector;
            distanceVector = tilePosMidSelected - tilePosMidCompare;
            distance = Vector2.Distance(tilePosMidSelected, tilePosMidCompare);
            if (!distanceOldSet)
            {
                distanceOld = distance;
                distanceOldSet = true;
            }
            // if tiles close enough return
            if (distance < sizeTile * 2)
            {
                return true;
            }
            return false;
        }
        // Check if next tile chosen is same bearing (horizontal, diagonal direction) as prior
        private static bool SameBearing()
        {
            Console.WriteLine($"bearing: {tilePosMidSelected - tilePosMidCompare}, bearingOld: {bearingOld}");

            if (bearingOld == tilePosMidSelected - tilePosMidCompare)
            //if (distance == distanceOld)
            {
                return true;
            }

            return false;
        }
        // Check if current selected work matches word to find
        private static bool SelectionIsWord()
        {
            foreach (string word in MainGame.listWordsToFind)
            {
                if (word == MainGame.wordTilesTemporary)
                {                    
                    return true;
                }
            }


            return false;
        }
    }
}