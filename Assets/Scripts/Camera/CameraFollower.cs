#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    // Target that the camera will follow
    public Transform Target;

    // Offset between the camera and the target
    public Vector3 CameraOffset;

    // The time between each movement of the camera
    public float SmoothDampTime = 0.2f;

    // Reference needed to use the "SmoothDamp" method
    private Vector3 _smoothDampVelocityRef;

    public void OffsetZoom(Vector3 newOffset)
    {
        CameraOffset = newOffset;
    }

    private void LateUpdate()
    {
        // Update the position of the camera
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        // Call of the "SmoothDamp" method to move from position to another smoothly
        transform.position = Vector3.SmoothDamp(transform.position, Target.position - -CameraOffset,
            ref _smoothDampVelocityRef, SmoothDampTime);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(CameraFollower))]
public class CameraFollowEditor : Editor
{
    private void OnSceneGUI()
    {
        // Put Black square on "EditMode"
    }
}

#endif