using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance {  get { return instance; } }

    public List<Transform> targets;
    private bool[] lineOccupied;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else 
            instance = this;

        Init();
    }

    private void Init()
    {
        targets = GameObject.Find("Line").GetComponentsInChildren<Transform>().ToList();
        targets.RemoveAt(0);
        lineOccupied = new bool[targets.Count];
    }

    public int GetCustomerIndex()
    {
        for (int i = 0; i < lineOccupied.Length; i++)
        {
            if (!lineOccupied[i])
            {
                lineOccupied[i] = true;
                return i;
            }
        }
        return -1;
    }

    public void LeaveCustomer(int index)
    {
        if (index >= 0 && index < lineOccupied.Length)
        {
            lineOccupied[index] = false;
        }
    }
}