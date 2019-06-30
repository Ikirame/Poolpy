using UnityEngine;

public class RayCastController2D
{
    // Width of the skin of the character
    public const float SkinWidth = 0.02f;

    // Number of Horizontal and Vertical rays
    public const int HorizontalRaysCount = 8;
    public const int VerticalRaysCount = 4;

    // Distance between each Horizontal and Vertical rays
    public float VerticalDistanceBetweenRays;
    public float HorizontalDistanceBetweenRays;

    // Rays origins
    public Vector2 RayCastTopLeft;
    public Vector2 RayCastBottomLeft;
    public Vector2 RayCastBottomRight;

    public RayCastController2D(Collider2D collider)
    {
        // Calculates horizontal distance between each ray
        var colliderWidth = collider.bounds.size.x - 2 * SkinWidth;
        HorizontalDistanceBetweenRays = colliderWidth / (VerticalRaysCount - 1);

        // Calculates vertical distance between each ray
        var colliderHeight = collider.bounds.size.x - 2 * SkinWidth;
        VerticalDistanceBetweenRays = colliderHeight / (HorizontalRaysCount - 1);
    }

    public void CalculateRayOrigins(Collider2D collider, Transform transform)
    {
        // Calculates the size of the collider
        var size = collider.bounds.size / 2;

        // Calculates the center of the collider
        var center = new Vector2(collider.offset.x * transform.localScale.x,
            collider.offset.y * transform.localScale.y);

        // Calculates rays origins
        RayCastTopLeft = transform.position +
                         new Vector3(center.x - size.x + SkinWidth, center.y + size.y - SkinWidth);
        RayCastBottomRight = transform.position +
                             new Vector3(center.x + size.x - SkinWidth, center.y - size.y + SkinWidth);
        RayCastBottomLeft = transform.position +
                            new Vector3(center.x - size.x + SkinWidth, center.y - size.y + SkinWidth);
    }
}
