using UnityEngine;

public class PlatformController2D
{
    // Active global position of the platform
    public Vector3 ActiveGlobalPlatformPoint;

    // Active local position of the platform
    public Vector3 ActiveLocalPlatformPoint;

    // Platform game object
    public GameObject Platform;

    public void HandlePlatforms(Transform transform)
    {
        // Check if the character is on a platform
        if (Platform != null)
        {
            // Gets the new global position of the platform
            var newGlobalPlatformPoint = Platform.transform.TransformPoint(ActiveLocalPlatformPoint);

            // Calculates moving distance
            var moveDistance = newGlobalPlatformPoint - ActiveGlobalPlatformPoint;

            // Check if the moving distance is different to zero
            if (moveDistance != Vector3.zero)
                transform.Translate(moveDistance, Space.World);
        }

        // Sets the platform game object to null
        Platform = null;
    }
}
