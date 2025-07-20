using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    public Item recipe;
    private Inventory inventory;

    private Image recipeImage;
    private TextMeshProUGUI countText;
    private GameObject makeLock;
    private GameObject lockImage;

    private int totalMakeCount = 0;

    private void Start()
    {
        recipeImage = transform.GetChild(0).GetComponent<Image>();
        countText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        makeLock = transform.GetChild(0).GetChild(1).gameObject;
        lockImage = transform.GetChild(1).gameObject;

        Init();
    }

    private void Init()
    {
        if (recipe == null)
            return;

        recipe.isUnLock = true;
        recipeImage.sprite = recipe.item.ItemImage;
        countText.text = totalMakeCount.ToString();

        if (recipe.isUnLock)
            lockImage.SetActive(false);
    }

    private void TotalMakeCount()
    {

    }

    // 제조에 필요한 수량 확인
    private void CheckQuantity()
    {
        for(int i = 0; i < 5; i++)
        {

        }
    }
}