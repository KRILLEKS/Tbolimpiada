using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillsHandler
{
   public static int AvailableSkillPoints { private set; get; } = 10;
   
   public enum SkillUpgrades
   {
      AttackDamageIncrease,
      PickaxeDamageIncrease,
      AxeDamageIncrease,
      HoeDamageIncrease,
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

   public static void Upgrade(SkillUpgrades upgradeType, UpgradeValue valueType)
   {
      int value = 0;
      switch (valueType)
      {
         case UpgradeValue.Small:
            value = 1;
            break;
         case UpgradeValue.Medium:
            value = 2;
            break;
         case UpgradeValue.High:
            value = 3;
            break;
         case UpgradeValue.SuperHigh:
            value = 6;
            break;
      }

      switch (upgradeType)
      {
         case SkillUpgrades.AttackDamageIncrease:
            PlayerData.AttackDamageIncrement += value;
            break;
         case SkillUpgrades.PickaxeDamageIncrease:
            PlayerData.PickaxeDamageIncrement += value;
            break;
         case SkillUpgrades.AxeDamageIncrease:
            PlayerData.AxeDamageIncrement += value;
            break;
         case SkillUpgrades.HoeDamageIncrease:
            PlayerData.HoeDamageIncrement += value;
            break;
      }

      AvailableSkillPoints--;
   }
}
