using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    private GameObject soldOut;
    private Outline outLine;

    private float lastClickTime = 0;
    private int currentNum = 0;
    private int totalNum = 0;

    private bool isSoldOut = false;
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

        soldOut = transform.GetChild(3).gameObject;
        outLine = GetComponent<Outline>();
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
    public void SetMenuSlot(int _currentNum, int _totalNum)
    {
        currentNum += _currentNum;
        totalNum += _totalNum;
        menuCount.text = string.Format("{0} / {1}", currentNum, totalNum);

        if(currentNum > 0)
        {
            isSoldOut = false;
            soldOut.SetActive(false);
        }
        else if(currentNum <= 0 && recipe != null)
        {
            // 판매 완료
            isSoldOut = true;
            soldOut.SetActive(true);
        }
    }

    // 더블 클릭 시 메뉴 리스트에서 삭제
    public void OnPointerClick(PointerEventData eventData)
    {
        if((Time.time - lastClickTime) < 0.35f)
        {
            if(recipe != null && !RestaurantManager.Instance.isCooking)
            {
                UseIngredient(currentNum);

                recipe = null;
                nullBaseText.SetActive(true);
                menuPhanel.SetActive(false);
                menuPhanelImage.color = Color.black;
                lastClickTime = -1;

                SetMenuSlot(-currentNum, 0);
            }
            else if(recipe != null && RestaurantManager.Instance.isCooking && !isSoldOut)
            {
                RaycastHit2D _hit = GameObject.Find("Player").GetComponent<PlayerBehaviour>().hit;

                if(_hit.collider != null)
                {
                    MachineController _machine = _hit.collider?.GetComponent<MachineController>();

                    if( _machine.machineType == recipe.item.RecipeType)
                    {
                        Debug.Log("해당 메뉴 조리 시작");
                        SetMenuSlot(-1, 0);
                        GameManager.Instance.isUIOpen = false;
                        RecipeManager.Instance.menuBoard.SetActive(false);
                        GameObject.Find("Player").GetComponent<PlayerCooking>().CookMenuSelect(recipe);
                    }
                }
            }
        }
        else
        {
            lastClickTime = Time.time;
        }
    }
    
    // 재료 사용
    public void UseIngredient(int _num)
    {
        for (int i = 0; i < recipe.item.ingredients.Length; i++)
        {
            inventory.AcquireItem(recipe.item.ingredients[i], recipe.item.ingredientsCount[i] * _num);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(recipe != null && !isSoldOut)
        {
            outLine.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outLine.enabled = false;
    }
}
