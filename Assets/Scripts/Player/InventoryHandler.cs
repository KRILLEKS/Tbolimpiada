using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryHandler
{
   private static Dictionary<Constants.Items, int> _inventoryDictionary = new ();

   // returns isAble2AddItem
   public static bool AddItem2Dictionary(Constants.Items item2Add)
   {
      // inventory is full
      if (_inventoryDictionary.ContainsKey(item2Add) == false && _inventoryDictionary.Count >= Constants.InventoryMaxItemsAmount)
      {
         Debug.Log("Inventory is full");
         return false;
      }
      
      if (_inventoryDictionary.ContainsKey(item2Add))
         _inventoryDictionary[item2Add]++;
      else
         _inventoryDictionary.Add(item2Add, 1);

      return true;
   }

   // so we can't accidentally change elements in inventory
   public static Dictionary<Constants.Items, int> GetInventoryDictionary()
   {
      return new (_inventoryDictionary);
   }

   public static int GetItemAmount(Constants.Items itemType)
   {
      return _inventoryDictionary.ContainsKey(itemType) ? _inventoryDictionary[itemType] : -1;
   }

   public static void EatItem(Constants.Items itemType)
   {
      GetRidOfItem(itemType,1);

      var item = ItemsHandler.GetItem(itemType);
      PlayerEnergyController.AddEnergy(item.EnergyGain);
      PlayerHealthController.AddHealth(item.HealthGain);
   }
   
   public static void DestroyItemForEssence(Constants.Items itemType, int amount)
   {
      GetRidOfItem(itemType, amount);
      
      PlayerLevelHandler.AddEssence(ItemsHandler.GetItem(itemType).EssenceGain * amount);
   }

   public static void GetRidOfItem(Constants.Items itemType, int amount)
   {
      _inventoryDictionary[itemType] -= amount;

      if (_inventoryDictionary[itemType] == 0)
         _inventoryDictionary.Remove(itemType);
   }
}
