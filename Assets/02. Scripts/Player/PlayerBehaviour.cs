using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // 주문 받기
    public void OnTakeOrder()
    {

    }

    public void OnCookFinished(string cookedMenu)
    {
        Debug.Log($"주문 요리 완료: {cookedMenu}");
        OrderManager.Instance.ServeNextOrder(cookedMenu);
    }
}