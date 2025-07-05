using TMPro;
using UnityEngine;
using Utils.EnumTypes;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject interactionObject;
    public TextMeshProUGUI interactionText;

    private GameObject servingFood;
    private Transform foodPos;

    private RaycastHit2D hit;
    public Vector2 dir;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactionObject = transform.GetChild(3).GetChild(0).gameObject;
        interactionText = interactionObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        foodPos = transform.GetChild(4).transform;
    }

    private void Update()
    {
        if (playerController.moveDir != Vector2.zero)
            dir = playerController.moveDir;

        OnDirection();
        OnTakeOrder();
        OnCooking();
        OnServeing();
    }

    // 주문받기
    public void OnTakeOrder()
    {
        if (playerController.isHolding)
            return;

        if (hit.collider != null)
        {
            CustomerController _customer = hit.collider?.GetComponent<CustomerController>();
            if(_customer != null && _customer.isWaitOrder)
            {
                interactionText.text = "주문받기";
                interactionObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _customer.Init();
                    _customer.state = CustomerState.Order;
                }
            }
            else
            {
                interactionObject.SetActive(false);
            }
        }
    }

    // 요리하기
    public void OnCooking()
    {
        if (playerController.isHolding)
            return;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("GasStove"))
            {
                interactionText.text = "요리하기";
                interactionObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerController.isHolding = true;
                    servingFood = Instantiate(hit.transform?.GetChild(0).gameObject, foodPos);
                }
            }
        }
    }

    // 서빙하기
    public void OnServeing()
    {
        if (hit.collider != null)
        {
            CustomerController _customer = hit.collider?.GetComponent<CustomerController>();
            if (_customer != null && _customer.isWaitFood && playerController.isHolding)
            {
                if (_customer.orderMenu.item.ItemID == servingFood.GetComponent<Item>().item.ItemID)
                {
                    interactionText.text = "서빙하기";
                    interactionObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(servingFood);
                        playerController.isHolding = false;
                        StartCoroutine(_customer.ReceiveFood());
                    }
                }
            }
        }
    }

    // 타격
    public void OnBlow()
    {

    }

    // 방향 확인
    public void OnDirection()
    {
        hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, 2f, (1 << 7) + (1 << 14));
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), dir * 2.0f, Color.red);

        if(hit.collider == null)
            interactionObject.SetActive(false);
    }
}