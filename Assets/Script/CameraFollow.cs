using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private Player player;

    [Header("카메라 위치")]
    [SerializeField] private float distance = 6f;
    [SerializeField] private float targetHeight = 1.5f;

    [Header("마우스 회전")]
    [SerializeField] private float mouseSensitivity = 3f;

    [Tooltip("우클릭 후 이 값 이상 마우스를 움직여야 드래그가 시작됩니다.")]
    [SerializeField] private float dragStartThreshold = 0.01f;

    [Header("상하 회전 제한")]
    [SerializeField] private float minimumPitch = -20f;
    [SerializeField] private float maximumPitch = 65f;

    [Header("카메라 추적")]
    [SerializeField] private float smoothTime = 0.05f;

    private float yaw;
    private float pitch = 15f;

    private bool isRightMouseHeld;
    private bool isDragging;

    private Vector3 followVelocity;

    private void Start()
    {
        if (player != null)
        {
            yaw = player.transform.eulerAngles.y;
            player.SetFacingYaw(yaw);
        }

        ShowCursor();
    }

    private void Update()
    {
        if (player == null)
            return;

        // 우클릭을 처음 누른 순간
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseHeld = true;
            isDragging = false;
        }

        if (isRightMouseHeld && Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // 우클릭만 하고 마우스를 움직이지 않았다면
            // 아직 커서를 숨기지 않음
            if (!isDragging)
            {
                float mouseMovement =
                    Mathf.Abs(mouseX) + Mathf.Abs(mouseY);

                if (mouseMovement >= dragStartThreshold)
                {
                    isDragging = true;
                    HideCursor();
                }
            }

            // 실제 우클릭 드래그가 시작된 상태
            if (isDragging)
            {
                // 좌우 회전
                yaw += mouseX * mouseSensitivity;

                // 상하 회전
                pitch -= mouseY * mouseSensitivity;
                pitch = Mathf.Clamp(
                    pitch,
                    minimumPitch,
                    maximumPitch
                );

                // 좌우 회전은 플레이어에게도 전달
                player.SetFacingYaw(yaw);
            }
        }

        // 우클릭을 놓으면 회전 종료 및 커서 복원
        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseHeld = false;
            isDragging = false;

            ShowCursor();
        }
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Transform target = player.transform;

        // 플레이어 머리 또는 상체 부근을 중심으로 설정
        Vector3 focusPosition =
            target.position + Vector3.up * targetHeight;

        // 카메라의 좌우·상하 회전
        Quaternion cameraRotation =
            Quaternion.Euler(pitch, yaw, 0f);

        // 플레이어 뒤쪽 카메라 위치 계산
        Vector3 desiredPosition =
            focusPosition
            - cameraRotation * Vector3.forward * distance;

        // 플레이어를 부드럽게 따라감
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref followVelocity,
            smoothTime
        );

        transform.rotation = cameraRotation;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        ShowCursor();
    }
}