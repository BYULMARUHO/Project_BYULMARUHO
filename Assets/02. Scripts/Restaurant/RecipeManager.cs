using System.Collections.Generic;
using UnityEngine;
using Utils.ClassUtility;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager instance;
    public static RecipeManager Instance {  get { return instance; } }

    private JSONParser parser;
    public List<RecipeData> recipe;

    public GameObject menuBoard;
    private GameObject recipeParent;
    private RecipeSlot[] recipeSlots;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        Init();
    }

    private void Start()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        recipe = parser.LoadRecipeDataFromJSON();

        menuBoard = transform.GetChild(0).gameObject;
        recipeParent = menuBoard.transform.GetChild(0).gameObject;
        recipeSlots = recipeParent.GetComponentsInChildren<RecipeSlot>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuBoard.activeSelf)
                menuBoard.SetActive(false);
        }
    }

    public void Init()
    {

    }

    // 레시피 관리
    public void RecipeHandler()
    {

    }
}