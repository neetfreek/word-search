/*==========================================================*
*  Handle actions for selecting tiles with mouse in-game    *
*===========================================================*/
using System;
using Microsoft.Xna.Framework;

namespace WordSearch
{
    public static class ManagerSelectTile
    {
        public static void SelectTile()
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;

            // If > 0 tiles in listTilesTemporary
            if (!MainGame.listTilesTemporary.Contains(MainGame.ClickedTile) &&
                MainGame.listTilesTemporary.Count > 0 && TileAdjacentToAdded())
            {
                // Check clicked tile adjacent to any in listTilesTemporary
                if (MainGame.listTilesTemporary.Count > 1)
                {
                    // Check same directionm
                }
                AddTileListTilesTemporary();

                // Add clicked tile to listTilesTemporary
            }
            // Else add tile to listTilesTemporary
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
            }
        }

        private static bool TileAdjacentToAdded()
        {
            ButtonTile tileCompare = MainGame.listTilesTemporary[ MainGame.listTilesTemporary.Count - 1];
            Vector2 tilePosMidSelected;
            Vector2 tilePosMidCompare;
            Vector2 distanceVector;
            float distance;
            float sizeTile = (MainGame.spriteLetters.WidthSprite + MainGame.spriteLetters.HeightSprite) * 0.5f;

            tilePosMidSelected.X = MainGame.ClickedTile.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidSelected.Y = MainGame.ClickedTile.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);

            // if position within one tile of clicked tiles's position
            // get center pos of clicked tile
            tilePosMidCompare.X = tileCompare.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidCompare.Y = tileCompare.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);
            // get distance between tiles
            distanceVector = tilePosMidSelected - tilePosMidCompare;
            distance = Vector2.Distance(tilePosMidSelected, tilePosMidCompare);
            // if tiles close enough return
            System.Console.WriteLine(distance);
            if (distance < sizeTile * 2)
            {
                return true;
            }

            return false;
        }
    }
}
