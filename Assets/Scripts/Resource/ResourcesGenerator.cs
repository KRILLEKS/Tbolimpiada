using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
   [SerializeField] private Transform folderSerializable;
   [SerializeField] private float resourceSpawnRate; // in seconds

   private float _lastResourceGenerationTime;

   private static Transform _commonFolder;

   private void Awake()
   {
      _commonFolder = folderSerializable;
   }

   void Update()
   {
      if (Time.time - _lastResourceGenerationTime > resourceSpawnRate)
      {
         foreach (var resource in ResourcesHandler.PlayerResourcesList)
            SpawnResource(resource);

         _lastResourceGenerationTime = Time.time;
      }
   }

   // TODO: make folder for spawned resources by types
   private static void SpawnResource(Constants.Resources resource2Spawn)
   {
      if (ResourcesHandler.ResourceDatas[resource2Spawn].CurrentResourceAmountOnMap >= ResourcesHandler.ResourceDatas[resource2Spawn].ResourceSpawnLimit)
         return;

      // TODO: make object pooling
      var tileData = TilemapHandler.GetRandomEmptyTile();
      var instance = Instantiate(ResourcesHandler.ResourceDatas[resource2Spawn].Object2Spawn,
                                 new Vector3(tileData.X + .5f, tileData.Y + .5f),
                                 Quaternion.identity,
                                 _commonFolder);

      TilemapHandler.OccupyTile(tileData, instance.GetComponent<ObjectOnTile>());
   }
}