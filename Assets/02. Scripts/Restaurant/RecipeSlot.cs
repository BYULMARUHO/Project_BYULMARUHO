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

    private Image menuImage;
    private TextMeshProUGUI menuName;
    private TextMeshProUGUI countNum;
    private TextMeshProUGUI maxNum;

    public int selectNum = 0;
    public int totalMakeNum = 0;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        recipeSlotToolTip = GameObject.Find("SlotToolTip").GetComponent<RecipeSlotToolTip>();

        recipeImage = transform.GetChild(0).GetComponent<Image>();
        makeLock = transform.GetChild(0).GetChild(0).gameObject;
        countText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        lockImage = transform.GetChild(1).gameObject;

        menuImage = RecipeManager.Instance.menuSelectBoard.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        menuName = RecipeManager.Instance.menuSelectBoard.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        countNum = RecipeManager.Instance.countSlider.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        maxNum = RecipeManager.Instance.countSlider.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

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
        {
            countText.color = Color.white;
            makeLock.SetActive(true);
        }
        else
        {
            countText.color = Color.black;
            makeLock.SetActive(false);
        }

        countText.text = totalMakeNum.ToString();
    }

    // 메뉴 수량 조절
    public void MenuSelectHandler()
    {
        selectNum = (int)RecipeManager.Instance.countSlider.value;
        countNum.text = string.Format("{0} / {1}", selectNum, totalMakeNum);
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
        if (recipe != null && totalMakeNum != 0 && recipe.isUnLock)
        {
            RecipeManager.Instance.countSlider.onValueChanged.AddListener(delegate { MenuSelectHandler(); });
            RecipeManager.Instance.checkButton.onClick.AddListener(delegate { RecipeManager.Instance.MenuRegistration(recipe, selectNum); });

            MenuSelectHandler();
            menuImage.sprite = recipe.item.ItemImage;
            menuName.text = recipe.item.ItemName;
            maxNum.text = totalMakeNum.ToString();
            RecipeManager.Instance.countSlider.maxValue = totalMakeNum;
            RecipeManager.Instance.countSlider.value = 0;
            RecipeManager.Instance.menuSelectBoard.SetActive(true);
        }
    }
}