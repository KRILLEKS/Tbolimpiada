using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

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

   public static Vector2 GetRandomEmptyTileForResource()
   {
      var emptyTile = _emptyTiles[Random.Range(0, _emptyTiles.Count)];

      _emptyTiles.Remove(emptyTile);

      return new Vector2(emptyTile.x, emptyTile.y);
   }
   
}