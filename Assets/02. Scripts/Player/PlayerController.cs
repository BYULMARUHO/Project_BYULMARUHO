using Spine.Unity;
using UnityEngine;
using Utils.ClassUtility;
using Utils.EnumTypes;

public class PlayerController : MonoBehaviour
{
    private JSONParser parser;
    public PlayerData status;

    private Rigidbody2D rb;
    private Collider2D cd;
    private SkeletonAnimation[] skeletons = new SkeletonAnimation[3];

    public PlayerState state;
    public Vector2 dir;

    private void Awake()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();

        for (int i = 0; i < 3; i++)
            skeletons[i] = transform.GetChild(i).GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        PlayerStateController();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Init()
    {
        status = parser.LoadPlayerDataFromJSON(0);
        state = PlayerState.Idle;
    }

    public void PlayerStateController()
    {

    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * status.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}