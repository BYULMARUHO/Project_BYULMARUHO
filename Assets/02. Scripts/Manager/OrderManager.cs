using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private static OrderManager instance;
    public static OrderManager Instance {  get { return instance; } }

    private Queue<CustomerController> orderQueue = new Queue<CustomerController>();

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public void AddOrder(CustomerController customer, string menuName)
    {
        Debug.Log($"[¡÷πÆ] {menuName}");
        orderQueue.Enqueue(customer);
    }

    public void ServeNextOrder(string menuName)
    {
        //foreach (var customer in orderQueue)
        //{
        //    if (customer != null && customer.enabled && customer.menuText.text == menuName)
        //    {
        //        customer.ReceiveFood();
        //        orderQueue = new Queue<CustomerController>(orderQueue.Except(new[] { customer }));
        //        break;
        //    }
        //}
    }
}