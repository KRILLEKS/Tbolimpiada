using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
   public const int PlayerBaseDamage = 20;
   public const int PlayerMaxHealth = 1000;
   public const int PlayerMaxEnergy = 100;

   public const int InventoryMaxItemsAmount = 12;
   
   public const float ItemDropXMin = -0.5f;
   public const float ItemDropXMax = 0.5f;
   public const float ItemDropYMin = 0.5f;
   public const float ItemDropYMax = 0.9f;
   public const float ItemDropFlyTime = 1f; // in seconds 

   public enum Resources
   {
      Wood,
      Stone,
      IronOre,
      Crystal,
      Coal,
      BerryBush,
      Flowers,
      Hardwood,
      Pumpkin,
      Obelisk,
      SoulShard,
   }
   
   public enum Items
   {
      Log, // from wood - toBuild
      Cone, // from wood - toEat
      StoneShard, // from stone - toBuild
      Diamond, // from stone - toBuild (very rare material)
      Ferrum, // from ironOre - toBuild TODO: make smelting if have enough time so Steel can be made from ferrum and coal
      Steel, // from ironOre - toBuild (rare material)
      RedCrystal, // from crystal - forEssence
      BlueCrystal, // from crystal - forEssence (more essence) (rare)
      PurpleCrystal, // from crystal - forEssence (even more essence) (super rare)
      Coal, // from coal - to build / cook TODO: make cooking if have enough time
      Berry, // from berryBush - toEat
      Flower, // from flowers - forEssence
      Hardwood, // from hardwood - toBuild
      Pumpkin, // from pumpkin - toEat
      ObeliskShard, // from obelisk - toBuild
      ObeliskCore, // from obelisk - forEssence (super rare)
      Soul, // from soulShard - forEssence
      PureSoul // from soulShard - toBuild / toEat (super rare)
   }
}
