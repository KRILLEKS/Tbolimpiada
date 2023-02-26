using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnTile : MonoBehaviour
{
   [HideInInspector] public GameObject GO;
   [HideInInspector] public string PoolName;

   internal HealthBarController _healthBarController;

   internal virtual void Awake()
   {
      GO = gameObject;
      _healthBarController = GO.GetComponentInChildren<HealthBarController>();
   }

   // returns true if object was destroyed
   public bool ReceiveDamage(float damage)
   {
      return _healthBarController.ReceiveDamage(damage);
   }

   public void ResetObject()
   {
      _healthBarController.ResetHealthBar();
      _healthBarController.TurnOffHealthBar();
   }

   public void Return2Pool()
   {
      ObjectPooler.ReturnObject2Pool(PoolName, this);
      TilemapHandler.ReleaseTile(GO.transform.position);
   }
}
