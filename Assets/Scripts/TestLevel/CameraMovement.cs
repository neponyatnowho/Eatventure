using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private TouchInput _touchInput;
    [SerializeField] private float _zMin;
    [SerializeField] private float _zMax;
    [SerializeField, Range(0f,10f)] private float cameraSpeed;

    private Camera mainCamera;
    private Vector2 startTouchPosition;

    private void Start()
    {
        mainCamera = Camera.main;
        _touchInput.OnTouchBegan += HandleTouchBegan;
        _touchInput.OnTouchMoved += HandleTouchMoved;
    }

    private void HandleTouchBegan(Vector2 touchPosition)
    {
        startTouchPosition = touchPosition;
    }

    private void HandleTouchMoved(Vector2 touchPosition)
    {
        float deltaZ = (touchPosition.y - startTouchPosition.y) * cameraSpeed * Time.deltaTime;
        Vector3 cameraPosition = mainCamera.transform.position;
        
        cameraPosition.z += deltaZ;
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, _zMin, _zMax);
        mainCamera.transform.position = cameraPosition;
        startTouchPosition = touchPosition;
    }
}
