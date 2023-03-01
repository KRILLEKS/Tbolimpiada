using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
   [SerializeField] private Transform folderSerializable;
   [SerializeField] private float resourceSpawnRate; // in seconds

   private static bool _toGenerateResources = true;
   private static float _lastResourceGenerationTime;

   private static Transform _commonFolder;

   private void Awake()
   {
      _commonFolder = folderSerializable;
   }

   // hard code
   // in future it will be replaced with resource upgrade system
   private void Start()
   {
      for (int i = 0; i < 600; i++)
      {
         SpawnResource(Constants.Resources.Wood);
         SpawnResource(Constants.Resources.Stone);
      }
      
      for (int i = 0; i < 10; i++)
      {
         SpawnResource(Constants.Resources.Coal);
         SpawnResource(Constants.Resources.Crystal);
         SpawnResource(Constants.Resources.Flowers);
         SpawnResource(Constants.Resources.Hardwood);
         SpawnResource(Constants.Resources.Obelisk);
         SpawnResource(Constants.Resources.Pumpkin);
         SpawnResource(Constants.Resources.BerryBush);
         SpawnResource(Constants.Resources.IronOre);
      }
   }

   void Update()
   {
      if (_toGenerateResources == false)
         return;
      
      if (Time.time - _lastResourceGenerationTime > resourceSpawnRate)
      {
         foreach (var resource in ResourcesHandler.PlayerResourcesList)
            SpawnResource(resource);

         _lastResourceGenerationTime = Time.time;
      }
   }

   // public static void SetResourceGenerationState(bool toGenerate)
   // {
   //    _toGenerateResources = toGenerate;
   // }

   // TODO: make folder for spawned resources by types
   private static void SpawnResource(Constants.Resources resource2Spawn)
   {
      if (ResourcesHandler.ResourceDatas[resource2Spawn].CurrentResourceAmountOnMap >= ResourcesHandler.ResourceDatas[resource2Spawn].ResourceSpawnLimit)
         return;

      var tileData = TilemapHandler.GetRandomEmptyTile();
      var poolObject = ObjectPooler.GetObjectFromPool("resource_"+resource2Spawn);
      
      poolObject.transform.parent = _commonFolder;
      poolObject.SpriteRenderer.sortingOrder = -tileData.Y * 3 + 3;
      poolObject.transform.position = new Vector3(tileData.X + .5f, tileData.Y + .5f, -1);
      TilemapHandler.OccupyTile(tileData, poolObject as ObjectOnTile);
   }
}