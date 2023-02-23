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
         foreach (var resource in PlayerResources.PlayerResourcesList)
            SpawnResource(resource);

         _lastResourceGenerationTime = Time.time;
      }
   }

   // TODO: make folder for spawned resources by types
   private static void SpawnResource(Constants.Resources resource2Spawn)
   {
      var resourcePos = TilemapHandler.GetRandomEmptyTileForResource();
   }
}