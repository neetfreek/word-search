/*==========================================================*
*  Handle actions for selecting tiles with mouse in-game    *
*===========================================================*/

namespace WordSearch
{
    public static class ManagerSelectTile
    {
        public static void SelectTile()
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;

            if (!MainGame.listTilesTemporary.Contains(MainGame.ClickedTile))
            {
                MainGame.listTilesTemporary.Add(MainGame.ClickedTile);
            }
        }

        public static void UnselectTile()
        {
            MainGame.ClickedTile = MainGame.MousedOverTile;

            if (MainGame.listTilesTemporary.Contains((MainGame.ClickedTile)))
            {
                MainGame.listTilesTemporary.Remove(MainGame.ClickedTile);
            }
        }
    }
}
