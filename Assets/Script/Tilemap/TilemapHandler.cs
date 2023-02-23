using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapHandler : MonoBehaviour
{
   [SerializeField] private Tilemap tilemap;

   private static Dictionary<int, Dictionary<int, TileData>> _tiles = new (); // x -> y -> tileData
   private static List<TileData> _emptyTiles = new (); // so resources may be generated on this tiles

   private void Awake()
   {
      _emptyTiles = TilemapExtensionMethods.GetAllTiles(tilemap).ToList();

      // fill _tiles array
      foreach (var tile in _emptyTiles)
      {
         var x = tile.x;
         var y = tile.y;

         if (_tiles.ContainsKey(x) == false)
            _tiles.Add(x, new Dictionary<int, TileData>());

         _tiles[x].Add(y, tile);
      }
   }
}