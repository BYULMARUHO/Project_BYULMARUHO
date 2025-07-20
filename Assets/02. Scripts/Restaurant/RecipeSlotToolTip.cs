using TMPro;
using UnityEngine;

public class RecipeSlotToolTip : MonoBehaviour
{
    private GameObject toolTipBase;
    private GameObject recipePhanel;
    private GameObject ingredientPhanel;

    private TextMeshProUGUI recipeNameText;

    private void Awake()
    {
        toolTipBase = transform.GetChild(0).gameObject;
    }
}
