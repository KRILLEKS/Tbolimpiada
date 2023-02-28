using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUIController : MonoBehaviour
{
   [SerializeField] private Transform overlayCanvas;
   
   private void Awake()
   {
      PlayerLevelUIHandler.Initialize(overlayCanvas.Find("Level"));

      var topLeftInfoFolder = overlayCanvas.Find("TopLeftInfo");
      PlayerHealthUIController.Initialize(topLeftInfoFolder.Find("Health").Find("Fill").GetComponent<Image>());
      PlayerEnergyUIController.Initialize(topLeftInfoFolder.Find("Energy").Find("Fill").GetComponent<Image>());
   }
}
