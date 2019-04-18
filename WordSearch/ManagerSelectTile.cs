/*==========================================================*
*  Handle actions for selecting tiles with mouse in-game    *
*===========================================================*/
using System;
using Microsoft.Xna.Framework;

namespace WordSearch
{
    public static class ManagerSelectTile
    {
        private static float distance;
        private static float distanceOld;
        private static bool distanceOldSet;

        public static void SelectTile()
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;

            // If > 0 tiles in listTilesTemporary
            if (!MainGame.listTilesTemporary.Contains(MainGame.ClickedTile) &&
                MainGame.listTilesTemporary.Count > 0  && TileAdjacentToAdded())
            {
                // Third+ tiles
                if (MainGame.listTilesTemporary.Count > 1)
                {
                    if (SameBearing())
                    {
                        AddTileListTilesTemporary();
                    }
                }
                // Second tile
                else if (MainGame.listTilesTemporary.Count == 1)
                {
                    AddTileListTilesTemporary();
                }
            }
            // First tile
            else if (MainGame.listTilesTemporary.Count == 0)
            {
                AddTileListTilesTemporary();
            }
        }

        public static void UnselectTile()
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;
            RemoveTileListTilesTemporary();
        }

        public static void ResetValues()
        {
            distance = 0;
            distanceOld = 0;
            distanceOldSet = false;
        }

        private static void AddTileListTilesTemporary()
        {
            if (!MainGame.listTilesTemporary.Contains(MainGame.ClickedTile))
            {
                MainGame.listTilesTemporary.Add(MainGame.ClickedTile);
            }
        }

        private static void RemoveTileListTilesTemporary()
        {
            if (MainGame.listTilesTemporary.Contains((MainGame.ClickedTile)))
            {
                MainGame.listTilesTemporary.Remove(MainGame.ClickedTile);

                if (MainGame.listTilesTemporary.Count == 0)
                {
                    ResetValues();
                }
            }
        }

        private static bool TileAdjacentToAdded()
        {
            ButtonTile tileCompare = MainGame.listTilesTemporary[ MainGame.listTilesTemporary.Count - 1];
            Vector2 tilePosMidSelected;
            Vector2 tilePosMidCompare;
            Vector2 distanceVector;
            //float distance;
            float sizeTile = (MainGame.spriteLetters.WidthSprite + MainGame.spriteLetters.HeightSprite) * 0.5f;

            tilePosMidSelected.X = MainGame.ClickedTile.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidSelected.Y = MainGame.ClickedTile.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);

            // get center pos of clicked tile
            tilePosMidCompare.X = tileCompare.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidCompare.Y = tileCompare.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);
            // get distance between tiles
            distanceVector = tilePosMidSelected - tilePosMidCompare;
            distance = Vector2.Distance(tilePosMidSelected, tilePosMidCompare);
            if (!distanceOldSet)
            {
                distanceOld = distance;
                Console.WriteLine("DISTANCE OLD SET");
                distanceOldSet = true;
            }
            // if tiles close enough return
            if (distance < sizeTile * 2)
            {
                return true;
            }
            return false;
        }

        // Check if next tile chosen is along the same bearing as previous selections
        private static bool SameBearing()
        {
            Console.WriteLine($"distance: {distance}, distanceOld: {distanceOld}");

            if (distance == distanceOld)
            {
                return true;
            }

            return false;
        }
    }
}
