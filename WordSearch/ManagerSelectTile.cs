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
                MainGame.listTilesTemporary.Count > 0)
            {
                // Check clicked tile adjacent to any in listTilesTemporary
                if (TileAdjacentToAdded())
                {
                    AddTileListTilesTemporary();
                }
                // Add clicked tile to listTilesTemporary
            }
            // Else add tile to listTilesTemporary
            else
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
            Vector2 tilePosMidSelected;
            Vector2 tilePosMidCompare;
            Vector2 distanceVector;
            float distance;
            float sizeTile = (MainGame.spriteLetters.WidthSprite + MainGame.spriteLetters.HeightSprite) * 0.5f;

            tilePosMidSelected.X = MainGame.ClickedTile.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
            tilePosMidSelected.Y = MainGame.ClickedTile.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);

            foreach (ButtonTile tile in MainGame.listTilesTemporary)
            {
                // if position within one tile of clicked tiles's position
                // get center pos of clicked tile
                tilePosMidCompare.X = tile.Pos.X + (float)(MainGame.spriteLetters.WidthSprite * 0.5);
                tilePosMidCompare.Y = tile.Pos.Y + (float)(MainGame.spriteLetters.HeightSprite * 0.5);
                // get distance between tiles
                distanceVector = tilePosMidSelected - tilePosMidCompare;
                distance = Vector2.Distance(tilePosMidSelected, tilePosMidCompare);
                // if tiles close enough return
                if (distance < sizeTile * 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
