using UnityEngine;
using Utils.ClassUtility;

public class JSONParser : MonoBehaviour
{
    private string playerDataFilePath = "JSON/PlayerData";

    public PlayerData LoadPlayerDataFromJSON(int index)
    {
        TextAsset loadJson = Resources.Load<TextAsset>(playerDataFilePath);
        PlayerDataList players = JsonUtility.FromJson<PlayerDataList>(loadJson.text);

        return players.Players[index];
    }
}