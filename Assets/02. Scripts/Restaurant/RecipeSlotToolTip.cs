using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlotToolTip : MonoBehaviour
{
    private GameObject toolTipBase;
    private GameObject recipePhanel;
    private GameObject ingredientPhanel;
    private GameObject[] ingredients = new GameObject[5];

    private Image recipeImage;
    private TextMeshProUGUI recipeNameText;
    private TextMeshProUGUI decriptionText;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI delightText;

    private void Awake()
    {
        toolTipBase = transform.GetChild(0).gameObject;
        recipePhanel = toolTipBase.transform.GetChild(0).gameObject;
        ingredientPhanel = toolTipBase.transform.GetChild(1).gameObject;

        recipeImage = recipePhanel.transform.GetChild(0).GetComponent<Image>();
        recipeNameText = recipePhanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        decriptionText = recipePhanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        costText = ingredientPhanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>(true);
        delightText = ingredientPhanel.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>(true);

        for (int i = 0; i < ingredients.Length; i++)
            ingredients[i] = ingredientPhanel.transform.GetChild(i + 3).gameObject;
    }

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        toolTipBase.SetActive(true);

        _pos += new Vector3(toolTipBase.GetComponent<RectTransform>().rect.width * 0.5f,
            -toolTipBase.GetComponent<RectTransform>().rect.height * 0.5f, 0);
        toolTipBase.transform.position = _pos;

        recipeImage.sprite = _item.item.ItemImage;
        recipeNameText.text = _item.item.ItemName;
        decriptionText.text = _item.item.ItemDescription;
        costText.text = _item.item.ItemCost.ToString();
        delightText.text = _item.item.ItemDelight.ToString();

        for(int i = 0; i < ingredients.Length; i++)
        {
            int _inedx = _item.item.ingredients.Length;

            if(i < _inedx)
            {
                ingredients[i].transform.GetChild(0).gameObject.SetActive(true);
                ingredients[i].transform.GetChild(0).GetComponent<Image>().sprite = _item.item.ingredients[i].ItemImage;
                ingredients[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _item.item.ingredientsCount[i].ToString();
            }
            else
            {
                ingredients[i].transform.GetChild(0).gameObject.SetActive(false);
                ingredients[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public void HideToolTip()
    {
        toolTipBase.SetActive(false);
    }
}