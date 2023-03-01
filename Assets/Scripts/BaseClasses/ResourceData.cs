using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData
{
   // initialize new data 
   public ResourceData(ResourceSO resourceSo)
   {
      ResourceType = resourceSo.resourceType;
      Object2Spawn = resourceSo.object2Instantiate;

      _resourceLimitIncrement = resourceSo.resourceLimitIncrement;
      _resource2XChanceIncrement = resourceSo.resource2XChanceIncrement;
      // -1 base value. Means that resource isn't unlocked
      ResourceLevel = -1;
      ResourceSpawnLimit = resourceSo.baseResourceLimit;
      Resource2XChance = 0;
      CurrentResourceAmountOnMap = 0;

      ResourceMaxHealth = resourceSo.resourceHealthAmount;

      ItemDrops = resourceSo.itemDrops;
   }

   public Constants.Resources ResourceType { private set; get; }
   public GameObject Object2Spawn { private set; get; }
   public int ResourceLevel { private set; get; }
   public int ResourceSpawnLimit { private set; get; }
   public int Resource2XChance { private set; get; }
   public int CurrentResourceAmountOnMap { private set; get; }
   public float ResourceMaxHealth { private set; get; }

   public ItemDrop[] ItemDrops { private set; get; }

   private int _resourceLimitIncrement;
   private int _resource2XChanceIncrement;

   public void IncreaseResourceLevel()
   {
      if (ResourceLevel == -1)
      {
         ResourceLevel = 1;
      }
      else
      {
         ResourceLevel++;
         ResourceSpawnLimit += _resourceLimitIncrement;
         Resource2XChance = _resource2XChanceIncrement;
      }
   }

   public void ResourceWasSpawned()
   {
      CurrentResourceAmountOnMap++;
   }
}