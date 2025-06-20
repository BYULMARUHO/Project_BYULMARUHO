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
        Debug.Log($"�ֹ� �丮 �Ϸ�: {cookedMenu}");
        OrderManager.Instance.ServeNextOrder(cookedMenu);
    }
}
