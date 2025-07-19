using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Utils.EnumTypes;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject orderObject;
    private GameObject delightObject;
    private TextMeshProUGUI coinText;
    private TextMeshProUGUI delightText;
    private Slider bubbleSlider;
    private Image menuImage;
    public Sprite menuBoard;

    public CustomerState state;
    public Item orderMenu;
    private int chairIndex = -1;
    private int delight = 50;

    private float moveSpeed = 2.5f;
    private float currentTime = 0.0f;
    private float orderWaitTime = 15.0f;
    private float foodWaitTime = 30.0f;
    private float eatingTime = 15.0f;

    public bool isWaitOrder = false;
    public bool isWaitFood = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        orderObject = transform.GetChild(1).GetChild(0).gameObject;
        delightObject = transform.GetChild(1).GetChild(1).gameObject;
        coinText = delightObject.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        delightText = delightObject.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        bubbleSlider = orderObject.transform.GetChild(0).GetComponent<Slider>();
        menuImage = orderObject.transform.GetChild(1).GetComponent<Image>();

        agent.speed = moveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        Init();
        MoveToChair();
    }

    // 초기화
    public void Init()
    {
        isWaitOrder = false;
        isWaitFood = false;

        currentTime = 0;
        menuImage.sprite = null;
        orderObject.SetActive(false);
    }

    private void Update()
    {
        StateHandler();
    }

    public void StateHandler()
    {
        switch (state)
        {
            case CustomerState.Walk:
                break;
            case CustomerState.WaitOrder:
                OnWaitOrder();
                break;
            case CustomerState.Order:
                StartCoroutine(RequestOrder());
                break;
            case CustomerState.Wait:
                StartWaiting();
                break;
            case CustomerState.Eat:
                OnEating();
                break;
            case CustomerState.Drink:
                break;
            case CustomerState.ReJoice:
                break;
            case CustomerState.Angry:
                OnAngry();
                break;
            case CustomerState.Truth:
                break;
        }
    }

    // 비어있는 좌석으로 이동
    public void MoveToChair()
    {
        chairIndex = CustomerManager.Instance.GetChairIndex();
        agent.SetDestination(CustomerManager.Instance.chairs[chairIndex].transform.position);
    }

    // 주문서
    private void OnWaitOrder()
    {
        menuImage.sprite = menuBoard;
        orderObject.SetActive(true);
        state = CustomerState.Wait;
        isWaitOrder = true;
    }

    // 메뉴 주문
    private IEnumerator RequestOrder()
    {
        isWaitOrder = false;
        isWaitFood = true;

        orderObject.SetActive(false);
        orderMenu = RestaurantManager.Instance.AddOrder();

        yield return new WaitForSeconds(1.5f);

        menuImage.sprite = orderMenu.item.ItemImage;
        orderObject.SetActive(true);
        state = CustomerState.Wait;
    }

    // 기다리는 중
    public void StartWaiting()
    {
        float _waitTime = (isWaitOrder) ? orderWaitTime : foodWaitTime;

        if (currentTime < _waitTime)
        {
            currentTime += Time.deltaTime;
            bubbleSlider.value = currentTime / _waitTime;
        }
        else
        {
            Init();
            state = (delight <= 0) ? CustomerState.Angry : CustomerState.WaitOrder;
        }
    }

    // 음식 받음
    public IEnumerator ReceiveFood()
    {
        Init();
        DelightHandler(true);
        delightObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        delightObject.SetActive(false);
        state = CustomerState.Eat;
        orderMenu = null;
    }

    // 식사하기
    public void OnEating()
    {
        if (currentTime < eatingTime)
        {
            currentTime += Time.deltaTime;
            bubbleSlider.value = currentTime / eatingTime;
        }
        else
        {
            Init();
            state = CustomerState.WaitOrder;
        }
    }

    // 화남
    public void OnAngry()
    {
        // 화난 애니메이션 및 효과 넣기
        LeaveStore();
    }

    // 가게 떠나기
    private void LeaveStore()
    {
        agent.SetDestination(CustomerManager.Instance.doorPosition.position);
        state = CustomerState.Walk;
    }

    // 만족도 변화
    public void DelightHandler(bool isDelight, int num = 0)
    {
        if(isDelight)
        {
            delightObject.SetActive(true);
            coinText.text = string.Format("+" + "<color=green>{0}</color>", orderMenu.item.ItemCost);
            delightText.text = string.Format("+" + "<color=green>{0}</color>", orderMenu.item.ItemDelight);

            GameManager.Instance.CoinHandler(orderMenu.item.ItemCost);
        }
        else
        {
            delightObject.SetActive(true);
            delightText.text = string.Format("-" + "<color=red>{0}</color>", orderMenu.item.ItemDelight);
        }
    }

    // AI가 목적지에 도착했는지 확인용
    private bool HasReacheDestination()
    {
        // 경로 계산이 끝났다면
        if (!agent.pathPending)
        {
            // 남은 거리가 짧다면
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                // 움직이지 않는 상태라면
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                    return true;
            }
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Chair") && HasReacheDestination())
        {
            state = CustomerState.WaitOrder;
        }
        else if(coll.CompareTag("Door") && HasReacheDestination())
        {
            CustomerManager.Instance.LeaveCustomer(chairIndex);
            Destroy(gameObject, 1.0f);
            Debug.Log("Customer LEave");
        }
    }
}