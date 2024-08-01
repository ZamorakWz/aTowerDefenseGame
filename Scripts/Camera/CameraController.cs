using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    [SerializeField] private Vector2 _xLimit;
    [SerializeField] private Vector2 _zLimit;
    [SerializeField] private Vector3 _startPosition = new Vector3(10, 25, 45);

    private Camera _cam;
    private float _currentRotationX = 45f;
    private float _currentRotationY = 225f;

    private void Start()
    {
        _cam = Camera.main;
        SetFirstCameraView();
    }

    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }

    private void SetFirstCameraView()
    {
        _cam.transform.position = _startPosition;
        _cam.transform.rotation = Quaternion.Euler(45f, 225f, 0f);
    }

    private void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = _cam.transform.forward.normalized;
        forward.y = 0;
        Vector3 right = _cam.transform.right.normalized;
        right.y = 0;

        Vector3 movement = (forward * vertical + right * horizontal) * _moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, _xLimit.x, _xLimit.y);
        newPosition.z = Mathf.Clamp(newPosition.z, _zLimit.x, _zLimit.y);

        transform.position = newPosition;
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            _currentRotationY += mouseX * _rotateSpeed * Time.deltaTime;
            _currentRotationX -= mouseY * _rotateSpeed * Time.deltaTime;

            _currentRotationX = Mathf.Clamp(_currentRotationX, 10f, 80f);

            _cam.transform.rotation = Quaternion.Euler(_currentRotationX, _currentRotationY, 0f);
        }
    }

    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = _cam.transform.position;
        position.y += scroll * _zoomSpeed;
        position.y = Mathf.Clamp(position.y, _minZoom, _maxZoom);
        _cam.transform.position = position;
    }
}