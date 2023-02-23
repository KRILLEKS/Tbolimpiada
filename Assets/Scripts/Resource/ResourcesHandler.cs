using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{
   public static List<Constants.Resources> PlayerResourcesList = new ();
   private Dictionary<Constants.Resources, ResourceData> _resources = new ();

   private void Awake()
   {
      var resourcesSOs = Resources.LoadAll<ResourceSO>("ResourcesGOs/");

      foreach (var resourcesSO in resourcesSOs)
         _resources.Add(resourcesSO.resourceType, new ResourceData(resourcesSO.resourceType, resourcesSO.object2Instantiate));

      AddResource(Constants.Resources.Wood);
      AddResource(Constants.Resources.Stone);
   }

   public static void AddResource(Constants.Resources resourceType)
   {
      PlayerResourcesList.Add(resourceType);
      PlayerResourcesLevels.Add(resourceType, 1);
   }
}