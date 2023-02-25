using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
   // we won't use camera.main in case we'll have multiple cameras
   [SerializeField] private Camera camera;
   [SerializeField] private GameObject objectSelectionCanvas;

   public static ObjectOnTile SelectedObject;
   
   private Vector2Int _previousTileUnderCursor;

   private void Update()
   {
      var cursorWorldPos = camera.ScreenToWorldPoint(Input.MousePosition);
      Vector2Int tileUnderCursor = new Vector2Int(Mathf.FloorToInt(cursorWorldPos.x), Mathf.FloorToInt(cursorWorldPos.y));

      if (tileUnderCursor != _previousTileUnderCursor)
      {
         if (TilemapHandler.IsTileEmpty(tileUnderCursor) == false)
         {
            objectSelectionCanvas.SetActive(true);
            objectSelectionCanvas.transform.position = tileUnderCursor + new Vector2(.5f, .5f);
         }
         else
            objectSelectionCanvas.SetActive(false);

         _previousTileUnderCursor = tileUnderCursor;
      }
   }
}