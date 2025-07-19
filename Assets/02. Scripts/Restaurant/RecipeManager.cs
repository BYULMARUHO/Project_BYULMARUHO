using System.Collections.Generic;
using UnityEngine;
using Utils.ClassUtility;

public class RecipeManager : MonoBehaviour
{
    private static RecipeManager instance;
    public static RecipeManager Instance {  get { return instance; } }

    private JSONParser parser;
    public List<RecipeData> recipe;
    public List<Item> items;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        Init();
    }

    public void Init()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        recipe = parser.LoadRecipeDataFromJSON();

        // 테스트를 위한 레시피 해금 (나중에 삭제 필요)
        for (int i = 0; i < items.Count; i++)
            items[i].isUnLock = true;
    }
}