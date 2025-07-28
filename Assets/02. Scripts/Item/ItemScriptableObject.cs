using UnityEngine;
using UnityEngine.UI;
using Utils.EnumTypes;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public int ItemID;             // 아이템 번호
    public Sprite ItemImage;       // 아이템 이미지
    public string ItemName;        // 아이템 이름
    public string ItemDescription; // 아이템 설명
    public ItemType ItemType;      // 아이템 유형
    public MachineType RecipeType; // 레시피 유형
    public int ItemCost;           // 구매, 판매 가격
    public int ItemDelight;        // 만족도
    public float CookTime;         // 조리 시간

    public ItemScriptableObject[] ingredients;   // 필요한 재료 종류
    public int[] ingredientsCount;              // 필요한 재료 수량
}