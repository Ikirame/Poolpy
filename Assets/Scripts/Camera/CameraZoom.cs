using UnityEngine;
using UnityEngine.U2D;

public class CameraZoom : MonoBehaviour
{
    public float MinOrthographicSize;

    public float MaxOrthographicSize;

    public float SmoothSpeed;

    public float Increment;

    private bool _shouldZoomIn;

    private bool _shouldZoomOut;

    private PixelPerfectCamera _pixelPerfectCamera;

    private void Start()
    {
        _pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    private void Update()
    {
        if (_shouldZoomIn)
            ZoomInCamera();
        else if (_shouldZoomOut)
            ZoomOutCamera();
    }

    private void ZoomInCamera()
    {
        if (Camera.main.orthographicSize > MinOrthographicSize)
        {
            _pixelPerfectCamera.enabled = false;

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
                Camera.main.orthographicSize + -Increment, SmoothSpeed * Time.deltaTime);
        }
        else
            _shouldZoomIn = false;
    }

    private void ZoomOutCamera()
    {
        if (Camera.main.orthographicSize < MaxOrthographicSize)
        {
            _pixelPerfectCamera.enabled = false;

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
                Camera.main.orthographicSize + Increment, SmoothSpeed * Time.deltaTime);
        }
        else
            _shouldZoomOut = false;
    }

    public void ZoomIn()
    {
        _shouldZoomIn = true;
    }

    public void ZoomOut()
    {
        _shouldZoomOut = true;
    }
}