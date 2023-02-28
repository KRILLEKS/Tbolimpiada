using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLevelHandler : MonoBehaviour
{
   [FormerlySerializedAs("essence2FirstLevelUp")] [SerializeField]
   private int essence2FirstLevelUpSerializable;
   [FormerlySerializedAs("essenceIncrement2LevelUp")] [Range(0, 1)] [SerializeField]
   private float essenceIncrement2LevelUpSerializable; // percent

   private static int _essence2NextLevelUp;
   private static float _essenceIncrement2LevelUp;
   private static int _currentEssenceAmount;
   private static int _currentLevel = 0;

   private void Awake()
   {
      _essence2NextLevelUp = essence2FirstLevelUpSerializable;
      _essenceIncrement2LevelUp = essenceIncrement2LevelUpSerializable;

      _currentEssenceAmount = 0;

      PlayerLevelUIHandler.UpdateLevelUIInfo(0, essence2FirstLevelUpSerializable, 0);
   }

   [Title("Development"), Button]
   public static void AddEssence(int amount)
   {
      _currentEssenceAmount += amount;

      // level up
      if (_currentEssenceAmount >= _essence2NextLevelUp)
      {
         _currentEssenceAmount -= _essence2NextLevelUp;
         _essence2NextLevelUp = Mathf.FloorToInt(_essence2NextLevelUp * (1 + _essenceIncrement2LevelUp));

         _currentLevel++;
         PlayerSkillsHandler.AddSkillPoint();
      }

      PlayerLevelUIHandler.UpdateLevelUIInfo(_currentEssenceAmount, _essence2NextLevelUp, _currentLevel);
   }
}