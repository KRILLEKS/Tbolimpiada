using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUIHandler
{
    private static Image _fillerImage;
    private static TextMeshProUGUI _textMeshProUGUI;

    public static void Initialize(Transform levelUIFolder)
    {
        _fillerImage = levelUIFolder.transform.Find("Filler").GetComponent<Image>();
        _textMeshProUGUI = levelUIFolder.GetComponentInChildren<TextMeshProUGUI>();
    }

    public static void UpdateLevelUIInfo(int currentEssenceAmount, int levelUpEssenceAmount, int currentLevel)
    {
        _fillerImage.fillAmount = (float)currentEssenceAmount / levelUpEssenceAmount;
        _textMeshProUGUI.text = $"Level  {currentLevel}   {currentEssenceAmount}/{levelUpEssenceAmount}";
    }
}
