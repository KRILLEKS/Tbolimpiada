using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simplified version of object pooler
// it can be improved
public class ObjectPooler : MonoBehaviour
{
   private static readonly Dictionary<string, Queue<ObjectOnTile>> _poolDictionary = new (); // name of the pool, pool

   public static void InitializeNewPool(string poolName, GameObject poolGO)
   {
      _poolDictionary.Add(poolName, new Queue<ObjectOnTile>());

      _poolDictionary[poolName].Enqueue(poolGO.GetComponent<ObjectOnTile>());
      poolGO.SetActive(false);
   }

   public static ObjectOnTile GetObjectFromPool(string poolName)
   {
      ObjectOnTile object2Pool;

      if (_poolDictionary[poolName].Count < 2)
      {
         object2Pool = Instantiate(_poolDictionary[poolName].Peek().GO).GetComponent<ObjectOnTile>();
         object2Pool.PoolName = poolName;
      }
      else
         object2Pool = _poolDictionary[poolName].Dequeue();

      object2Pool.GO.SetActive(true);
      object2Pool.ResetObject();
      return object2Pool;
   }

   public static void ReturnObject2Pool(string poolName, ObjectOnTile poolObject)
   {
      _poolDictionary[poolName].Enqueue(poolObject);
      poolObject.GO.SetActive(false);
   }
}