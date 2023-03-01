using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
   private void Awake()
   {
      var CorePos = Vector2Int.FloorToInt(transform.position - new Vector3(1,1));
      for (int x = 0; x <= 1; x++)
         for (int y = 0; y <= 1; y++)
            TilemapHandler.OccupyTile(TilemapHandler.GetTileOnPosition(CorePos + new Vector2Int(x,y)));
   }
}
