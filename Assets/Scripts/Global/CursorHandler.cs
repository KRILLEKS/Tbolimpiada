using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CursorHandler : MonoBehaviour
{
   // we won't use camera.main in case we'll have multiple cameras
   [FormerlySerializedAs("camera"),SerializeField] private Camera mainCamera;
   [FormerlySerializedAs("objectSelectionCanvas"),SerializeField] private GameObject objectSelectionCanvasSerializable;
   
   public static ObjectOnTile SelectedObject;

   private static GameObject _objectSelectionCanvas;
   private static Vector2Int _previousTileUnderCursor;

   private void Awake()
   {
      _objectSelectionCanvas = objectSelectionCanvasSerializable;
   }

   private void Update()
   {
      var cursorWorldPos = mainCamera.ScreenToWorldPoint(Input.MousePosition);
      Vector2Int tileUnderCursor = new Vector2Int(Mathf.FloorToInt(cursorWorldPos.x), Mathf.FloorToInt(cursorWorldPos.y));

      if (tileUnderCursor != _previousTileUnderCursor)
      {
         if (TilemapHandler.IsTileEmpty(tileUnderCursor) == false)
         {
            SelectedObject = TilemapHandler.GetTileOnPosition(tileUnderCursor).ObjectOnTile;
            _objectSelectionCanvas.SetActive(true);
            _objectSelectionCanvas.transform.position = tileUnderCursor + new Vector2(.5f, .5f);
         }
         else
            TurnOffSelection();

         _previousTileUnderCursor = tileUnderCursor;
      }
   }

   public static void TurnOffSelection()
   {
      _objectSelectionCanvas.SetActive(false);
      SelectedObject = null;
   }
}