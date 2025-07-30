using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    private static RestaurantManager instance;
    public static RestaurantManager Instance {  get { return instance; } }

    private float currentTime = 120.0f;
    private float openHours = 120.0f;

    public bool isCooking = false;
    public bool isOpen = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
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

    public void OpenRestaurant()
    {
        isOpen = true;
    }
}