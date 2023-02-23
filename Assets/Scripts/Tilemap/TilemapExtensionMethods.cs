using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapExtensionMethods
{
   public static IEnumerable<TileData> GetAllTiles(Tilemap tilemap)
   // public static void GetAllTilesPosition(Tilemap tilemap)
   {
      tilemap.CompressBounds();

      var bounds = tilemap.cellBounds;
      for (int x = bounds.xMin; x < bounds.xMax; x++)
      {
         for (int y = bounds.yMin; y < bounds.yMax; y++)
         {
            var cellPosition = new Vector3Int(x, y, 0);
            var tile = tilemap.GetTile(cellPosition);
            
            if (tile == null)
               continue;

            var tileData = new TileData()
            {
               x = x,
               y = y,
               isEmpty = true,
            };
            yield return tileData;
         }
      }
   }
}
