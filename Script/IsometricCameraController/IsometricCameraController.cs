using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float moveSpeed = 10f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float smoothTime = 0.3f;

    [Header("Movement Bounds")]
    public Vector2 mapBounds = new Vector2(50f, 50f);

    private Camera cam;
    private Vector3 targetPosition;
    private float targetZoom;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        cam = GetComponent<Camera>();

        transform.rotation = Quaternion.Euler(30f, 45f, 0f); //30 degress on X axis

        transformPosition = transform.position;
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        HandleMovementInput();
        HandleZoomInput();
        UpdateCameraTransform();
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movement bsed on camera angle
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 worldMoveDirection = transform.TrasformDirection(moveDirection);
        worldMoveDirection.y = 0f; // Keep movement on horizontal plane

        targetPosition += worldMoveDirection * moveSpeed * Time.deltaTime;

        // Clamo to map bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, -mapBounds.x, mapBounds.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -mapBounds.y, mapBounds.y);

    }
    void HandleZoomInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
    }

    void UpdateCameraTransform()
    {
        // Smooth camera movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Smooth zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 2f);
    }

    public void FocusOnPosition(Vector3 worldPosition)
    {
        targetPosition = new Vector3(worldPosition.x, Transform.y, worldPosition.z);
    }

}