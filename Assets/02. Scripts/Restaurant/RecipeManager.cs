using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.ClassUtility;
using Utils.EnumTypes;
using System.Collections.Generic;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager instance;
    public static RecipeManager Instance {  get { return instance; } }

    private JSONParser parser;
    public List<RecipeData> recipe;
    public List<Item> items;
    private Inventory inventory;

    private List<MenuSlot> menuList;
    public Image backImage;

    public GameObject recipeBoard;
    private GameObject recipeParent;
    private RecipeSlot[] recipeSlots;

    public GameObject menuBoard;
    private GameObject menuParent;
    private MenuSlot[] menuSlots;
    private TextMeshProUGUI menuBoardTitle;

    public GameObject menuSelectBoard;
    public Slider countSlider;
    public Button checkButton;
    public Button cancleButton;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        recipe = parser.LoadRecipeDataFromJSON();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        backImage = GetComponent<Image>();
        menuBoard = transform.GetChild(0).gameObject;
        recipeBoard = transform.GetChild(1).gameObject;

        recipeParent = recipeBoard.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        recipeSlots = recipeParent.GetComponentsInChildren<RecipeSlot>();

        menuParent = menuBoard.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        menuSlots = menuParent.GetComponentsInChildren<MenuSlot>();
        menuBoardTitle = menuBoard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        menuSelectBoard = recipeBoard.transform.GetChild(2).gameObject;
        countSlider = menuSelectBoard.transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        checkButton = menuSelectBoard.transform.GetChild(0).GetChild(2).GetComponent<Button>();
        cancleButton = menuSelectBoard.transform.GetChild(0).GetChild(3).GetComponent<Button>();

        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuSelectBoard.activeSelf)
                menuSelectBoard.SetActive(false);
            else if (recipeBoard.activeSelf || menuBoard.activeSelf)
            {
                GameManager.Instance.isUIOpen = false;
                RestaurantManager.Instance.isCooking = false;
                backImage.enabled = false;
                recipeBoard.SetActive(false);
                menuBoard.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {

        }
    }

    public void Init()
    {
        for(int i = 0; i < menuSlots.Length; i++)
        {
            if (i < 3)
                menuSlots[i].isUnLook = true;
            else
                menuSlots[i].isUnLook = false;
        }

        cancleButton.onClick.AddListener(delegate { MenuBoardInit(); });
        cancleButton.onClick.AddListener(delegate { menuSelectBoard.SetActive(false); });
    }

    public void MenuBoardInit()
    {
        countSlider.onValueChanged.RemoveAllListeners();
        checkButton.onClick.RemoveAllListeners();
    }

    // 메뉴 등록
    public void MenuRegistration(Item _recipe, int _num)
    {
        // 해당 메뉴가 이미 등재되어 있을 경우
        for(int i = 0; i < menuSlots.Length; i++)
        {
            if (menuSlots[i].isUnLook && menuSlots[i].recipe != null)
            {
                if (menuSlots[i].recipe.item.ItemID == _recipe.item.ItemID)
                {
                    menuSlots[i].SetMenuSlot(_num, _num);
                    menuSelectBoard.SetActive(false);

                    for (int j = 0; j < _recipe.item.ingredients.Length; j++)
                    {
                        inventory.UseItem(_recipe.item.ingredients[j].ItemID, _recipe.item.ingredientsCount[j] * _num);
                    }

                    menuList[i] = menuSlots[i];
                    MenuBoardInit();
                    return;
                }
            }
        }

        // 새로운 판매 메뉴 추가
        for(int i = 0; i <= menuSlots.Length; i++)
        {
            if (menuSlots[i].isUnLook && menuSlots[i].recipe == null)
            {
                menuSlots[i].AddMenuSlot(_recipe, _num, _num);
                menuSelectBoard.SetActive(false);

                for (int j = 0; j < _recipe.item.ingredients.Length; j++)
                {
                    inventory.UseItem(_recipe.item.ingredients[j].ItemID, _recipe.item.ingredientsCount[j] * _num);
                }

                menuList.Add(menuSlots[i]);
                MenuBoardInit();
                return;
            }
        }
    }

    // 메뉴판 관리
    public void MenuBoardHandler(MachineType _machineType, float _pos)
    {
        menuBoardTitle.text = (_machineType == MachineType.None) ? "메뉴판" : ((_machineType == MachineType.GasStove) ? "가스 버너" : "음료 제작대");
        menuBoard.GetComponent<RectTransform>().localPosition = new Vector3(_pos, 0, 0);
        menuBoard.SetActive(true);

        if(_machineType == MachineType.None)
        {

        }
        else if(_machineType == MachineType.GasStove)
        {
            for (int i = 0; i < menuSlots.Length; i++)
                menuSlots[i].DisableInit();

            for(int i = 0; i <  menuSlots.Length; i++)
            {
                if (menuList[i] != null)
                {
                    if(menuList[i].recipe.item.RecipeType == MachineType.GasStove)
                    {

                    }
                }
            }

        }
        else if(_machineType == MachineType.BeverageMachine)
        {

        }
    }

    public Item AddOrder()
    {
        int index = Random.Range(0, recipe.Count);

        for (int i = 0; i < items.Count; i++)
        {
            if (recipe[index].RecipeID == items[i].item.ItemID)
            {
                return items[i];
            }
        }
        return null;
    }
}