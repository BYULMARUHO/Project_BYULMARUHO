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
    private SkeletonAnimation currentSkeleton;

    private PlayerState state;
    private Vector2 input;
    private Vector2 moveDir;
    private Direction currentDir;

    private string currentAnim = "";

    // 이동 유지용 grace period
    private float moveTimer = 0f;
    private readonly float gracePeriod = 0.1f;
    private const float moveThreshold = 0.01f;

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
        Move();
        SetAnim();
    }

    // 초기화 함수
    void Init()
    {
        status = parser.LoadPlayerDataFromJSON(0);
        state = PlayerState.Idle;
    }

    // 이동 구현
    public void Move()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 방향 전환
        if (input.y > 0)
            SetDirection(Direction.Back);
        else if (input.y < 0)
            SetDirection(Direction.Front);
        else if (input.x != 0)
            SetDirection(input.x > 0 ? Direction.Right : Direction.Left);

        moveDir = new Vector2(input.x, input.y).normalized;
        rb.MovePosition(rb.position + (moveDir * status.moveSpeed * Time.deltaTime));
    }

    // 방향 변경
    public void SetDirection(Direction dir)
    {
        if (dir == currentDir && currentSkeleton != null)
            return;

        currentDir = dir;

        skeletons[0]?.gameObject.SetActive(dir == Direction.Front);
        skeletons[1]?.gameObject.SetActive(dir == Direction.Back);
        skeletons[2]?.gameObject.SetActive(dir == Direction.Left || dir == Direction.Right);

        if (dir == Direction.Left)
            skeletons[2].transform.localScale = new Vector3(1f, 1f, 1f);
        else if (dir == Direction.Right)
            skeletons[2].transform.localScale = new Vector3(-1f, 1f, 1f);

        currentSkeleton = dir switch
        {
            Direction.Front => skeletons[0],
            Direction.Back => skeletons[1],
            _ => skeletons[2]
        };

        currentAnim = "";

        if (currentSkeleton == null)
            Debug.LogError("currentSkeleton is null in SetDirection! Direction: " + dir);
    }

    // 애니메이션 변경
    void SetAnim()
    {
        bool isMoving = moveDir.sqrMagnitude > moveThreshold;

        if (isMoving)
        {
            moveTimer = gracePeriod;
            PlayAnim("Walking");
        }
        else
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0f)
                PlayAnim("Idle");
        }
    }

    // 애니메이션 실행
    void PlayAnim(string animName, bool loop = true)
    {
        if (currentSkeleton == null || currentAnim == animName)
            return;

        currentSkeleton.AnimationState.SetAnimation(0, animName, loop);
        currentAnim = animName;
    }
}