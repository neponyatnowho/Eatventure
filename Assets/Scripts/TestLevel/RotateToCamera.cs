using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
        RotateLikeCamera();
    }
    public void RotateLikeCamera()
    {
        transform.rotation = _mainCamera.transform.rotation;
    }
}
