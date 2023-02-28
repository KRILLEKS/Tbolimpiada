using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPooler
{
   private static readonly Dictionary<string, Queue<PoolObject>> _poolDictionary = new (); // name of the pool, pool

   public static void InitializeNewPool(string poolName, GameObject poolGO)
   {
      if (_poolDictionary.ContainsKey(poolName))
         return;
      
      _poolDictionary.Add(poolName, new Queue<PoolObject>());

      var poolObj = poolGO.GetComponent<PoolObject>();
      poolObj.PoolName = poolName;
      poolObj.SpriteRenderer = poolObj.transform.Find("Texture")?.GetComponent<SpriteRenderer>();
      _poolDictionary[poolName].Enqueue(poolGO.GetComponent<PoolObject>());
      
      poolGO.SetActive(false);
   }

   public static PoolObject GetObjectFromPool(string poolName)
   {
      PoolObject object2Pool;
      
      if (_poolDictionary[poolName].Count < 2)
         object2Pool = Object.Instantiate(_poolDictionary[poolName].Peek());
      else
         object2Pool = _poolDictionary[poolName].Dequeue();

      object2Pool.gameObject.SetActive(true);
      object2Pool.ResetObject();
      return object2Pool;
   }

   public static void ReturnObject2Pool(string poolName, PoolObject poolObject)
   {
      _poolDictionary[poolName].Enqueue(poolObject);
      poolObject.gameObject.SetActive(false);
   }
}