using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Object = System.Object;

public class ObjectOnTile : PoolObject
{
   public string objectTag;
   internal ObjectHealthBarController ObjectHealthBarController;

   internal virtual void Awake()
   {
      ObjectClass = this;
      ObjectHealthBarController = gameObject.GetComponentInChildren<ObjectHealthBarController>();

      objectTag = gameObject.tag;

      InitializeHealthBar();
   }

   public virtual void InitializeHealthBar()
      => throw new WarningException("InitializeHealthBar method doesn't have implementation");

   public virtual void OnObjectDestroy()
      => throw new WarningException("OnObjectDestroy method doesn't have implementation");

   public override void ResetObject()
   {
      ObjectHealthBarController.ResetHealthBar();
      ObjectHealthBarController.TurnOffHealthBar();
   }

   // returns true if object was destroyed
   public bool ReceiveDamage(float damage)
   {
      bool objectWasDestroyed = ObjectHealthBarController.ReceiveDamage(damage);
      
      if (objectWasDestroyed)
         OnObjectDestroy();
      
      return objectWasDestroyed;
   }

   public void Return2Pool()
   {
      ObjectPooler.ReturnObject2Pool(PoolName, this);
      TilemapHandler.ReleaseTile(transform.position);
   }
}