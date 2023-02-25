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

      foreach (var tile in _emptyTiles)
         tile.IsEmpty = true;

      // fill _tiles array
      foreach (var tile in _emptyTiles)
      {
         var x = tile.X;
         var y = tile.Y;

         if (_tiles.ContainsKey(x) == false)
            _tiles.Add(x, new Dictionary<int, TileData>());

         _tiles[x].Add(y, tile);
      }
   }

   public static bool IsTileEmpty(Vector2Int tilePos)
   {
      if (_tiles.ContainsKey(tilePos.x) && _tiles[tilePos.x].ContainsKey(tilePos.y))
         return _tiles[tilePos.x][tilePos.y].IsEmpty;

      return false;
   }

   public static TileData GetRandomEmptyTile()
   {
      return _emptyTiles[Random.Range(0, _emptyTiles.Count)];
   }

   public static void OccupyTile(TileData tile2Occupy, ObjectOnTile ObjectOnTileClass)
   {
      // we'll spawn resource on this pos
      _emptyTiles.Remove(tile2Occupy);
      tile2Occupy.ObjectOnTile = ObjectOnTileClass;
      _tiles[tile2Occupy.X][tile2Occupy.Y].IsEmpty = false;
   }
}