using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
      PlayerSphereController.Fly2Object(Constants.PlayerBaseDamage + GetExtraDamageVal(CursorHandler.SelectedObject));

      PlayerEnergyController.SpendEnergy(1);
   }

   private static int GetExtraDamageVal(ObjectOnTile selectedObject)
   {
      switch (selectedObject.tag)
      {
         case "MinedByPickaxe":
            return PlayerData.PickaxeDamageIncrement;
         case "MinedByAxe":
            return PlayerData.AxeDamageIncrement;
         case "MinedByHoe":
            return PlayerData.HoeDamageIncrement;
      }

      return 0;
   }
}
