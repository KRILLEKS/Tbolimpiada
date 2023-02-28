using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// we make separate class in case we'll want to develop app. Add regeneration or anything else 
public class PlayerEnergyController : MonoBehaviour
{
   public static float CurrentEnergyAmount
   {
      get
      {
         return _currentEnergyAmount;
      }
      private set
      {
         _currentEnergyAmount = value;
         PlayerEnergyUIController.SetFill(_currentEnergyAmount / Constants.PlayerMaxEnergy);
      }
   }

   private static float _currentEnergyAmount;

   private void Awake()
   {
      CurrentEnergyAmount = Constants.PlayerMaxEnergy;
   }

   public static void AddEnergy(float amount2Add)
   {
      CurrentEnergyAmount += amount2Add;
   }

   public static void SpendEnergy(float amount2Spend)
   {
      if (CurrentEnergyAmount - amount2Spend > 0)
         CurrentEnergyAmount -= amount2Spend;
      else if (_currentEnergyAmount != 0)
         CurrentEnergyAmount = 0;
   }
}