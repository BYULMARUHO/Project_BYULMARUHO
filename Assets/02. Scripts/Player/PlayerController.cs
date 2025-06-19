using Spine.Unity;
using UnityEngine;
using Utils.ClassUtility;
using Utils.EnumTypes;

public class PlayerController : MonoBehaviour
{
    private JSONParser parser;
    public PlayerData status;

    private Rigidbody2D rb;
    private SkeletonAnimation[] skeletons = new SkeletonAnimation[3];
    private SkeletonAnimation currentSkeleton;

    private Direction currentDir;
    private Vector2 moveDir;

    private string currentAnim = "";
    private const float moveThreshold = 0.01f;

    private bool isMove = false;

    private void Awake()
    {
        parser = GameObject.Find("JSONParser").GetComponent<JSONParser>();
        status = parser.LoadPlayerDataFromJSON(0);
        rb = GetComponent<Rigidbody2D>();
        skeletons = GetComponentsInChildren<SkeletonAnimation>(true);
    }

    private void Update()
    {
        Move();
        AnimStateHandler();
    }

    // 이동 구현
    public void Move()
    {
        moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.MovePosition(rb.position + (moveDir.normalized * status.moveSpeed * Time.deltaTime));

        if (moveDir.y > 0)
            SetDirection(Direction.Back);
        else if (moveDir.y < 0)
            SetDirection(Direction.Front);
        else if (moveDir.x != 0)
            SetDirection(moveDir.x > 0 ? Direction.Right : Direction.Left);

        isMove = moveDir.sqrMagnitude > moveThreshold;
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
    }

    // 애니메이션 상태 관리
    void AnimStateHandler()
    {
        if (isMove)
        {
            PlayAnim("Walking");
        }
        else
        {
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