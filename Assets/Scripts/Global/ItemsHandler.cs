using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsHandler : MonoBehaviour
{
   private static readonly Dictionary<Constants.Items, Item> _itemsDictionary = new ();
   
   private void Awake()
   {
      var itemSOArray = Resources.LoadAll<ItemSO>("Items/");

      foreach (var itemSo in itemSOArray)
      {
         var item = new Item(itemSo);
         _itemsDictionary.Add(item.ItemType, item);
      }
   }

   public static Item GetItem(Constants.Items itemType)
   {
      return _itemsDictionary[itemType];
   }
}
