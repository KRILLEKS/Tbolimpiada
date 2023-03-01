using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsHandler
{
   public static int AvailableSkillPoints { private set; get; } = 10;
   
   public enum SkillUpgrades
   {
      AttackIncrease,
      
   }
   public enum UpgradeValue
   {
      Small, // +1
      Medium, // +2
      High, // +3
      SuperHigh // +6
   }
   
   public static void AddSkillPoint()
   {
      AvailableSkillPoints++;
   }

   public static void Upgrade(SkillUpgrades upgradeType, UpgradeValue value)
   {
      Debug.Log($"Upgrade: {upgradeType} + {value}");
   }
}
