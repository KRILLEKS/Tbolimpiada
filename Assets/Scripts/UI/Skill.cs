using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
   [SerializeField] private Skill[] upgradesRequirement;
   [SerializeField] private PlayerSkillsHandler.SkillUpgrades upgradeType;
   [SerializeField] private PlayerSkillsHandler.UpgradeValue amount;
   
   public bool isUnlocked;
   
   private void Awake()
   {
      GetComponent<Button>().onClick.AddListener(UpgradeIfAble);
   }

   private void UpgradeIfAble()
   {
      // requirementsAreMet
      foreach (var upgrade in upgradesRequirement)
         if (upgrade.isUnlocked == false)
            return;
      
      if (PlayerSkillsHandler.AvailableSkillPoints == 0)
         return;

      GetComponent<Button>().interactable = false;
      transform.Find("SkillTreeBackground").GetComponent<RawImage>().texture = Resources.Load<Texture2D>("UI/SkillTree/UnlockedSkill");

      foreach (var childrenRawImage in transform.Find("Transitions").GetComponentsInChildren<RawImage>())
         childrenRawImage.color = SkillTreeController.UnlockedTransitionColor;

      isUnlocked = true;
      PlayerSkillsHandler.Upgrade(upgradeType, amount);
      SkillTreeController.UpdateUIInfo();
   }
}
