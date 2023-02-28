using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
public class ResourceSO : ScriptableObject
{
   public Constants.Resources resourceType;
   public GameObject object2Instantiate;
   
   public int baseResourceLimit;
   public int resourceLimitIncrement;
   public int resource2XChanceIncrement;

   public float resourceHealthAmount;

   public ItemDrop[] itemDrops;
}
