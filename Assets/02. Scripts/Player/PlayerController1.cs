using UnityEngine;
using Spine.Unity;

public class PlayerController1 : MonoBehaviour
{
    [Header("Skeletons")]
    public SkeletonAnimation frontSkeleton;
    public SkeletonAnimation sideSkeleton;
    public SkeletonAnimation backSkeleton;

    [Header("Movement")]
    public float moveSpeed = 2.5f;

    private SkeletonAnimation currentSkeleton;
    private Vector2 input;
    private Vector3 moveDir;

    private enum Direction { Front, Back, Left, Right }
    private Direction currentDir;

    private string currentAnim = "";

    // 이동 유지용 grace period
    private float moveTimer = 0f;
    private readonly float gracePeriod = 0.1f;
    private const float moveThreshold = 0.01f;

    void Start()
    {
        SetDirection(Direction.Front);
        PlayAnim("Idle");
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        UpdateAnimation();
    }

    void HandleInput()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 방향 전환
        if (input.y > 0)
            SetDirection(Direction.Back);
        else if (input.y < 0)
            SetDirection(Direction.Front);
        else if (input.x != 0)
            SetDirection(input.x > 0 ? Direction.Right : Direction.Left);
    }

    void HandleMovement()
    {
        moveDir = new Vector3(input.x, input.y, 0).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void UpdateAnimation()
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

    void SetDirection(Direction dir)
    {
        if (dir == currentDir && currentSkeleton != null)
            return;
    
        currentDir = dir;
    
        frontSkeleton?.gameObject.SetActive(dir == Direction.Front);
        backSkeleton?.gameObject.SetActive(dir == Direction.Back);
        sideSkeleton?.gameObject.SetActive(dir == Direction.Left || dir == Direction.Right);
    
        if (dir == Direction.Left)
            sideSkeleton.transform.localScale = new Vector3(1f, 1f, 1f);
        else if (dir == Direction.Right)
            sideSkeleton.transform.localScale = new Vector3(-1f, 1f, 1f);
    
        currentSkeleton = dir switch
        {
            Direction.Front => frontSkeleton,
            Direction.Back => backSkeleton,
            _ => sideSkeleton
        };
    
        // ✅ 이 줄이 중요함!
        currentAnim = "";  // Skeleton이 바뀌었으니 애니메이션 상태도 초기화
    
        if (currentSkeleton == null)
            Debug.LogError("currentSkeleton is null in SetDirection! Direction: " + dir);
    }


    void PlayAnim(string animName, bool loop = true)
    {
        if (currentSkeleton == null)
        {
            Debug.LogWarning("currentSkeleton is null. Cannot play animation: " + animName);
            return;
        }

        if (currentAnim == animName) return;

        currentSkeleton.AnimationState.SetAnimation(0, animName, loop);
        currentAnim = animName;
    }
}
