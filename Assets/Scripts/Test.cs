using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
   [HorizontalGroup("Test", Title = "Test methods")]
   [SerializeField] private bool isEnabled;
   [ShowIf("@isEnabled==true")]
   [HorizontalGroup("Test")]
   [Button]
   public void InvokeMethod()
   {
      if (isEnabled == false)
         return;

   }

   [Title("Add item 2 inventory")]
   [Button]
   public void AddItem2Inventory(Constants.Items itemType, int amount)
   {
      for (int i = 0; i < amount; i++)
         InventoryHandler.AddItem2Dictionary(itemType);
   }

   [Title("Set player energy")]
   [Button]
   public void SetEnergy(int amount)
   {
      var currentEnergyAmount = PlayerEnergyController.CurrentEnergyAmount;
      
      if (currentEnergyAmount < amount)
         PlayerEnergyController.AddEnergy(amount - currentEnergyAmount);
      else
         PlayerEnergyController.SpendEnergy(currentEnergyAmount - amount);
   }
}
