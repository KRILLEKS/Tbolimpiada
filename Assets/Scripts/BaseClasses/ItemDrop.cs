using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDrop
{
   public Constants.Items Item;
   [Range(0,1)]public float DropChance;
   public int MinAmount;
   public int MaxAmount;
}
