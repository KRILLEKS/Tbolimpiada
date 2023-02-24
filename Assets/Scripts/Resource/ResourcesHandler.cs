using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{
   public static List<Constants.Resources> PlayerResourcesList = new ();
   public static Dictionary<Constants.Resources, ResourceData> ResourceDatas = new ();

   private void Awake()
   {
      var resourcesSOs = Resources.LoadAll<ResourceSO>("ResourcesGOs/");

      foreach (var resourcesSO in resourcesSOs)
         ResourceDatas.Add(resourcesSO.resourceType, new ResourceData(resourcesSO));

      IncreaseResourceLevel(Constants.Resources.Wood);
      IncreaseResourceLevel(Constants.Resources.Stone);
   }

   public static void IncreaseResourceLevel(Constants.Resources resourceType)
   {
      if (PlayerResourcesList.Contains(resourceType) == false)
         PlayerResourcesList.Add(resourceType);
      
      ResourceDatas[resourceType].IncreaseResourceLevel();
   }
}