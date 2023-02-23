using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
   [SerializeField] private bool isEnabled;
   [SerializeField] private Tilemap tilemap;

   private void Awake()
   {
      if (isEnabled == false)
         return;
      
      TilemapExtensionMethods.GetAllTiles(tilemap);
   }
}
