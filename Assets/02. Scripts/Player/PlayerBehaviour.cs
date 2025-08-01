using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.EnumTypes;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject interactionObject;
    public TextMeshProUGUI interactionText;

    public RaycastHit2D hit;
    public Vector2 dir;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        interactionObject = transform.GetChild(3).GetChild(0).gameObject;
        interactionText = interactionObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (RestaurantManager.Instance.isCooking)
        {
            interactionObject.SetActive(false);
            return;
        }

        if (playerController.moveDir != Vector2.zero)
            dir = playerController.moveDir;

        OnDirection();
        OnMenuSetting();
        OnTakeOrder();
        OnCooking();
        OnFoodStand();
        OnServeing();
    }

    // 메뉴 결정
    public void OnMenuSetting()
    {
        if (playerController.isHolding)
            return;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("MenuBoard"))
            {
                InteractionHandler("메뉴설정");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.Instance.isUIOpen = true;
                    RecipeManager.Instance.backImage.enabled = true;
                    RecipeManager.Instance.recipeBoard.SetActive(true);
                    RecipeManager.Instance.menuBoard.SetActive(true);
                }
            }
        }
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
                InteractionHandler("주문받기");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _customer.Init();
                    _customer.state = CustomerState.Order;
                }
            }
        }
    }

    // 요리하기
    public void OnCooking()
    {
        if (playerController.isHolding || !RestaurantManager.Instance.isOpen)
            return;

        if (hit.collider != null)
        {
            MachineController _machine = hit.collider?.GetComponent<MachineController>();

            if (_machine != null)
            {
                if(_machine.machineType == MachineType.GasStove || _machine.machineType == MachineType.BeverageMachine)
                {
                    InteractionHandler("요리하기");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameManager.Instance.isUIOpen = true;
                        RestaurantManager.Instance.isCooking = true;
                        RecipeManager.Instance.MenuBoardHandler(_machine.machineType);
                    }
                }
            }
        }
    }

    // 음식 거치하기
    public void OnFoodStand()
    {
        if(hit.collider != null)
        {
            MachineController _machine = hit.collider?.GetComponent<MachineController>();

            if (_machine != null)
            {
                if(_machine.machineType == MachineType.FoodStand)
                {
                    if(_machine.food == null)
                    {
                        InteractionHandler("거치하기");

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            _machine.AddStorageFood(playerController.servingFood, hit.collider.transform);
                            playerController.ChangeSkin("default");
                            playerController.isHolding = false;
                        }
                    }
                    else
                    {
                        InteractionHandler("서빙하기");

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            playerController.ChangeSkin(_machine.food.GetComponent<Item>().item.ItemName);
                            playerController.isHolding = true;
                            _machine.DellStorageFood();
                        }
                    }
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
                if (_customer.orderMenu.item.ItemID == playerController.servingFood.GetComponent<Item>().item.ItemID)
                {
                    InteractionHandler("서빙하기");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        playerController.isHolding = false;
                        StartCoroutine(_customer.ReceiveFood());
                    }
                }
            }
        }
    }

    // 상호작용 텍스트 변경
    public void InteractionHandler(string _text)
    {
        interactionText.text = _text;
        interactionObject.SetActive(true);
    }

    // 방향 확인
    public void OnDirection()
    {
        hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, 2f, (1 << 7) + (1 << 9) + (1 << 12));
        Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), dir * 2.0f, Color.red);

        if(hit.collider == null)
            interactionObject.SetActive(false);
    }
}