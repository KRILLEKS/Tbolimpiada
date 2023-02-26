using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory
{
   private static Dictionary<Constants.Items, int> _inventoryDictionary = new ();

   public static void AddItem2Dictionary(Constants.Items item2Add)
   {
      if (_inventoryDictionary.ContainsKey(item2Add))
         _inventoryDictionary[item2Add]++;
      else
         _inventoryDictionary.Add(item2Add, 1);
   }
}
