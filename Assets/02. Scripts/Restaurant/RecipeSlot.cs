using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Item recipe;
    private Inventory inventory;
    private RecipeSlotToolTip recipeSlotToolTip;

    private Image recipeImage;
    private TextMeshProUGUI countText;
    private GameObject makeLock;
    private GameObject lockImage;

    private GameObject menuSelectPhanel;
    private Image menuImage;
    private TextMeshProUGUI menuName;

    private int totalMakeNum = 0;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        recipeSlotToolTip = GameObject.Find("SlotToolTip").GetComponent<RecipeSlotToolTip>();

        recipeImage = transform.GetChild(0).GetComponent<Image>();
        countText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        makeLock = transform.GetChild(0).GetChild(1).gameObject;
        lockImage = transform.GetChild(1).gameObject;

        menuSelectPhanel = recipeSlotToolTip.transform.parent.GetChild(3).gameObject;
        menuImage = menuSelectPhanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        menuName = menuSelectPhanel.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        Init();
    }

    private void Update()
    {
        if (recipe == null)
            return;

        CheckQuantity();
    }

    private void Init()
    {
        if (recipe == null)
            return;

        recipe.isUnLock = true;
        recipeImage.sprite = recipe.item.ItemImage;
        countText.text = totalMakeNum.ToString();

        if (recipe.isUnLock)
            lockImage.SetActive(false);
    }

    // 제조에 필요한 수량 확인
    private void CheckQuantity()
    {
        int[] _nums = new int[recipe.item.ingredients.Length];
        List<int> _makeNum = new List<int>(recipe.item.ingredients.Length);

        for (int i = 0; i < recipe.item.ingredients.Length; i++)
        {
            _nums[i] = inventory.ChechQuantityItem(recipe.item.ingredients[i].ItemID);
            _makeNum.Add(_nums[i] / recipe.item.ingredientsCount[i]);
        }
        totalMakeNum = _makeNum.Min();

        if (totalMakeNum == 0)
            makeLock.SetActive(true);
        else
            makeLock.SetActive(false);

        countText.text = totalMakeNum.ToString();
        //for (int i = 0; i < recipe.item.ingredients.Length; i++)
        //{

        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (recipe != null && recipe.isUnLock)
        {
            recipeSlotToolTip.ShowToolTip(recipe, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (recipe != null && recipe.isUnLock)
        {
            recipeSlotToolTip.HideToolTip();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (recipe != null && recipe.isUnLock)
        {
            menuImage.sprite = recipe.item.ItemImage;
            menuName.text = recipe.item.ItemName;
            menuSelectPhanel.SetActive(true);
        }
    }
}