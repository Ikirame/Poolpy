using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Defines the objects can be picked up
    public enum PickUpType
    {
        Apple,
        Orange,
        Bread,
        Milk,
        Coconut,
        Key
    }

    // Instance of the enum
    public PickUpType Type;
}