using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
   public GameObject ItemGO { private set; get; }
   public string Description { private set; get; }
   public Constants.Items ItemType { private set; get; }
   public Sprite Sprite { private set; get; }
   
   public int EssenceGain { private set; get; }
   
   public bool IsConsumable { private set; get; }
   public int HealthGain { private set; get; }
   public int EnergyGain { private set; get; }

   public string PoolName { private set; get; }
   

   public Item(ItemSO itemSo)
   {
      ItemGO = itemSo.ItemGO;
      Description = itemSo.Description;
      ItemType = ItemGO.GetComponent<ItemOnTheGround>().ItemType;
      PoolName = ItemGO.GetComponent<ItemOnTheGround>().PoolName;
      Sprite = ItemGO.transform.Find("Texture").GetComponent<SpriteRenderer>().sprite;

      IsConsumable = itemSo.IsConsumable;
      
      EssenceGain = itemSo.EssenceGain;
      HealthGain = itemSo.HealthGain;
      EnergyGain = itemSo.EnergyGain;
   }
}
