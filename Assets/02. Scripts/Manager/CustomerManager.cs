using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public List<GameObject> chairs;
    public GameObject customerPrefab;

    private float currentTime = 0.0f;
    private float appeatedTime = 0.0f;
    private float minAppearedTime = 5.0f;
    private float maxAppearedTime = 15.0f;

    private int todayAppearedCustomerNum = 0;
    private int maxCustomerNum = 10;

    private bool[] isChairOccupied;
    private bool isFullHouse = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else 
            instance = this;

        Init();
    }

    private void Update()
    {
        AppearedCustomer();
    }

    private void Init()
    {
        chairs = GameObject.FindGameObjectsWithTag("Chair").ToList();
        isChairOccupied = new bool[chairs.Count];

        appeatedTime = Random.Range(minAppearedTime, maxAppearedTime);
        todayAppearedCustomerNum = 0;
    }

    public int GetChairIndex()
    {
        for (int i = 0; i < isChairOccupied.Length; i++)
        {
            if (!isChairOccupied[i])
            {
                isChairOccupied[i] = true;
                return i;
            }
        }
        return -1;
    }

    // ¼Õ´Ô ¹æ¹®
    public void AppearedCustomer()
    {
        if (GetChairIndex() == -1)
            return;

        if (currentTime < appeatedTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;
            appeatedTime = Random.Range(minAppearedTime, maxAppearedTime);

        }
    }

    // ¼Õ´Ô ÅðÀå
    public void LeaveCustomer(int index)
    {
        if (index >= 0 && index < isChairOccupied.Length)
        {
            isChairOccupied[index] = false;
        }
    }
}