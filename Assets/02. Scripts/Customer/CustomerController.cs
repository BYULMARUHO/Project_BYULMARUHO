using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Utils.EnumTypes;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject orderBubble;
    private Image menuImage;
    private Slider orderSlider;

    public CustomerState state;
    public Item orderMenu;
    private int chairIndex = -1;

    private float moveSpeed = 2.5f;
    private const float waitTime = 30.0f;
    private float currentTime = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        orderBubble = transform.GetChild(1).GetChild(0).gameObject;
        menuImage = orderBubble.transform.GetChild(1).GetComponent<Image>();
        orderSlider = orderBubble.transform.GetChild(0).GetComponent<Slider>();
    }

    private void Start()
    {
        Init();
        MoveToChair();
    }

    // 초기화
    public void Init()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        currentTime = 0;
        orderMenu = null;
        menuImage.sprite = null;
        orderBubble.SetActive(false);
    }

    private void Update()
    {
        StateHandler();
    }

    public void StateHandler()
    {
        switch (state)
        {
            case CustomerState.Idle:
                break;
            case CustomerState.Walk:
                break;
            case CustomerState.Order:
                StartCoroutine(RequestOrder());
                break;
            case CustomerState.Wait:
                StartWaiting();
                break;
            case CustomerState.Eat:
                break;
            case CustomerState.Drink:
                break;
            case CustomerState.ReJoice:
                break;
            case CustomerState.Angry:
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

    // 주문하기
    private IEnumerator RequestOrder()
    {
        if (!HasReacheDestination())
            yield return null;

        orderMenu = OrderManager.Instance.AddOrder();

        yield return new WaitForSeconds(1.5f);

        menuImage.sprite = orderMenu.item.ItemImage;
        orderBubble.SetActive(true);
        state = CustomerState.Wait;
    }

    // 기다리는 중
    public void StartWaiting()
    {
        if (currentTime < waitTime)
        {
            currentTime += Time.deltaTime;
            orderSlider.value = currentTime / waitTime;
        }
        else
        {
            Init();
            state = CustomerState.Order;
        }
    }

    // 음식 받음
    public void ReceiveFood()
    {

    }

    // 화남
    public void Angry()
    {

    }

    // 가게 떠나기
    private void LeaveStore()
    {
        CustomerManager.Instance.LeaveCustomer(chairIndex);

        if(state == CustomerState.Angry)
        {

        }

        Destroy(gameObject, 1.0f);
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Chair"))
        {
            state = CustomerState.Order;
        }
    }
}