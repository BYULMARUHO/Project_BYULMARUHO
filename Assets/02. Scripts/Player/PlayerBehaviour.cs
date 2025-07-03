using UnityEngine;
using Utils.EnumTypes;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject takeOrderObject;

    public Vector2 dir;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        takeOrderObject = transform.GetChild(3).GetChild(0).gameObject;
    }

    private void Update()
    {
        if (playerController.moveDir != Vector2.zero)
            dir = playerController.moveDir;

        OnTakeOrder();
    }

    // 주문 받기
    public void OnTakeOrder()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, 2f, 1 << 7);
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), dir * 2.0f, Color.red);

        if (hit.collider != null)
        {
            CustomerController _customer = hit.collider?.GetComponent<CustomerController>();
            if(_customer != null && _customer.isWaitOrder)
            {
                takeOrderObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _customer.Init();
                    _customer.state = CustomerState.Order;
                }
            }
            else
            {
                takeOrderObject.SetActive(false);
            }
        }
        else
        {
            takeOrderObject.SetActive(false);
        }
    }

    public void OnCookFinished(string cookedMenu)
    {
        Debug.Log($"주문 요리 완료: {cookedMenu}");
    }
}