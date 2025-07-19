using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public List<GameObject> chairs;
    public GameObject customerPrefab;
    public Transform doorPosition;

    public float currentTime = 0.0f;
    public float appeatedTime = 0.0f;
    private int minAppearedTime = 10;
    private int maxAppearedTime = 15;

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
        if (!RestaurantManager.Instance.isOpen)
            return;

        isFullHouse = IsFullHouse();
        AppearedCustomer();
    }

    private void Init()
    {
        doorPosition = GameObject.FindGameObjectWithTag("Door").transform;
        chairs = GameObject.FindGameObjectsWithTag("Chair").ToList();
        isChairOccupied = new bool[chairs.Count];

        appeatedTime = Random.Range(minAppearedTime, maxAppearedTime);
        todayAppearedCustomerNum = 0;
    }

    // 남아있는 좌석 위치 확인
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
        return -1 ;
    }

    // 가게가 만석인지 확인
    public bool IsFullHouse()
    {
        int chairOccupiedNum = 0;

        for (int i = 0; i < isChairOccupied.Length; i++)
        {
            if (isChairOccupied[i])
                chairOccupiedNum++;
        }
        return (chairOccupiedNum >= isChairOccupied.Length) ? true : false;
    }

    // 손님 방문
    public void AppearedCustomer()
    {
        if (isFullHouse || todayAppearedCustomerNum >= maxCustomerNum)
            return;

        if (currentTime < appeatedTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;
            appeatedTime = Random.Range(minAppearedTime, maxAppearedTime);
            Instantiate(customerPrefab, doorPosition.position, Quaternion.identity);
            todayAppearedCustomerNum++;
        }
    }

    // 손님 퇴장
    public void LeaveCustomer(int index)
    {
        if (index >= 0 && index < isChairOccupied.Length)
        {
            isChairOccupied[index] = false;
        }
    }
}