using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
public class ResourceSO : ScriptableObject
{
   // TODO: resource level rules (limit, 2X)
   public Constants.Resources resourceType;
   public GameObject object2Instantiate;
}
