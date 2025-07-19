using System.Collections.Generic;
using UnityEngine;
using Utils.ClassUtility;

public class RestaurantManager : MonoBehaviour
{
    private static RestaurantManager instance;
    public static RestaurantManager Instance {  get { return instance; } }

    private JSONParser parser;
    public List<RecipeData> recipe;
    public List<Item> items;

    private float currentTime = 120.0f;
    private float openHours = 120.0f;

    public bool isOpen = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;

        Init();
    }

    private void Update()
    {
        if (!isOpen)
            return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UIManager.Instance.ChangeTimerText(currentTime);
        }
        else
        {
            currentTime = openHours;
            isOpen = false;
        }
    }

    public void Init()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        recipe = parser.LoadRecipeDataFromJSON();
    }

    public Item AddOrder()
    {
        // 재료 갯수 확인
        int index = Random.Range(0, recipe.Count);

        for(int i = 0; i < items.Count; i++)
        {
            if (recipe[index].RecipeID == items[i].item.ItemID)
            {
                return items[i];
            }
        }
        return null;
    }

    public void OpenRestaurant()
    {
        isOpen = true;
    }
}