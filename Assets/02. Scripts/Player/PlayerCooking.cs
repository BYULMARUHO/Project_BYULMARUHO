using UnityEngine;
using UnityEngine.UI;

public class PlayerCooking : MonoBehaviour
{
    private PlayerController playerController;
    private Item cookFood;
    private Slider cookingSlider;

    public GameObject[] foods;

    private float cookingTime = 0.0f;
    private float currentTime = 0.0f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cookingSlider = transform.GetChild(3).GetChild(1).GetComponent<Slider>();
    }

    private void Update()
    {
        if (cookFood == null)
            return;

        if(currentTime < cookingTime)
        {
            currentTime += Time.deltaTime;
            cookingSlider.gameObject.SetActive(true);
            CookingSliderHandler();
        }
        else
        {
            CompleteCook();
            playerController.ChangeSkin(cookFood.item.ItemName);
            RestaurantManager.Instance.isCooking = false;
            cookingSlider.gameObject.SetActive(false);
            playerController.isHolding = true;
            currentTime = 0.0f;
            cookFood = null;
        }
    }

    // 요리 슬라이더 조절
    public void CookingSliderHandler()
    {
        Color _color;

        if((cookingTime / 3) * 2 <  currentTime)
            _color = Color.green;
        else if((cookingTime / 3) * 1 < currentTime)
            _color = Color.white;
        else
            _color = Color.red;

        cookingSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = _color;
        cookingSlider.value = (float)(currentTime / cookingTime);
    }

    // 요리 메뉴 선택
    public void CookMenuSelect(Item _cookItem)
    {
        cookFood = _cookItem;
        currentTime = 0.0f;
        cookingTime = cookFood.item.CookTime * 2.0f;
    }

    // 요리 완성
    public void CompleteCook()
    {
        for(int i = 0; i < foods.Length; i++)
        {
            if(foods[i].GetComponent<Item>().item.ItemName == cookFood.item.ItemName)
            {
                playerController.servingFood = foods[i];
                return;
            }
        }
    }
}