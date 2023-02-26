using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
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
      Log,
   }
}
