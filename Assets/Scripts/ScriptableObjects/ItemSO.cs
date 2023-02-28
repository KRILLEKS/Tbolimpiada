using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
   public GameObject ItemGO;
   [TextArea]
   public string Description;
   public int EssenceGain;

   public bool IsConsumable;
   [ShowIf("@IsConsumable==true")]
   public int HealthGain;
   [ShowIf("@IsConsumable==true")]
   public int EnergyGain;
}
