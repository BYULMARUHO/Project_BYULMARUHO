using UnityEngine;
using Utils.ClassUtility;

public class PlayerController : MonoBehaviour
{
    private JSONParser parser;
    public PlayerData status;

    private Rigidbody2D rb;
    private Collider2D cd;

    public Vector2 dir;

    private void Awake()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Init()
    {
        status = parser.LoadPlayerDataFromJSON(0);
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * status.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}