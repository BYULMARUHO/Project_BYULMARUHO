using UnityEngine;

public class PlayerCooking : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void OnCookFinished(string cookedMenu)
    {
        Debug.Log($"주문 요리 완료: {cookedMenu}");
        OrderManager.Instance.ServeNextOrder(cookedMenu);
    }
}
