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
      if (_inventoryDictionary.Count > Constants.InventoryMaxItemsAmount)
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
}
