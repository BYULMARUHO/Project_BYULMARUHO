using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.ClassUtility;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager instance;
    public static RecipeManager Instance {  get { return instance; } }

    private JSONParser parser;
    public List<RecipeData> recipe;
    public List<Item> items;
    private Inventory inventory;

    public GameObject menuBoard;
    private GameObject recipeParent;
    private RecipeSlot[] recipeSlots;
    private GameObject menuParent;
    private MenuSlot[] menuSlots;

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
        menuBoard = transform.GetChild(0).gameObject;
        recipeParent = menuBoard.transform.GetChild(0).gameObject;
        recipeSlots = recipeParent.GetComponentsInChildren<RecipeSlot>();
        menuParent = menuBoard.transform.GetChild(1).gameObject;
        menuSlots = menuParent.GetComponentsInChildren<MenuSlot>();

        menuSelectBoard = menuBoard.transform.GetChild(3).gameObject;
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
            else if (menuBoard.activeSelf)
                menuBoard.SetActive(false);
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

        cancleButton.onClick.AddListener(delegate { MenuBoardHandler(); });
        cancleButton.onClick.AddListener(delegate { menuSelectBoard.SetActive(false); });
    }

    // 메뉴 보드 관리
    public void MenuBoardHandler()
    {
        countSlider.onValueChanged.RemoveAllListeners();
        checkButton.onClick.RemoveAllListeners();
    }

    // 메뉴 등록
    public void MenuRegistration(Item _recipe, int _num)
    {
        Debug.Log("::: 메뉴 등록 성공 :::");
        for(int i = 0; i < menuSlots.Length; i++)
        {
            if (menuSlots[i].isUnLook && menuSlots[i].recipe == null)
            {
                menuSlots[i].AddMenuSlot(_recipe, _num);
                menuSelectBoard.SetActive(false);

                for(int j = 0; j < _recipe.item.ingredients.Length; j++)
                {
                    inventory.UseItem(_recipe.item.ingredients[j].ItemID, _recipe.item.ingredientsCount[j] * _num);
                }

                MenuBoardHandler();
                return;
            }
            else if(menuSlots[i].isUnLook && menuSlots[i].recipe != null)
            {
                if(menuSlots[i].recipe.item.ItemID == _recipe.item.ItemID)
                {
                    menuSlots[i].SetMenuSlot(_num);
                    menuSelectBoard.SetActive(false);

                    for (int j = 0; j < _recipe.item.ingredients.Length; j++)
                    {
                        inventory.UseItem(_recipe.item.ingredients[j].ItemID, _recipe.item.ingredientsCount[j] * _num);
                    }

                    MenuBoardHandler();
                    return;
                }
            }
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