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

    private int totalMakeCount = 0;

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