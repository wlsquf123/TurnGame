using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("플레이어 능력치")]
    public int Level = 1;
    public float Exp = 0f;
    public float Hp = 100f;
    public float Mp = 50f;
    public float Def = 10f;
    public float Attack = 20f;
    public float Speed = 10f; // 전투 턴 순서용
    public int Bag = 6;
    public int Crit = 10;
    public int Eva = 15;

    [Header("필드 이동")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rigid;

    // X: 좌우, Y: 전후
    private Vector2 moveInput;

    // 카메라가 전달하는 플레이어의 Y축 회전값
    private float targetYaw;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        targetYaw = transform.eulerAngles.y;
    }

    private void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // 좌우 이동
        if (Input.GetKey(KeyCode.A))
            horizontal -= 1f;

        if (Input.GetKey(KeyCode.D))
            horizontal += 1f;

        // 전후 이동
        if (Input.GetKey(KeyCode.W))
            vertical += 1f;

        if (Input.GetKey(KeyCode.S))
            vertical -= 1f;

        moveInput = new Vector2(horizontal, vertical);

        // 대각선 이동 속도 제한
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);
    }

    private void FixedUpdate()
    {
        // 플레이어가 바라볼 수평 방향
        Quaternion facingRotation =
            Quaternion.Euler(0f, targetYaw, 0f);

        rigid.MoveRotation(facingRotation);

        // 회전값을 기준으로 앞과 오른쪽 방향 계산
        Vector3 forwardDirection =
            facingRotation * Vector3.forward;

        Vector3 rightDirection =
            facingRotation * Vector3.right;

        // W/S는 앞뒤, A/D는 좌우
        Vector3 moveDirection =
            forwardDirection * moveInput.y +
            rightDirection * moveInput.x;

        moveDirection.Normalize();

        // 중력을 유지하면서 X/Z축만 이동
        Vector3 currentVelocity = rigid.linearVelocity;

        currentVelocity.x = moveDirection.x * moveSpeed;
        currentVelocity.z = moveDirection.z * moveSpeed;

        rigid.linearVelocity = currentVelocity;
    }

    public void SetFacingYaw(float yaw)
    {
        targetYaw = yaw;
    }
}