using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
   public static void SwitchTimeState()
   {
      if (Time.timeScale == 0)
      {
         Time.timeScale = 1;
      }
      else
      {
         Time.timeScale = 0;
      }
   }
}
