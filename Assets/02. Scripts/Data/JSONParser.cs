using UnityEngine;
using System.Collections.Generic;
using Utils.ClassUtility;

public class JSONParser : MonoBehaviour
{
    private string playerDataFilePath = "JSON/PlayerData";
    private string recipeDataFilePath = "JSON/RecipeData";

    public PlayerData LoadPlayerDataFromJSON(int index)
    {
        TextAsset loadJson = Resources.Load<TextAsset>(playerDataFilePath);
        PlayerDataList players = JsonUtility.FromJson<PlayerDataList>(loadJson.text);

        return players.Players[index];
    }

    public List<RecipeData> LoadRecipeDataFromJSON()
    {
        TextAsset loadJson = Resources.Load<TextAsset>(recipeDataFilePath);
        RecipeDataList recipes = JsonUtility.FromJson<RecipeDataList>(loadJson.text);

        return recipes.Recipes;
    }
}