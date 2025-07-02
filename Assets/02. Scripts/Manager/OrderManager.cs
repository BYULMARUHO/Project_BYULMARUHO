using System.Collections.Generic;
using UnityEngine;
using Utils.ClassUtility;

public class OrderManager : MonoBehaviour
{
    private static OrderManager instance;
    public static OrderManager Instance {  get { return instance; } }

    private JSONParser parser;
    public List<RecipeData> recipe;
    public List<Item> items;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;

        Init();
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
}