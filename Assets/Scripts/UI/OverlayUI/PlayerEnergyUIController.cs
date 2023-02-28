using UnityEngine.UI;

// we make separate class in case we'll want to develop app. Add regeneration or anything else 
public class PlayerEnergyUIController
{
    private static Image _fill;
   
    public static void Initialize(Image fill)
    {
        _fill = fill;
    }

    public static void SetFill(float fillAmount)
    {
        _fill.fillAmount = fillAmount;
    }
}
