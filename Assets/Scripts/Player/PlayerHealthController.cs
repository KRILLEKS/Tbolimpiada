using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we make separate class in case we'll want to develop app. Add regeneration or anything else 
public class PlayerHealthController : MonoBehaviour
{
   private static float CurrentHealthAmount
   {
      get
      {
         return _currentHealthAmount;
      }
      set
      {
         _currentHealthAmount = value;
         PlayerHealthUIController.SetFill(_currentHealthAmount / Constants.PlayerMaxHealth);
      }
   }
   private static float _currentHealthAmount;

   private void Awake()
   {
      CurrentHealthAmount = Constants.PlayerMaxHealth;
   }

   public static void AddHealth(float amount2Add)
   {
      CurrentHealthAmount += amount2Add;
   }

   public static void ReceiveDamage(float amount2Receive)
   {
      CurrentHealthAmount -= amount2Receive;
   }
}
