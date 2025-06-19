using Spine;
using UnityEngine;
using UnityEngine.AI;
using Utils.EnumTypes;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Pathfinder pathFinder;
    private TargetController targetController;

    public CustomerState state;
    private float moveSpeed = 100.0f;

    private float waitTime = 10.0f;
    private float currentTime = 0.0f;

    private bool isOrder = false;
    private bool isSit = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pathFinder = GetComponent<Pathfinder>();
        targetController = GameObject.Find("Line").GetComponent<TargetController>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //pathFinder.moveSpeed = moveSpeed;
        StandInLine();
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

    public void StandInLine()
    {
        //pathFinder.target = targetController.TargetHandler();
        if(targetController.TargetHandler() !=  null)
        {
            Transform target = targetController.TargetHandler();
            agent.SetDestination(target.position);
        }
    }

    public void RequestOrder()
    {
        Debug.Log("::: Order :::");
        isOrder = true;
    }

    public void StartOrderProcess()
    {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Line"))
        {
            RequestOrder();
        }
    }
}