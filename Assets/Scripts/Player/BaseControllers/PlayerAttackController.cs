using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
   [SerializeField] private float attackRateSerializable; // attacks per second

   private static float _attackRate; // attacks per second
   private static float _lastAttackTime;
   
   private void Awake()
   {
      _attackRate = attackRateSerializable;
      
      Input.InvokeOnLeftClickRepeating.AddListener(PerformAttack);

      _lastAttackTime = float.MinValue;
   }

   public static void PerformAttack()
   {
      if (CursorHandler.SelectedObject == null ||
          Time.time < _lastAttackTime + 1f/(PlayerEnergyController.CurrentEnergyAmount > 0 ? _attackRate : (_attackRate / 2)))
         return;

      _lastAttackTime = Time.time;
      PlayerAnimatorController.SetAttackAnimation();
      // TODO: mb scale attack
      // if object was destroyed after attack then we turn off selection
      if (CursorHandler.SelectedObject.ReceiveDamage(10))
         CursorHandler.TurnOffSelection();

      PlayerEnergyController.SpendEnergy(1);
   }
}
