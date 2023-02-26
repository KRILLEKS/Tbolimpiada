using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ObjectOnTile : PoolObject
{
   internal HealthBarController HealthBarController;

   internal virtual void Awake()
   {
      ObjectClass = this;
      HealthBarController = gameObject.GetComponentInChildren<HealthBarController>();

      InitializeHealthBar();
   }

   public virtual void InitializeHealthBar()
      => throw new WarningException("InitializeHealthBar method doesn't have implementation");

   public virtual void OnObjectDestroy()
      => throw new WarningException("OnObjectDestroy method doesn't have implementation");

   public override void ResetObject()
   {
      HealthBarController.ResetHealthBar();
      HealthBarController.TurnOffHealthBar();
   }

   // returns true if object was destroyed
   public bool ReceiveDamage(float damage)
   {
      bool objectWasDestroyed = HealthBarController.ReceiveDamage(damage);
      
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