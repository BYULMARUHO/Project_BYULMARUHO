using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    private Pathfinder pathFinder;
    private TargetController targetController;

    private float moveSpeed = 100.0f;
    private bool isOrder = false;

    private void Awake()
    {
        pathFinder = GetComponent<Pathfinder>();
        targetController = GameObject.Find("Line").GetComponent<TargetController>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        pathFinder.moveSpeed = moveSpeed;
        pathFinder.target = targetController.TargetHandler();
    }

    private void Update()
    {
        
    }

    public void RequestOrder()
    {

    }

    public void StartOrderProcess()
    {

    }
}