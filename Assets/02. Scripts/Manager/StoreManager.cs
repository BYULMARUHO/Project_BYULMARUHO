using UnityEngine;

public class StoreManager : MonoBehaviour
{
    private static StoreManager instance;
    public static StoreManager Instance {  get { return instance; } }

    private Inventory inventory;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        
    }

    // ±¸¸Å
    public void OnBuy(Item _item)
    {
        if (GameManager.Instance.CoinHandler(-_item.item.ItemCost))
        {
            inventory.AcquireItem(_item.item);
        }
    }
}