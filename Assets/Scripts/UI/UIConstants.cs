using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConstants
{
    public const float InventorySelectedItemMenuAppearTime = .42f;
    public const float InventorySelectedItemMenuItemChangeTime = .6f;

    public class ColorRGB
    {
        public ColorRGB(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public int R;
        public int G;
        public int B;
    }
}
