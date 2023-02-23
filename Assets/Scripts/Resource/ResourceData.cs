using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceData
{
   // initialize new data 
   public ResourceData(Constants.Resources resourceType, GameObject object2Spawn)
   {
      ResourceType = resourceType;
      Object2Spawn = object2Spawn;
      // -1 base value. Means that resource isn't unlocked
      ResourceLevel = -1;
      ResourceSpawnLimit = -1;
      Resource2XChance = -1;
   }

   public Constants.Resources ResourceType { private set; get; }
   public GameObject Object2Spawn { private set; get; }

   public int ResourceLevel { private set; get; }
   public int ResourceSpawnLimit { private set; get; }
   public int Resource2XChance { private set; get; }

   public void IncreaseResourceLevel()
   {
      
   }
}