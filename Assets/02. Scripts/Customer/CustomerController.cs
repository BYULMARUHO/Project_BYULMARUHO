using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils.EnumTypes;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject orderBubble;

    public CustomerState state;
    private float moveSpeed = 2.5f;

    private const float waitTime = 10.0f;
    private float currentTime = 0.0f;
    private int lineIndex = -1;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        orderBubble = transform.GetChild(1).GetChild(0).gameObject;
    }

    private void Start()
    {
        Init();
        StandInLine();
    }

    // �ʱ�ȭ
    public void Init()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
        state = CustomerState.Idle;
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
            case CustomerState.Wait:
                RequestOrder();
                break;
            case CustomerState.Sit:
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

    // ī���ͷ� �̵� �� �ټ���
    public void StandInLine()
    {
        lineIndex = CustomerManager.Instance.GetCustomerIndex();
        agent.SetDestination(CustomerManager.Instance.targets[lineIndex].position);
    }

    // �ֹ��ϱ�
    public void RequestOrder()
    {
        if (!HasReacheDestination())
            return;

        orderBubble.SetActive(true);

        if (currentTime < waitTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;

        }
    }

    // ���� ����
    public void ReceiveFood()
    {

    }

    // ���� ������
    private void LeaveStore()
    {
        CustomerManager.Instance.LeaveCustomer(lineIndex);

        if(state == CustomerState.Angry)
        {

        }

        Destroy(gameObject, 1.0f);
    }

    // AI�� �������� �����ߴ��� Ȯ�ο�
    private bool HasReacheDestination()
    {
        // ��� ����� �����ٸ�
        if (!agent.pathPending)
        {
            // ���� �Ÿ��� ª�ٸ�
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                // �������� �ʴ� ���¶��
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                    return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Line"))
        {
            state = CustomerState.Wait;
        }
    }
}