using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/Tower")]
public class TowerSO : ScriptableObject
{
   public Constants.TowerTypes towerType;
   public float attackDamage;
   public float attackSpeed; // attacks per second
   public float healthAmount;
   public GameObject towerGO;
   public Vector2Int size;
   public ItemCompound[] price;
}
