using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// attaches to each resource GO
public class Resource : ObjectOnTile
{
   [SerializeField] private Constants.Resources resourceType;
   
   internal override void Awake()
   {
      base.Awake();
      
      _healthBarController.InitializeHealthBar(this, ResourcesHandler.ResourceDatas[resourceType].ResourceMaxHealth);
   }
}
