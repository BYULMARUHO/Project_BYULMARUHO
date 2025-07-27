using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSlot : MonoBehaviour, IPointerClickHandler
{
    public Item recipe;
    private Inventory inventory;

    private GameObject menuPhanel;
    private GameObject lockPhanel;
    private GameObject nullBaseText;

    private Image menuPhanelImage;
    private Image menuImage;
    private TextMeshProUGUI menuName;
    private TextMeshProUGUI menuCount;

    private float lastClickTime = 0;
    private int currentNum = 0;
    private int totalNum = 0;

    public bool isUnLook = false;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        menuPhanel = transform.GetChild(0).gameObject;
        lockPhanel = transform.GetChild(1).gameObject;
        nullBaseText = transform.GetChild(2).gameObject;

        menuPhanelImage = GetComponent<Image>();
        menuImage = menuPhanel.GetComponent<Image>();
        menuName = menuPhanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        menuCount = menuPhanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // 새로운 메뉴 슬롯 생성
    public void AddMenuSlot(Item _item, int _num)
    {
        nullBaseText.SetActive(false);
        menuPhanel.SetActive(true);

        Color color;
        ColorUtility.TryParseHtmlString("#989898", out color);
        menuPhanelImage.color = color;

        recipe = _item;
        menuImage.sprite = recipe.item.ItemImage;
        menuName.text = recipe.item.ItemName;
        currentNum = _num;
        totalNum = _num;
        menuCount.text = string.Format("{0} / {1}", currentNum, totalNum);
    }

    // 메뉴 추가 및 판매
    public void SetMenuSlot(int _num)
    {
        currentNum += _num;
        totalNum += _num;
        menuCount.text = string.Format("{0} / {1}", currentNum, totalNum);

        if(currentNum <= 0)
        {
            // 판매 완료

        }
    }

    // 더블 클릭 시 메뉴 리스트에서 삭제
    public void OnPointerClick(PointerEventData eventData)
    {
        if((Time.time - lastClickTime) < 0.35f)
        {
            if(recipe != null)
            {
                for(int i  = 0; i < recipe.item.ingredients.Length; i++)
                {
                    inventory.AcquireItem(recipe.item.ingredients[i], recipe.item.ingredientsCount[i] * currentNum);
                }

                recipe = null;
                nullBaseText.SetActive(true);
                menuPhanel.SetActive(false);
                menuPhanelImage.color = Color.black;
                lastClickTime = -1;
            }
        }
        else
        {
            lastClickTime = Time.time;
        }
    }
}
