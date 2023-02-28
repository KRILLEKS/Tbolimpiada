using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

// attaches to each resource GO
public class Resource : ObjectOnTile
{
   [SerializeField] private Constants.Resources resourceType;
   
   public override void InitializeHealthBar()
   {
      ObjectHealthBarController.InitializeHealthBar(this, ResourcesHandler.ResourceDatas[resourceType].ResourceMaxHealth);
   }

   public override void OnObjectDestroy()
   {
      foreach (var poolName in GetItemDropList())
      {
         var GO = ObjectPooler.GetObjectFromPool(poolName).gameObject;

         GO.transform.position = gameObject.transform.position;
         GO.transform.DOMoveX(transform.position.x + Random.Range(Constants.ItemDropXMin, Constants.ItemDropXMax), Constants.ItemDropFlyTime);
         GO.transform.DOMoveY(transform.position.y + Random.Range(Constants.ItemDropYMin, Constants.ItemDropYMax), Constants.ItemDropFlyTime).SetEase(Ease.OutQuart);
         // GO.transform.DOMove()
      }
   }

   private List<string> GetItemDropList()
   {
      List<string> dropList = new ();
      
      foreach (var ItemDropInfo in ResourcesHandler.ResourceDatas[resourceType].ItemDrops)
      {
         var poolName = ItemDropInfo.Item.ToString();
         for (int i = 0; i < ItemDropInfo.MinAmount; i++)
            dropList.Add(poolName);

         for (int i = ItemDropInfo.MinAmount; i < ItemDropInfo.MaxAmount; i++)
            if (Random.value < ItemDropInfo.DropChance)
               dropList.Add(poolName);
      }

      return dropList;
   }
}
