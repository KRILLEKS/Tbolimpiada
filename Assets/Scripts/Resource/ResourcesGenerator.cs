using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
   [SerializeField] private bool toGenerateResources;
   [Space]
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
      if (toGenerateResources == false)
         return;
      
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

      var tileData = TilemapHandler.GetRandomEmptyTile();
      var poolObject = ObjectPooler.GetObjectFromPool(resource2Spawn.ToString());
      
      poolObject.transform.position = new Vector3(tileData.X + .5f, tileData.Y + .5f);
      poolObject.transform.parent = _commonFolder;
      TilemapHandler.OccupyTile(tileData, poolObject.GetComponent<Resource>());
   }
}