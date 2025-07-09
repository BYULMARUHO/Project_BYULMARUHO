using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.EnumTypes;

public class Slot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int itemCount;
    public Image itemImage;

    private GameObject countObject;
    private TextMeshProUGUI countText;

    public void Init()
    {
        itemImage = transform.GetChild(0).GetComponentInChildren<Image>(true);
        countObject = transform.GetChild(0).GetChild(0).gameObject;
        countText = countObject.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    // 아이템 이미지 투명도 조절
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(ItemScriptableObject _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.ItemImage;

        if (item.ItemType != ItemType.Equipment)
        {
            countObject.SetActive(true);
            countText.text = itemCount.ToString();
        }
        else
        {
            countText.text = "0";
            countObject.SetActive(false);
        }

        SetColor(1);
    }

    //  해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        countText.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        countText.text = "0";
        countObject.SetActive(false);
    }
}