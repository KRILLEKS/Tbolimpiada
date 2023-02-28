using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemOnTheGround : PoolObject
{
   public Constants.Items ItemType;

   public override void ResetObject()
   {
   }

   private void OnTriggerStay2D(Collider2D colliderInfo)
   {
      if (colliderInfo.CompareTag("Player") == false)
         return;

      if (InventoryHandler.AddItem2Dictionary(ItemType))
         ObjectPooler.ReturnObject2Pool(PoolName, this);
   }
}