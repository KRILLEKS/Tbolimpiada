using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
   [SerializeField] private float attackRateSerializable; // attacks per second

   private static float _attackRate;
   private static float _lastAttackTime;
   
   private void Awake()
   {
      _attackRate = attackRateSerializable;
      
      Input.InvokeOnLeftClickRepeating.AddListener(PerformAttack);
   }

   public static void PerformAttack()
   {
      if ((Time.time > _lastAttackTime + (1f/_attackRate)) == false || CursorHandler.SelectedObject == null)
         return;

      _lastAttackTime = Time.time;
      PlayerAnimatorController.SetAttackAnimation();
      // TODO: scale attack
      // if object was destroyed after attack then we turn off selection
      if (CursorHandler.SelectedObject.ReceiveDamage(10))
         CursorHandler.TurnOffSelection();
   }
}
