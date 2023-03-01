using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeHandler : MonoBehaviour
{
   [SerializeField] private Color unlockedTransitionColorSerializable;

   public static Color UnlockedTransitionColor;

   private void Awake()
   {
      UnlockedTransitionColor = unlockedTransitionColorSerializable;
   }
}
