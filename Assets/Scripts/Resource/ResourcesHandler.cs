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
      IncreaseResourceLevel(Constants.Resources.Obelisk);
      IncreaseResourceLevel(Constants.Resources.BerryBush);
      IncreaseResourceLevel(Constants.Resources.Coal);
      IncreaseResourceLevel(Constants.Resources.IronOre);
      IncreaseResourceLevel(Constants.Resources.Pumpkin);
      IncreaseResourceLevel(Constants.Resources.Flowers);
      IncreaseResourceLevel(Constants.Resources.Hardwood);
   }

   public static void IncreaseResourceLevel(Constants.Resources resourceType)
   {
      if (PlayerResourcesList.Contains(resourceType) == false)
      {
         PlayerResourcesList.Add(resourceType);

         var instance = Instantiate(ResourceDatas[resourceType].Object2Spawn);
         ObjectPooler.InitializeNewPool("resource_"+resourceType, instance);

         foreach (var itemDrop in ResourceDatas[resourceType].ItemDrops)
            ObjectPooler.InitializeNewPool(itemDrop.Item.ToString(), ItemsHandler.GetItem(itemDrop.Item).ItemGO);
      }

      ResourceDatas[resourceType].IncreaseResourceLevel();
   }
}