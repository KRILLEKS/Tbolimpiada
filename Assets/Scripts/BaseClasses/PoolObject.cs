using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolObject : MonoBehaviour
{
   [HideInInspector] public string PoolName;
   [HideInInspector] public PoolObject ObjectClass;
   [HideInInspector] public SpriteRenderer SpriteRenderer; // needs to set order in layer

   public virtual void ResetObject()
      => throw new WarningException("ResetObject method doesn't have implementation");
}