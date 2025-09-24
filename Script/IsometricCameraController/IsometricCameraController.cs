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

    
}